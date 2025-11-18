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

namespace GMS.Subscriptions
{
    public partial class frmAddEditSubscription : Form
    {
        enum enMode { AddNew, Update}
        enMode Mode = enMode.AddNew;
        private int _SubscriptionID = -1;
        private int _SelectedPersonID = -1;
        private clsSubscription _Subscription;
        public frmAddEditSubscription()
        {
            InitializeComponent();
            Mode = enMode.AddNew;
        }

        public frmAddEditSubscription(int SubscriptionID)
        {
            InitializeComponent();
            Mode = enMode.Update;
            _SubscriptionID = SubscriptionID;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Mode == enMode.Update) 
            {
                btnNext.Enabled = true;
                tpSubscriptionInfo.Enabled = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpSubscriptionInfo"];
                return;
            }

            if(ctrlPersonCardWithFilter1.PersonID != -1) 
            {
                if (clsTrainer.IsTrainerExistByPersonID(ctrlPersonCardWithFilter1.PersonID))
                {
                    MessageBox.Show("This Person is a trainer, Choose another person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();
                }
                else
                {
                    btnNext.Enabled = true;
                    tpSubscriptionInfo.Enabled = true;
                    tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpSubscriptionInfo"];
                }
            }

            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void _ResetDefaultValue() 
        {
            if (Mode == enMode.AddNew)
            {
                this.Text = "Add New Subscription";
                lblTitle.Text = "Add New Subscription";
                _Subscription = new clsSubscription();

                ctrlPersonCardWithFilter1.FilterFocus();
                tpSubscriptionInfo.Enabled = false;
                lblFees.Text = clsApplicationType.Find((int)clsApplication
                    .enApplicationType.NewSubscription).Fees.ToString();
                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
                lblAddedByUser.Text = clsGlobalVars.CurrentUser.UserName;
            }
            else
            {
                this.Text = "Update Subscription";
                lblTitle.Text = "Update Subscription";
                tpSubscriptionInfo.Enabled = true;
                btnSave.Enabled = true;
            }
        }

        private void _FillSubscriptionData() 
        {
            ctrlPersonCardWithFilter1.FilterEnabled = false;
            _Subscription = clsSubscription.FindByID(_SubscriptionID);

            if (_Subscription == null)
            {
                MessageBox.Show("No Subscription with ID = " + _SubscriptionID, "Subscription Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            ctrlPersonCardWithFilter1.LoadPersonInfo(_Subscription.ApplicantPersonID);
            lblSubscriptionID.Text = _Subscription.SubscriptionID.ToString();
            lblApplicationDate.Text = _Subscription.ApplicationDate.ToString();
            lblFees.Text = _Subscription.PaidFees.ToString();
            lblAddedByUser.Text = _Subscription.UserInfo.UserName;
        }
        private void frmAddEditSubscription_Load(object sender, EventArgs e)
        {
            _ResetDefaultValue();
            if(Mode == enMode.Update) _FillSubscriptionData();
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(object sender, int e)
        {
            _SelectedPersonID = e;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(clsApplication.DoesPersonHaveActiveApplication(ctrlPersonCardWithFilter1.PersonID
                , (int)clsApplication.enApplicationType.NewSubscription)) 
            {
                MessageBox.Show("Person already have an active subscription application", "Not allowed"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            _Subscription.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
            _Subscription.ApplicationDate = DateTime.Now;
            _Subscription.ApplicationTypeID = 1;
            _Subscription.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _Subscription.LastStatusDate = DateTime.Now;
            _Subscription.PaidFees = Convert.ToSingle(lblFees.Text);
            _Subscription.AddedByUserID = clsGlobalVars.CurrentUser.UserID;

            if (_Subscription.Save()) 
            {
                lblSubscriptionID.Text = _Subscription.SubscriptionID.ToString();
                Mode = enMode.Update;
                lblTitle.Text = "Update Subscription Application";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully."
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
