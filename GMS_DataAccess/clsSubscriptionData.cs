using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS_DataAccess
{
    public class clsSubscriptionData
    {
        public static bool GetSubscriptionByID(int SubscriptionID, ref int ApplicationID) 
        {
            bool IsFound = false;
            try 
            {
                using(SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    string query = "Select * From Subscriptions where SubscriptionID = @SubscriptionID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        command.Parameters.AddWithValue("@SubscriptionID", SubscriptionID);
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if (reader.Read()) 
                            {
                                IsFound = true;
                                ApplicationID = (int)reader["ApplicationID"];
                            }
                            else 
                            {
                                IsFound = false;  
                            }
                        }
                    }
                }
            } catch (Exception ex) { IsFound = false; }
            return IsFound;
        }

        public static bool GetSubscriptionByApplicationID(int ApplicationID, ref int SubscriptionID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = "Select * from Subscriptions Where ApplicationID = @ApplicationID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                SubscriptionID = (int)reader["SubscriptionID"];
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

        public static int AddNewSubscription(int ApplicationID) 
        {
            int SubscriptionID = -1;
            try 
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = @"INSERT INTO Subscriptions
                                   (ApplicationID)
                                   VALUES
                                   (@ApplicationID)
                                    SELECT SCOPE_IDENTITY()";
                    using(SqlCommand command = new SqlCommand(query, connection)) 
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                            SubscriptionID = insertedID;
                        else
                            SubscriptionID = -1;
                            
                    }
                }
            } catch (Exception ex) { SubscriptionID = -1; }
            return SubscriptionID;
        }

        public static bool UpdateSubscription(int SubscriptionID, int ApplicationID)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = @"UPDATE Subscriptions
                                   SET ApplicationID = @ApplicationID
                                   WHERE SubscriptionID = @SubscriptionID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@SubscriptionID", SubscriptionID);
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                        RowAffected = command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex) { RowAffected = 0; }
            return RowAffected != 0;
        }

        public static bool DeleteSubscription(int SubscriptionID)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = @"Delete Subscriptions
                                   WHERE SubscriptionID = @SubscriptionID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@SubscriptionID", SubscriptionID);
                        RowAffected = command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex) { RowAffected = 0; }
            return RowAffected != 0;
        }

        public static DataTable GetAllSubscriptions() 
        {
            DataTable SubscriptionsDataTable = new DataTable();
            try 
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    string query = @"select * from Subscription_View
                                   order by ApplicationDate";

                    using(SqlCommand command = new SqlCommand(query, connection)) 
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if(reader.HasRows) SubscriptionsDataTable.Load(reader);
                        }
                    }
                }
            } catch (Exception ex) { }
            return SubscriptionsDataTable;
        }

    }
}
