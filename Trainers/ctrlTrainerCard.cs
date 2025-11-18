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
    public partial class ctrlTrainerCard : UserControl
    {
        int _TrainerID = -1;
        public int TrainerID { get { return _TrainerID; }}
        clsTrainer _Trainer;
        public clsTrainer TrainerInfo { get { return _Trainer; } }
        public ctrlTrainerCard()
        {
            InitializeComponent();
        }

        private void _ResetDefaultValues() 
        {
            ctrlPersonCard1.ResetPersonInfo();
            lblTrainerID.Text = "???";
            lblEmploymentStatus.Text = "???";
            lblSpeciality.Text = "???";
            lblHireDate.Text = "???";
            lblEndDate.Text = "???";
            lblSpeciality.Text = "???";
            lblSalary.Text = "???";
            lblIsActive.Text = "???";
            lblAddedByUser.Text = "???";
        }

        private void _LoadTrainerInfo() 
        {
            ctrlPersonCard1.LoadPersonInfo(_Trainer.PersonID);
            lblTrainerID.Text = _Trainer.TrainerID.ToString();
            lblEmploymentStatus.Text = _Trainer.EmploymentText;
            lblSpeciality.Text = _Trainer.Speciality;
            lblHireDate.Text = _Trainer.HireDate.ToShortDateString();
            lblEndDate.Text = _Trainer.EndDate.ToShortDateString();
            lblSalary.Text = _Trainer.Salary.ToString();

            if (_Trainer.IsActive) lblIsActive.Text = "Active";
            else lblIsActive.Text = "UnActive";

                lblAddedByUser.Text = _Trainer.UserInfo.UserName;
        }

        public void LoadTrainerInfo(int TrainerID) 
        {
            _TrainerID = TrainerID;
            _Trainer = clsTrainer.FindByID(TrainerID);
            if (_Trainer == null) 
            {
                _ResetDefaultValues();
                MessageBox.Show("No Trainer with id = " + TrainerID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LoadTrainerInfo();
        }
    }
}
