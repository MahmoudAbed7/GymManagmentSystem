using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMS.clsGlobal;
using GMS.GlobalClasses;
using GMS.Properties;
using GMS_Business;

namespace GMS
{
    public partial class frmAddEditPeople : Form
    {
        //public delegate void DataBackHandler(object sender, int PersonID);
        //public DataBackHandler DataBack;

        public Action<int> DataBackHandler = PersonID => PersonID = _Person.PersonID;
        enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;
        private int _PersonID = -1;
        static private clsPerson _Person;
        public frmAddEditPeople()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddEditPeople(int PersonID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _PersonID = PersonID;
        }


        private void _FillCountryInComboBox() 
        {
            DataTable dtCountries = clsCountry.GetAllCountries();
            foreach (DataRow drCountry in dtCountries.Rows)
            {
                cbCountry.Items.Add(drCountry["CountryName"]);
            }
        }


        private void _ResetDefalutValue() 
        {
            _FillCountryInComboBox();
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Person";
                this.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else 
            {
                lblTitle.Text = "Update Person";
                this.Text = "Update Person";
            }

            if (rbMale.Checked)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            llRemoveImage.Visible = pbPersonImage.ImageLocation != null;

            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-15);
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-80);

            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            cbCountry.SelectedIndex = cbCountry.FindString("State Of Palestine");


            txtFirstName.Text = string.Empty;
            txtSecondName.Text = string.Empty; 
            txtThirdName.Text = string.Empty;
            txtLastName.Text = string.Empty;           
            rbMale.Checked = true;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
           

            

        }

        private bool _handlePersonImage()
        {
            if (_Person.ProfileImage !=  pbPersonImage.ImageLocation)
            {
                if (_Person.ProfileImage != "")
                {
                    try
                    {
                        File.Delete(_Person.ProfileImage);
                    }
                    catch (IOException)
                    {

                    }
                }
            }

            if (pbPersonImage.ImageLocation != null) 
            {
                string ImageFile = pbPersonImage.ImageLocation.ToString();
                if (clsUtil.CopyImageToProjectImageFolder(ref ImageFile))
                {
                    pbPersonImage.ImageLocation = ImageFile;
                    return true;
                }
                else
                {
                    MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        private void _LoadPersonInfo() 
        {
            _Person = clsPerson.Find(_PersonID);
            if(_Person == null) 
            {
                MessageBox.Show($"No person with id: {_Person.PersonID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            
            lblPersonID.Text = _Person.PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            cbCountry.SelectedItem = _Person.CountryInfo.ID;

            if (_Person.Gendor == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            dtpDateOfBirth.Value = _Person.DateOfBirth;
            txtEmail.Text = _Person.Email;
            txtPhone.Text = _Person.Phone;
            txtAddress.Text = _Person.Address;

            if (_Person.ProfileImage != "")
                pbPersonImage.ImageLocation = _Person.ProfileImage;
           

            llRemoveImage.Visible = _Person.ProfileImage != "";
        }

        private void frmAddEditPerople_Load(object sender, EventArgs e)
        {
            _ResetDefalutValue();
            if(_Mode == enMode.Update)
                _LoadPersonInfo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TextField_Validating(object sender, CancelEventArgs e)
        {
            TextBox temp = (TextBox)sender;

            if(temp.Text == string.Empty) 
            {
                e.Cancel = true;
                errorProvider1.SetError(temp, "This field is required");
            }
            else 
            {
                e.Cancel = false;
                errorProvider1.SetError(temp, null);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if(txtEmail.Text == string.Empty) return;

            if (!clsValidating.ValidateEmail(txtEmail.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            }

        }


        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files| *.Png; *.Jpeg; *.jpg; *.gif";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if(openFileDialog1.ShowDialog() == DialogResult.OK) 
            {
                string SelectedFileName = openFileDialog1.FileName;
                pbPersonImage.Load(SelectedFileName);
                llRemoveImage.Visible = true;
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;

            if (pbPersonImage.ImageLocation == null)
            {
                if (rbMale.Checked) pbPersonImage.Image = Resources.Male_512;
                else pbPersonImage.Image = Resources.Female_512;
            }
            llRemoveImage.Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren()) 
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_handlePersonImage()) return;


            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.NationalCountryID = clsCountry.Find(cbCountry.Text).ID;

            if (rbMale.Checked) _Person.Gendor = 0;
            else _Person.Gendor = 1;

            _Person.Phone = txtPhone.Text.Trim();
            _Person.Address = txtAddress.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();

            if (pbPersonImage.ImageLocation == null) _Person.ProfileImage = "";
            else _Person.ProfileImage = pbPersonImage.ImageLocation;

            if (_Person.Save())
            {
                lblTitle.Text = "Update Person";
                lblPersonID.Text = _Person.PersonID.ToString();
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBackHandler(_Person.PersonID);
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Resources.Male_512;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Resources.Female_512;
        }
    }
}
