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

namespace GMS.Trainers
{
    public partial class frmAddUpdateTrainer : Form
    {
        enum enMode { AddNew =  0, Update = 1 }
        enMode Mode = enMode.AddNew;
        int _TrainerID = -1;
        clsTrainer _Trainer;
        public frmAddUpdateTrainer()
        {
            InitializeComponent();
            Mode = enMode.AddNew;
        }
        public frmAddUpdateTrainer(int TrainerID)
        {
            InitializeComponent();
            _TrainerID = TrainerID;
            Mode = enMode.Update;
        }

        private void _ResetDefaultValues() 
        {
            if(Mode == enMode.AddNew) 
            {
                this.Text = "Add New Trainer";
                lblTitle.Text = "Add New Trainer";
                _Trainer = new clsTrainer();
                cbEmploymentStatus.SelectedIndex = 0;
                tpTrainerInfo.Enabled = false;
                ctrlPersonCardWithFilter1.FilterFocus();
            }
            else 
            {
                this.Text = "Update Trainer";
                lblTitle.Text = "Update Trainer";
                btnNext.Enabled = true;
                tpTrainerInfo.Enabled = true;
            }
        }

        private void _LoadTrainerInfo() 
        {
            _Trainer = clsTrainer.FindByID(_TrainerID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;
            if(_Trainer == null) 
            {
                MessageBox.Show("No Trainer with ID = " + _Trainer, "Trainer Not Found"
                    , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            ctrlPersonCardWithFilter1.LoadPersonInfo(_Trainer.PersonID);
            lblTrainerID.Text = _Trainer.TrainerID.ToString();
            cbEmploymentStatus.Text = _Trainer.EmploymentText;
            txtSpeciality.Text = _Trainer.Speciality;
            txtSalary.Text = _Trainer.Salary.ToString();
            chkIsActive.Checked = _Trainer.IsActive;

            
        }

        private void frmAddUpdateTrainer_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if(Mode == enMode.Update) _LoadTrainerInfo();
        }

        private void txtSpeciality_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSpeciality.Text)) 
            {
                e.Cancel = true;
                errorProvider1.SetError(txtSpeciality, "This field is required");
            }
            else 
            {
                errorProvider1.SetError(txtSpeciality, null);
            }
        }

        private void txtSalary_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSalary.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtSalary, "This field is required");
            }
            else
            {
                errorProvider1.SetError(txtSalary, null);
            }
        }

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(Mode == enMode.Update) 
            {
                btnNext.Enabled = true;
                btnNext.Enabled = true;
                tcTrainerInfo.SelectedIndex = 1;
                return;
            }

            if(ctrlPersonCardWithFilter1.PersonID != -1) 
            {
                if (clsTrainer.IsTrainerExistByPersonID(ctrlPersonCardWithFilter1.PersonID)) 
                {
                    MessageBox.Show("Selected Person already a trainer, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();
                }
                else if(clsMember.IsMemberExistByPersonID(ctrlPersonCardWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected person is a member and you can't be both member and trainer, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();
                }
                else
                {
                    tpTrainerInfo.Enabled = true;
                    btnSave.Enabled = true;
                    tcTrainerInfo.SelectedIndex = 1;
                    return;
                }
            }
            else 
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                  "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Trainer.PersonID = ctrlPersonCardWithFilter1.PersonID;
            switch (cbEmploymentStatus.Text) 
            {
                case "Full Time":
                    _Trainer.EmploymentStatus = clsTrainer.enEmploymentStatus.FullTime;
                    break;
                case "Part Time":
                    _Trainer.EmploymentStatus = clsTrainer.enEmploymentStatus.PartTime;
                    break;
                case "Contractor":
                    _Trainer.EmploymentStatus = clsTrainer.enEmploymentStatus.Contractor;
                    break;
            }
            _Trainer.Speciality = txtSpeciality.Text;
            _Trainer.HireDate = DateTime.Now;
            _Trainer.EndDate = _Trainer.HireDate.AddYears(1);
            _Trainer.Salary = Convert.ToSingle(txtSalary.Text); 
            _Trainer.IsActive = chkIsActive.Checked;
            _Trainer.AddedByUserID = clsGlobalVars.CurrentUser.UserID;

            if (_Trainer.Save())
            {
                lblTrainerID.Text = _Trainer.TrainerID.ToString();
                lblTitle.Text = "Update Trainer";
                this.Text = "Update Trainer";
                Mode = enMode.Update;

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
