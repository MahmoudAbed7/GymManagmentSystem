using GMS.clsGlobal;
using GMS.GlobalClasses;
using GMS.Properties;
using GMS_Business;
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

namespace GMS.Equipments
{
    public partial class frmAddUpdateEquipments : Form
    {
        public delegate void DataBackEventHandler(object sender, int EquipmentID);
        public DataBackEventHandler DataBack;
        enum enMode { AddNew, Update}
        enMode Mode = enMode.AddNew;

        int _EquipmentID = -1;
        static clsEquipment _Equipment;
        public frmAddUpdateEquipments()
        {
            InitializeComponent();
            Mode = enMode.AddNew;
        }

        public frmAddUpdateEquipments(int EquipmentID)
        {
            InitializeComponent();
            Mode = enMode.Update;
            _EquipmentID = EquipmentID;
        }

        private void _ResetDefaultValue() 
        {
            if(Mode == enMode.AddNew) 
            {
                _Equipment = new clsEquipment();
                this.Text = "Add New Equipment";
                lblTitle.Text = "Add New Equipment";
            }
            else 
            {
                this.Text = "Update Equipment";
                lblTitle.Text = "Update Equipment";
            }

            txtName.Text = string.Empty;
            dtpPurchaseDate.Value = DateTime.Now;
            rdUnbroken.Checked = true;
            lblAddedByUserID.Text = "???";

        }

        private void _FillEquipmentData() 
        {
            _Equipment = clsEquipment.Find(_EquipmentID);
            if( _Equipment == null) 
            {
                MessageBox.Show($"There is now equipment with id: {_EquipmentID}", "Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblEquipmentID.Text = _Equipment.EquipmentID.ToString();
            txtName.Text = _Equipment.Name;
            lblAddedByUserID.Text = _Equipment.UserInfo.UserName;
            dtpPurchaseDate.Value = _Equipment.PurchaseDate;


            if (_Equipment.Condition == clsEquipment.enCondition.Unbroken)
                rdUnbroken.Checked = true;
            else
                rdBroken.Checked = true;

            if (_Equipment.EquipmentImage != "")
                pbEquipmentImage.ImageLocation = _Equipment.EquipmentImage;

            llRemoveImage.Visible = pbEquipmentImage.ImageLocation != "";

        }

        private bool _HandleEquipmentImage() 
        {
            if(_Equipment.EquipmentImage != pbEquipmentImage.ImageLocation) 
            {
                if(_Equipment.EquipmentImage != "") 
                {
                    try 
                    {
                        File.Delete(_Equipment.EquipmentImage);
                    }
                    catch(IOException)
                    {

                    }
                }
            }

            if(pbEquipmentImage.ImageLocation != null) 
            {
                string ImagePath = pbEquipmentImage.ImageLocation.ToString();
                if (clsUtil.CopyImageToProjectImageFolder(ref ImagePath))
                {
                    pbEquipmentImage.ImageLocation = ImagePath;
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

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text)) 
            {
                e.Cancel = true;
                errorProvider1.SetError(txtName, "This Field is required");
            }
            else 
            {
                errorProvider1.SetError(txtName, null);
            }
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files| *.Png; *.Jpeg; *.jpg; *.gif";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string SelectedFileName = openFileDialog1.FileName;
                pbEquipmentImage.Load(SelectedFileName);
                llRemoveImage.Visible = true;
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbEquipmentImage.ImageLocation = null;

            if (pbEquipmentImage.ImageLocation == null)
                pbEquipmentImage.Image = Resources.barbell;

            llRemoveImage.Visible = false;
        }

        private void frmAddUpdateEquipments_Load(object sender, EventArgs e)
        {
            _ResetDefaultValue();
            if (Mode == enMode.Update) _FillEquipmentData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren()) 
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_HandleEquipmentImage()) return;

            _Equipment.Name = txtName.Text;
            _Equipment.PurchaseDate = dtpPurchaseDate.Value;

            if (rdUnbroken.Checked) _Equipment.Condition = clsEquipment.enCondition.Unbroken;
            if (rdBroken.Checked) _Equipment.Condition = clsEquipment.enCondition.Broken;

            if (pbEquipmentImage.ImageLocation != null)
                _Equipment.EquipmentImage = pbEquipmentImage.ImageLocation;
            else
                _Equipment.EquipmentImage = "";

            _Equipment.AddedByUserID = clsGlobalVars.CurrentUser.UserID;

            if (_Equipment.Save()) 
            {
                lblTitle.Text = "Update Equipment";
                lblEquipmentID.Text = _Equipment.EquipmentID.ToString();
                Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke(this, _Equipment.EquipmentID);
            }
        }

    }
}
