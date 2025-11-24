using System;
using System.CodeDom;
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

namespace DriverLicense.Application.RenewLocalDrinvingLIcense
{
    public partial class FrmRenewLocalDrivingLIcense : Form
    {
        int _RenewedLicenseID = -1;
        public FrmRenewLocalDrivingLIcense()
        {
            InitializeComponent();
        }

        private bool HandleExpiredLicense()
        {
            //check The License  is not Expire.
            if (!controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show("Selected License is not yet expired, it will expire on:" + clsFormat.DateToShort(controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.ExpirationDate), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueRenewLicense.Enabled = false;
                return false;
            }
            return true;
        }

        private bool HandlIsActiveLicense()
        {
            //check The License  is not Active.
            if (!controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is Not Active, choose an active license.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueRenewLicense.Enabled = false;
                return false;
            }
            return true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            
            //incase of add new mode.
            if (controleLocalDrivingLicenseWithFilter1.LicenseID != -1)
            {
                //check The License  is not Expire.
                if (!HandleExpiredLicense())
                    return;

                //check The License  is not Active.
                if (!HandlIsActiveLicense())
                    return;


                btnIssueRenewLicense.Enabled = true;
                tpRenewLicenseLocal.Enabled = true;
                tcRenewLicense.SelectedTab = tcRenewLicense.TabPages["tpRenewLicenseLocal"];
            }
            else
            {
                MessageBox.Show("Please Select License", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                controleLocalDrivingLicenseWithFilter1.TxtFilterFocus();
            }
        }

        private void FrmRenewLocalDrivingLIcense_Load(object sender, EventArgs e)
        {
            
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).Fees.ToString();
            lblCraatedBy.Text = LoggedInUser.CurrentUser.UserName;
            tpRenewLicenseLocal.Enabled = false; // Disable the renew license tab until a license is selected
        }

        private void controleLocalDrivingLicenseWithFilter1_OnDrivingLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;
            lblOldLicenseID.Text = SelectedLicenseID.ToString();
            llShowLicensesHistory.Enabled = (SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)
            {
                return;
            }

            int DefaultValidityLenght = controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.LicenseClassInfo.ValidityLenghtYear;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(DefaultValidityLenght));

            lblLicenseFees.Text = clsLicense.Find(SelectedLicenseID)?.PaidFees.ToString() ?? "0";
            lblTotalFees.Text = (Convert.ToInt32(lblApplicationFees.Text) + Convert.ToInt32(lblLicenseFees.Text)).ToString();

            //check The License is not Expire.
            if(!HandleExpiredLicense())
               return;
            
            //check The License is not Active.
            if (!HandlIsActiveLicense())
               return;

             btnIssueRenewLicense.Enabled = true;
        }

        private void btnIssueRenewLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            //check The License  is not Expire.
            if (!HandleExpiredLicense())
                return;

            //check The License  is not Active.
            if (!HandlIsActiveLicense())
                return;

            int CreatedByUserID = LoggedInUser.CurrentUser.UserID;
            string Notes = txtNotes.Text;
            clsLicense RenewedLicense = controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.RenewLicense(Notes,CreatedByUserID);
            if (RenewedLicense == null)
            {
                MessageBox.Show($"{controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.MessageError}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblApplicationID.Text = RenewedLicense.ApplicationID.ToString();
            _RenewedLicenseID = RenewedLicense.LicenseID;
            lblInternationalLicenseID.Text = _RenewedLicenseID.ToString();
            MessageBox.Show("Renew License Issued Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            llShowLicensesInfo.Enabled = true;
            btnIssueRenewLicense.Enabled = false;
            controleLocalDrivingLicenseWithFilter1.Enabled = false;
        }

        private void llShowLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmShowLicenseDetails frmShowLicenseDetails = new FrmShowLicenseDetails(_RenewedLicenseID);
            frmShowLicenseDetails.ShowDialog();
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmShowPersonLIcenseHistory frmShowPersonLIcenseHistory = new FrmShowPersonLIcenseHistory(controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.DriverInfo.PersonID);
            frmShowPersonLIcenseHistory.ShowDialog();
            //FrmRenewLocalDrivingLIcense_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmRenewLocalDrivingLIcense_Activated(object sender, EventArgs e)
        {
            controleLocalDrivingLicenseWithFilter1.TxtFilterFocus();

        }
    }
}
