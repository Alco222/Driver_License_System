namespace DriverLicense
{
    partial class FormTests
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
            this.btnTest2 = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnTest2
            // 
            this.btnTest2.Location = new System.Drawing.Point(438, 62);
            this.btnTest2.Name = "btnTest2";
            this.btnTest2.Size = new System.Drawing.Size(117, 38);
            this.btnTest2.TabIndex = 0;
            this.btnTest2.Text = "button1";
            this.btnTest2.UseVisualStyleBackColor = true;
            this.btnTest2.Click += new System.EventHandler(this.btnTest2_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(588, 62);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(117, 32);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "button2";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(528, 18);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(27, 20);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "??";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(80, 213);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(124, 38);
            this.button3.TabIndex = 3;
            this.button3.Text = "Exists Person";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(54, 157);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(194, 26);
            this.textBox1.TabIndex = 4;
            // 
            // FormTests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(814, 440);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnTest2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormTests";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormTests";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTest2;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
    }
}