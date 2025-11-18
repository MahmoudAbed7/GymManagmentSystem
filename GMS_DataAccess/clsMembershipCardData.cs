using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS_DataAccess
{
    public class clsMembershipCardData
    {
        public static bool GetMembershipCardByID(int CardID, ref int ApplicationID
            , ref int MemberID, ref DateTime IssueDate
            , ref DateTime ExpirationDate, ref byte IssueReason, ref bool IsActive
            , ref int AddedByUserID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = "Select * from MemberShipCards where CardID = @CardID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@CardID", CardID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                ApplicationID = (int)reader["ApplicationID"];
                                MemberID = (int)reader["MemberID"];
                                IssueDate = (DateTime)reader["IssueDate"];
                                ExpirationDate = (DateTime)reader["ExpirationDate"];
                                IssueReason = (byte)reader["IssueReason"];
                                IsActive = (bool)reader["IsActive"];
                                AddedByUserID = (int)reader["AddedByUserID"];
                            }
                            else
                            {
                                IsFound = false;
                            }
                        }
                    }
                }
            }
            catch (Exception e) { IsFound = false; }
            return IsFound;
        }

        public static int AddNewMembershipCard(int ApplicationID
            , int MemberID, DateTime IssueDate
            , DateTime ExpirationDate, byte IssueReason, bool IsActive
            , int AddedByUserID)
        {
            int CardID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = @"INSERT INTO MemberShipCards
                                   (ApplicationID, MemberID, IssueDate
                                   , ExpirationDate, IssueReason, IsActive, AddedByUserID)
                                   VALUES
                                   (@ApplicationID, @MemberID, @IssueDate
                                   ,@ExpirationDate, @IssueReason, @IsActive, @AddedByUserID)
                                   Select SCOPE_IDENTITY()";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        command.Parameters.AddWithValue("@MemberID", MemberID);
                        command.Parameters.AddWithValue("@IssueDate", IssueDate);
                        command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                        command.Parameters.AddWithValue("@IssueReason", IssueReason);
                        command.Parameters.AddWithValue("@IsActive", IsActive);
                        command.Parameters.AddWithValue("@AddedByUserID", AddedByUserID);
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                        {
                            CardID = InsertedID;
                        }
                    }
                }
            }
            catch (Exception e) { CardID = -1; }
            return CardID;
        }

        public static bool UpdateMembershipCard(int CardID, int ApplicationID
           , int MemberID, DateTime IssueDate
           , DateTime ExpirationDate, byte IssueReason, bool IsActive
           , int AddedByUserID)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = @"UPDATE MemberShipCards
                                   SET ApplicationID = @ApplicationID
                                      ,MemberID = @MemberID
                                      ,IssueDate = @IssueDate
                                      ,ExpirationDate = @ExpirationDate
                                      ,IssueReason = @IssueReason
                                      ,IsActive = @IsActive
                                      ,AddedByUserID = @AddedByUserID
                                 WHERE CardID = @CardID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@CardID", CardID);
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        command.Parameters.AddWithValue("@MemberID", MemberID);
                        command.Parameters.AddWithValue("@IssueDate", IssueDate);
                        command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                        command.Parameters.AddWithValue("@IssueReason", IssueReason);
                        command.Parameters.AddWithValue("@IsActive", IsActive);
                        command.Parameters.AddWithValue("@AddedByUserID", AddedByUserID);
                        RowAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e) { RowAffected = 0; }
            return RowAffected != 0;
        }

        public static int GetActiveMembershipCardForMembersByPersonID(int PersonID) 
        {
            int CardID = -1;
            try 
            {
                using(SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    string query = @"SELECT MemberShipCards.CardID
                                   FROM     MemberShipCards INNER JOIN
                                   Members ON MemberShipCards.MemberID = Members.MemberID
                                   where Members.PersonID = @PersonID
                                   and MemberShipCards.IsActive = 1;";
                    using(SqlCommand command = new SqlCommand(query, connection)) 
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int InsertedID)) 
                        {
                            CardID = InsertedID;
                        }

                    }
                }
            } catch (Exception e) { CardID = -1; }
            return CardID;
        }

        public static bool DeactivateCard(int CardID)
        {

            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {

                    string query = @"UPDATE MemberShipCards
                           SET 
                              IsActive = 0
                             
                         WHERE CardID=@CardID";

                    using (SqlCommand command = new SqlCommand(query, connection)) {
                        connection.Open();
                        command.Parameters.AddWithValue("@CardID", CardID);
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e) { rowsAffected = 0; }

            return (rowsAffected > 0);
        }

        public static DataTable GetAllCards() 
        {
            DataTable table = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    string query = "Select * from MemberShipCards";
                    using(SqlCommand command = new SqlCommand(query, connection)) 
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if(reader.HasRows) table.Load(reader);
                        }
                    }
                }
            }
            catch (Exception e) { }
            return table;
        }

        public static DataTable GetMemberCards(int MemberID)
        {
            DataTable table = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = @"SELECT MemberShipCards.CardID, MemberShipCards.ApplicationID
                                   , MemberShipCards.IssueDate
                                   , MemberShipCards.ExpirationDate, MemberShipCards.IsActive
                                   FROM  MemberShipCards where  
                                   order by IsActive desc, ExpirationDate desc";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@MemberID", MemberID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) table.Load(reader);
                        }
                    }
                }
            }
            catch (Exception e) { }
            return table;
        }
    }
}
