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

namespace GMS.Users
{
    public partial class ctrlUserCard : UserControl
    {
        private int _UserID = -1;
        public int UserID { get { return _UserID; } }

        private clsUser _User;
        public clsUser UserInfo {  get { return _User; } }
        public ctrlUserCard()
        {
            InitializeComponent();
        }

        private void _ResetPersonInfo() 
        {
            ctrlPersonCard1.ResetPersonInfo();
            lblUserName.Text = "????";
            lblUserID.Text = "????";
            lblIsActive.Text = "????";
        }

        private void _FillUserInfo() 
        {
            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
            _UserID = _User.UserID;
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName;

            if (_User.IsActive) lblIsActive.Text = "Active";
            else lblIsActive.Text = "Not Active";
        }

        public void LoadUserInfo(int UserID) 
        {
            _User = clsUser.FindByUserID(UserID);
            if (_User == null) 
            {
                _ResetPersonInfo();
                MessageBox.Show("No User with id = " + UserID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillUserInfo();
        }
    }
}
