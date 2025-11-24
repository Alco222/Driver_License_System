using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DriverLicense.Classes;
using DriverLicenseBusinessLayer;

namespace DriverLicense.Application.Replacement_of_Demaged
{
    public partial class FrmReplacementForDamagedOrLostLicense : Form
    {
        int _ReplacementLicenseID = -1;
        public FrmReplacementForDamagedOrLostLicense()
        {
            InitializeComponent();
        }

        private bool _HandlingSelectedLicenseInfo()
        {
            if (controleLocalDrivingLicenseWithFilter1.selectLicenseInfo == null)
            {
                MessageBox.Show("Please Selected an active license.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool _HandlingIsActiveLicense()
        {
            if (!controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is Not Active, choose an active license.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool _HandilingIsDetainLicense()
        {
            if(!controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License is Not Detain , choose not Detain license.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void FrmReplacementForDamagedOrLostLicense_Load(object sender, EventArgs e)
        {
            controleLocalDrivingLicenseWithFilter1.TxtFilterFocus();

            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCraatedBy.Text = LoggedInUser.CurrentUser.UserName;

            if (rbDamagedLicense.Checked)
            {
                lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense).Fees.ToString();
                ctrCustormerHeaderForm1.TitleText = "Repalcement for Damaged  License";
                
            }
            else
            {
                lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReplaceLostDrivingLicense).Fees.ToString();
                ctrCustormerHeaderForm1.TitleText = "Repalcement for Lost License";
               
            }
            
        }

        private void controleLocalDrivingLicenseWithFilter1_OnDrivingLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;
            lblOldLicenseID.Text = SelectedLicenseID.ToString();
            llShowLicensesHistory.Enabled = (SelectedLicenseID != -1);

            if(SelectedLicenseID ==-1)
            {
                return;
            }
            //check The License  is not Active.
            if (!_HandlingIsActiveLicense())
            {
                btnIssueReplacementLicense.Enabled = false;
                return;
            }

            //check The License  is not Detained.
            if (!_HandilingIsDetainLicense())
            {
                btnIssueReplacementLicense.Enabled = false;
                return;
            }

            btnIssueReplacementLicense.Enabled = true;
        }

        private void btnIssueReplacementLicense_Click(object sender, EventArgs e)
        {
            //check The object License is null.
            if (!_HandlingSelectedLicenseInfo())
            {
                btnIssueReplacementLicense.Enabled = false;
                return;
            }

            //check The License  is not Active.
            if (!_HandlingIsActiveLicense())
            { 
                btnIssueReplacementLicense.Enabled = false;
                return; 
            }

            //check The License  is not Detained.
            if (!_HandilingIsDetainLicense())
            {
                btnIssueReplacementLicense.Enabled = false;
                return;
            }

            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            int CreatedByUserID = LoggedInUser.CurrentUser.UserID;
            clsLicense.enIssueReason IssueReason;
            if (rbDamagedLicense.Checked)
              IssueReason  = clsLicense.enIssueReason.DamagedReplacement;
            else
                IssueReason = clsLicense.enIssueReason.LostReplacement;

            clsLicense ReplacementLicense = controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.Replace(IssueReason, CreatedByUserID);
            if(ReplacementLicense == null)
            {
                MessageBox.Show($"{controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.MessageError}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblApplicationID.Text = ReplacementLicense.ApplicationID.ToString();
            _ReplacementLicenseID = ReplacementLicense.LicenseID;
            lblReplacementLicenseID.Text = _ReplacementLicenseID.ToString();
            MessageBox.Show("Replacement License Issued Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            controleLocalDrivingLicenseWithFilter1.Enabled = false; 
            btnIssueReplacementLicense.Enabled = false;
            llShowLicensesInfo.Enabled = true;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            FrmShowLicenseDetails frmShowLicenseDetails = new FrmShowLicenseDetails(_ReplacementLicenseID);
            frmShowLicenseDetails.ShowDialog();
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmShowPersonLIcenseHistory frmShowPersonLIcenseHistory = new FrmShowPersonLIcenseHistory();
            frmShowPersonLIcenseHistory.ShowDialog();
            
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense).Fees.ToString();
            ctrCustormerHeaderForm1.TitleText = "Repalcement for Damaged License";
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReplaceLostDrivingLicense).Fees.ToString();
            ctrCustormerHeaderForm1.TitleText = "Repalcement for Lost License";
        }

        private void FrmReplacementForDamagedOrLostLicense_Activated(object sender, EventArgs e)
        {
            controleLocalDrivingLicenseWithFilter1.TxtFilterFocus();
        }
    }
}
