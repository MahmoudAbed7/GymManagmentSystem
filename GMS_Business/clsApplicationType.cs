using GMS_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GMS_Business
{
    public class clsApplicationType
    {
        enum enMode { AddNew, Update}
        enMode Mode = enMode.AddNew;
        public int ApplicationTypeID {  get; set; }
        public string Title {  get; set; }
        public float Fees {  get; set; }

        public clsApplicationType() 
        {
            ApplicationTypeID = -1; Title = ""; Fees = 0;
            Mode = enMode.AddNew;
        }

        public clsApplicationType(int applicationTypeID, string title, float fees)
        {
            ApplicationTypeID = applicationTypeID;
            Title = title;
            Fees = fees;
            Mode = enMode.Update;
        }

        public static clsApplicationType Find(int ApplicationTypeID) 
        {
            string title = ""; float fees = 0;
            bool IsFound = clsApplicationTypesData.GetApplicationTypeByID(ApplicationTypeID
                , ref title, ref fees);
            if(IsFound) return new clsApplicationType(ApplicationTypeID, title, fees);
            else return null;
        }

        private bool _AddNewApplicationType() 
        {
            this.ApplicationTypeID = clsApplicationTypesData
                .AddNewApplicationType(this.Title, this.Fees);
            return this.ApplicationTypeID != -1;
        }

        private bool _UpdateApplicationType()
        {
            return clsApplicationTypesData
                .UpdateApplicationType(ApplicationTypeID, Title, Fees);
        }

        public bool Save() 
        {
            switch (Mode) 
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType()) 
                    {
                        Mode = enMode.Update;
                        return true;
                    }else return false;
                case enMode.Update:
                    return _UpdateApplicationType();
            }return false;
        }

        public static DataTable GetAllApplicationTypes() 
        {
            return clsApplicationTypesData.GetAllApplicationTypes();
        }
    }
}
