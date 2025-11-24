namespace DriverLicense
{
    partial class ControlDriverLicense
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.LocalLicenseMune = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showLocalLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InternationalMune = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showInternationalInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpLocalLicense = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvLocalLicenseHistory = new System.Windows.Forms.DataGridView();
            this.lblRecordLocal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tpInternationalLicense = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvInternationalLicenseHistory = new System.Windows.Forms.DataGridView();
            this.lblRecordInter = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblNoDataInternationalLicense = new System.Windows.Forms.Label();
            this.lblNoDataLocalLicense = new System.Windows.Forms.Label();
            this.LocalLicenseMune.SuspendLayout();
            this.InternationalMune.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpLocalLicense.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicenseHistory)).BeginInit();
            this.tpInternationalLicense.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicenseHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // LocalLicenseMune
            // 
            this.LocalLicenseMune.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.LocalLicenseMune.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLocalLicenseToolStripMenuItem});
            this.LocalLicenseMune.Name = "LocalLicenseMune";
            this.LocalLicenseMune.Size = new System.Drawing.Size(206, 34);
            this.LocalLicenseMune.Opening += new System.ComponentModel.CancelEventHandler(this.LocalLicenseMune_Opening);
            // 
            // showLocalLicenseToolStripMenuItem
            // 
            this.showLocalLicenseToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showLocalLicenseToolStripMenuItem.Image = global::DriverLicense.Properties.Resources.PersonDetails_32;
            this.showLocalLicenseToolStripMenuItem.Name = "showLocalLicenseToolStripMenuItem";
            this.showLocalLicenseToolStripMenuItem.Size = new System.Drawing.Size(205, 30);
            this.showLocalLicenseToolStripMenuItem.Text = "ShowLocalLicenseInfo";
            this.showLocalLicenseToolStripMenuItem.Click += new System.EventHandler(this.showLocalLicenseToolStripMenuItem_Click);
            // 
            // InternationalMune
            // 
            this.InternationalMune.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.InternationalMune.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showInternationalInfoToolStripMenuItem});
            this.InternationalMune.Name = "LocalLicenseMune";
            this.InternationalMune.Size = new System.Drawing.Size(209, 34);
            this.InternationalMune.Opening += new System.ComponentModel.CancelEventHandler(this.InternationalMune_Opening);
            // 
            // showInternationalInfoToolStripMenuItem
            // 
            this.showInternationalInfoToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showInternationalInfoToolStripMenuItem.Image = global::DriverLicense.Properties.Resources.PersonDetails_32;
            this.showInternationalInfoToolStripMenuItem.Name = "showInternationalInfoToolStripMenuItem";
            this.showInternationalInfoToolStripMenuItem.Size = new System.Drawing.Size(208, 30);
            this.showInternationalInfoToolStripMenuItem.Text = "ShowInternationalInfo";
            this.showInternationalInfoToolStripMenuItem.Click += new System.EventHandler(this.showInternationalInfoToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpLocalLicense);
            this.tabControl1.Controls.Add(this.tpInternationalLicense);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(6, 4);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(930, 242);
            this.tabControl1.TabIndex = 2;
            // 
            // tpLocalLicense
            // 
            this.tpLocalLicense.BackColor = System.Drawing.Color.AliceBlue;
            this.tpLocalLicense.Controls.Add(this.lblNoDataLocalLicense);
            this.tpLocalLicense.Controls.Add(this.label2);
            this.tpLocalLicense.Controls.Add(this.dgvLocalLicenseHistory);
            this.tpLocalLicense.Controls.Add(this.lblRecordLocal);
            this.tpLocalLicense.Controls.Add(this.label1);
            this.tpLocalLicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpLocalLicense.Location = new System.Drawing.Point(4, 25);
            this.tpLocalLicense.Margin = new System.Windows.Forms.Padding(2);
            this.tpLocalLicense.Name = "tpLocalLicense";
            this.tpLocalLicense.Padding = new System.Windows.Forms.Padding(2);
            this.tpLocalLicense.Size = new System.Drawing.Size(922, 213);
            this.tpLocalLicense.TabIndex = 0;
            this.tpLocalLicense.Text = "Local";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(65, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Local License  History";
            // 
            // dgvLocalLicenseHistory
            // 
            this.dgvLocalLicenseHistory.AllowUserToAddRows = false;
            this.dgvLocalLicenseHistory.AllowUserToDeleteRows = false;
            this.dgvLocalLicenseHistory.AllowUserToResizeRows = false;
            this.dgvLocalLicenseHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvLocalLicenseHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalLicenseHistory.ContextMenuStrip = this.LocalLicenseMune;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLocalLicenseHistory.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvLocalLicenseHistory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvLocalLicenseHistory.Location = new System.Drawing.Point(13, 17);
            this.dgvLocalLicenseHistory.MultiSelect = false;
            this.dgvLocalLicenseHistory.Name = "dgvLocalLicenseHistory";
            this.dgvLocalLicenseHistory.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLocalLicenseHistory.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvLocalLicenseHistory.RowHeadersWidth = 62;
            this.dgvLocalLicenseHistory.RowTemplate.Height = 28;
            this.dgvLocalLicenseHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLocalLicenseHistory.Size = new System.Drawing.Size(900, 161);
            this.dgvLocalLicenseHistory.TabIndex = 8;
            this.dgvLocalLicenseHistory.TabStop = false;
            // 
            // lblRecordLocal
            // 
            this.lblRecordLocal.AutoSize = true;
            this.lblRecordLocal.BackColor = System.Drawing.Color.Transparent;
            this.lblRecordLocal.Location = new System.Drawing.Point(105, 186);
            this.lblRecordLocal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordLocal.Name = "lblRecordLocal";
            this.lblRecordLocal.Size = new System.Drawing.Size(27, 20);
            this.lblRecordLocal.TabIndex = 2;
            this.lblRecordLocal.Text = "??";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 186);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "#Record";
            // 
            // tpInternationalLicense
            // 
            this.tpInternationalLicense.BackColor = System.Drawing.Color.AliceBlue;
            this.tpInternationalLicense.Controls.Add(this.lblNoDataInternationalLicense);
            this.tpInternationalLicense.Controls.Add(this.label5);
            this.tpInternationalLicense.Controls.Add(this.dgvInternationalLicenseHistory);
            this.tpInternationalLicense.Controls.Add(this.lblRecordInter);
            this.tpInternationalLicense.Controls.Add(this.label4);
            this.tpInternationalLicense.Location = new System.Drawing.Point(4, 25);
            this.tpInternationalLicense.Margin = new System.Windows.Forms.Padding(2);
            this.tpInternationalLicense.Name = "tpInternationalLicense";
            this.tpInternationalLicense.Padding = new System.Windows.Forms.Padding(2);
            this.tpInternationalLicense.Size = new System.Drawing.Size(922, 213);
            this.tpInternationalLicense.TabIndex = 1;
            this.tpInternationalLicense.Text = "International";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(49, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(210, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "International License History";
            // 
            // dgvInternationalLicenseHistory
            // 
            this.dgvInternationalLicenseHistory.AllowUserToAddRows = false;
            this.dgvInternationalLicenseHistory.AllowUserToDeleteRows = false;
            this.dgvInternationalLicenseHistory.AllowUserToResizeRows = false;
            this.dgvInternationalLicenseHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvInternationalLicenseHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInternationalLicenseHistory.ContextMenuStrip = this.InternationalMune;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInternationalLicenseHistory.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvInternationalLicenseHistory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvInternationalLicenseHistory.Location = new System.Drawing.Point(11, 24);
            this.dgvInternationalLicenseHistory.MultiSelect = false;
            this.dgvInternationalLicenseHistory.Name = "dgvInternationalLicenseHistory";
            this.dgvInternationalLicenseHistory.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInternationalLicenseHistory.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvInternationalLicenseHistory.RowHeadersWidth = 62;
            this.dgvInternationalLicenseHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInternationalLicenseHistory.Size = new System.Drawing.Size(900, 159);
            this.dgvInternationalLicenseHistory.TabIndex = 9;
            // 
            // lblRecordInter
            // 
            this.lblRecordInter.AutoSize = true;
            this.lblRecordInter.BackColor = System.Drawing.Color.Transparent;
            this.lblRecordInter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordInter.Location = new System.Drawing.Point(104, 188);
            this.lblRecordInter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordInter.Name = "lblRecordInter";
            this.lblRecordInter.Size = new System.Drawing.Size(27, 20);
            this.lblRecordInter.TabIndex = 5;
            this.lblRecordInter.Text = "??";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 188);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "#Record";
            // 
            // lblNoDataInternationalLicense
            // 
            this.lblNoDataInternationalLicense.BackColor = System.Drawing.Color.White;
            this.lblNoDataInternationalLicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoDataInternationalLicense.ForeColor = System.Drawing.Color.Red;
            this.lblNoDataInternationalLicense.Location = new System.Drawing.Point(206, 83);
            this.lblNoDataInternationalLicense.Name = "lblNoDataInternationalLicense";
            this.lblNoDataInternationalLicense.Size = new System.Drawing.Size(480, 29);
            this.lblNoDataInternationalLicense.TabIndex = 11;
            this.lblNoDataInternationalLicense.Text = "No data or driver International license available";
            this.lblNoDataInternationalLicense.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoDataInternationalLicense.Visible = false;
            // 
            // lblNoDataLocalLicense
            // 
            this.lblNoDataLocalLicense.BackColor = System.Drawing.Color.White;
            this.lblNoDataLocalLicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoDataLocalLicense.ForeColor = System.Drawing.Color.Red;
            this.lblNoDataLocalLicense.Location = new System.Drawing.Point(246, 78);
            this.lblNoDataLocalLicense.Name = "lblNoDataLocalLicense";
            this.lblNoDataLocalLicense.Size = new System.Drawing.Size(410, 29);
            this.lblNoDataLocalLicense.TabIndex = 12;
            this.lblNoDataLocalLicense.Text = "No data or driver Local license available";
            this.lblNoDataLocalLicense.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoDataLocalLicense.Visible = false;
            // 
            // ControlDriverLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ControlDriverLicense";
            this.Size = new System.Drawing.Size(936, 245);
            this.LocalLicenseMune.ResumeLayout(false);
            this.InternationalMune.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpLocalLicense.ResumeLayout(false);
            this.tpLocalLicense.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicenseHistory)).EndInit();
            this.tpInternationalLicense.ResumeLayout(false);
            this.tpInternationalLicense.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicenseHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip LocalLicenseMune;
        private System.Windows.Forms.ToolStripMenuItem showLocalLicenseToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip InternationalMune;
        private System.Windows.Forms.ToolStripMenuItem showInternationalInfoToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpLocalLicense;
        private System.Windows.Forms.Label lblRecordLocal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tpInternationalLicense;
        private System.Windows.Forms.Label lblRecordInter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvInternationalLicenseHistory;
        private System.Windows.Forms.DataGridView dgvLocalLicenseHistory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblNoDataInternationalLicense;
        private System.Windows.Forms.Label lblNoDataLocalLicense;
    }
}
