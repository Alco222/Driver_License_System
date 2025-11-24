namespace DriverLicense
{
    partial class FrmShowPersonLIcenseHistory
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrCustormerHeaderForm1 = new DriverLicense.CustomesHeaderForm.ctrCustormerHeaderForm();
            this.ctrlPersonCardWithFilter11 = new DriverLicense.ctrlPersonCardWithFilter1();
            this.controlDriverLicense1 = new DriverLicense.ControlDriverLicense();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.AliceBlue;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(951, 671);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Red;
            this.btnClose.Image = global::DriverLicense.Properties.Resources.close__1_;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(845, 634);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(99, 29);
            this.btnClose.TabIndex = 46;
            this.btnClose.Text = "       Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrCustormerHeaderForm1
            // 
            this.ctrCustormerHeaderForm1.BackColor = System.Drawing.Color.AliceBlue;
            this.ctrCustormerHeaderForm1.HeaderBackColor = System.Drawing.Color.AliceBlue;
            this.ctrCustormerHeaderForm1.IconImage = global::DriverLicense.Properties.Resources.download;
            this.ctrCustormerHeaderForm1.IconImage2 = null;
            this.ctrCustormerHeaderForm1.Location = new System.Drawing.Point(1, 1);
            this.ctrCustormerHeaderForm1.Name = "ctrCustormerHeaderForm1";
            this.ctrCustormerHeaderForm1.ParentFormRef = this;
            this.ctrCustormerHeaderForm1.Size = new System.Drawing.Size(950, 34);
            this.ctrCustormerHeaderForm1.TabIndex = 50;
            this.ctrCustormerHeaderForm1.TitleText = "License History";
            // 
            // ctrlPersonCardWithFilter11
            // 
            this.ctrlPersonCardWithFilter11.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ctrlPersonCardWithFilter11.BackColor = System.Drawing.Color.AliceBlue;
            this.ctrlPersonCardWithFilter11.FilterEnabled = true;
            this.ctrlPersonCardWithFilter11.ForeColor = System.Drawing.Color.Black;
            this.ctrlPersonCardWithFilter11.Location = new System.Drawing.Point(115, 57);
            this.ctrlPersonCardWithFilter11.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlPersonCardWithFilter11.Name = "ctrlPersonCardWithFilter11";
            this.ctrlPersonCardWithFilter11.ShowAddPerson = true;
            this.ctrlPersonCardWithFilter11.Size = new System.Drawing.Size(781, 319);
            this.ctrlPersonCardWithFilter11.TabIndex = 51;
            // 
            // controlDriverLicense1
            // 
            this.controlDriverLicense1.BackColor = System.Drawing.Color.AliceBlue;
            this.controlDriverLicense1.Location = new System.Drawing.Point(8, 381);
            this.controlDriverLicense1.Margin = new System.Windows.Forms.Padding(2);
            this.controlDriverLicense1.Name = "controlDriverLicense1";
            this.controlDriverLicense1.Size = new System.Drawing.Size(941, 246);
            this.controlDriverLicense1.TabIndex = 52;
            // 
            // FrmShowPersonLIcenseHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(951, 671);
            this.Controls.Add(this.controlDriverLicense1);
            this.Controls.Add(this.ctrlPersonCardWithFilter11);
            this.Controls.Add(this.ctrCustormerHeaderForm1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmShowPersonLIcenseHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmShowPersonLIcenseHistory";
            this.Load += new System.EventHandler(this.FrmShowPersonLIcenseHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnClose;
        private CustomesHeaderForm.ctrCustormerHeaderForm ctrCustormerHeaderForm1;
        private ControlDriverLicense controlDriverLicense1;
        private ctrlPersonCardWithFilter1 ctrlPersonCardWithFilter11;
    }
}