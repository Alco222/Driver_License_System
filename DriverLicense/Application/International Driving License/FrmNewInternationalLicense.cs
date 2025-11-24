using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DriverLicense.Classes;
using DriverLicenseBusinessLayer;

namespace DriverLicense
{
    public partial class FrmNewInternationalLicenseApplication : Form
    {
        int _InternationalLicenseID = -1;

        public FrmNewInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private bool HandlIsDetenedLicense()
        {
            //check The License is not Detained.
            if (controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.IsDetained)
            {
                MessageBox.Show("This License is already detained, choose anther one.", "Not Alowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            return false;
        }

        private bool HandlIsActiveLicense()
        {
            //check The License is not Active.
            if (!controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.IsActive)
            {
                MessageBox.Show("This License is Not Active, choose an Active License.", "Not Alowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void FrmNewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text =clsFormat.DateToShort( DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(2));
            lblFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewInternationalLicense).Fees.ToString();
            lblCraatedBy.Text = LoggedInUser.CurrentUser.UserName;
        }

        private void controleLocalDrivingLicenseWithFilter1_OnDrivingLicenseSelected(int LicenseID)
        {
            int SelectedLicenseID = LicenseID;
            lblLocalLicenseID.Text = SelectedLicenseID.ToString();
            llShowLicensesHistory.Enabled = (SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)
            {
                return;
            }

            if (HandlIsDetenedLicense())
                return;

            if (!HandlIsActiveLicense())
                return;

            //check the license class, person could not issue international license without having
            //normal license of class 3.
            if (controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.LicenseClassID != 3)
            {
                MessageBox.Show("Selected License Should be License Class 3.Selected onther one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //check if person already has international license.

            int ActiveInternationalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.DriverID);

            if (ActiveInternationalLicenseID != -1)
            {
                MessageBox.Show("This person already has an active international license with ID = " + ActiveInternationalLicenseID.ToString(), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowLicensesInfo.Enabled = true;
                _InternationalLicenseID = ActiveInternationalLicenseID;
                btnIssueLicense.Enabled = false;
                return;
            }

            btnIssueLicense.Enabled = true;

        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {

            if (HandlIsDetenedLicense())
                return;

            if (!HandlIsActiveLicense())
                return;

            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            //clsInternationalLicense NewInternationalLicense = new clsInternationalLicense();

            /* NewInternationalLicense.ApplicantPersonID = controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.DriverInfo.PersonID;
             NewInternationalLicense.ApplicationDate = DateTime.Now;
             NewInternationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;

             NewInternationalLicense.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewInternationalLicense).Fees;
             NewInternationalLicense.CreatedByUserID = LogedInUser.CurrentUser.UserID;

             NewInternationalLicense.DriverID = controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.DriverID;
             NewInternationalLicense.IssuedUsedLocalLicenseID = controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.LicenseID;
             NewInternationalLicense.IssueDate = DateTime.Now;
             NewInternationalLicense.ExpirationDate = DateTime.Now.AddYears(2);*/

            int ApplicantPersonID = controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.DriverInfo.PersonID;
            DateTime ApplicationDate = DateTime.Now;
            clsApplication.enApplicationStatus ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            float PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewInternationalLicense).Fees;
            int CreatedByUserID = LoggedInUser.CurrentUser.UserID;
            int DriverID = controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.DriverID;
            int IssuedUsedLocalLicenseID = controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.LicenseID;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now.AddYears(2);

            clsInternationalLicense NewInternationalLicense = clsInternationalLicense.IssueInternationalLicense(ApplicantPersonID,ApplicationDate,ApplicationStatus,PaidFees, CreatedByUserID, DriverID, IssuedUsedLocalLicenseID, IssueDate, ExpirationDate);

            //NewInternationalLicense.CreatedByUserID = LogedInUser.CurrentUser.UserID;
            if (NewInternationalLicense == null)
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblApplicationID.Text = NewInternationalLicense.ApplicationID.ToString();
            _InternationalLicenseID = NewInternationalLicense.InternationalLicenseID;
            lblInternationalLicenseID.Text = _InternationalLicenseID.ToString();
            MessageBox.Show("International License Issued Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            llShowLicensesInfo.Enabled = true;
            btnIssueLicense.Enabled = false;
            controleLocalDrivingLicenseWithFilter1.Enabled = false;
        }

        private void llShowLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmShowLicenseDetails frmShowLicenseDetails = new FrmShowLicenseDetails(_InternationalLicenseID);
            frmShowLicenseDetails.ShowDialog();

        }

        private void FrmNewInternationalLicenseApplication_Activated(object sender, EventArgs e)
        {
            controleLocalDrivingLicenseWithFilter1.TxtFilterFocus();
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmShowPersonLIcenseHistory frmShowPersonLIcenseHistory = new FrmShowPersonLIcenseHistory(controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.DriverInfo.PersonID);
            frmShowPersonLIcenseHistory.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
