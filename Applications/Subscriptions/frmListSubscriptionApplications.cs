using GMS.MembershipCards;
using GMS.Subscriptions;
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

namespace GMS.Applications.Subscriptions
{
    public partial class frmListSubscriptionApplications : Form
    {
        private DataTable _SubscriptionTable;
        public frmListSubscriptionApplications()
        {
            InitializeComponent();
        }

        private void frmListSubscriptionApplications_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _SubscriptionTable = clsSubscription.GetAllApplications();
            dgvSubscriptionApplications.DataSource = _SubscriptionTable;
            lblRecordsCount.Text = dgvSubscriptionApplications.Rows.Count.ToString();

            if (dgvSubscriptionApplications.Rows.Count > 0) 
            {
                dgvSubscriptionApplications.Columns[0].HeaderText = "Subscription ID";
                dgvSubscriptionApplications.Columns[0].Width = 100;

                dgvSubscriptionApplications.Columns[1].HeaderText = "Application ID";
                dgvSubscriptionApplications.Columns[1].Width = 100;

                dgvSubscriptionApplications.Columns[2].HeaderText = "Full Name";
                dgvSubscriptionApplications.Columns[2].Width = 300;

                dgvSubscriptionApplications.Columns[3].HeaderText = "Application Date";
                dgvSubscriptionApplications.Columns[3].Width = 200;

                dgvSubscriptionApplications.Columns[4].HeaderText = "Status";
                dgvSubscriptionApplications.Columns[4].Width = 100;

            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = cbFilterBy.Text != "None";
            if (cbFilterBy.Text != "None") 
            {
                txtFilterValue.Text = string.Empty;
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text) 
            {
                case "Subscription ID":
                    FilterColumn = "SubscriptionID";
                    break;
                case "Application ID":
                    FilterColumn = "ApplicationID";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Status":
                    FilterColumn = "Status";
                    break;
                    default:
                    FilterColumn = "None";
                    break;
            }

            if(txtFilterValue.Text == "" || cbFilterBy.Text == "None") 
            {
                _SubscriptionTable.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvSubscriptionApplications.Rows.Count.ToString();
                return;
            }

            if(cbFilterBy.Text == "Subscription ID" || cbFilterBy.Text == "Application ID")
                _SubscriptionTable.DefaultView.RowFilter = $"[{FilterColumn}] = {txtFilterValue.Text.Trim()}";
            else
                _SubscriptionTable.DefaultView.RowFilter = $"[{FilterColumn}] Like '{txtFilterValue.Text.Trim()}%'";
            lblRecordsCount.Text = dgvSubscriptionApplications.Rows.Count.ToString();


        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Subscription ID" || cbFilterBy.Text == "Application ID")
                e.Handled = !char.IsNumber(e.KeyChar) || !char.IsControl(e.KeyChar);
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddEditSubscription frm = new frmAddEditSubscription();
            frm.ShowDialog();

            frmListSubscriptionApplications_Load(null, null);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SubscriptionID = (int)dgvSubscriptionApplications.CurrentRow.Cells[0].Value;
            frmSubscriptionBasicInfo frm = new frmSubscriptionBasicInfo(SubscriptionID);
            frm.ShowDialog();

            frmListSubscriptionApplications_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SubscriptionID = (int)dgvSubscriptionApplications.CurrentRow.Cells[0].Value;
            frmAddEditSubscription frm = new frmAddEditSubscription(SubscriptionID);
            frm.ShowDialog();

            frmListSubscriptionApplications_Load(null, null);
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            int SubscriptionID = (int)dgvSubscriptionApplications.CurrentRow.Cells[0].Value;
            clsSubscription subscription = clsSubscription.FindByID(SubscriptionID);

            if (subscription != null)
            {

                if (subscription.ApplicationStatus == clsApplication.enApplicationStatus.New)
                {
                    editToolStripMenuItem.Enabled = true;
                    DeleteApplicationToolStripMenuItem.Enabled = true;
                    CancelApplicaitonToolStripMenuItem.Enabled = true;
                    issueMembershipCardFirstTimeToolStripMenuItem.Enabled = true;
                    showCardToolStripMenuItem.Enabled = false;
                }
                else
                {
                    showCardToolStripMenuItem.Enabled = true;
                    editToolStripMenuItem.Enabled = false;
                    DeleteApplicationToolStripMenuItem.Enabled = false;
                    CancelApplicaitonToolStripMenuItem.Enabled = false;
                    issueMembershipCardFirstTimeToolStripMenuItem.Enabled = false;
                }



            }
        }

        private void DeleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are sure you want to delete this subscription application"
                , "Delete Applicaiton", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation)
                == DialogResult.OK) 
            {
                int SubscriptionID = (int)dgvSubscriptionApplications.CurrentRow.Cells[0].Value;
                clsSubscription subscription = clsSubscription.FindByID(SubscriptionID);

                if (subscription != null)
                {
                    if (subscription.DeleteSubscription(SubscriptionID)) 
                    {
                        MessageBox.Show("Application Deleted Successfully.", "Deleted"
                            , MessageBoxButtons.OK, MessageBoxIcon.Information);

                        frmListSubscriptionApplications_Load(null, null);
                    }
                    else 
                    {
                        MessageBox.Show("Could not delete applicatoin, other data depends on it."
                            , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void CancelApplicaitonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are sure you want to cancel this subscription application"
                , "Cancel Applicaiton", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation)
                == DialogResult.OK)
            {
                int SubscriptionID = (int)dgvSubscriptionApplications.CurrentRow.Cells[0].Value;
                clsSubscription subscription = clsSubscription.FindByID(SubscriptionID);

                if (subscription != null)
                {
                    if (subscription.DeleteSubscription(SubscriptionID))
                    {
                        MessageBox.Show("Application Cancelled Successfully.", "Cancelled"
                            , MessageBoxButtons.OK, MessageBoxIcon.Information);

                        frmListSubscriptionApplications_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Could not Cancelled applicatoin, other data depends on it."
                            , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void issueMembershipCardFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SubscriptionID = (int)dgvSubscriptionApplications.CurrentRow.Cells[0].Value;
            frmIssueMembershipCardForTheFirstTime frm = new frmIssueMembershipCardForTheFirstTime(SubscriptionID);
            frm.ShowDialog();

            frmListSubscriptionApplications_Load(null, null);
            issueMembershipCardFirstTimeToolStripMenuItem.Enabled = false;
            showCardToolStripMenuItem.Enabled = true;
        }

        private void showCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ApplicationID = (int)dgvSubscriptionApplications.CurrentRow.Cells[1].Value;
            int CardID = clsSubscription.FindByApplicationID(ApplicationID).GetActiveCardID();
            if(CardID != -1)
            {
                frmShowMembershipCardInfo frm = new frmShowMembershipCardInfo(CardID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No Card Found!", "No Card", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
    }
}
