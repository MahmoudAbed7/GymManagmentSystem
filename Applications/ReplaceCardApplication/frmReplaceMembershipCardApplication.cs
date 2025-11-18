using GMS.GlobalClasses;
using GMS.MembershipCards;
using GMS_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMS.Applications.ReplaceCardApplication
{
    public partial class frmReplaceMembershipCardApplication : Form
    {
        int _NewCardID = -1;

        private void _ResetDefaultInfo()
        {
            ctrlMembershipCardInfoWithFilter1.txtLicenseIDFocus();
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUser.Text = clsGlobalVars.CurrentUser.UserName;
            lblApplicationFees.Text = clsApplicationType
                .Find((int)clsApplication.enApplicationType
                .RenewSubscription).Fees.ToString();
        }
        public frmReplaceMembershipCardApplication()
        {
            InitializeComponent();
        }

        private int _GetApplicationTypeID()
        {
            if (rbDamagedLicense.Checked)
                return (int)clsApplication.enApplicationType.CardReplacementForDamage;
            else
                return (int)clsApplication.enApplicationType.CardReplacementForLost;
        }

        private clsMembershipCard.enIssueReason _GetIssueReason()
        {
            if (rbDamagedLicense.Checked)
                return clsMembershipCard.enIssueReason.DamagedReplacement;
            else
                return clsMembershipCard.enIssueReason.LostReplacement;
        }

        private void ctrlMembershipCardInfoWithFilter1_OnCardSelected(int obj)
        {
            int SelectedCardID = obj;
            lblOldLicenseID.Text = SelectedCardID.ToString();

            if (SelectedCardID == -1)
            {
                return;
            }

            if (!ctrlMembershipCardInfoWithFilter1.SelectedMembershipCardInfo.IsActive)
            {
                MessageBox.Show("Selected card is not Not Active, choose an active card."
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueReplacement.Enabled = false;
                return;
            }

            btnIssueReplacement.Enabled = true;
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Issue a Replacement for the card?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsMembershipCard NewCard = ctrlMembershipCardInfoWithFilter1
                .SelectedMembershipCardInfo.Replace(_GetIssueReason()
                , clsGlobalVars.CurrentUser.UserID);

            if (NewCard == null)
            {
                MessageBox.Show("Faild to Issue a replacemnet for this  Card", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblApplicationID.Text = NewCard.ApplicationID.ToString();
            _NewCardID = NewCard.CardID;

            lblRreplacedLicenseID.Text = _NewCardID.ToString();
            MessageBox.Show("Card Replaced Successfully with ID=" + _NewCardID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssueReplacement.Enabled = false;
            gbReplacementFor.Enabled = false;
            ctrlMembershipCardInfoWithFilter1.FilterEnabled = false;
            llShowCardInfo.Enabled = true;
        }

        private void frmReplaceMembershipCardApplication_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUser.Text = clsGlobalVars.CurrentUser.UserName;

            rbDamagedLicense.Checked = true;
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replacement for Damaged License";
            this.Text = lblTitle.Text;
            lblApplicationFees.Text = clsApplicationType.Find
                (_GetApplicationTypeID()).Fees.ToString();
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replacement for Lost License";
            this.Text = lblTitle.Text;
            lblApplicationFees.Text = clsApplicationType
                .Find(_GetApplicationTypeID()).Fees.ToString();
        }

        private void llShowCardInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowMembershipCardInfo frm = new frmShowMembershipCardInfo(_NewCardID);
            frm.ShowDialog();
        }

    }
}
