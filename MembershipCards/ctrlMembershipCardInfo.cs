using GMS.Properties;
using GMS_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMS.MembershipCards
{
    public partial class ctrlMembershipCardInfo : UserControl
    {
        int _CardID = -1;
        public int CardID {  get { return _CardID; }}

        clsMembershipCard _MemberShipCard;
        public clsMembershipCard MembershipCard { get { return _MemberShipCard; }}

        private void _ResetDefaultValues() 
        {
            lblCardID.Text = "???";
            lblFullName.Text = "???";
            lblMemberID.Text = "???";
            lblCountry.Text = "???";
            lblGendor.Text = "???";
            lblIssueDate.Text = "???";
            lblIssueReason.Text = "???";
            lblIsActive.Text = "???";
            lblDateOfBirth.Text = "???";
            lblExpirationDate.Text = "???";
            lblCoach.Text = "???";
        }

        private void _LoadPersonImage() 
        {
            if (_MemberShipCard.MemberInfo.PersonInfo.Gendor == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _MemberShipCard.MemberInfo.PersonInfo.ProfileImage;
           if(ImagePath != "")
                if(File.Exists(ImagePath))
                    pbPersonImage.Load(ImagePath);
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        public void LoadInfo(int CardID) 
        {
            _CardID = CardID;
            _MemberShipCard = clsMembershipCard.Find(CardID);
            if( _MemberShipCard == null) 
            {
                MessageBox.Show("Could not find Card ID = " + _CardID.ToString(),
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _CardID = -1;
                _ResetDefaultValues();
                return;
            }

            lblCardID.Text = _MemberShipCard.CardID.ToString();
            lblFullName.Text = _MemberShipCard.MemberInfo.PersonInfo.FullName;
            lblMemberID.Text = _MemberShipCard.MemberID.ToString();
            lblCountry.Text = clsCountry.Find(_MemberShipCard.MemberInfo
                .PersonInfo.NationalCountryID).CountryName;
            lblGendor.Text = _MemberShipCard.MemberInfo
                .PersonInfo.Gendor == 0 ? "Male" : "Female";
            lblIssueDate.Text = _MemberShipCard.IssueDate.ToShortDateString();
            lblIssueReason.Text = _MemberShipCard.IssueReasonText;
            lblIsActive.Text = _MemberShipCard.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = _MemberShipCard.MemberInfo.PersonInfo.DateOfBirth.ToShortDateString();
            lblExpirationDate.Text = _MemberShipCard.ExpirationDate.ToShortDateString();
            lblCoach.Text = _MemberShipCard.MemberInfo.TrainerInfo.PersonInfo.FirstName + " "
                + _MemberShipCard.MemberInfo.TrainerInfo.PersonInfo.LastName;
            _LoadPersonImage();

        }
        public ctrlMembershipCardInfo()
        {
            InitializeComponent();
        }
    }
}
