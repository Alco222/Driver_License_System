using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DriverLicenseBusinessLayer;

namespace DriverLicense
{
    public partial class controleLocalDrivingLicenseWithFilter : UserControl
    {
        public event Action<int> OnDrivingLicenseSelected;
        protected virtual void LicenseSelected(int LicensID)
        {
            Action<int> handler = OnDrivingLicenseSelected;
            if (handler != null)
            {
                handler(LicensID);
            }
        }

        int _LicenseID = -1;
        public int LicenseID
        {
            get { return controlDrivingLicenseDetails1.LicenseID; }
        }

        public clsLicense selectLicenseInfo
        {
            get { return controlDrivingLicenseDetails1.SelectedLicenseInfo; }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }

        public controleLocalDrivingLicenseWithFilter()
        {
            InitializeComponent();
        }

        public void LoadInfo(int LicenseID)
        {
            txtFilter.Text = LicenseID.ToString();
            controlDrivingLicenseDetails1.LoadLicenseInfo(LicenseID);
            _LicenseID = controlDrivingLicenseDetails1.LicenseID;
            if (OnDrivingLicenseSelected != null && FilterEnabled)
                // Raise the event with a parameter
                OnDrivingLicenseSelected(_LicenseID);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            _LicenseID = int.Parse(txtFilter.Text);
            LoadInfo(_LicenseID);
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if (e.KeyChar == (char)13)
            {
                btnSearch.PerformClick();
            }
        }

        public void TxtFilterFocus()
        {
            txtFilter.Focus();
        }

        private void txtFilter_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilter.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilter, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtFilter, null);
            }
        }
    }
}
