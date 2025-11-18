namespace GMS.Trainers
{
    partial class frmTrainerInfo
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
            this.ctrlTrainerCard1 = new GMS.Trainers.ctrlTrainerCard();
            this.SuspendLayout();
            // 
            // ctrlTrainerCard1
            // 
            this.ctrlTrainerCard1.Location = new System.Drawing.Point(12, 12);
            this.ctrlTrainerCard1.Name = "ctrlTrainerCard1";
            this.ctrlTrainerCard1.Size = new System.Drawing.Size(1117, 614);
            this.ctrlTrainerCard1.TabIndex = 0;
            // 
            // frmTrainerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1130, 591);
            this.Controls.Add(this.ctrlTrainerCard1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmTrainerInfo";
            this.Text = "frmTrainerInfo";
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlTrainerCard ctrlTrainerCard1;
    }
}