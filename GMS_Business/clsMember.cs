using GMS_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS_Business
{
    public class clsMember
    {
        enum enMode { AddNew = 0, Update = 1 }
        enMode Mode = enMode.AddNew;

        public int MemberID {  get; set; }
        public int PersonID {  get; set; }
        public clsPerson PersonInfo;

        public int TrainerID {  get; set; }
        public clsTrainer TrainerInfo;

        public int AddedByUserID {  get; set; }
        public clsUser UserInfo;

        public DateTime AddedDate { get; set; }

        public clsMember()
        {
            MemberID = -1; PersonID = -1; TrainerID = -1;
            AddedByUserID = -1; AddedDate = DateTime.Now;
            Mode = enMode.AddNew;
        }

        public clsMember(int memberID, int personID, int trainerID, int addedByUserID, DateTime addedDate)
        {
            MemberID = memberID; PersonID = personID; TrainerID = trainerID;
            AddedByUserID = addedByUserID; AddedDate = addedDate;
            TrainerInfo = clsTrainer.FindByID(trainerID);
            PersonInfo = clsPerson.Find(personID);
            UserInfo = clsUser.FindByUserID(AddedByUserID);
            Mode = enMode.Update;
        }

        private bool _AddNewMember() 
        {
            this.MemberID = clsMemberData.AddNewMember(PersonID, TrainerID, AddedByUserID, AddedDate);
            return this.MemberID != -1;
        }

        private bool _UpdateMember()
        {
            return clsMemberData.UpdateMember(MemberID, PersonID, TrainerID, AddedByUserID, AddedDate);
        }

        public static clsMember FindByID(int MemberID) 
        {
            int PersonID = -1; int TrainerID = -1;
            int AddedByUserID = -1; DateTime AddedDate = DateTime.Now;

            if (clsMemberData.GetMemberByID(MemberID, ref PersonID, ref TrainerID
                , ref AddedByUserID, ref AddedDate))

                return new clsMember(MemberID, PersonID, TrainerID, AddedByUserID, AddedDate);
            else
                return null;
        }

        public static clsMember FindByPersonID(int PersonID)
        {
            int MemberID = -1; int TrainerID = -1;
            int AddedByUserID = -1; DateTime AddedDate = DateTime.Now;

            if (clsMemberData.GetMemberByPersonID(PersonID, ref MemberID, ref TrainerID
                , ref AddedByUserID, ref AddedDate))

                return new clsMember(MemberID, PersonID, TrainerID, AddedByUserID, AddedDate);
            else
                return null;
        }

        public static DataTable GetAllMembers()
        {
            return clsMemberData.GetAllMembers();

        }

        public static bool IsMemberExistByPersonID(int PersonID) 
        {
            return clsMemberData.IsMemberExistByPersonID(PersonID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewMember())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateMember();

            }

            return false;
        }
    }
}
