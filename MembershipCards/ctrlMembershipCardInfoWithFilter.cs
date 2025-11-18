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

namespace GMS.MembershipCards
{
    public partial class ctrlMembershipCardInfoWithFilter : UserControl
    {
        public event Action<int> OnCardSelected;
        protected virtual void CardSelected(object sender, int CardID)
        {
            Action<int> handler = OnCardSelected;
            if(handler != null)
            {
                handler(CardID);
            }
        }
        int _CardID = -1;
        public int CardID
        {  get { return _CardID; } }
        public clsMembershipCard SelectedMembershipCardInfo
        { get {  return ctrlMembershipCardInfo1.MembershipCard; } }

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

        public void txtLicenseIDFocus()
        {
            txtCardID.Focus();
        }
        public ctrlMembershipCardInfoWithFilter()
        {
            InitializeComponent();
        }

        public void LoadCardInfo(int cardID)
        {
            txtCardID.Text = cardID.ToString();
            ctrlMembershipCardInfo1.LoadInfo(cardID);
            _CardID = ctrlMembershipCardInfo1.CardID;
            if (OnCardSelected != null && _FilterEnabled)
                OnCardSelected(_CardID);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCardID.Focus();
                return;

            }
            _CardID = int.Parse(txtCardID.Text);
            LoadCardInfo(CardID);
        }

        private void txtCardID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }
        }

        private void txtCardID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCardID.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCardID, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtCardID, null);
            }
        }
    }
}
