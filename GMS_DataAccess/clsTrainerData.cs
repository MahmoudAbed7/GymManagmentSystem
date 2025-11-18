using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS_DataAccess
{
    public class clsTrainerData
    {
        public static bool GetTrainerByID(int TrainerID, ref int PersonID, ref byte EmploymentStatus
            , ref string Speciality, ref DateTime HireDate, ref DateTime EndDate, ref float Salary
            ,ref bool IsActive, ref int AddedByUserID) 
        {
            bool IsFound = false;
            try 
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    string query = "Select * from Trainers where TrainerID = @TrainerID";
                    using (SqlCommand command = new SqlCommand(query, connection)) 
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("TrainerID", TrainerID);
                        try
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    IsFound = true;
                                    PersonID = (int)reader["PersonID"];
                                    EmploymentStatus = (byte)reader["EmploymentStatus"];
                                    Speciality = (string)reader["Speciality"];
                                    HireDate = (DateTime)reader["HireDate"];

                                    if (reader["EndDate"] != DBNull.Value)
                                        EndDate = (DateTime)reader["EndDate"];
                                    else
                                        EndDate = DateTime.MaxValue;

                                    Salary = Convert.ToSingle(reader["Salary"]);
                                    IsActive = (bool)reader["IsActive"];
                                    AddedByUserID = (int)reader["AddedByUserID"];
                                }
                                else 
                                {
                                    IsFound = false;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            IsFound = false;
                        }
                    }
                }
            }catch(Exception ex) { }
            return IsFound;
        }

        public static bool GetTrainerByPersonID(int TrainerID, ref int PersonID, ref byte EmploymentStatus
            , ref string Speciality, ref DateTime HireDate, ref DateTime EndDate, ref float Salary
            , ref bool IsActive, ref int AddedByUserID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = "Select * from Trainers where PersonID = @PersonID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("PersonID", PersonID);
                        try
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    IsFound = true;
                                    TrainerID = (int)reader["TrainerID"];
                                    EmploymentStatus = (byte)reader["EmploymentStatus"];
                                    Speciality = (string)reader["Speciality"];
                                    HireDate = (DateTime)reader["HireDate"];

                                    if (reader["EndDate"] != DBNull.Value)
                                        EndDate = (DateTime)reader["EndDate"];
                                    else
                                        EndDate = DateTime.MaxValue;

                                    Salary = Convert.ToSingle(reader["Salary"]);
                                    IsActive = (bool)reader["IsActive"];
                                    AddedByUserID = (int)reader["AddedByUserID"];
                                }
                                else
                                {
                                    IsFound = false;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            IsFound = false;
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return IsFound;
        }

        public static int AddNewTrainer(int PersonID, byte EmploymentStatus
            , string Speciality, DateTime HireDate, DateTime EndDate, float Salary
            , bool IsActive, int AddedByUserID) 
        {
            int TrainerID = -1;
            try 
            {
                string query = @"INSERT INTO Trainers
                               (PersonID, EmploymentStatus, Speciality
                               , HireDate ,EndDate, Salary, IsActive, AddedByUserID)
                                VALUES (@PersonID, @EmploymentStatus, @Speciality
                               , @HireDate, @EndDate, @Salary, @IsActive, @AddedByUserID)
                                select SCOPE_IDENTITY()";
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    using (SqlCommand command = new SqlCommand(query, connection)) 
                    {
                        connection.Open();

                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@EmploymentStatus", EmploymentStatus);
                        command.Parameters.AddWithValue("@Speciality", Speciality);
                        command.Parameters.AddWithValue("@HireDate", HireDate);
                        command.Parameters.AddWithValue("@EndDate", EndDate);
                        command.Parameters.AddWithValue("@Salary", Salary);
                        command.Parameters.AddWithValue("@IsActive", IsActive);
                        command.Parameters.AddWithValue("@AddedByUserID", AddedByUserID);

                        try
                        {
                            object result = command.ExecuteScalar();
                            if (result != null && int.TryParse(result.ToString(), out int insertedID))
                            {
                                TrainerID = insertedID;
                            }
                        }
                        catch (Exception ex) { TrainerID = -1; }
                    }
                }
            }catch (Exception ex) { TrainerID = -1; }
            return TrainerID;
        }

        public static bool UpdateTrainer(int TrainerID, int PersonID, byte EmploymentStatus
            , string Speciality, DateTime HireDate, DateTime EndDate, float Salary
            , bool IsActive, int AddedByUserID)
        {
            int RowAffected = 0;
            try
            {
                string query = @"UPDATE Trainers
                  SET PersonID = @PersonID
                     ,EmploymentStatus = @EmploymentStatus
                     ,Speciality = @Speciality
                     ,HireDate = @HireDate
                     ,EndDate = @EndDate
                     ,Salary = @Salary
                     ,IsActive = @IsActive
                     ,AddedByUserID = @AddedByUserID
                WHERE TrainerID = @TrainerID";
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("TrainerID", TrainerID);
                        command.Parameters.AddWithValue("PersonID", PersonID);
                        command.Parameters.AddWithValue("EmploymentStatus", EmploymentStatus);
                        command.Parameters.AddWithValue("Speciality", Speciality);
                        command.Parameters.AddWithValue("HireDate", HireDate);
                        command.Parameters.AddWithValue("EndDate", EndDate);
                        command.Parameters.AddWithValue("Salary", Salary);
                        command.Parameters.AddWithValue("IsActive", IsActive);
                        command.Parameters.AddWithValue("AddedByUserID", AddedByUserID);

                        try
                        {
                            connection.Open();
                            RowAffected = command.ExecuteNonQuery();
                        }
                        catch (Exception ex) { RowAffected = 0; }
                    }
                }
            }
            catch (Exception ex) { RowAffected = 0; }
            return RowAffected != 0;
        }

        public static bool DeleteTrainer(int TrainerID)
        {
            int RowAffected = 0;
            try
            {
                string query = @"Delete Trainers
                WHERE TrainerID = @TrainerID";
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("TrainerID", TrainerID);
                        try
                        {
                            connection.Open();
                            RowAffected = command.ExecuteNonQuery();
                        }
                        catch (Exception ex) { RowAffected = 0; }
                    }
                }
            }
            catch (Exception ex) { RowAffected = 0; }
            return RowAffected != 0;
        }

        public static DataTable GetAllTrainers() 
        {
            DataTable TrainerTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = @"select * from Trainer_Views order by FullName";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        try
                        {
                            connection.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows) TrainerTable.Load(reader);
                            }
                        }
                        catch (Exception ex) { }
                    }
                }
            }catch (Exception ex) { }
            return TrainerTable;
        }

        public static bool IsTrainerExistByID(int TrainerID) 
        {
            bool IsFound = false;
            try 
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    string query = @"Select Find = 1 from Trainers
                    where TrainerID = @TrainerID";

                    using(SqlCommand cmd = new SqlCommand(query, connection)) 
                    {
                        cmd.Parameters.AddWithValue("@TrainerID", TrainerID);
                        try 
                        {
                            connection.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader()) 
                            {
                                IsFound = reader.HasRows;
                            }
                        }catch (Exception ex) {  IsFound = false; }
                    }
                }
            }catch(Exception ex) { }
            return IsFound;
        }

        public static bool IsTrainerExistByPersonID(int PersonID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = @"Select Find = 1 from Trainers
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

    }
}
