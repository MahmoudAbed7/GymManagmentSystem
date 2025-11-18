namespace GMS.Equipments
{
    partial class frmShowEquipmentInfo
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
            this.ctrlEquipmentCard1 = new GMS.Equipments.controls.ctrlEquipmentCard();
            this.SuspendLayout();
            // 
            // ctrlEquipmentCard1
            // 
            this.ctrlEquipmentCard1.Location = new System.Drawing.Point(12, 12);
            this.ctrlEquipmentCard1.Name = "ctrlEquipmentCard1";
            this.ctrlEquipmentCard1.Size = new System.Drawing.Size(1007, 333);
            this.ctrlEquipmentCard1.TabIndex = 0;
            // 
            // frmShowEquipmentInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 346);
            this.Controls.Add(this.ctrlEquipmentCard1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmShowEquipmentInfo";
            this.Text = "frmShowEquipmentInfo";
            this.ResumeLayout(false);

        }

        #endregion

        private controls.ctrlEquipmentCard ctrlEquipmentCard1;
    }
}