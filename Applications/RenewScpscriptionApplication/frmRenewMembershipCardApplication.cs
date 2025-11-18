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

namespace GMS.Applications.RenewScpscriptionApplication
{
    public partial class frmRenewMembershipCardApplication : Form
    {
        int _NewCardID = -1;
        public frmRenewMembershipCardApplication()
        {
            InitializeComponent();
        }

        private void _ResetDefaultInfo()
        {
            ctrlMembershipCardInfoWithFilter1.txtLicenseIDFocus();
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUser.Text = clsGlobalVars.CurrentUser.UserName;
            lblIssueDate.Text = lblApplicationDate.Text;
            lblApplicationFees.Text = clsApplicationType
                .Find((int)clsApplication.enApplicationType
                .RenewSubscription).Fees.ToString();
            lblExpirationDate.Text = "???";
        }

        private void ctrlMembershipCardInfoWithFilter1_OnCardSelected(int obj)
        {
            int SelectedCard = obj;

            lblOldCardID.Text = SelectedCard.ToString();

            if (SelectedCard == -1) return;

            lblExpirationDate.Text = ctrlMembershipCardInfoWithFilter1
                .SelectedMembershipCardInfo.ExpirationDate.ToShortDateString();
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication
                .enApplicationType.RenewSubscription).Fees.ToString();

            if (!ctrlMembershipCardInfoWithFilter1.SelectedMembershipCardInfo.IsCardExpired())
            {
                MessageBox.Show("Selected Card is not yet expiared, it will expire on: "
                    + ctrlMembershipCardInfoWithFilter1.SelectedMembershipCardInfo
                    .ExpirationDate.ToShortDateString()
                   , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenew.Enabled = false;
                return;
            }

            if (!ctrlMembershipCardInfoWithFilter1.SelectedMembershipCardInfo.IsActive)
            {
                MessageBox.Show("Selected Card is not Not Active, choose an active card."
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenew.Enabled = false;
                return;
            }

            btnRenew.Enabled = true;
        }

        private void frmRenewMembershipCardApplication_Load(object sender, EventArgs e)
        {
            _ResetDefaultInfo();
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Renew the card?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsMembershipCard NewCard = ctrlMembershipCardInfoWithFilter1.SelectedMembershipCardInfo
                .RenewMembershipCard(clsGlobalVars.CurrentUser.UserID);
                

            if (NewCard == null)
            {
                MessageBox.Show("Faild to Renew the Card", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblSubscriptionID.Text = NewCard.ApplicationID.ToString();
            _NewCardID = NewCard.CardID;
            lblRenewedCardID.Text = _NewCardID.ToString();
            MessageBox.Show("Card Renewed Successfully with ID=" + _NewCardID.ToString(), "Card Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRenew.Enabled = false;
            ctrlMembershipCardInfoWithFilter1.FilterEnabled = false;
            llShowNewCardInfo.Enabled = true;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowCardHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowMembershipCardInfo frm = new frmShowMembershipCardInfo(_NewCardID);
            frm.ShowDialog();
        }
    }
}
