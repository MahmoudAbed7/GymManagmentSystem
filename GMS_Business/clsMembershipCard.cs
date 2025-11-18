using GMS_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GMS_Business.clsMembershipCard;

namespace GMS_Business
{
    public class clsMembershipCard
    {
        enum enMode { AddNew = 0, Update = 1 }
        enMode Mode = enMode.AddNew;

        public enum enIssueReason
        {
            FirstTime = 1, Renew = 2
                , DamagedReplacement = 3, LostReplacement = 4
        };

        public clsMember MemberInfo {  get; set; }
        public clsApplication ApplicationInfo;
        public int CardID {  get; set; }
        public int ApplicationID {  get; set; }
        public int MemberID {  get; set; }
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public bool IsActive { set; get; }
        public int AddedByUserID {  get; set; }
        
        public enIssueReason IssueReason {  get; set; }
        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText(this.IssueReason);
            }
        }

        public static string GetIssueReasonText(enIssueReason IssueReason)
        {

            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Replacement for Damaged";
                case enIssueReason.LostReplacement:
                    return "Replacement for Lost";
                default:
                    return "First Time";
            }
        }

        public clsMembershipCard() 
        {
            Mode = enMode.AddNew;
            CardID = -1; ApplicationID = -1; MemberID = -1;
            IssueDate = DateTime.Now; ExpirationDate = DateTime.MaxValue;
            IssueReason = enIssueReason.FirstTime; IsActive = false; 
            AddedByUserID = -1;
        }

        public clsMembershipCard(int cardID, int applicationID, int memberID
            , DateTime issueDate, DateTime expirationDate
            , enIssueReason issueReason, bool isActive, int addedByUserID)
        {
            Mode = enMode.Update;
            CardID = cardID; ApplicationID = applicationID; MemberID = memberID;
            IssueDate = issueDate; ExpirationDate = expirationDate;
            IssueReason = issueReason; IsActive = isActive; AddedByUserID = addedByUserID;
            
            MemberInfo = clsMember.FindByID(memberID);
        }

        private bool _AddNewMembershipCard()
        {
            //call DataAccess Layer 

            this.CardID = clsMembershipCardData.AddNewMembershipCard(this.ApplicationID
            , this.MemberID, this.IssueDate, this.ExpirationDate
            , (byte)this.IssueReason, this.IsActive, this.AddedByUserID);


            return (this.CardID != -1);
        }

        private bool _UpdateMembershipCard()
        {
            //call DataAccess Layer 

            return clsMembershipCardData.UpdateMembershipCard(CardID, ApplicationID, MemberID
                , IssueDate, ExpirationDate, (byte)IssueReason,
                IsActive, AddedByUserID);
        }

        public static clsMembershipCard Find(int CardID) 
        {
            int cardID = -1; int applicationID = -1; int memberID = -1;
            DateTime issueDate = DateTime.Now;
            DateTime expirationDate = DateTime.MaxValue;
            byte issueReason = (byte)enIssueReason.FirstTime;
            bool isActive = false; int addedByUserID = -1;

            bool IsFound = clsMembershipCardData.GetMembershipCardByID(CardID
                , ref applicationID, ref memberID, ref issueDate
                , ref expirationDate, ref issueReason, ref isActive, ref addedByUserID);
            if (IsFound) return new clsMembershipCard(CardID, applicationID, memberID
                , issueDate, expirationDate, (enIssueReason) issueReason
                , isActive, addedByUserID);

            else return null;   
        }

        public static bool IsCardExistByPersonID(int PersonID) 
        {
            return clsMembershipCardData.GetActiveMembershipCardForMembersByPersonID(PersonID) != -1;
        }

        public static int GetActiveCardIDByPersonID(int PersonID) 
        {
            return clsMembershipCardData.GetActiveMembershipCardForMembersByPersonID(PersonID);
        }

        public static DataTable GetMemberCards(int MemberID) 
        {
            return clsMembershipCardData.GetMemberCards(MemberID);
        }

        public bool IsCardExpired() 
        {
            return this.ExpirationDate < DateTime.Now;
        }

        public bool DeactivateCurrentCard()
        {
            return (clsMembershipCardData.DeactivateCard(this.CardID));
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewMembershipCard())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateMembershipCard();

            }

            return false;
        }

        public clsMembershipCard RenewMembershipCard(int AddedByUserID)
        {
            clsApplication Application = new clsApplication();
            Application.ApplicantPersonID = MemberInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewSubscription;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.Find((int)clsApplication.
                enApplicationType.NewSubscription).Fees;
            Application.AddedByUserID = AddedByUserID;

            if (!Application.Save()) return null;

            clsMembershipCard Card = new clsMembershipCard();
            Card.ApplicationID = Application.ApplicationID;
            Card.MemberID = this.MemberID;
            Card.IssueDate = DateTime.Now;
            Card.ExpirationDate = DateTime.Now.AddYears(1);
            Card.IssueReason = enIssueReason.Renew;
            Card.IsActive = true;
            Card.AddedByUserID = this.AddedByUserID;

            if(!Card.Save()) return null;

            DeactivateCurrentCard();

            return Card;
        }

        public clsMembershipCard Replace(enIssueReason IssueReason, int CreatedByUserID)
        {
            clsApplication Application = new clsApplication();
            Application.ApplicantPersonID = MemberInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;

            Application.ApplicationTypeID = (IssueReason == enIssueReason.DamagedReplacement) ?
                           (int)clsApplication.enApplicationType.CardReplacementForDamage :
                           (int)clsApplication.enApplicationType.CardReplacementForLost;

            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.Find
                (Application.ApplicationTypeID).Fees;
            Application.AddedByUserID = CreatedByUserID;

            if (!Application.Save()) return null;

            clsMembershipCard NewCard = new clsMembershipCard();
            NewCard.ApplicationID = Application.ApplicationID;
            NewCard.MemberID = MemberInfo.MemberID;
            NewCard.IssueDate = DateTime.Now;

            NewCard.ExpirationDate = ExpirationDate;
            NewCard.IsActive = true;
            NewCard.IssueReason = IssueReason;
            NewCard.AddedByUserID = CreatedByUserID;

            if (!NewCard.Save()) return null;

            DeactivateCurrentCard();

            return NewCard;

        }
    }
}
