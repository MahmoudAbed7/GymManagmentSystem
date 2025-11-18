using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS_DataAccess
{
    public class clsApplicationTypesData
    {
        public static bool GetApplicationTypeByID(int ApplicationTypeID
            , ref string Title, ref float Fees) 
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = @"Select * from ApplicationTypes where ApplicationTypeID = @ApplicationTypeID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            try
                            {
                                if (reader.Read())
                                {
                                    IsFound = true;
                                    Title = (string)reader["ApplicationTypeTitle"];
                                    Fees = Convert.ToSingle(reader["ApplicationFees"]);
                                }
                            }
                            catch (Exception ex) { IsFound = false; }

                        }
                    }
                }
            }
            catch (Exception ex) { IsFound = false; }
            return IsFound;
        }

        public static int AddNewApplicationType(string Title, float Fees)
        {
            int ApplicationTypeID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = @"INSERT INTO ApplicationTypes
                                   (ApplicationTypeTitle, ApplicationFees)
                                   VALUES (@ApplicationTypeTitle, @ApplicationFees)
                                   SELECT SCOPE_IDENTITY()";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationTypeTitle", Title);
                        command.Parameters.AddWithValue("@ApplicationFees", Fees);
                            try
                            {
                                connection.Open();
                                object Result = command.ExecuteScalar();
                                if (Result != null && int.TryParse(Result.ToString(), out int insertedID))
                                {
                                    ApplicationTypeID = insertedID;
                                }
                            }
                            catch (Exception ex) { ApplicationTypeID = -1; }

                        }
                    }
            }
            catch (Exception ex) { ApplicationTypeID = -1; }
            return ApplicationTypeID;
        }

        public static bool UpdateApplicationType(int ApplicationTypeID
            , string Title, float Fees)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = @"Update  ApplicationTypes  
                            set ApplicationTypeTitle = @Title,
                                ApplicationFees = @Fees
                                where ApplicationTypeID = @ApplicationTypeID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                        command.Parameters.AddWithValue("@Title", Title);
                        command.Parameters.AddWithValue("@Fees", Fees);
                            try
                            {
                                RowAffected = command.ExecuteNonQuery();
                            }
                            catch (Exception ex) { RowAffected = 0; }
                    }
                }
            }
            catch (Exception ex) { RowAffected = 0; }
            return RowAffected != 0;
        }

        public static DataTable GetAllApplicationTypes()
        {
            DataTable ApplicationTypes = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    string query = "SELECT * FROM ApplicationTypes order by ApplicationTypeTitle";
                    using(SqlCommand command = new SqlCommand(query, connection)) 
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            try 
                            {
                                if (reader.HasRows) ApplicationTypes.Load(reader);
                            }
                            catch(Exception ex) { }
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return ApplicationTypes;
        }
    }
}
