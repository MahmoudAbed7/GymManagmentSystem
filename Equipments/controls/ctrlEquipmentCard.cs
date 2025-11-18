using GMS.clsGlobal;
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

namespace GMS.Equipments.controls
{
    public partial class ctrlEquipmentCard : UserControl
    {
        private int _EquipmentID = -1;
        public int EquipmentID { get { return _EquipmentID; } }

        private clsEquipment _Equipment;
        public clsEquipment Equipment { get { return _Equipment; } }

        private void _ResetDefaultInfo() 
        {
            lblEquipmentID.Text = "???";
            lblMachineName.Text = "???";
            lblPurchaseDate.Text = "DD/MM/YYYY";
            lblLastMaintenanceDate.Text = "No Maintained yet";
            lblStatus.Text = "???";
            lblAddedByUserID.Text = "???";
        }

        private void _LoadEquipmentImage() 
        {
            if(_Equipment.Condition == clsEquipment.enCondition.Unbroken
                || _Equipment.Condition == clsEquipment.enCondition.Broken) 
            {
                pbEquipmentImage.Image = Resources.barbell;
            }

            string ImagePath = _Equipment.EquipmentImage;
            if (ImagePath != "")
            {
                if (File.Exists(ImagePath))
                    pbEquipmentImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void _FillEquipmentInfo() 
        {
            _LoadEquipmentImage();

            llEditEquipmentInfo.Enabled = true;
            lblEquipmentID.Text = _Equipment.EquipmentID.ToString();
            lblMachineName.Text = _Equipment.Name;
            lblPurchaseDate.Text = _Equipment.PurchaseDate.ToString();

            if(_Equipment.LastMaintenanceDate == DateTime.MaxValue)
            lblLastMaintenanceDate.Text = "No Maintained yet";
            else
            lblLastMaintenanceDate.Text = _Equipment.LastMaintenanceDate.ToString();

            if (_Equipment.Condition == clsEquipment.enCondition.Unbroken)
                lblStatus.Text = "Unbroken";
            else
                lblStatus.Text = "Broken";

            lblAddedByUserID.Text = _Equipment.UserInfo.UserName;
        }

        public void LoadEquipmentInfo(int EquipmentID)
        {
            _EquipmentID = EquipmentID;
            _Equipment = clsEquipment.Find(_EquipmentID);
            if(_Equipment == null) 
            {
                MessageBox.Show($"There is no equipment to show for id = {_EquipmentID}"
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillEquipmentInfo();
        }

        public void LoadEquipmentInfo(string Name)
        {
            _Equipment = clsEquipment.Find(Name);
            if (_Equipment == null)
            {
                MessageBox.Show($"There is no equipment to show for name = {Name}"
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetDefaultInfo();
                return;
            }
            _FillEquipmentInfo();
        }
        public ctrlEquipmentCard()
        {
            InitializeComponent();
        }

        private void llEditEquipmentInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdateEquipments frm = new frmAddUpdateEquipments(_EquipmentID);
            frm.ShowDialog();
            LoadEquipmentInfo(_EquipmentID);
        }
    }
}
