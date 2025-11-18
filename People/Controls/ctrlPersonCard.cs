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
using GMS.Properties;
using GMS_Business;

namespace GMS.People.Controls
{
    public partial class ctrlPersonCard : UserControl
    {
        private int _PersonID = -1;
        public int PersonID { get { return _PersonID; } }

        private clsPerson _Person;

        public clsPerson SelectedPersonInfo {  get { return _Person; } }
        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        private void _LoadPersonImage() 
        {
            if (_Person.Gendor == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _Person.ProfileImage;
            if (ImagePath != "") 
            {
                if(File.Exists(ImagePath))
                {
                    pbPersonImage.ImageLocation = ImagePath;
                }
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadPersonInfo(int PersonID)
        {
            _PersonID = PersonID;
            _Person = clsPerson.Find(_PersonID);
            if (_Person == null) 
            {
                MessageBox.Show($"No person with id = {PersonID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            llEditPersonInfo.Enabled = true;
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblFullName.Text = _Person.FullName;
            lblCountry.Text = _Person.CountryInfo.CountryName;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblEmail.Text = _Person.Email;
            lblAddress.Text = _Person.Address;
            lblPhone.Text = _Person.Phone;
            lblGendor.Text = _Person.Gendor == 0 ? "Male" : "Female";

            _LoadPersonImage();

        }

        public void ResetPersonInfo()
        {
            _PersonID = -1;
            lblPersonID.Text = "[????]";
            lblFullName.Text = "[????]";
            pbGendor.Image = Resources.Man_32;
            lblGendor.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAddress.Text = "[????]";
            pbPersonImage.Image = Resources.Male_512;
        }
        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPeople frm = new frmAddEditPeople(_PersonID);
            frm.ShowDialog();
            LoadPersonInfo(_PersonID);
        }
    }
}
