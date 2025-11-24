namespace DriverLicense
{
    partial class FrmShowLicenseDetails
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
            this.controlDrivingLicenseDetails1 = new DriverLicense.ControlDrivingLicenseDetails();
            this.btnClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ctrCustormerHeaderForm1 = new DriverLicense.CustomesHeaderForm.ctrCustormerHeaderForm();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // controlDrivingLicenseDetails1
            // 
            this.controlDrivingLicenseDetails1.BackColor = System.Drawing.Color.AliceBlue;
            this.controlDrivingLicenseDetails1.ForeColor = System.Drawing.Color.Black;
            this.controlDrivingLicenseDetails1.Location = new System.Drawing.Point(8, 74);
            this.controlDrivingLicenseDetails1.Margin = new System.Windows.Forms.Padding(1);
            this.controlDrivingLicenseDetails1.Name = "controlDrivingLicenseDetails1";
            this.controlDrivingLicenseDetails1.Size = new System.Drawing.Size(830, 275);
            this.controlDrivingLicenseDetails1.TabIndex = 42;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Red;
            this.btnClose.Image = global::DriverLicense.Properties.Resources.close__1_;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(728, 357);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(99, 29);
            this.btnClose.TabIndex = 41;
            this.btnClose.Text = "       Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.AliceBlue;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(848, 395);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // ctrCustormerHeaderForm1
            // 
            this.ctrCustormerHeaderForm1.BackColor = System.Drawing.Color.AliceBlue;
            this.ctrCustormerHeaderForm1.HeaderBackColor = System.Drawing.Color.AliceBlue;
            this.ctrCustormerHeaderForm1.IconImage = global::DriverLicense.Properties.Resources.LicenseView_400;
            this.ctrCustormerHeaderForm1.IconImage2 = null;
            this.ctrCustormerHeaderForm1.Location = new System.Drawing.Point(2, 0);
            this.ctrCustormerHeaderForm1.Name = "ctrCustormerHeaderForm1";
            this.ctrCustormerHeaderForm1.ParentFormRef = this;
            this.ctrCustormerHeaderForm1.Size = new System.Drawing.Size(742, 37);
            this.ctrCustormerHeaderForm1.TabIndex = 43;
            this.ctrCustormerHeaderForm1.TitleText = "Driver License Details";
            // 
            // FrmShowLicenseDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(848, 395);
            this.Controls.Add(this.ctrCustormerHeaderForm1);
            this.Controls.Add(this.controlDrivingLicenseDetails1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmShowLicenseDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmShowLicenseDetails";
            this.Load += new System.EventHandler(this.FrmShowLicenseDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnClose;
        private ControlDrivingLicenseDetails controlDrivingLicenseDetails1;
        private CustomesHeaderForm.ctrCustormerHeaderForm ctrCustormerHeaderForm1;
    }
}