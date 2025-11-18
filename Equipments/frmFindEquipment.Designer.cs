namespace GMS.Equipments.controls
{
    partial class frmFindEquipment
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrlEquipmentCardWithFilter1 = new GMS.Equipments.controls.ctrlEquipmentCardWithFilter();
            this.SuspendLayout();
            // 
            // ctrlEquipmentCardWithFilter1
            // 
            this.ctrlEquipmentCardWithFilter1.FilterEnabled = true;
            this.ctrlEquipmentCardWithFilter1.FilterValue = true;
            this.ctrlEquipmentCardWithFilter1.Location = new System.Drawing.Point(12, 9);
            this.ctrlEquipmentCardWithFilter1.Name = "ctrlEquipmentCardWithFilter1";
            this.ctrlEquipmentCardWithFilter1.ShowEquipment = true;
            this.ctrlEquipmentCardWithFilter1.Size = new System.Drawing.Size(1010, 472);
            this.ctrlEquipmentCardWithFilter1.TabIndex = 0;
            // 
            // frmFindEquipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 469);
            this.Controls.Add(this.ctrlEquipmentCardWithFilter1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmFindEquipment";
            this.Text = "frmFindEquipment";
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlEquipmentCardWithFilter ctrlEquipmentCardWithFilter1;
    }
}