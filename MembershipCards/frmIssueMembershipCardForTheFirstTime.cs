using GMS.GlobalClasses;
using GMS.Trainers;
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

namespace GMS.MembershipCards
{
    public partial class frmIssueMembershipCardForTheFirstTime : Form
    {
        DataTable dataTable;
        int _TrainerID = -1;
        int _SubscriptionID = -1;
        clsSubscription _Subscription;
        public frmIssueMembershipCardForTheFirstTime(int SubscriptionID)
        {
            InitializeComponent();
            _SubscriptionID = SubscriptionID;
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            int CardID = _Subscription.IssueMembershipCardForFirstTime(_TrainerID ,clsGlobalVars.CurrentUser.UserID);
            if (CardID != -1)
            {
                MessageBox.Show("Card Issued Successfully with Card ID = " + CardID.ToString(),
                    "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show("Card Was not Issued ! ",
                 "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void _FillDgvTrainersWithData() 
        {
            DataTable dt = clsTrainer.GetAllTrainers();
            dgvTrainers.DataSource = dt;

            if (dgvTrainers.Rows.Count > 0) 
            {
                dgvTrainers.Columns[0].HeaderText = "Trainer ID";
                dgvTrainers.Columns[0].Width = 100;

                dgvTrainers.Columns[1].HeaderText = "Full Name";
                dgvTrainers.Columns[1].Width = 300;

                dgvTrainers.Columns[2].HeaderText = "Gender";
                dgvTrainers.Columns[2].Width = 100;

                dgvTrainers.Columns[3].HeaderText = "Employment Status";
                dgvTrainers.Columns[3].Width = 200;

                dgvTrainers.Columns[4].HeaderText = "Speciality";
                dgvTrainers.Columns[4].Width = 200;

                dgvTrainers.Columns[5].HeaderText = "Salary";
                dgvTrainers.Columns[5].Width = 100;

                dgvTrainers.Columns[6].HeaderText = "Is Active";
                dgvTrainers.Columns[6].Width = 100;

                dgvTrainers.Columns[7].HeaderText = "Managed Members";
                dgvTrainers.Columns[7].Width = 100;
            }
        }
        private void frmIssueMembershipCardForTheFirstTime_Load(object sender, EventArgs e)
        {
            _FillDgvTrainersWithData();
            _Subscription = clsSubscription.FindByID(_SubscriptionID);

            if (_Subscription == null)
            {
                MessageBox.Show("No Applicaiton with ID=" + _SubscriptionID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int CardID = _Subscription.GetActiveCardID();
            if (CardID != -1)
            {
                MessageBox.Show("Person already has Card before with Card ID=" + CardID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            ctrlSubscriptionInfo1.LoadSubscriptionInfoByID(_SubscriptionID);
        }

        private void dgvTrainers_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void dgvTrainers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int TrainerID = (int)dgvTrainers.CurrentRow.Cells[0].Value;
            frmTrainerInfo frm = new frmTrainerInfo(TrainerID);
            frm.ShowDialog();
            _TrainerID = TrainerID;
        }
    }
}
