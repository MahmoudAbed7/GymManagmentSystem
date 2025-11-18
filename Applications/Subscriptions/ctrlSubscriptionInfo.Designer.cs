namespace GMS.Applications.Subscriptions
{
    partial class ctrlSubscriptionInfo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrlApplicationBasicInfo1 = new GMS.Applications.Control.ctrlApplicationBasicInfo();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.llShowMembershipCardInfo = new System.Windows.Forms.LinkLabel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblSubscriptionID = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlApplicationBasicInfo1
            // 
            this.ctrlApplicationBasicInfo1.Location = new System.Drawing.Point(3, 0);
            this.ctrlApplicationBasicInfo1.Name = "ctrlApplicationBasicInfo1";
            this.ctrlApplicationBasicInfo1.Size = new System.Drawing.Size(1192, 270);
            this.ctrlApplicationBasicInfo1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.llShowMembershipCardInfo);
            this.groupBox1.Controls.Add(this.pictureBox3);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.lblSubscriptionID);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(3, 276);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1189, 183);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Subscription Application Basic info";
            // 
            // llShowMembershipCardInfo
            // 
            this.llShowMembershipCardInfo.AutoSize = true;
            this.llShowMembershipCardInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llShowMembershipCardInfo.Location = new System.Drawing.Point(319, 105);
            this.llShowMembershipCardInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.llShowMembershipCardInfo.Name = "llShowMembershipCardInfo";
            this.llShowMembershipCardInfo.Size = new System.Drawing.Size(260, 25);
            this.llShowMembershipCardInfo.TabIndex = 196;
            this.llShowMembershipCardInfo.TabStop = true;
            this.llShowMembershipCardInfo.Text = "Show Membership Card Info";
            this.llShowMembershipCardInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowMembershipCardInfo_LinkClicked);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::GMS.Properties.Resources.License_View_32;
            this.pictureBox3.Location = new System.Drawing.Point(269, 98);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(40, 32);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 195;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::GMS.Properties.Resources.Number_32;
            this.pictureBox2.Location = new System.Drawing.Point(268, 52);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(40, 32);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 194;
            this.pictureBox2.TabStop = false;
            // 
            // lblSubscriptionID
            // 
            this.lblSubscriptionID.AutoSize = true;
            this.lblSubscriptionID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubscriptionID.Location = new System.Drawing.Point(319, 52);
            this.lblSubscriptionID.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSubscriptionID.Name = "lblSubscriptionID";
            this.lblSubscriptionID.Size = new System.Drawing.Size(62, 25);
            this.lblSubscriptionID.TabIndex = 193;
            this.lblSubscriptionID.Text = "[???]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(38, 52);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(211, 25);
            this.label4.TabIndex = 192;
            this.label4.Text = "Subscription.App ID:";
            // 
            // ctrlSubscriptionInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ctrlApplicationBasicInfo1);
            this.Name = "ctrlSubscriptionInfo";
            this.Size = new System.Drawing.Size(1195, 467);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Control.ctrlApplicationBasicInfo ctrlApplicationBasicInfo1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel llShowMembershipCardInfo;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblSubscriptionID;
        private System.Windows.Forms.Label label4;
    }
}
