using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS_DataAccess
{
    public class clsApplicationData
    {
        public static bool GetApplicationByID(int ApplicationID, ref int ApplicantPersonID
            , ref DateTime ApplicationDate, ref int ApplicationTypeID, ref byte ApplicationStatus
            , ref DateTime LastStatusDate, ref float PaidFees, ref int AddedByUserID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = "Select * from Applications Where ApplicationID = @ApplicationID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if (reader.Read()) 
                            {
                                IsFound = true;
                                ApplicantPersonID = (int)reader["ApplicantPersonID"];
                                ApplicationDate = (DateTime)reader["ApplicationDate"];
                                ApplicationTypeID = (int)reader["ApplicationTypeID"];
                                ApplicationStatus = (byte)reader["ApplicationStatus"];
                                LastStatusDate = (DateTime)reader["LastStatusDate"];
                                PaidFees = Convert.ToSingle(reader["PaidFees"]);
                                AddedByUserID = (int)reader["AddedByUserID"];
                            }
                            else 
                            {
                                IsFound = false ;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { IsFound = false; }
            return IsFound;
        }

        public static bool GetApplicationByPersonID(int ApplicantPersonID, ref int ApplicationID
           , ref DateTime ApplicationDate, ref int ApplicationTypeID, ref byte ApplicationStatus
           , ref DateTime LastStatusDate, ref float PaidFees, ref int AddedByUserID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = "Select * from Applications Where ApplicantPersonID = @ApplicantPersonID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                ApplicationID = (int)reader["ApplicationID"];
                                ApplicationDate = (DateTime)reader["ApplicationDate"];
                                ApplicationTypeID = (int)reader["ApplicationTypeID"];
                                ApplicationStatus = (byte)reader["ApplicationStatus"];
                                LastStatusDate = (DateTime)reader["LastStatusDate"];
                                PaidFees = Convert.ToSingle(reader["PaidFees"]);
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
            catch (Exception ex) { IsFound = false; }
            return IsFound;
        }

        public static int AddNewApplication(int ApplicantPersonID, DateTime ApplicationDate,
          int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate
          , float PaidFees, int AddedByUserID)
        {
            int ApplicationID = -1;
            try 
            {
                using(SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    string query = @"INSERT INTO Applications ( 
                            ApplicantPersonID,ApplicationDate,ApplicationTypeID,
                            ApplicationStatus,LastStatusDate,
                            PaidFees,AddedByUserID)
                             VALUES (@ApplicantPersonID,@ApplicationDate,@ApplicationTypeID,
                                      @ApplicationStatus,@LastStatusDate,
                                      @PaidFees,   @AddedByUserID);
                             SELECT SCOPE_IDENTITY();";
                    using (SqlCommand command = new SqlCommand(query, connection)) 
                    {
                        connection.Open();


                        command.Parameters.AddWithValue("ApplicantPersonID", @ApplicantPersonID);
                        command.Parameters.AddWithValue("ApplicationDate", @ApplicationDate);
                        command.Parameters.AddWithValue("ApplicationTypeID", @ApplicationTypeID);
                        command.Parameters.AddWithValue("ApplicationStatus", @ApplicationStatus);
                        command.Parameters.AddWithValue("LastStatusDate", @LastStatusDate);
                        command.Parameters.AddWithValue("PaidFees", @PaidFees);
                        command.Parameters.AddWithValue("AddedByUserID", AddedByUserID);

                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            ApplicationID = insertedID;
                        }
                    }
                }
            }catch (Exception ex) { ApplicationID = -1; }
            return ApplicationID;
        }

        public static bool UpdateApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate,
         int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate
         , float PaidFees, int CreatedByUserID)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = @"Update  Applications  
                            set ApplicantPersonID = @ApplicantPersonID,
                                ApplicationDate = @ApplicationDate,
                                ApplicationTypeID = @ApplicationTypeID,
                                ApplicationStatus = @ApplicationStatus, 
                                LastStatusDate = @LastStatusDate,
                                PaidFees = @PaidFees,
                                CreatedByUserID=@CreatedByUserID
                            where ApplicationID=@ApplicationID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();


                        command.Parameters.AddWithValue("ApplicationID", ApplicationID);
                        command.Parameters.AddWithValue("ApplicantPersonID", @ApplicantPersonID);
                        command.Parameters.AddWithValue("ApplicationDate", @ApplicationDate);
                        command.Parameters.AddWithValue("ApplicationTypeID", @ApplicationTypeID);
                        command.Parameters.AddWithValue("ApplicationStatus", @ApplicationStatus);
                        command.Parameters.AddWithValue("LastStatusDate", @LastStatusDate);
                        command.Parameters.AddWithValue("PaidFees", @PaidFees);
                        command.Parameters.AddWithValue("CreatedByUserID", @CreatedByUserID);

                        RowAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { RowAffected = 0; }
            return RowAffected != 0;
        }

        public static bool DeleteApplication(int ApplicationID)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = @"Delete  Applications  
                            where ApplicationID=@ApplicationID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        command.Parameters.AddWithValue("ApplicationID", ApplicationID);
                        RowAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { RowAffected = 0; }
            return RowAffected != 0;
        }

        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return GetActiveApplicationID(PersonID, ApplicationTypeID) != -1;
        }

        public static int GetActiveApplicationID(int ApplicantPersonID, int ApplicationTypeID)
        {
            int ActiveApplicationID = -1;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    Connection.Open();
                    string query = @"select ApplicationID as ActiveApplicationID from Applications
                             Where ApplicantPersonID = @ApplicantPersonID and ApplicationTypeID = @ApplicationTypeID
                             and ApplicationStatus = 1";
                    using (SqlCommand command = new SqlCommand(query, Connection))
                    {
                        command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
                        command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                        {
                            ActiveApplicationID = InsertedID;
                        }
                        else
                        {
                            ActiveApplicationID = -1;
                        }
                    }

                }
            }
            catch (Exception ex) { }
            return ActiveApplicationID;
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    string query = "SELECT Found=1 FROM Applications WHERE ApplicationID = @ApplicationID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            isFound = reader.HasRows;
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return isFound;
        }

        public static bool UpdateStatus(int ApplicationID, short NewStatus) 
        {
            int RowAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = @"Update  Applications  
                            set ApplicationStatus = @NewStatus, 
                                LastStatusDate = @LastStatusDate
                            where ApplicationID = @ApplicationID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        command.Parameters.AddWithValue("ApplicationID", ApplicationID);
                        command.Parameters.AddWithValue("NewStatus", NewStatus);
                        command.Parameters.AddWithValue("LastStatusDate", DateTime.Now);
                        RowAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { RowAffected = 0; }
            return RowAffected != 0;
        }

        public static DataTable GetAllApplications()
        {

            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = "select * from Subscription_View order by ApplicationDate desc";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)

                            {
                                dt.Load(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return dt;
        }

    }
}
