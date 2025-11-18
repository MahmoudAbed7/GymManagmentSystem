using GMS_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS_Business
{
    public class clsApplication
    {
        public enum enMode { AddNew = 1, Update = 2}
        public enMode Mode = enMode.AddNew;

        public enum enApplicationType
        {NewSubscription = 1, RenewSubscription = 3,
            CardReplacementForLost = 5, CardReplacementForDamage = 6}

        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3}

        public int ApplicationID { get; set; }
        public int ApplicantPersonID {  get; set; }
        public clsPerson PersonInfo;

        public string ApplicantFullName { get { return PersonInfo.FullName; } }

        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID {  get; set; }
        public clsApplicationType ApplicationTypeInfo;

        public enApplicationStatus ApplicationStatus { get; set; }

        public DateTime LastStatusDate {  get; set; }
        public float PaidFees { get; set; }
        public int AddedByUserID { get; set; }
        public clsUser UserInfo;

        public clsApplication() 
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = enApplicationStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.AddedByUserID = -1;

            Mode = enMode.AddNew;
        }

        public clsApplication(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int AddedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            PersonInfo = clsPerson.Find(ApplicantPersonID);
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            ApplicationTypeInfo = clsApplicationType.Find(ApplicationTypeID);
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.AddedByUserID = AddedByUserID;
            UserInfo = clsUser.FindByUserID(AddedByUserID);
            Mode = enMode.Update;
        }

        public static clsApplication FindByID(int ApplicationID) 
        {
            int ApplicantPersonID = -1;
            DateTime ApplicationDate = DateTime.Now;
            int ApplicationTypeID = (int)enApplicationType.NewSubscription;
            byte ApplicationStatus = 1;
            DateTime LastStatusDate = DateTime.Now; float PaidFees = 0; int AddedByUserID = -1;

            bool IsFound = clsApplicationData.GetApplicationByID(ApplicationID, ref ApplicantPersonID
                , ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate
                , ref PaidFees, ref AddedByUserID);

            if(IsFound) return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID
                , (enApplicationStatus) ApplicationStatus, LastStatusDate, PaidFees, AddedByUserID);
            else return null;
        }

        public static clsApplication FindByPersonID(int ApplicantPersonID)
        {
            int ApplicationID = -1;
            DateTime ApplicationDate = DateTime.Now;
            int ApplicationTypeID = (int)enApplicationType.NewSubscription;
            byte ApplicationStatus = 1;
            DateTime LastStatusDate = DateTime.Now; float PaidFees = 0; int AddedByUserID = -1;

            bool IsFound = clsApplicationData.GetApplicationByPersonID(ApplicantPersonID, ref ApplicationID
                , ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate
                , ref PaidFees, ref AddedByUserID);

            if (IsFound) return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID
                , (enApplicationStatus)ApplicationStatus, LastStatusDate, PaidFees, AddedByUserID);
            else return null;
        }

        private bool _AddNewApplication() 
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(ApplicantPersonID, ApplicationDate, ApplicationTypeID
                , (byte)ApplicationStatus, LastStatusDate, PaidFees, AddedByUserID);
            return this.ApplicationID != -1;
        }

        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(ApplicationID, ApplicantPersonID, ApplicationDate
                , ApplicationTypeID, (byte)ApplicationStatus, LastStatusDate, PaidFees, AddedByUserID);
        }

        public bool Delete() 
        {
            return clsApplicationData.DeleteApplication(this.ApplicationID); 
        }

        public bool Cancel() 
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID, 2);
        }

        public bool SetComplete() 
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID, 3);
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            return clsApplicationData.IsApplicationExist(ApplicationID);
        }

        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return clsApplicationData.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }

        public bool DoesPersonHaveActiveApplication(int ApplicationTypeID)
        {
            return clsApplicationData.DoesPersonHaveActiveApplication(ApplicantPersonID, ApplicationTypeID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateApplication();

            }

            return false;
        }

        public static DataTable GetAllApplications() 
        {
            return clsApplicationData.GetAllApplications();
        }
    }
}
