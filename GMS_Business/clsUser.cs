using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using GMS_DataAccess;

namespace GMS_Business
{
    public class clsUser
    {
        enum enMode { AddNew = 0, Update = 1 }
        enMode _Mode = enMode.AddNew;

        public int UserID {  get; set; }
        public int PersonID {  get; set; }
        public clsPerson PersonInfo;
        public string UserName { get; set; }
        public string Password {  get; set; }
        public bool IsActive {  get; set; }

        public clsUser()
        {
            UserID = -1; PersonID = -1; UserName = ""; Password = ""; IsActive = false;
            _Mode = enMode.AddNew;
        }

        public clsUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            this.UserID = UserID; this.PersonID = PersonID;
            PersonInfo = clsPerson.Find(PersonID);
            this.UserName = UserName; this.Password = Password; this.IsActive = IsActive;
            _Mode = enMode.Update;
        }

        public static clsUser FindByUserID(int UserID) 
        {
            int PersonID = -1; string UserName = "", Password = ""; bool IsActive = false;

            bool IsFound = clsUsersData.GetUserInfoByID(UserID, ref PersonID, ref UserName, ref Password, ref IsActive);
            if (IsFound) return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else return null;
        }

        public static clsUser FindByPersonID(int PersonID)
        {
            int UserID = -1; string UserName = "", Password = ""; bool IsActive = false;

            bool IsFound = clsUsersData.GetUserInfoByPersonID(PersonID, ref UserID, ref UserName, ref Password, ref IsActive);
            if (IsFound) return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else return null;
        }

        public static clsUser FindByUserNameAndPassword(string UserName, string Password)
        {
            int UserID = -1; int PersonID = -1; bool IsActive = false;

            bool IsFound = clsUsersData.GetUserInfoByUserNameAndPassword(UserName, Password, ref UserID, ref PersonID, ref IsActive);
            if (IsFound) return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else return null;
        }

        private bool _AddNewUser() 
        {
            this.UserID = clsUsersData.AddNewUser(PersonID, UserName, Password, IsActive);
            return this.UserID != -1;
        }

        private bool _UpdateUser()
        {
            return clsUsersData.UpdateUser(UserID, PersonID, UserName, Password, IsActive);
        }

        public static DataTable GetAllUsers() 
        {
            return clsUsersData.GetAllUsers();
        }

        public static  bool IsUserExist(int UserID)
        {
            return clsUsersData.IsUserExist(UserID);
        }

        public static bool IsUserExist(string UserName)
        {
            return clsUsersData.IsUserExist(UserName);
        }

        public static bool IsUserExistForPersonID(int PersonID)
        {
            return clsUsersData.IsUserExistForPersonID(PersonID);
        }
        public static bool DeleteUser(int UserID) 
        {
            return clsUsersData.DeleteUser(UserID);
        }

        public bool Save()
        {
            switch (_Mode) 
            {
                case enMode.AddNew:
                    if (_AddNewUser()) 
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else 
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateUser();
            }return false;
        }
    }
}
