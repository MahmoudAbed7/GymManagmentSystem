using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMS_Business;

namespace GMS.People
{
    public partial class frmListPeople : Form
    {
        static DataTable _dtAllPeople = clsPerson.GetAllPeople();
        DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "FullName"
              , "CountryName", "DateOfBirth", "Gendor", "Phone", "Address");

        public frmListPeople()
        {
            InitializeComponent();
        }

        private void _RefreshPeopleList() 
        {
            _dtAllPeople = clsPerson.GetAllPeople();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "FullName"
                  , "CountryName", "DateOfBirth", "Gendor", "Phone", "Address");

            dgvPeople.DataSource = _dtPeople;
            lblPeopleRecords.Text = dgvPeople.Rows.Count.ToString();
        }
        private void frmListPeople_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            dgvPeople.DataSource = _dtPeople;
            lblPeopleRecords.Text = dgvPeople.Rows.Count.ToString();

            if(dgvPeople.Rows.Count > 0) 
            {
                dgvPeople.Columns[0].Width = 100;
                dgvPeople.Columns[0].HeaderText = "Person ID";

                dgvPeople.Columns[1].Width = 250;
                dgvPeople.Columns[1].HeaderText = "Full Name";

                dgvPeople.Columns[2].Width = 150;
                dgvPeople.Columns[2].HeaderText = "Country Name";

                dgvPeople.Columns[3].Width = 100;
                dgvPeople.Columns[3].HeaderText = "Date Of Birth";

                dgvPeople.Columns[4].Width = 100;
                dgvPeople.Columns[4].HeaderText = "Gendor";

                dgvPeople.Columns[5].Width = 100;
                dgvPeople.Columns[5].HeaderText = "Phone";

                dgvPeople.Columns[6].Width = 100;
                dgvPeople.Columns[6].HeaderText = "Address";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = cbFilterBy.Text != "None";
            if (txtFilterValue.Visible) 
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text) 
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Nationality":
                    FilterColumn = "CountryName";
                    break;
                case "Gendor":
                    FilterColumn = "Gendor";
                    break;
                case "Phone":
                    FilterColumn = "Phone";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if(txtFilterValue.Text == "" || cbFilterBy.Text == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblPeopleRecords.Text = dgvPeople.Rows.Count.ToString();
                return;
            }

            if (cbFilterBy.Text == "Person ID")
                _dtPeople.DefaultView.RowFilter = $"[{FilterColumn}] = {txtFilterValue.Text.Trim()}";
            else
                _dtPeople.DefaultView.RowFilter = $"[{FilterColumn}] Like '{txtFilterValue.Text.Trim()}%'";
            lblPeopleRecords.Text = dgvPeople.Rows.Count.ToString();

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "Phone")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPeople frm = new frmAddEditPeople();
            frm.ShowDialog();
            _RefreshPeopleList();

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeople.CurrentRow.Cells[0].Value;
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPeople frm = new frmAddEditPeople();
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeople.CurrentRow.Cells[0].Value;
            frmAddEditPeople frm = new frmAddEditPeople(PersonID);
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Person [" + dgvPeople.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)

            {
                    
                //Perform Delele and refresh
                if (clsPerson.DeletePerson((int)dgvPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshPeopleList();
                }

                else
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
