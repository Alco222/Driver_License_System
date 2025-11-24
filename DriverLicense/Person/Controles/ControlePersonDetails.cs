using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DriverLicense.Properties;
using DriverLicenseBusinessLayer;


namespace DriverLicense
{
    public partial class ControlePersonDetails : UserControl
    {

        int _PersonID =-1;
        clsPerson _Person;

        public clsPerson SelectedPersonInfo
        {
            get { return _Person; }
        }

        public int PersonID
        {
            get { return _PersonID; }
        }
        public ControlePersonDetails()
        {
            InitializeComponent();
        }

        private void _LoadPersonImage()
        {

            string ImagePath = _Person.ImagePath;

            if (ImagePath != "")
            {
                if (File.Exists(ImagePath))
                    pbImagePersonel.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image" + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (_Person.Gender == 0)
                    pbImagePersonel.Image = Resources.person_boy;
                else
                    pbImagePersonel.Image = Resources.person_girl;

            }
        }
  
        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with PersonID = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();

        }

        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with National No. = " + NationalNo.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }

        private void _FillPersonInfo()
        {
            llEditPerson.Enabled = true;
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblNationalNO.Text = _Person.NationalNo;
            lblName.Text = _Person.FullName;
            lblGender.Text = _Person.Gender == 0 ? "Male" : "Female";
            lblEmail.Text = _Person.Email;
            lblPhone.Text = _Person.Phone;
            lblBirthDate.Text = _Person.BirthDate.ToShortDateString();
            lblCountry.Text = clsCountry.Find(_Person.NationalityID).CountryName;
            lblAddress.Text = _Person.Address;
            _LoadPersonImage();

        }

        public void ResetPersonInfo()
        {
            _PersonID = -1;
            lblPersonID.Text = "[????]";
            lblNationalNO.Text = "[????]";
            lblName.Text = "[????]";
            lblGender.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblBirthDate.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAddress.Text = "[????]";
            pbImagePersonel.Image = Resources.person_boy;

        }

        

        private void llEditPerson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            FrmAddEditPersonInfo frm = new FrmAddEditPersonInfo(_PersonID);
            frm.DataBack += FrmAddEditPerson_DataBack;

            frm.ShowDialog();
        }

        private void FrmAddEditPerson_DataBack(object sender, int PersonID)
        {
            LoadPersonInfo(PersonID);

        }

    }
}
