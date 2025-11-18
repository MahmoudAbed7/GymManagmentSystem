using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMS.Applications.ApplicationTypes;
using GMS.Applications.RenewScpscriptionApplication;
using GMS.Applications.ReplaceCardApplication;
using GMS.Applications.Subscriptions;
using GMS.Equipments;
using GMS.GlobalClasses;
using GMS.Login;
using GMS.Members;
using GMS.People;
using GMS.Subscriptions;
using GMS.Trainers;
using GMS.Users;
using GMS_Business;

namespace GMS
{
    public partial class frmMain : Form
    {
        frmLogin _frmLogin;
        public frmMain(frmLogin frmLogin)
        {
            InitializeComponent();
            _frmLogin = frmLogin;

        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListPeople frm = new frmListPeople();
            frm.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListUsers frm = new frmListUsers();
            frm.ShowDialog();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListApplicationTypes frm = new frmListApplicationTypes();
            frm.ShowDialog();
        }

        private void equipmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListEquipments frm = new frmListEquipments();
            frm.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(clsGlobalVars.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobalVars.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            clsGlobalVars.CurrentUser = null;
            _frmLogin.ShowDialog();
        }

        private void newSubscriptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditSubscription frm = new frmAddEditSubscription();
            frm.ShowDialog();
        }

        private void manageApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListSubscriptionApplications frm = new frmListSubscriptionApplications();
            frm.ShowDialog();
        }

        private void trainersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTrainers frm = new frmListTrainers();
            frm.ShowDialog();
        }

        private void membersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListMembers frm = new frmListMembers();
            frm.ShowDialog();
        }

        private void renewSubscriptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewMembershipCardApplication frm = new frmRenewMembershipCardApplication();
            frm.ShowDialog();

        }

        private void replaceSubscriptionCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplaceMembershipCardApplication frm = new frmReplaceMembershipCardApplication();
            frm.ShowDialog();
        }
    }
}
