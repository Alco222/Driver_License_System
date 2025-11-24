using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DriverLicenseBusinessLayer;

namespace DriverLicense
{
    public partial class FrmAddEditUser : Form
    {

        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;

        int _PersonID;
        int _UserID;
        clsUsers _User;

    
        private bool _ValidateUserName(string UserName)
        {

            clsUsers User = clsUsers.FindByUserName(UserName);

            if (User != null && _Mode == enMode.AddNew)
                return true;
            else
                return false;

        }

        private void _ResetDefaulValue()
        {
            // If we are adding a new contact
            if (_Mode == enMode.AddNew)
            {
                ctrCustormerHeaderForm1.TitleText = "Add New User"; // Update label to reflect mode
                _User = new clsUsers(); // Create a new empty contact object
                tpUserInfo.Enabled = false; // Disable the User Info tab until a person is selected
                return; //Exit

            }
            else
            {
                ctrCustormerHeaderForm1.TitleText = $"Edit User";
                tpUserInfo.Enabled = true; // Enable the User Info tab for editing
                btnSave.Enabled = true; // Enable the Save button
            }

            lblUserID.Text = "[??]";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfPass.Text = "";
            chbActive.Checked = true;
        }
        private void _LoadData()
        {
           
            ctrlPersonCardWithFilter11.FilterEnabled = false;

            // If you are editing, retrieve the contact by ID
            _User = clsUsers.Find(_UserID);


            // If contact not found, show a message and close the form
            if (_User == null)
            {
                MessageBox.Show($"This form will be closed because no User with ID = {_UserID}");
                this.Close();
                return;
            }

            // Update labels and form fields with contact's data
            ctrCustormerHeaderForm1.TitleText = $"Update User";
            ctrlPersonCardWithFilter11.LoadPersonInfo(_User.PersonID);
            _PersonID = _User.PersonID;
            lblUserID.Text = _UserID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfPass.Text = _User.Password;
            chbActive.Checked = _User.IsActive;

           
            
        }

        public FrmAddEditUser(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;

            if (_UserID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;
        }

        private void FrmAddEditUser_Load(object sender, EventArgs e)
        {
            _ResetDefaulValue();

            if (_Mode == enMode.Update)
              _LoadData();
           
        } 
        
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpUserInfo.Enabled = true;
                tcUsreInfo.SelectedTab = tcUsreInfo.TabPages["tpUserInfo"];
                return;
            }

            //incase of add new mode.
            if (ctrlPersonCardWithFilter11.PersonID != -1)
            {

                if (clsUsers.IsUserExists(ctrlPersonCardWithFilter11.PersonID))
                {

                    MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter11.FilterFocus();
                }

                else
                {
                    btnSave.Enabled = true;
                    tpUserInfo.Enabled = true;
                    tcUsreInfo.SelectedTab = tcUsreInfo.TabPages["tpUserInfo"];
                }
            }

            else

            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter11.FilterFocus();

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            // Assign the form fields to the _Contact object's properties
            _User.UserName = txtUserName.Text;
            _User.Password = txtPassword.Text;
            _User.IsActive = chbActive.Checked;
            _User.PersonID = _PersonID;

           
            if (_User.Save())
                MessageBox.Show("Data Saved Successfully.","ADD Or Update User", MessageBoxButtons.OK,MessageBoxIcon.Information);
            else
                MessageBox.Show("Error: Data Is not Saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // After saving, switch to Update Mode and update labels
            _Mode = enMode.Update;
            ctrCustormerHeaderForm1.TitleText = $"Update User";
            lblUserID.Text = _User.UserID.ToString();
            ctrlPersonCardWithFilter11.FilterEnabled = false; // Disable the filter after saving
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtConfPass_TextChanged(object sender, EventArgs e)
        {
            if (txtConfPass.Text != txtPassword.Text)
            {
                errorProvider1.SetError(txtConfPass, "Passwor Confirmation does not matched Password!");
                txtConfPass.Focus();
            }
            else
            {
                errorProvider1.SetError(txtConfPass, "");
            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "User Name cannot be blank!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtUserName, "");

            }


            if (_ValidateUserName(txtUserName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "User Name is used for andther User");
            
            }
            else
            {
                errorProvider1.SetError(txtUserName, "");
            }
        }

        private void TextBox_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBox, "This field cannot be empty.");
            }
            else
            {
                errorProvider1.SetError(textBox, "");
            }
        }

        private void ctrlPersonCardWithFilter11_OnGetPesonIDCompleted(int obj)
        {
            _PersonID = obj;
        }

        private void FrmAddEditUser_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter11.FilterFocus();
        }

    }
}
