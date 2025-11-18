using GMS.MembershipCards;
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

namespace GMS.Applications.Subscriptions
{
    public partial class ctrlSubscriptionInfo : UserControl
    {
        private int _SubscriptionID = -1;
        private int _MembershipCardID = -1;
        public int SubscriptionID {  get { return _SubscriptionID; } }

        private clsSubscription _Subscription;
        public ctrlSubscriptionInfo()       
        {
            InitializeComponent();
        }

        private void _ResetDefalutValues() 
        {
            _SubscriptionID = -1;
            lblSubscriptionID.Text = "???";
            ctrlApplicationBasicInfo1.ResetDefaultValues();
        }

        private void _FillSubscriptionInfo() 
        {
            _SubscriptionID = _Subscription.SubscriptionID;
            lblSubscriptionID.Text = _Subscription.SubscriptionID.ToString();
            ctrlApplicationBasicInfo1.LoadApplicationInfo(_Subscription.ApplicationID);
        }

        public void LoadSubscriptionInfoByID(int subscriptionID) 
        {
            _Subscription = clsSubscription.FindByID(subscriptionID);
            if (_Subscription == null) 
            {
                _ResetDefalutValues();

                MessageBox.Show("No subscription with subscriptionID = " + subscriptionID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else 
            {
                _FillSubscriptionInfo();
            }
        }

        public void LoadSubscriptionInfoByApplicationID(int applicationID)
        {
            _Subscription = clsSubscription.FindByApplicationID(applicationID);
            if (_Subscription == null)
            {
                _ResetDefalutValues();

                MessageBox.Show("No subscription with applicationID = " + applicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                _FillSubscriptionInfo();
            }
        }

        private void llShowMembershipCardInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowMembershipCardInfo frm = new frmShowMembershipCardInfo(_Subscription.GetActiveCardID());
            frm.ShowDialog();
        }
    }
}
