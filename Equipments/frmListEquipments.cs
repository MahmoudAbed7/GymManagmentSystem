using GMS.GlobalClasses;
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

namespace GMS.Equipments
{
    public partial class frmListEquipments : Form
    {
        private DataTable _EquipmentsDT;

        public frmListEquipments()
        {
            InitializeComponent();
        }

        private void frmListEquipments_Load(object sender, EventArgs e)
        {
            _EquipmentsDT = clsEquipment.GetAllEquipments();
            dgvEquipments.DataSource = _EquipmentsDT;
            cbFilterBy.SelectedIndex = 0;
            lblEquipmentRecords.Text = dgvEquipments.Rows.Count.ToString();

            if (dgvEquipments.Rows.Count > 0) 
            {
                dgvEquipments.Columns[0].HeaderText = "Equipment ID";
                dgvEquipments.Columns[0].Width = 50;

                dgvEquipments.Columns[1].HeaderText = "Machine Name";
                dgvEquipments.Columns[1].Width = 150;

                dgvEquipments.Columns[2].HeaderText = "Machine Status";
                dgvEquipments.Columns[2].Width = 100;

                dgvEquipments.Columns[3].HeaderText = "Purchase Date";
                dgvEquipments.Columns[3].Width = 150;

                dgvEquipments.Columns[4].HeaderText = "Last Maintenance Date";
                dgvEquipments.Columns[4].Width = 150;

                dgvEquipments.Columns[5].HeaderText = "Added By User ID";
                dgvEquipments.Columns[5].Width = 100;
            }
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
                case "Equipment ID":
                    FilterColumn = "EquipmentID";
                    break;
                case "Machine Name":
                    FilterColumn = "Name";
                    break;

                case "Status":
                    FilterColumn = "Condition";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if(cbFilterBy.Text == "None" || txtFilterValue.Text == "") 
            {
                _EquipmentsDT.DefaultView.RowFilter = "";
                lblEquipmentRecords.Text = dgvEquipments.Rows.Count.ToString();
                return;
            }

            if (cbFilterBy.Text == "Equipment ID")
                _EquipmentsDT.DefaultView.RowFilter = $"[{FilterColumn}] = {txtFilterValue.Text}";
            else
                _EquipmentsDT.DefaultView.RowFilter = $"[{FilterColumn}] Like '{txtFilterValue.Text}%'";

            lblEquipmentRecords.Text = dgvEquipments.Rows.Count.ToString();

        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdateEquipments frmAdd = new frmAddUpdateEquipments();
            frmAdd.ShowDialog();

            frmListEquipments_Load(null, null);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int EquipmentID = (int)dgvEquipments.CurrentRow.Cells[0].Value;
            frmShowEquipmentInfo frm = new frmShowEquipmentInfo(EquipmentID);
            frm.ShowDialog();

            frmListEquipments_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int EquipmentID = (int)dgvEquipments.CurrentRow.Cells[0].Value;
            frmAddUpdateEquipments frm = new frmAddUpdateEquipments(EquipmentID);
            frm.ShowDialog();

            frmListEquipments_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int EquipmentID = (int)dgvEquipments.CurrentRow.Cells[0].Value;
            if (clsEquipment.DeleteEquipment(EquipmentID))
            {
                MessageBox.Show("Equipment has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmListEquipments_Load(null, null);
            }

            else
                MessageBox.Show("Equipment is not delted due to data connected to it.", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Under development yet", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void callPhoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Under development yet", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "Equipment ID") 
            {
                e.Handled = !char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void fixEquipemtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int EquipmentID = (int)dgvEquipments.CurrentRow.Cells[0].Value;
            clsEquipment equipment = clsEquipment.Find(EquipmentID);

            if(equipment.Condition == clsEquipment.enCondition.Unbroken) 
            {
                MessageBox.Show("This machine don't need to fix", "Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to fix this machine?", "Fixing Machine"
                , MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK) 
            {
                MessageBox.Show("Fixing machine done successfully", "Fixing Machine", MessageBoxButtons.OK, MessageBoxIcon.Information);
                equipment.UpdateLastMaintenanceDate(DateTime.Now, (byte)clsEquipment.enCondition.Unbroken, clsGlobalVars.CurrentUser.UserID);

                frmListEquipments_Load(null, null);
            }

            
        }
    }
}
