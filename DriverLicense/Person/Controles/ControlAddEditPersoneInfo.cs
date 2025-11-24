using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DriverLicense.Properties;
using DriverLicense.Classes;
using DriverLicenseBusinessLayer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using DVLD.Classes;

namespace DriverLicense
{
    public partial class ControlAddEditPersoneInfo : UserControl
    {
  
        public class sendPersonInformationCompleteEventArgs : EventArgs
        {
            public int PersonID { get; }
            public string ModeString { get; }


            public sendPersonInformationCompleteEventArgs(int PersonID,string ModeString)
            {
                this.PersonID = PersonID;
              
                this.ModeString = ModeString;

            }
        }

        public event EventHandler<sendPersonInformationCompleteEventArgs> OnSendPersonInformationComplete;

        public void RaisOnSendPersonInformationComplete(int PersonID,string ModeString)
        {
            RaisOnSendPersonInformationComplete(new sendPersonInformationCompleteEventArgs(PersonID,ModeString));
        }

        public virtual void RaisOnSendPersonInformationComplete(sendPersonInformationCompleteEventArgs e)
        {
            OnSendPersonInformationComplete?.Invoke(this, e);
        }

        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        public enum enGender { Male =0 , Female =1 }

        int _PersonID;
        clsPerson _Person;
        string ModeString;
        int lblPersonID;
       
        public ControlAddEditPersoneInfo()
        {
            InitializeComponent();
            
        }

        private bool _ProcesingImage()
        {
            //this procedure will handle the person image,
            //it will take care of deleting the old image from the folder
            //in case the image changed. and it will rename the new image with guid and 
            // place it in the images folder.

            //_Person.ImagePath contains the old Image, we check if it changed then we copy the new image

            if(_Person.ImagePath != pbImage.ImageLocation)
            {
                if(_Person.ImagePath != "")
                {
                    //first we delete the old image from the folder in case there is any.

                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch(IOException)
                    {
                        // We could not delete the file.
                        //log it later
                    }

                }

                if(pbImage.ImageLocation != null)
                {
                    //then we copy the new image to the image folder after we rename it
                    string SourceImageFile = pbImage.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        public void SetPersonID(int personID)
        {
            _PersonID = personID;
          
            if (_PersonID == -1)
                _Mode=enMode.AddNew; 
            else
               _Mode = enMode.Update;
        }

        private void _FillCountriesInComeboBox()
        {
            // Create a DataTable and load all countries from the database into it
            DataTable dtCountries = clsCountry.GetAllCountries();

            // Loop through each country and add it to the ComboBox
            foreach (DataRow row in dtCountries.Rows)
            {
                // Add the country name to the ComboBox list
                cbCountry.Items.Add(row["CountryName"]);
            }
        }

        private void _ResetDefualtValues()
        {
            //this will initialize the reset the defaule values
            _FillCountriesInComeboBox();

            if (_Mode == enMode.AddNew)
            {
                ModeString = "Add New Person";
                _Person = new clsPerson();
            }
            else
            {
                ModeString = "Update Person";
            }

            //set default image for the person.
            if (rbMale.Checked)
                pbImage.Image = Resources.person_boy;
            else
                pbImage.Image = Resources.person_girl;

            //hide/show the remove linke incase there is no image for the person.
            llRemove.Visible = (pbImage.ImageLocation != null);

            //we set the max date to 18 years from today, and set the default value the same.
            dtBirthDate.MaxDate = DateTime.Now.AddYears(-18);
            dtBirthDate.Value = dtBirthDate.MaxDate;

            //should not allow adding age more than 100 years
            dtBirthDate.MinDate = DateTime.Now.AddYears(-100);

            //this will set default country to jordan.
            cbCountry.SelectedIndex = cbCountry.FindString("Morocco");

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            rbMale.Checked = true;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";


        }

        private void _LoadData()
        {
            Form Frm = this.FindForm();

            // If you are editing, retrieve the contact by ID
            _Person = clsPerson.Find(_PersonID);

            // If contact not found, show a message and close the form
            if (_Person == null)
            {
                MessageBox.Show($"This form will be closed because no Person with ID = {_PersonID}");
                Frm.Close();
                return;
            }

            // Update labels and form fields with contact's data
            ModeString = $"Update Person";
            lblPersonID = _PersonID;
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtNationalNo.Text = _Person.NationalNo;
            txtEmail.Text = _Person.Email;
            txtPhone.Text = _Person.Phone;
            txtAddress.Text = _Person.Address;
            dtBirthDate.Value = _Person.BirthDate;

            if (_Person.Gender == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;


            // If image path is not empty, load the image in the PictureBox
            //load person image incase it was set.
            if (_Person.ImagePath != "")
            {
                pbImage.ImageLocation = _Person.ImagePath;

            }
            else
            {
                if (rbMale.Checked)
                    pbImage.Image = Resources.person_boy;
                else
                    pbImage.Image = Resources.person_girl;
            }


            // Show the "Remove Image" link only if image exists
            llRemove.Visible = (_Person.ImagePath != "");

            // Select the correct country in the ComboBox by matching the country name
            cbCountry.SelectedIndex = cbCountry.FindString(_Person.countryInfo.CountryName);

            if (OnSendPersonInformationComplete != null)
            {
                RaisOnSendPersonInformationComplete(_PersonID, ModeString);
            }
        }

        private void ControlAddEditPersoneInfo_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if(_Mode == enMode.Update)
                _LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_ProcesingImage())
             return;

            // Get the selected country's ID by its name from the ComboBox
            int CountryID = clsCountry.Find(cbCountry.Text).ID;

            // Assign the form fields to the _Contact object's properties
            _Person.FirstName = txtFirstName.Text;
            _Person.SecondName = txtSecondName.Text;
            _Person.ThirdName = txtThirdName.Text;
            _Person.LastName = txtLastName.Text;
            _Person.NationalNo = txtNationalNo.Text;
            _Person.Email = txtEmail.Text;
            _Person.Phone = txtPhone.Text;
            _Person.Address = txtAddress.Text;
            _Person.BirthDate = dtBirthDate.Value.Date;
            _Person.NationalityID = CountryID;
            if (rbMale.Checked)
                _Person.Gender = (byte)enGender.Male;
            else
                _Person.Gender = (byte)enGender.Female;


            if (pbImage.ImageLocation != null)
                _Person.ImagePath = pbImage.ImageLocation;
            else
                _Person.ImagePath = "";


            // Save the contact object (either Add or update)
            if (_Person.Save())
                MessageBox.Show("Data Saved Successfully.", "ADD Or Update Person", MessageBoxButtons.OK,MessageBoxIcon.Information);
            else
                MessageBox.Show("Error: Data Is not Saved.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

            // After saving, switch to Update Mode and update labels
            _Mode = enMode.Update;
            ModeString = $"Update Person";
            lblPersonID = _Person.PersonID;

            if (OnSendPersonInformationComplete != null)
            {
                RaisOnSendPersonInformationComplete(lblPersonID,ModeString);
            }

        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                pbImage.ImageLocation =selectedFilePath;
                llRemove.Visible = true;

               
                pbImage.Load( selectedFilePath);
                llRemove.Visible = true;

            }
        }
       
        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }

        }

        private void llRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbImage.ImageLocation = null;

            if (rbMale.Checked)
                pbImage.Image = Resources.person_boy;
            else
                pbImage.Image = Resources.person_girl;

            llRemove.Visible = false;

             
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }

            //Make sure the national number is not used by another person
            if (txtNationalNo.Text.Trim() != _Person.NationalNo && clsPerson.IsPersonExists(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "National Number is used for another person!");

            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            //no need to validate the email incase it's empty.
            if (txtEmail.Text.Trim() == "")
                return;

            //validate email format
            if (!clsValidatoin.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            };
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Form frm = this.FindForm();
            frm.Close();
        }

        private void rbMale_Click(object sender, EventArgs e)
        {
            //change the defualt image to male incase there is no image set.
            if (pbImage.ImageLocation == null)
                pbImage.Image = Resources.person_boy;
        }

        private void rbFemale_Click(object sender, EventArgs e)
        {
            //change the defualt image to female incase there is no image set.
            if (pbImage.ImageLocation == null)
                pbImage.Image = Resources.person_girl;
        }
    }
}
