using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS_DataAccess
{
    public class clsEquipmentsData
    {
        public static bool GetEquipmentByID(int EquipmentID, ref string Name
            , ref DateTime PurchaseDate, ref byte Condition
            , ref DateTime LastMaintenanceDate, ref string EquipmentImage
            , ref int AddedByUserID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = "Select * from Equipments where EquipmentID = @EquipmentID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@EquipmentID", EquipmentID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                Name = (string)reader["Name"];
                                PurchaseDate = (DateTime)reader["PurchaseDate"];
                                Condition = (byte)reader["Condition"];

                                if(reader["LastMaintenanceDate"] != DBNull.Value)
                                    LastMaintenanceDate = (DateTime)reader["LastMaintenanceDate"];
                                else
                                    LastMaintenanceDate = DateTime.MaxValue;

                                if (reader["EquipmentImage"] != DBNull.Value)
                                    EquipmentImage = (string)reader["EquipmentImage"];
                                else
                                    EquipmentImage = "";
                                AddedByUserID = (int)reader["AddedByUserID"];
                            }
                            else
                            {
                                isFound = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { isFound = false; }
            return isFound;
        }

        public static bool GetEquipmentByName(string Name, ref int EquipmentID
           , ref DateTime PurchaseDate, ref byte Condition
           , ref DateTime LastMaintenanceDate, ref string EquipmentImage
           , ref int AddedByUserID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = "Select * from Equipments where Name = @Name";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@Name", Name);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                EquipmentID = (int)reader["EquipmentID"];
                                PurchaseDate = (DateTime)reader["PurchaseDate"];
                                Condition = (byte)reader["Condition"];

                                if(reader["LastMaintenanceDate"] != DBNull.Value)
                                LastMaintenanceDate = (DateTime)reader["LastMaintenanceDate"];
                                else
                                LastMaintenanceDate = DateTime.MaxValue;

                                if (reader["EquipmentImage"] != DBNull.Value)
                                    EquipmentImage = (string)reader["EquipmentImage"];
                                else
                                    EquipmentImage = "";
                                AddedByUserID = (int)reader["AddedByUserID"];
                            }
                            else
                            {
                                isFound = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { isFound = false; }
            return isFound;
        }

        public static int AddNewEquipment(string Name, DateTime PurchaseDate
            , byte Condition, DateTime LastMaintenanceDate, string EquipmentImage
           , int AddedByUserID)
        {
            int EquipmentID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = @"INSERT INTO Equipments
                                   (Name, PurchaseDate, Condition
                                   , EquipmentImage, AddedByUserID)
                                   VALUES
                                   (@Name, @PurchaseDate, @Condition
                                   , @EquipmentImage, @AddedByUserID)
                                   select SCOPE_IDENTITY();";
                    using (SqlCommand command = new SqlCommand(query, connection)) 
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@Name", Name);
                        command.Parameters.AddWithValue("@PurchaseDate", PurchaseDate);
                        command.Parameters.AddWithValue("@Condition", Condition);


                        if (EquipmentImage != null)
                            command.Parameters.AddWithValue("@EquipmentImage", EquipmentImage);
                        else
                            command.Parameters.AddWithValue("@EquipmentImage", DBNull.Value);

                        command.Parameters.AddWithValue("@AddedByUserID", AddedByUserID);

                        try 
                        {
                            object Result = command.ExecuteScalar();
                            if(Result != null && int.TryParse(Result.ToString(), out int insertedID)) 
                            {
                                EquipmentID = insertedID;
                            }
                        }catch (Exception ex) { EquipmentID = -1; }
                    }
                }
            }
            catch (Exception ex) { EquipmentID = -1; }
            return EquipmentID;
        }

        public static bool UpdateEquipment(int EquipmentID, string Name, DateTime PurchaseDate
           , byte Condition, DateTime LastMaintenanceDate, string EquipmentImage
          , int AddedByUserID)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = @"UPDATE Equipments
                                   SET Name = @Name
                                   ,PurchaseDate = @PurchaseDate
                                   ,Condition = @Condition
                                   ,LastMaintenanceDate = @LastMaintenanceDate
                                   ,EquipmentImage = @EquipmentImage
                                   ,AddedByUserID = @AddedByUserID
                                   WHERE EquipmentID = @EquipmentID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@EquipmentID", EquipmentID);
                        command.Parameters.AddWithValue("@Name", Name);
                        command.Parameters.AddWithValue("@PurchaseDate", PurchaseDate);
                        command.Parameters.AddWithValue("@Condition", Condition);
                        command.Parameters.AddWithValue("@LastMaintenanceDate", LastMaintenanceDate);

                        if (EquipmentImage != null)
                            command.Parameters.AddWithValue("@EquipmentImage", EquipmentImage);
                        else
                            command.Parameters.AddWithValue("@EquipmentImage", DBNull.Value);

                        command.Parameters.AddWithValue("@AddedByUserID", AddedByUserID);

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

        public static bool UpdateLasMaintenanceDate(int EquipmentID, DateTime LastMaintenanceDate, byte Condition, int AddedByUserID)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = @"UPDATE Equipments
                                   SET LastMaintenanceDate = @LastMaintenanceDate
                                   ,Condition = @Condition
                                   ,AddedByUserID = @AddedByUserID
                                   WHERE EquipmentID = @EquipmentID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@EquipmentID", EquipmentID);
                        command.Parameters.AddWithValue("@LastMaintenanceDate", LastMaintenanceDate);
                        command.Parameters.AddWithValue("@Condition", Condition);
                        command.Parameters.AddWithValue("@AddedByUserID", AddedByUserID);

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

        public static bool DeleteEquipment(int EquipmentID)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = @"Delete Equipments
                                   WHERE EquipmentID = @EquipmentID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@EquipmentID", EquipmentID);
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

        public static DataTable GetAllEquipments() 
        {
            DataTable EquipmentTable = new DataTable();
            try 
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    string query = @"select Equipments.EquipmentID, Equipments.Name
                                   , case when Equipments.Condition = 0 then 'Unbroken'
                                   when Equipments.Condition = 1 then 'Broken'
                                   end
                                   as Condition, Equipments.PurchaseDate
                                   , Equipments.LastMaintenanceDate,
                                   Equipments.AddedByUserID from Equipments
                                   order by Equipments.EquipmentID;";
                    using(SqlCommand command = new SqlCommand(query, connection)) 
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if (reader.HasRows) EquipmentTable.Load(reader);
                        }
                    }
                }
            } catch (Exception ex) { }
            return EquipmentTable;
        }


        public static bool IsEquipmentExist(int EquipmentID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = "Select Find = 1 from Equipments where EquipmentID = @EquipmentID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@EquipmentID", EquipmentID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = reader.HasRows;
                               
                            }
                            else
                            {
                                isFound = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { isFound = false; }
            return isFound;
        }

    }
}
