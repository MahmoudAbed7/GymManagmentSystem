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

namespace GMS.Equipments.controls
{
    public partial class ctrlEquipmentCardWithFilter : UserControl
    {
        public event EventHandler<int> OnEquipmentSelected;
        protected virtual void EquipmentSelected(int EquipmentID)
        {
           OnEquipmentSelected?.Invoke(this, EquipmentID);
        }

        public int EquipmentID { get { return ctrlEquipmentCard1.EquipmentID; } }
        public clsEquipment Equipment { get { return ctrlEquipmentCard1.Equipment; } }

        private bool _FilterEnabled = true;
        public bool FilterEnabled 
        {
            set 
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
            get { return _FilterEnabled; }
        }

        private bool _FilterValue = true;
        public bool FilterValue 
        {
            set 
            {
                _FilterValue = value;
                txtFilterValue.Enabled = _FilterValue;
            }
            get { return _FilterValue; }
        }

        private bool _ShowEquipment = true;
        public bool ShowEquipment 
        {
            set 
            {
                _ShowEquipment = value;
                btnFind.Enabled = _ShowEquipment;
            }
            get { return _ShowEquipment; }
        }

        private void _FindNow() 
        {
           switch (cbFilterBy.SelectedIndex) 
            {
                case 0:
                    ctrlEquipmentCard1.LoadEquipmentInfo(int.Parse(txtFilterValue.Text));
                    break;
                case 1:
                    ctrlEquipmentCard1.LoadEquipmentInfo(txtFilterValue.Text);
                    break;
            }

            if (OnEquipmentSelected != null && FilterEnabled)
                OnEquipmentSelected(this, ctrlEquipmentCard1.EquipmentID);
        }
        public void LoadEquipmentInfo(int EquipmentID) 
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = EquipmentID.ToString();
            _FindNow();
        }
        public ctrlEquipmentCardWithFilter()
        {
            InitializeComponent();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();

        }

        private void ctrlEquipmentCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Focus();
        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterValue.Text)) 
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "This field is required");
            }
            else
            {
                errorProvider1.SetError(txtFilterValue, "This field is required");
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13) 
            {
                btnFind.PerformClick();
            }
            if (cbFilterBy.SelectedIndex == 0) 
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren()) 
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FindNow();
        }

        private void btnAddNewEquipment_Click(object sender, EventArgs e)
        {
            frmAddUpdateEquipments frm = new frmAddUpdateEquipments();
            frm.DataBack += EquipmentID_DataBack;
            frm.ShowDialog();
        }

        public void EquipmentID_DataBack(object sender, int EquipmentID) 
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = EquipmentID.ToString();
            ctrlEquipmentCard1.LoadEquipmentInfo(EquipmentID);
        }
    }
}
