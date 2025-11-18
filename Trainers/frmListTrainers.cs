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

namespace GMS.Trainers
{
    public partial class frmListTrainers : Form
    {
        DataTable _TrainerDataTable;
        public frmListTrainers()
        {
            InitializeComponent();
        }

        private void frmListTrainers_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _TrainerDataTable = clsTrainer.GetAllTrainers();
            dgvTrainers.DataSource = _TrainerDataTable;
            lblRecordsCount.Text = dgvTrainers.Rows.Count.ToString();

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

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateTrainer frm = new frmAddUpdateTrainer();
            frm.ShowDialog();

            frmListTrainers_Load(null, null);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }
            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsActive.Visible = false;
                if (txtFilterValue.Visible)
                {
                    txtFilterValue.Text = "";
                    txtFilterValue.Focus();
                }
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text) 
            {
                case "Trainer ID":
                    FilterColumn = "TrainerID";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Gender":
                    FilterColumn = "Gender";
                    break;
                case "Employment Status":
                    FilterColumn = "EmploymentStatus";
                    break;
                case "Speciality":
                    FilterColumn = "Speciality";
                    break;
                case "Salary":
                    FilterColumn = "Salary";
                    break;

            }


            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _TrainerDataTable.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvTrainers.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "TrainerID")
                //in this case we deal with numbers not string.
                _TrainerDataTable.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _TrainerDataTable.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = _TrainerDataTable.Rows.Count.ToString();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text;

            switch (FilterValue) 
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }

            if (cbIsActive.Text == "All")
                _TrainerDataTable.DefaultView.RowFilter = "";
            else
                _TrainerDataTable.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

                lblRecordsCount.Text = dgvTrainers.Rows.Count.ToString();

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TrainerID = (int)dgvTrainers.CurrentRow.Cells[0].Value;
            frmTrainerInfo frm = new frmTrainerInfo(TrainerID);
            frm.ShowDialog();

            frmListTrainers_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TrainerID = (int)dgvTrainers.CurrentRow.Cells[0].Value;
            frmAddUpdateTrainer frm = new frmAddUpdateTrainer(TrainerID);
            frm.ShowDialog();

            frmListTrainers_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TrainerID = (int)dgvTrainers.CurrentRow.Cells[0].Value;
            if (clsTrainer.DeleteTrainer(TrainerID))
            {
                MessageBox.Show("Trainer has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmListTrainers_Load(null, null);
            }

            else
                MessageBox.Show("Trainer is not delted due to data connected to it.", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
