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

namespace DriverLicense.Application.Release_Detain_LIcense
{
    public partial class FrmReleaseDetainLicenseApp : Form
    {
        int _SetcetedLicenseID = -1;
        public FrmReleaseDetainLicenseApp()
        {
            InitializeComponent();

        }

        public FrmReleaseDetainLicenseApp(int LicenseID)
        {
            InitializeComponent();
            _SetcetedLicenseID = LicenseID;
            controleLocalDrivingLicenseWithFilter1.LoadInfo(_SetcetedLicenseID);
            controleLocalDrivingLicenseWithFilter1.FilterEnabled = false;
        }

        private bool HandelIsDetenedLicense()
        {
            //check The License is not Detained.
            if (!controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.IsDetained)
            {                MessageBox.Show("This License is Not detained, choose License Detained.", "Not Alowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void FrmReleaseDetainLicenseApp_Load(object sender, EventArgs e)
        {
            controleLocalDrivingLicenseWithFilter1.TxtFilterFocus();
            tpReleaseDetainLicense.Enabled = false; // Disable the release detain license tab until a license is selected
            
            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).Fees.ToString();
            lblReleasedBy.Text = LoggedInUser.CurrentUser.UserName;

        }

        private void controleLocalDrivingLicenseWithFilter1_OnDrivingLicenseSelected(int obj)
        {
            _SetcetedLicenseID = obj;
            lblLicenseID.Text = _SetcetedLicenseID.ToString();
            llShowLicensesHistory.Enabled = (_SetcetedLicenseID != -1);
            if (_SetcetedLicenseID == -1)
            {
                return;
            }

            if (!HandelIsDetenedLicense())
                return;


            if (controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.DetainedInfo != null)
            {
                lblDetainDate.Text = clsFormat.DateToShort(controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.DetainedInfo.DetainDate);
                lblDetainID.Text = controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.DetainedInfo.DetainID.ToString();
                lblDetainedBy.Text = controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.DetainedInfo.CreatedByUserInfo.UserName.ToString();
                lblFineFees.Text = controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.DetainedInfo.FineFees.ToString();
                lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblFineFees.Text)).ToString();
            }
            else
            {
                lblDetainDate.Text = "[????]";
                lblDetainID.Text = "[????]";
                lblFineFees.Text = "[????]";
                lblTotalFees.Text = "[????]";
            }

            btnReleaseLicense.Enabled = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (!HandelIsDetenedLicense())
                return;

            //incase of add new mode.
            if (controleLocalDrivingLicenseWithFilter1.LicenseID != -1)
            {
                btnReleaseLicense.Enabled = true;
                tpReleaseDetainLicense.Enabled = true;
                tcReleaseDetainLicense.SelectedTab = tcReleaseDetainLicense.TabPages["tpReleaseDetainLicense"];
            }
            else

            {
                MessageBox.Show("Please Select anther License", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                controleLocalDrivingLicenseWithFilter1.TxtFilterFocus();
            }
        }

        private void btnReleaseLicense_Click(object sender, EventArgs e)
        {
            if (!HandelIsDetenedLicense())
                return;

            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int ReleasedByUserID = LoggedInUser.CurrentUser.UserID;
            int ApplicationID = -1;

            bool IsRelesed = controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.ReleaseDetainedLicense(ReleasedByUserID, ref ApplicationID);
            
            lblApplicationID.Text = ApplicationID.ToString();
            
            if(!IsRelesed)
            {
                MessageBox.Show("Failed to release the detained license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            

            MessageBox.Show("The License has been released successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
           
            btnReleaseLicense.Enabled = false;
            controleLocalDrivingLicenseWithFilter1.FilterEnabled = false;
            llShowLicensesInfo.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int LicenseID = controleLocalDrivingLicenseWithFilter1.LicenseID;
            FrmShowLicenseDetails frmShowLicenseDetails = new FrmShowLicenseDetails(LicenseID);
            frmShowLicenseDetails.ShowDialog();
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.DriverInfo.PersonID;
            FrmShowPersonLIcenseHistory frmShowPersonLIcenseHistory = new FrmShowPersonLIcenseHistory(PersonID);
            frmShowPersonLIcenseHistory.ShowDialog();
        }

        private void FrmReleaseDetainLicenseApp_Activated(object sender, EventArgs e)
        {
            controleLocalDrivingLicenseWithFilter1.TxtFilterFocus();
        }
    }
}
