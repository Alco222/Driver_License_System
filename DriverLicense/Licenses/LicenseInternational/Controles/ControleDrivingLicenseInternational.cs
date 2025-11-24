using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DriverLicense.Properties;
using DriverLicenseBusinessLayer;
using DriverLicense.Classes;

namespace DriverLicense
{
    public partial class ControleDrivingLicenseInternational : UserControl
    {
        int _InternationalLicenseID = -1;
        clsInternationalLicense _InternationalDrivingLicense;

        public ControleDrivingLicenseInternational()
        {
            InitializeComponent();
        }

        public int InternationalLicenseID
        {
            get { return _InternationalLicenseID; }
        }

        clsInternationalLicense SelectedInternationalLicenseInfo
        {
            get { return _InternationalDrivingLicense; }
        }


        private void _LoadImagePersone()
        {
            if (_InternationalDrivingLicense.DriverInfo.PersonInfo.Gender == 0)
            {
                pbPersonImage.Image = Resources.person_boy;
                pbGender.Image = Properties.Resources.Man_32;
            }
            else
            {
                pbPersonImage.Image = Resources.person_girl;
                pbGender.Image = Resources.Woman_32;
            }

            string ImagePath = _InternationalDrivingLicense.DriverInfo.PersonInfo.ImagePath;
            if (ImagePath != "")
            {
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadLicenseInternationalInfo(int InternationalLicenseID)
        {
            _InternationalLicenseID = InternationalLicenseID;

            _InternationalDrivingLicense = clsInternationalLicense.Find(_InternationalLicenseID);
            if (_InternationalDrivingLicense == null)
            {
                MessageBox.Show("Could not find this International License ID: " + _InternationalLicenseID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lbInternationalID.Text = _InternationalDrivingLicense.InternationalLicenseID.ToString();
            lblApplicationID.Text = _InternationalDrivingLicense.ApplicationID.ToString();
            lblDriverID.Text = _InternationalDrivingLicense.DriverID.ToString();
            LblLicenseID.Text = _InternationalDrivingLicense.IssuedUsedLocalLicenseID.ToString();
            lblActive.Text = _InternationalDrivingLicense.IsActive ? "Yes" : "No";
            lblIssueDate.Text = clsFormat.DateToShort(_InternationalDrivingLicense.IssueDate);
            lblExpirationDate.Text = clsFormat.DateToShort(_InternationalDrivingLicense.ExpirationDate);

            lblName.Text = _InternationalDrivingLicense.DriverInfo.PersonInfo.FullName;
            LblNationalNo.Text = _InternationalDrivingLicense.DriverInfo.PersonInfo.NationalNo;
            lblGender.Text = _InternationalDrivingLicense.DriverInfo.PersonInfo.Gender ==0 ?"Male" : "Female";
            lblBirthDate.Text = clsFormat.DateToShort(_InternationalDrivingLicense.DriverInfo.PersonInfo.BirthDate);
            _LoadImagePersone();

        }
    }
}
