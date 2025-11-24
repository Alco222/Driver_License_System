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
using DVLD.Classes;

namespace DriverLicense.Application.Release_Detain_LIcense
{
    public partial class FrmDetainLIcense : Form
    {
        private int _DetainID = -1;
        private int _SelectedLicenseID = -1;

        public FrmDetainLIcense()
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
                return false ;
            }
            return true;
        }

        private void FrmDetainLIcense_Load(object sender, EventArgs e)
        {

            lblDetainDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCraatedBy.Text = LoggedInUser.CurrentUser.UserName;

        }

        private void controleLocalDrivingLicenseWithFilter1_OnDrivingLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;
            lblLicenseID.Text = _SelectedLicenseID.ToString();
            llShowLicensesHistory.Enabled = (_SelectedLicenseID != -1);
            if (_SelectedLicenseID == -1)
            {
                return;
            }

            if(HandlIsDetenedLicense())
                return;

            if (!HandlIsActiveLicense())
                return;

            btnDetain.Enabled = true;
            txtFineFees.Focus();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (HandlIsDetenedLicense())
                return;

            if (!HandlIsActiveLicense())
                return;

            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int CreatedByUserID = LoggedInUser.CurrentUser.UserID;
            float FineFees = Convert.ToSingle(txtFineFees.Text);

            _DetainID = controleLocalDrivingLicenseWithFilter1.selectLicenseInfo.Detain(FineFees, CreatedByUserID);

            if (_DetainID == -1)
            {
                MessageBox.Show("Faild to Issue Replacement License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblDetainID.Text = _DetainID.ToString();
            MessageBox.Show($"Detain License Issued Successfully with ID = {_DetainID} ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnDetain.Enabled = false;
            controleLocalDrivingLicenseWithFilter1.Enabled = false;
            llShowLicensesInfo.Enabled = true;
            txtFineFees.Enabled = false;

        }

        private void FrmDetainLIcense_Activated(object sender, EventArgs e)
        {
            controleLocalDrivingLicenseWithFilter1.TxtFilterFocus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmShowLicenseDetails frmShowLicenseDetails = new FrmShowLicenseDetails(controleLocalDrivingLicenseWithFilter1.LicenseID);
            frmShowLicenseDetails.ShowDialog();
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmShowPersonLIcenseHistory frmShowPersonLIcenseHistory = new FrmShowPersonLIcenseHistory();
            frmShowPersonLIcenseHistory.ShowDialog();
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (!clsValidatoin.IsNumber(txtFineFees.Text))
            {
                errorProvider1.SetError(txtFineFees, "Please enter a valid number for Fine Fees.");
                txtFineFees.Focus();
            }
            else
            {
                errorProvider1.SetError(txtFineFees, "");
            }
        }
    }
}
