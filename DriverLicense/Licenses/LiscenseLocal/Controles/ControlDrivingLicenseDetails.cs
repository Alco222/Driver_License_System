using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DriverLicense.Classes;
using DriverLicense.Properties;
using DriverLicenseBusinessLayer;

namespace DriverLicense
{
    public partial class ControlDrivingLicenseDetails : UserControl
    {
        int _LicenseID = -1;
        clsLicense _License;

        public ControlDrivingLicenseDetails()
        {
            InitializeComponent();
        }

        public int LicenseID
        {
            get { return _LicenseID; }
        }

        public clsLicense SelectedLicenseInfo
        {
            get { return _License; }
        }

        private void _LoadPersonImage()
        {
            if (_License.DriverInfo.PersonInfo.Gender == 0)
            {
                pbPersonImage.Image = Resources.person_boy;
                pbGender.Image = Resources.Man_32;
            }
            else
            {
                pbPersonImage.Image = Resources.person_girl;
                pbGender.Image = Resources.Woman_32;
            }

            string ImagePath = _License.DriverInfo.PersonInfo.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void RestControlDrivingLicenseDetails()
        {
            LblLicenseID.Text = "[?????]";
            lblActive.Text = "[?????]";
            lblDetained.Text = "[?????]";
            lblClass.Text = "[?????]";
            lblName.Text = "[?????]";
            LblNationalNo.Text = "[?????]";
            lblGender.Text = "[?????]";
            lblBirthDate.Text = "[?????]";
            lblDriverID.Text = "[?????]";
            lblIssueDate.Text = "[?????]";
            lblExpirationDate.Text = "[?????]";
            lblIssueReason.Text = "[?????]";
            lblNotes.Text = "[?????]";
            pbPersonImage.Image = Resources.person_boy;
            pbGender.Image = Resources.Man_32;

        }

        public void LoadLicenseInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            _License = clsLicense.Find(_LicenseID);
            if (_License == null)
            {
                MessageBox.Show("Could not find License ID = " + _LicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                RestControlDrivingLicenseDetails();
                return;
            }

            LblLicenseID.Text = _LicenseID.ToString();
            lblActive.Text = _License.IsActive ? "Yes" : "No";
            lblDetained.Text = _License.IsDetained ? "Yes" : "No";
            lblClass.Text = _License.LicenseClassInfo.Name;
            lblName.Text = _License.DriverInfo.PersonInfo.FullName;
            LblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNo;
            lblGender.Text = _License.DriverInfo.PersonInfo.Gender == 0 ? "Male" : "Female";
            lblBirthDate.Text = clsFormat.DateToShort(_License.DriverInfo.PersonInfo.BirthDate);
            lblDriverID.Text = _License.DriverID.ToString();
            lblIssueDate.Text = clsFormat.DateToShort(_License.IssueDate);
            lblExpirationDate.Text = clsFormat.DateToShort(_License.ExpirationDate);
            lblIssueReason.Text = _License.IssueReasonText;
            lblNotes.Text = _License.Notes == "" ? "No Notes" : _License.Notes;
            _LoadPersonImage();
        }

      
    }
}
