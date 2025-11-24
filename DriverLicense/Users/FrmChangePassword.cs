using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DriverLicenseBusinessLayer;

namespace DriverLicense
{
    public partial class FrmChangePassword : Form
    {
        int _UserID;
        clsUsers _User;

        public FrmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void _ResetDefaultValue()
        {
            txtCurrentPassword.Text = "";
            txtNewPass.Text = "";
            txtConfirmPass.Text = "";
        }

        private void FrmChangePassword_Load(object sender, EventArgs e)
        {
            _ResetDefaultValue();

            _User = clsUsers.Find(_UserID);
            if (_User == null)
            {
                MessageBox.Show($"Could not Find User With UserID = {_UserID}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            controleUserInfo1.LoadDataUser(_UserID);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validateion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            /*if (MessageBox.Show("Are you sure you want to change Password.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }*/
            // Assign the form fields to the _Contact object's properties
            _User.Password = txtNewPass.Text;

            if (clsUsers.ChangePassword(_UserID, txtNewPass.Text.Trim()))
            {
                MessageBox.Show("Password Change Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ResetDefaultValue();
            }
            else
            {
                MessageBox.Show("Error: Password Is not Changed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current Password Is Blank!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            };

            if (_User.Password != clsSecurityHelper.ComputeHash(txtCurrentPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current Password Is Wrong!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            };
        }

        private void txtConfirmPass_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtConfirmPass.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPass, "Confirme Password Is Blank!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPass, null);
            };

            if (txtNewPass.Text.Trim() != txtConfirmPass.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPass, "New Password And Confirm Password Not Match!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPass,null);
            };
        }

        private void txtNewPass_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtNewPass.Text.Trim()))
            { 
                e.Cancel = true;
            errorProvider1.SetError(txtNewPass, "Confirme Password Is Blank!");
            }
            else
            {
                errorProvider1.SetError(txtNewPass, null);
            };

            if(txtNewPass.Text.Trim() == txtCurrentPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPass, "New Password Must Be Different From Current Password!");
            }
            else
            {
                errorProvider1.SetError(txtNewPass, null);
            };
        }

        private void chbShowCurrentPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chbShowCurrentPass.Checked)
                txtCurrentPassword.PasswordChar = char.MinValue;
            else
                txtCurrentPassword.PasswordChar = '*';
            
        }

        private void chbShowNewPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chbShowNewPass.Checked)
                txtNewPass.PasswordChar = char.MinValue;
            else
                txtNewPass.PasswordChar = '*';
        }

        private void chbShowConfirmPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chbShowConfirmPass.Checked)
                txtConfirmPass.PasswordChar = char.MinValue;
            else
                txtCurrentPassword.PasswordChar = '*';
        }
    }
}
