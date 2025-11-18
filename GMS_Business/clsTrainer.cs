using GMS_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS_Business
{
    public class clsTrainer
    {
        public enum enMode { AddNew = 0, Update = 1}
        enMode Mode = enMode.AddNew;

        public enum enEmploymentStatus { FullTime = 1, PartTime = 2, Contractor = 3 }

        public int TrainerID {  get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo;

        public enEmploymentStatus EmploymentStatus { get; set; }

        public string Speciality {  get; set; }

        public DateTime HireDate {  get; set; }

        public DateTime EndDate { get; set; }

        public float Salary {  get; set; }
        public bool IsActive {  get; set; }

        public int AddedByUserID {  get; set; }
        public clsUser UserInfo;

        public string EmploymentText 
        { get 
            {
                switch (EmploymentStatus) 
                {
                    case enEmploymentStatus.FullTime:
                        return "Full-Time";
                    case enEmploymentStatus.PartTime:
                        return "Part-Time";
                    case enEmploymentStatus.Contractor:
                        return "Contractor";
                    default:
                        return "UnEmployed";
                }
            } 
        }

        public clsTrainer() 
        {
            TrainerID = -1; PersonID = -1;AddedByUserID = -1;
            EmploymentStatus = enEmploymentStatus.FullTime;
            Speciality = ""; Salary = 0; IsActive = true;
            HireDate = DateTime.Now; EndDate = DateTime.MaxValue;
            Mode = enMode.AddNew;
        }

        public clsTrainer(int TrainerID, int PersonID, enEmploymentStatus EmploymentStatus
            , string Speciality, DateTime HireDate, DateTime EndDate, float Salary
            , bool IsActive, int AddedByUserID)
        {
            this.TrainerID = TrainerID;
            this.PersonID = PersonID;
            this.EmploymentStatus = EmploymentStatus;
            this.Speciality = Speciality;
            PersonInfo = clsPerson.Find(PersonID);
            this.HireDate = HireDate;
            this.EndDate = EndDate;
            this.Salary = Salary;
            this.IsActive = IsActive;
            this.AddedByUserID = AddedByUserID;
            UserInfo = clsUser.FindByUserID(AddedByUserID);
            Mode = enMode.Update;
        }

        public static bool IsTrainerExist(int TrainerID) 
        {
            return clsTrainerData.IsTrainerExistByID(TrainerID);
        }

        public static bool IsTrainerExistByPersonID(int PersonID)
        {
            return clsTrainerData.IsTrainerExistByPersonID(PersonID);
        }

        public static clsTrainer FindByID(int TrainerID) 
        {
            int personID = -1; int addedByUserID = -1;
            byte employmentStatus = (byte)enEmploymentStatus.FullTime;
            string speciality = ""; float salary = 0; bool isActive = true;
            DateTime hireDate = DateTime.Now; DateTime endDate = DateTime.MaxValue;

            bool IsFound = clsTrainerData.GetTrainerByID(TrainerID, ref personID,ref employmentStatus
                , ref speciality, ref hireDate, ref endDate, ref salary, ref isActive, ref addedByUserID);
           
            if(IsFound) return new clsTrainer(TrainerID, personID, (enEmploymentStatus)employmentStatus
                , speciality, hireDate, endDate, salary, isActive, addedByUserID);
            else return null;
        }

        public static clsTrainer FindByPersonID(int PersonID)
        {
            int trainerID = -1; int addedByUserID = -1;
            byte employmentStatus = (byte)enEmploymentStatus.FullTime;
            string speciality = ""; float salary = 0; bool isActive = true;
            DateTime hireDate = DateTime.Now; DateTime endDate = DateTime.MaxValue;

            bool IsFound = clsTrainerData.GetTrainerByID(PersonID, ref trainerID, ref employmentStatus
                , ref speciality, ref hireDate, ref endDate, ref salary, ref isActive, ref addedByUserID);

            if (IsFound) return new clsTrainer(trainerID, PersonID, (enEmploymentStatus)employmentStatus
                , speciality, hireDate, endDate, salary, isActive, addedByUserID);
            else return null;
        }

        private bool _AddNewTrainer() 
        {
            this.TrainerID = clsTrainerData.AddNewTrainer(this.PersonID, (byte)this.EmploymentStatus
                , this.Speciality, this.HireDate, this.EndDate, this.Salary
                , this.IsActive, this.AddedByUserID);
            return this.TrainerID != -1;
        }

        private bool _UpdateTrainer()
        {
            return clsTrainerData.UpdateTrainer(this.TrainerID, this.PersonID, (byte)this.EmploymentStatus
                , this.Speciality, this.HireDate, this.EndDate, this.Salary, this.IsActive, this.AddedByUserID);
        }

        public static bool DeleteTrainer(int TrainerID) 
        {
            return clsTrainerData.DeleteTrainer(TrainerID);
        }

        public bool Save() 
        {
            switch (Mode) 
            {
                case enMode.AddNew:
                    if (_AddNewTrainer()) 
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else 
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateTrainer();
            }return false;
        }

        public static DataTable GetAllTrainers()
        { return clsTrainerData.GetAllTrainers(); }


    }
}
