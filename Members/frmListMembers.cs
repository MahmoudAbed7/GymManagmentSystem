using GMS.People;
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

namespace GMS.Members
{
    public partial class frmListMembers : Form
    {
        DataTable dt;
        public frmListMembers()
        {
            InitializeComponent();
        }

        private void frmListMembers_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            dt = clsMember.GetAllMembers();
            dgvMembers.DataSource = dt;
            lblRecordsCount.Text = dgvMembers.Rows.Count.ToString();

            if (dgvMembers.Rows.Count > 0) 
            {
                dgvMembers.Columns[0].HeaderText = "Member ID";
                dgvMembers.Columns[0].Width = 100;

                dgvMembers.Columns[1].HeaderText = "Person ID";
                dgvMembers.Columns[1].Width = 100;

                dgvMembers.Columns[2].HeaderText = "Full Name";
                dgvMembers.Columns[2].Width = 300;

                dgvMembers.Columns[3].HeaderText = "Gender";
                dgvMembers.Columns[3].Width = 300;

                dgvMembers.Columns[4].HeaderText = "Added Date";
                dgvMembers.Columns[4].Width = 100;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = cbFilterBy.Text != "None";
            if (txtFilterValue.Visible) 
            {
                txtFilterValue.Focus();
                txtFilterValue.Text = "";
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text) 
            {
                case "Member ID":
                    FilterColumn = "MemberID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text == "" || cbFilterBy.Text == "None")
            {
                dt.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvMembers.Rows.Count.ToString();
                return;
            }

            if (FilterColumn != "FullName" && FilterColumn != "NationalNo")
                dt.DefaultView.RowFilter = $"[{FilterColumn}] = {txtFilterValue.Text.Trim()}";
            else
                dt.DefaultView.RowFilter = $"[{FilterColumn}] Like '{txtFilterValue.Text.Trim()}%'";

            lblRecordsCount.Text = dgvMembers.Rows.Count.ToString();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvMembers.CurrentRow.Cells[1].Value;
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();


            frmListMembers_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "Person ID" ||  txtFilterValue.Text == "MemberID") 
            e.Handled = !char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
