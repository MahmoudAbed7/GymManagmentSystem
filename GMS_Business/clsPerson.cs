using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMS_DataAccess;

namespace GMS_Business
{
    public class clsPerson
    {
        enum enMode { AddNew = 0, Update = 1 }
        enMode _Mode = enMode.AddNew;
        enum enGendor { Male = 0, Female = 1 }
        
        public int PersonID {  get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; } }
        public int NationalCountryID {  get; set; }
        public clsCountry CountryInfo;
        public DateTime DateOfBirth { get; set; }
        public byte Gendor {  get; set; }
        public string Phone {  get; set; }
        public string Email {  get; set; }
        public string Address {  get; set; }
        public string ProfileImage {  get; set; }

        public clsPerson() 
        {
            PersonID = -1; NationalCountryID = -1;
            FirstName = ""; SecondName = ""; ThirdName = ""; LastName = "";
            Phone = ""; Email = ""; Address = ""; ProfileImage = "";
            DateOfBirth = DateTime.Now;
            _Mode = enMode.AddNew;
        }

        private clsPerson(int personID, string firstName, string secondName, string thirdName
            , string lastName, int nationalCountryID, DateTime dateOfBirth, byte gendor, string phone
            , string email , string address, string profileImage)
        {
            this.PersonID = personID;
            this.NationalCountryID = nationalCountryID;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.ThirdName = thirdName;
            this.LastName = lastName;
            this.DateOfBirth =(DateTime)dateOfBirth;
            this.Gendor = gendor;
            this.Phone = phone;
            this.Email = email; 
            this.Address = address;
            this.ProfileImage = profileImage;
            this.CountryInfo = clsCountry.Find(NationalCountryID);
            _Mode = enMode.Update;
        }

        public static bool IsPersonExist(int PersonID) 
        {
            return clsPersonData.IsPersonExist(PersonID);
        }

        public static clsPerson Find(int PersonID) 
        {
            string firstName = ""; string secondName = ""; string thirdName = "";
            string lastName = ""; int nationalityCountryID = -1; DateTime dateOfBirth = DateTime.Now;
            byte gendor = 0; string phone = ""; string email = ""; string address = ""; string profileImage = "";

            if (clsPersonData.GetPersonByID(PersonID, ref firstName, ref secondName, ref thirdName
                , ref lastName, ref nationalityCountryID, ref dateOfBirth, ref gendor, ref phone
                , ref email, ref address, ref profileImage))
            {
                return new clsPerson(PersonID, firstName, secondName, thirdName, lastName, nationalityCountryID,
                    dateOfBirth, gendor, phone, email, address, profileImage);
            }
            else return null;
        }

        private bool _AddNewPerson() 
        {
            this.PersonID = clsPersonData.AddNewPerson(FirstName, SecondName, ThirdName, LastName
                , NationalCountryID, DateOfBirth, Gendor, Phone, Email, Address, ProfileImage);

            return this.PersonID != -1;
        }

        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(PersonID,FirstName, SecondName, ThirdName, LastName
                , NationalCountryID, DateOfBirth, Gendor, Phone, Email, Address, ProfileImage);
        }

        public static bool DeletePerson(int ID) 
        {
            return clsPersonData.DeletePerson(ID);
        }

        public bool Save() 
        {
            switch (_Mode) 
            {
                case enMode.AddNew:
                    if (_AddNewPerson()) 
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else 
                    {
                        return false;
                    }
                case enMode.Update:
                   return _UpdatePerson();
            }
            return false;
        }

        public static DataTable GetAllPeople() 
        {
            return clsPersonData.GetAllPeople();
        }
    }
}
