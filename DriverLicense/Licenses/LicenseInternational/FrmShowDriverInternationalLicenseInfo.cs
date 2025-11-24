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
    public partial class FrmShowDriverInternationalLicenseInfo : Form
    {
        int _InternationalLicenseID = -1;
        public FrmShowDriverInternationalLicenseInfo(int InternationalLicenseID)
        {
            InitializeComponent();
            _InternationalLicenseID = InternationalLicenseID;
        }

        private void FrmShowDriverInternationalLicenseInfo_Load(object sender, EventArgs e)
        {
            controleDrivingLicenseInternational1.LoadLicenseInternationalInfo(_InternationalLicenseID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
