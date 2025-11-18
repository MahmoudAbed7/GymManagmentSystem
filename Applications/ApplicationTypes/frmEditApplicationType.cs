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

namespace GMS.Applications.ApplicationTypes
{
    public partial class frmEditApplicationType : Form
    {
        enum enMode { AddNew, Update};
        enMode Mode = enMode.AddNew;

        int _ApplicationTypeID = -1;
        clsApplicationType _ApplicationType;
        public frmEditApplicationType()
        {
            Mode = enMode.AddNew;
            InitializeComponent();
        }

        public frmEditApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            Mode = enMode.Update;
            _ApplicationTypeID = ApplicationTypeID;
        }

        private void _ResetDefaultValues() 
        {
            if (Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Application Type";
                this.Text = lblTitle.Text;
                _ApplicationType = new clsApplicationType();
            }
            lblApplicationTypeID.Text = "??";
            txtTitle.Text = "";
            txtTitle.Text = "";
        }

        private void _FillApplicationData(int ApplicationTypeID) 
        {
            _ApplicationTypeID = ApplicationTypeID;
            
             _ApplicationType = clsApplicationType.Find(_ApplicationTypeID);

            if (_ApplicationType == null) 
            {
                MessageBox.Show($"There is no application type with id: {_ApplicationTypeID}", "Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            lblTitle.Text = "Update Application Type";
            this.Text = txtTitle.Text;
            lblApplicationTypeID.Text = _ApplicationType.ApplicationTypeID.
                ToString();
            txtTitle.Text = _ApplicationType.Title;
            txtFees.Text = _ApplicationType.Fees.ToString();
        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            if (Mode == enMode.AddNew) _ResetDefaultValues();
            else _FillApplicationData(_ApplicationTypeID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text)) 
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "This field is required");
            }
            else 
            {
                errorProvider1.SetError(txtTitle, null);
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "This field is required");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            }
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren()) 
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _ApplicationType.Title = txtTitle.Text;
            _ApplicationType.Fees = Convert.ToSingle(txtFees.Text);

            if (_ApplicationType.Save()) 
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblApplicationTypeID.Text = _ApplicationType.ApplicationTypeID.ToString();
            }
            else 
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
