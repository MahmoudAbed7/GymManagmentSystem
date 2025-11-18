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

namespace GMS.Applications.Control
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        private int _ApplicationID = -1;
        public int ApplicationID{ get { return _ApplicationID; }}

        private clsApplication _Application;

        public void ResetDefaultValues() 
        {
            _ApplicationID = -1;
            lblApplicationID.Text = "???";
            lblApplicant.Text = "???";
            lblCreatedByUser.Text = "???";
            lblDate.Text = "???";
            lblStatus.Text = "???";
            lblFees.Text = "???";
            lblStatusDate.Text = "???";
            lblType.Text = "???";
        }

        private void _FillApplicationInfo()
        {
            _ApplicationID = _Application.ApplicationID;
            lblApplicationID.Text = _Application.ApplicationID.ToString();
            lblApplicant.Text = _Application.ApplicantFullName;
            lblCreatedByUser.Text = _Application.AddedByUserID.ToString();
            lblDate.Text = _Application.ApplicationDate.ToShortDateString();

            switch (_Application.ApplicationStatus) 
            {
                case clsApplication.enApplicationStatus.New:
                    lblStatus.Text = "New";
                    break;
                case clsApplication.enApplicationStatus.Cancelled:
                    lblStatus.Text = "Cancelled";
                    break;
                case clsApplication.enApplicationStatus.Completed:
                    lblStatus.Text = "Complete";
                    break;
            }

            lblFees.Text = _Application.PaidFees.ToString();
            lblStatusDate.Text = _Application.LastStatusDate.ToShortDateString();
            lblType.Text = _Application.ApplicationTypeInfo.Title;
        }

        public void LoadApplicationInfo(int ApplicationID)
        {
            _ApplicationID = ApplicationID;
            _Application = clsApplication.FindByID(ApplicationID);
            if (_Application == null)
            {
                ResetDefaultValues();
                MessageBox.Show("No Application with ApplicationID = " + ApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
            else
                _FillApplicationInfo();
        }

        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            frmShowPersonInfo frm = new frmShowPersonInfo(_Application.ApplicantPersonID);
            frm.ShowDialog();

            LoadApplicationInfo(ApplicationID);
        }
    }
}
