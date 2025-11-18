using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS_DataAccess
{
    public class clsMemberData
    {
        public static bool GetMemberByID(int MemberID, ref int PersonID
            , ref int TrainerID, ref int AddedByUserID, ref DateTime AddedDate) 
        {
            bool IsFound = false;
            try 
            {
                using(SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    string query = "select * from Members where MemberID = @MemberID";
                    using (SqlCommand command = new SqlCommand(query, connection)) 
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@MemberID", MemberID);
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if (reader.Read()) 
                            {
                                IsFound = true;
                                PersonID = (int)reader["PersonID"];
                                TrainerID = (int)reader["TrainerID"];
                                AddedByUserID = (int)reader["AddedByUserID"];
                                AddedDate = (DateTime)reader["AddedDate"];
                            }
                            else 
                            {
                                IsFound = false;
                            }
                        }
                    }
                }
            } catch (Exception e) { IsFound = false; }
            return IsFound;
        }

        public static bool GetMemberByPersonID(int PersonID, ref int MemberID
           , ref int TrainerID, ref int AddedByUserID, ref DateTime AddedDate)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = "select * from Members where PersonID = @PersonID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                MemberID = (int)reader["MemberID"];
                                TrainerID = (int)reader["TrainerID"];
                                AddedByUserID = (int)reader["AddedByUserID"];
                                AddedDate = (DateTime)reader["AddedDate"];
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

        public static int AddNewMember(int PersonID, int TrainerID
            , int AddedByUserID, DateTime AddedDate) 
        {
            int MemberID = -1;
            try 
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    string query = @"Insert Into Members (PersonID, TrainerID, AddedByUserID, AddedDate)
                            Values (@PersonID, @TrainerID, @AddedByUserID, @AddedDate);
                            SELECT SCOPE_IDENTITY();";

                    using(SqlCommand command = new SqlCommand(query, connection)) 
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@TrainerID", TrainerID);
                        command.Parameters.AddWithValue("@AddedByUserID", AddedByUserID);
                        command.Parameters.AddWithValue("@AddedDate", AddedDate);

                        object result = command.ExecuteScalar();
                        if(result != null && int.TryParse(result.ToString(), out int InsertedID))
                            MemberID = InsertedID;
                        else
                            MemberID = -1;
                    }
                }
            } catch (Exception e) { MemberID = -1; }
            return MemberID;
        }

        public static bool UpdateMember(int MemberID, int PersonID, int TrainerID
           , int AddedByUserID, DateTime AddedDate)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = @"Update  Members  
                            set PersonID = @PersonID,
                                TrainerID = @TrainerID,
                                AddedByUserID = @AddedByUserID,
                                AddedDate = @AddedDate
                                where MemberID = @MemberID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@MemberID", MemberID);
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@TrainerID", TrainerID);
                        command.Parameters.AddWithValue("@AddedByUserID", AddedByUserID);
                        command.Parameters.AddWithValue("@AddedDate", AddedDate);

                        RowAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e) { RowAffected = 0; }
            return RowAffected != 0;
        }

        public static bool IsMemberExistByPersonID(int PersonID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = @"Select Find = 1 from Members
                    where PersonID = @PersonID";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@PersonID", PersonID);
                        try
                        {
                            connection.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                IsFound = reader.HasRows;
                            }
                        }
                        catch (Exception ex) { IsFound = false; }
                    }
                }
            }
            catch (Exception ex) { }
            return IsFound;
        }

        public static DataTable GetAllMembers() 
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    string query = " select * from Members_View order by FullName desc";
                    using(SqlCommand command = new SqlCommand(query, connection)) 
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if(reader.HasRows) dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception e) { }
            return dt;
        }
    }
}
