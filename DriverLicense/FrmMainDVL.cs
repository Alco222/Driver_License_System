using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using DriverLicense.Application.Release_Detain_LIcense;
using DriverLicense.Application.Renew_Local_Drinving_LIcense;
using DriverLicense.Application.RenewLocalDrinvingLIcense;
using DriverLicense.Application.Replacement_of_Demaged;
using DriverLicenseBusinessLayer;

namespace DriverLicense
{
    public partial class FrmMainDVL : Form
    {
        FrmLogin  _frmLogin;
        public FrmMainDVL(FrmLogin frmlogin)
        {
            InitializeComponent();
            _frmLogin = frmlogin;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //Desactivate the license if it is expired Finshed.
            //clsLicense.DesactivateLicenseFinshedDate();
            
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MangerPerson Mgper = new MangerPerson();
            //Mgper.MdiParent = this;

            Mgper.StartPosition = FormStartPosition.Manual;

     
            int x = (this.ClientSize.Width - Mgper.Width) / 2;
            int y = (this.ClientSize.Height - Mgper.Height) / 2;

            Mgper.Location = new Point(x, y);

            Mgper.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageUser MgperUser = new ManageUser();
            //MgperUser.MdiParent = this;

            MgperUser.StartPosition = FormStartPosition.Manual;


            int x = (this.ClientSize.Width - MgperUser.Width) / 2;
            int y = (this.ClientSize.Height - MgperUser.Height) / 2;

            MgperUser.Location = new Point(x, y);

            MgperUser.ShowDialog();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            LoggedInUser.CurrentUser = null;
            _frmLogin.Show();
            this.Close();


        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            FrmChangePassword frmChange = new FrmChangePassword(LoggedInUser.CurrentUser.UserID);
            //frmChange.MdiParent = this;
            frmChange.ShowDialog();
        }

        private void currentInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowUserDetails frmShow = new frmShowUserDetails(LoggedInUser.CurrentUser.UserID);
            //frmShow.MdiParent = this;
            frmShow.ShowDialog();
        }

        private void applicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageApplicationTypes manageApplicationTypes = new ManageApplicationTypes();
            //manageApplicationTypes.MdiParent = this;
            manageApplicationTypes.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MangeTestTypes manageTest = new MangeTestTypes();
            //manageTest.MdiParent = this;
            manageTest.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            FrmAddEditApplicationDVL frmAddEditApplicationDVL = new FrmAddEditApplicationDVL();
            //frmAddEditApplicationDVL.MdiParent = this;
            frmAddEditApplicationDVL.ShowDialog();
        }

        private void manageLocalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ApplicationID = int.TryParse(manageLocalDrivingLicenseApplicationsToolStripMenuItem.Tag.ToString(), out int result) ? result : 0;
            ManageApplicationDVL manageApplicationDVL = new ManageApplicationDVL();
            //manageApplicationDVL.MdiParent = this;
            manageApplicationDVL.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDriverList frmDriverList = new FrmDriverList();
            frmDriverList.ShowDialog();
            
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
           FrmNewInternationalLicenseApplication frmNewInternationalLicenseApplication = new FrmNewInternationalLicenseApplication();
            frmNewInternationalLicenseApplication.ShowDialog();
        }

        private void internationalLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListInternationalLicenseApplication frmListInternationalLicenseApplication = new FrmListInternationalLicenseApplication();
            frmListInternationalLicenseApplication.ShowDialog();
        }

        private void renewDrivingLIcenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRenewLocalDrivingLIcense frmRenewLocalDrivingLIcense = new FrmRenewLocalDrivingLIcense();
            frmRenewLocalDrivingLIcense.ShowDialog();
            
        }

        private void replacementForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReplacementForDamagedOrLostLicense frmReplacementForDamagedOrLostLicense = new FrmReplacementForDamagedOrLostLicense();
            frmReplacementForDamagedOrLostLicense.ShowDialog();
        }

        private void reeaseDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReleaseDetainLicenseApp frmReleaseDetainLicenseApp = new FrmReleaseDetainLicenseApp();
            frmReleaseDetainLicenseApp.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmDetainLIcense frmDetainLIcense = new FrmDetainLIcense();
            frmDetainLIcense.ShowDialog();
        }

        private void managementDetainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageDetainLicenses manageDetainLicenses = new ManageDetainLicenses();
          
            manageDetainLicenses.ShowDialog();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReleaseDetainLicenseApp frmReleaseDetainLicenseApp = new FrmReleaseDetainLicenseApp();
            frmReleaseDetainLicenseApp.ShowDialog();
        }

        private void panelCenter_Resize(object sender, EventArgs e)
        {
            pictureBox2.Left = (panelCenter.Width - pictureBox2.Width) / 2;
            pictureBox2.Top = (panelCenter.Height - pictureBox2.Height) / 2;
        }

        private void FrmMainDVL_FormClosing(object sender, FormClosingEventArgs e)
        {
           if(LoggedInUser.CurrentUser != null)
               System.Windows.Forms.Application.Exit();
        }
    }
}
