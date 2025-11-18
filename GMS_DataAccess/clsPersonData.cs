using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace GMS_DataAccess
{
    public class clsPersonData
    {
        public static bool GetPersonByID(int PersonID, ref string FirstName, ref string SecondName
            ,ref string ThirdName, ref string LastName,ref int NationalityCountryID
            , ref DateTime DateOfBirth,ref byte Gendor, ref string Phone
            , ref string Email, ref string Address, ref string ProfileImage)
        {
            bool IsFound = false;
            try 
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    string query = "Select * from People where PersonID = @PersonID";
                    using (SqlCommand command = new SqlCommand(query, connection)) 
                    {
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        try
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    IsFound = true;
                                    FirstName = (string)reader["FirstName"];
                                    SecondName = (string)reader["SecondName"];

                                    if (reader["ThirdName"] != DBNull.Value)
                                        ThirdName = (string)reader["ThirdName"];
                                    else ThirdName = "";

                                    LastName = (string)reader["LastName"];

                                    NationalityCountryID = (int)reader["NationalityCountryID"];
                                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                                    Gendor = (byte)reader["Gendor"];
                                    Phone = (string)reader["Phone"];

                                    if (reader["Email"] != DBNull.Value)
                                        Email = (string)reader["Email"];
                                    else Email = "";

                                    Address = (string)reader["Address"];

                                    if (reader["ProfileImage"] != DBNull.Value)
                                        ProfileImage = (string)reader["ProfileImage"];
                                    else ProfileImage = "";


                                }
                            }
                        }
                        catch (Exception ex) { IsFound = false; }
                    }
                }
            } catch(Exception ex) { }
           return IsFound; 
        }

        public static int AddNewPerson(string FirstName, string SecondName
            , string ThirdName, string LastName, int NationalityCountryID
            , DateTime DateOfBirth, byte Gendor, string Phone
            , string Email, string Address, string ProfileImage)
        {
            int PersonID = -1;
            try 
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    string query = @"INSERT INTO People
                                     (FirstName, SecondName, ThirdName, LastName, NationalityCountryID
                                      , DateOfBirth, Gendor, Phone, Email, Address, ProfileImage)
                                      VALUES(@FirstName, @SecondName, @ThirdName, @LastName, @NationalityCountryID
                                      , @DateOfBirth, @Gendor, @Phone, @Email, @Address, @ProfileImage)
                                      select SCOPE_IDENTITY();";
                    using (SqlCommand command = new SqlCommand(query, connection)) 
                    {
                        command.Parameters.AddWithValue("@FirstName", FirstName);
                        command.Parameters.AddWithValue("@SecondName", SecondName);

                        if(ThirdName != null)
                        command.Parameters.AddWithValue("@ThirdName", ThirdName);
                        else
                            command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

                        command.Parameters.AddWithValue("@LastName", LastName);
                        command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
                        command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                        command.Parameters.AddWithValue("@Gendor", Gendor);
                        command.Parameters.AddWithValue("@Phone", Phone);

                        if (Email != null)
                            command.Parameters.AddWithValue("@Email", Email);
                        else
                            command.Parameters.AddWithValue("@Email", DBNull.Value);

                        command.Parameters.AddWithValue("@Address", Address);

                        if (ProfileImage != null)
                            command.Parameters.AddWithValue("@ProfileImage", ProfileImage);
                        else
                            command.Parameters.AddWithValue("@ProfileImage", DBNull.Value);

                        try
                        {
                                connection.Open();
                                object Result = command.ExecuteScalar();
                                if (Result != null && int.TryParse(Result.ToString(), out int InstalledID))
                                {
                                    PersonID = InstalledID;
                                }
                            }
                            catch (Exception ex) { PersonID = -1; }
                    }
                }
            }catch(Exception ex) { PersonID = -1; }
            return PersonID;
        }

        public static bool UpdatePerson(int PersonID, string FirstName, string SecondName
           , string ThirdName, string LastName, int NationalityCountryID
           , DateTime DateOfBirth, byte Gendor, string Phone
           , string Email, string Address, string ProfileImage)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = @"UPDATE People
                                    SET FirstName = @FirstName
                                       ,SecondName = @SecondName
                                       ,ThirdName = @ThirdName
                                       ,LastName = @LastName
                                       ,NationalityCountryID = @NationalityCountryID
                                       ,DateOfBirth = @DateOfBirth
                                       ,Gendor = @Gendor
                                       ,Phone = @Phone
                                       ,Email = @Email
                                       ,Address = @Address
                                       ,ProfileImage = @ProfileImage
                                    WHERE PersonID = @PersonID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@FirstName", FirstName);
                        command.Parameters.AddWithValue("@SecondName", SecondName);

                        if (ThirdName != null)
                            command.Parameters.AddWithValue("@ThirdName", ThirdName);
                        else
                            command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

                        command.Parameters.AddWithValue("@LastName", LastName);
                        command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
                        command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                        command.Parameters.AddWithValue("@Gendor", Gendor);
                        command.Parameters.AddWithValue("@Phone", Phone);

                        if (Email != null)
                            command.Parameters.AddWithValue("@Email", Email);
                        else
                            command.Parameters.AddWithValue("@Email", DBNull.Value);

                        command.Parameters.AddWithValue("@Address", Address);

                        if (ProfileImage != null)
                            command.Parameters.AddWithValue("@ProfileImage", ProfileImage);
                        else
                            command.Parameters.AddWithValue("@ProfileImage", DBNull.Value);
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
            return RowAffected > 0;
        }


        public static bool DeletePerson(int PersonID)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = @"Delete People
                                    WHERE PersonID = @PersonID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        try
                        {
                            connection.Open();
                            RowAffected = command.ExecuteNonQuery();
                        }
                        catch (Exception ex) { PersonID = -1; }
                    }
                }
            }
            catch (Exception ex) { RowAffected = 0; }
            return RowAffected > 0;
        }

        public static DataTable GetAllPeople() 
        {
            DataTable dtPeople = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = @"SELECT People.PersonID, People.FirstName + ' ' + People.SecondName +' ' + People.ThirdName +' ' + People.LastName  as FullName, Countries.CountryName, People.DateOfBirth,
                                     case People.Gendor
                                     when 0 then 'Male'
                                     when 1 then 'Female'
                                     end as Gendor
                                     , People.Phone, People.Email, People.Address, 
                                     People.ProfileImage
                                     FROM            People INNER JOIN
                                     Countries ON People.NationalityCountryID = Countries.CountryID
						             order by FirstName;";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if(reader.HasRows) dtPeople.Load(reader);
                           
                        }
                        catch (Exception ex) {  }
                    }
                }
            }
            catch (Exception ex) {  }
            return dtPeople;
        }

        public static bool IsPersonExist(int PersonID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    string query = @"Select Find = 1 from People WHERE PersonID = @PersonID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            IsFound = reader.HasRows;
                        }
                        catch (Exception ex) { PersonID = -1; }
                    }
                }
            }
            catch (Exception ex) { IsFound = false ; }
            return IsFound;
        }
    }
}
