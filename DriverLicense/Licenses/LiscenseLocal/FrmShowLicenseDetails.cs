using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriverLicense
{
    public partial class FrmShowLicenseDetails : Form
    {
        int _LicenseID = -1;
        public FrmShowLicenseDetails(int LisenseID)
        {
            InitializeComponent();
            _LicenseID = LisenseID;
        }

        private void FrmShowLicenseDetails_Load(object sender, EventArgs e)
        {
            controlDrivingLicenseDetails1.LoadLicenseInfo(_LicenseID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
