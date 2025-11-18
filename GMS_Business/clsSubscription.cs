using GMS_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS_Business
{
    public class clsSubscription : clsApplication
    {
        enum enMode { AddNew = 1, Update = 2 };
        enMode Mode = enMode.AddNew;

        public int SubscriptionID {  get; set; }
        public string FullName { get { return clsPerson.Find(ApplicantPersonID).FullName; } }
        
        public clsSubscription() 
        {
            Mode = enMode.AddNew;
            SubscriptionID = -1;
        }

        public clsSubscription(int  subscriptionID, int applicationID, int applicantPersonID
            , DateTime applicationDate, int applicationTypeID, enApplicationStatus applicationStatus
            , DateTime lastStatusDate, float paidFees, int addedByUserID)
        {
            Mode = enMode.Update;
            SubscriptionID = subscriptionID;
            ApplicationID = applicationID;
            ApplicantPersonID = applicantPersonID;
            ApplicationDate = applicationDate;
            ApplicationTypeID = applicationTypeID;
            ApplicationStatus = applicationStatus;
            LastStatusDate = lastStatusDate;
            PaidFees = paidFees;
            AddedByUserID = addedByUserID;
        }

        public static clsSubscription FindByID(int SubscriptionID) 
        {
            int ApplicationID = -1; 
            bool IsFound = clsSubscriptionData.GetSubscriptionByID(SubscriptionID, ref ApplicationID);
            if (IsFound) 
            {
                clsApplication Application = clsApplication.FindByID(ApplicationID);
                return new clsSubscription(SubscriptionID, ApplicationID, Application.ApplicantPersonID
                    , Application.ApplicationDate, Application.ApplicationTypeID
                    , Application.ApplicationStatus, Application.LastStatusDate,
                    Application.PaidFees, Application.AddedByUserID);
            }else return null;
        }

        public static clsSubscription FindByApplicationID(int ApplicationID)
        {
            int SubscriptionID = -1;
            bool IsFound = clsSubscriptionData.GetSubscriptionByApplicationID(ApplicationID, ref SubscriptionID);
            if (IsFound)
            {
                clsApplication Application = clsApplication.FindByID(ApplicationID);
                return new clsSubscription(SubscriptionID, ApplicationID, Application.ApplicantPersonID
                    , Application.ApplicationDate, Application.ApplicationTypeID
                    , Application.ApplicationStatus, Application.LastStatusDate,
                    Application.PaidFees, Application.AddedByUserID);
            }
            else return null;
        }

        private bool _AddNewSubscription() 
        {
            this.SubscriptionID = clsSubscriptionData.AddNewSubscription(ApplicationID);
            return SubscriptionID != -1;
        }

        private bool _UpdateSubscription()
        {
            return clsSubscriptionData.UpdateSubscription(SubscriptionID, ApplicationID);
        }

        public bool DeleteSubscription(int SubscriptionID) 
        {
            bool IsSubscriptionDeleted = clsSubscriptionData.DeleteSubscription(SubscriptionID);
            if (!IsSubscriptionDeleted) return false;

            bool IsBaseApplicationDeleted = base.Delete();
            return IsBaseApplicationDeleted;
        }

        public static DataTable GetAllSubscriptions() 
        {
            return clsSubscriptionData.GetAllSubscriptions();
        }

        public bool Save() 
        {
            (base.Mode) = (clsApplication.enMode) Mode;
            if(!base.Save()) return false;

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewSubscription())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;

                    }
                case enMode.Update:
                    return _UpdateSubscription();
            }
            return false;

        }

        public int IssueMembershipCardForFirstTime(int TrainerID, int AddedByUserID) 
        {
            int MemberID = -1;
            clsMember Member = clsMember.FindByPersonID(ApplicantPersonID);
            if (Member == null)
            {
                Member = new clsMember();
                Member.PersonID = ApplicantPersonID;
                Member.TrainerID = TrainerID;
                Member.AddedByUserID = AddedByUserID;
                Member.AddedDate = DateTime.Now;

                if (Member.Save()) MemberID = Member.MemberID;
                else MemberID = -1;
            }
            else 
            {
                MemberID = Member.MemberID;
            }

            clsMembershipCard membershipCard = new clsMembershipCard();
            membershipCard.MemberID = MemberID;
            membershipCard.ApplicationID = ApplicationID;
            membershipCard.IssueDate = DateTime.Now;
            membershipCard.ExpirationDate = membershipCard.IssueDate.AddYears(1);
            membershipCard.IssueReason = clsMembershipCard.enIssueReason.FirstTime;
            membershipCard.IsActive = true;
            membershipCard.AddedByUserID = AddedByUserID;

            if (membershipCard.Save()) 
            {
                this.SetComplete();
                return membershipCard.CardID;
            }
            else 
            {
                return -1;
            }
        }

        public bool IsCardIssued() 
        {
            return GetActiveCardID() != -1;
        }

        public int GetActiveCardID() 
        {
            return clsMembershipCard.GetActiveCardIDByPersonID(ApplicantPersonID);
        }
    }
}
