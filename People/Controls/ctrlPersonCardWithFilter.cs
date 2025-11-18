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

namespace GMS.People.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public event EventHandler<int> OnPersonSelected;
        protected virtual void PersonSelected(int PersonID)
        {
            OnPersonSelected?.Invoke(this, PersonID);
        }

        private int _PersonID = -1;

        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson = value;
                btnAddNewPerson.Enabled = _ShowAddPerson;
            }
        }


        private bool _FilterEnabled = true;
        public bool FilterEnabled 
        {
            get { return _FilterEnabled; }
            set 
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }

        private bool _FilterValue = true;
        public bool FilterValue
        {
            get { return _FilterValue; }
            set
            {
                _FilterValue = value;
                txtFilterValue.Enabled = _FilterEnabled;
            }
        }

        public int PersonID  { get { return ctrlPersonCard1.PersonID; } }

        public clsPerson SelectedPersonInfo { get { return ctrlPersonCard1.SelectedPersonInfo;} }
        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        public void LoadPersonInfo(int PersonID)
        {

            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = PersonID.ToString();
            FindNow();

        }

        private void _LoadPersonInfo() 
        {
            _PersonID = int.Parse(txtFilterValue.Text);
            ctrlPersonCard1.LoadPersonInfo(_PersonID);
        }
        private void FindNow() 
        {
            _LoadPersonInfo();
            if (OnPersonSelected != null && FilterEnabled) PersonSelected(ctrlPersonCard1.PersonID);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fields are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            FindNow();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPeople frm = new frmAddEditPeople();
            frm.DataBackHandler += DataEventBack;
            frm.ShowDialog();
        }
        private void DataEventBack(int PersonID)
        {
            cbFilterBy.Text = "Person ID";
            txtFilterValue.Text = PersonID.ToString();
            ctrlPersonCard1.LoadPersonInfo(PersonID);
        }
        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if(txtFilterValue.Text == string.Empty) 
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "This field is required");
            }
            else 
            {
                errorProvider1.SetError(txtFilterValue, null);
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if(e.KeyChar == 13) btnFind.PerformClick();
        }

        public void FilterFocus() 
        {
            txtFilterValue.Focus();
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            txtFilterValue.Focus();
            cbFilterBy.Text = "Person ID";

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }
    }
}
