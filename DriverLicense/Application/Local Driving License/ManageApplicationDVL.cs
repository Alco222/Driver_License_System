using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using DriverLicenseBusinessLayer;
using DVLD.Classes;

namespace DriverLicense
{
    public partial class ManageApplicationDVL : Form
    {
        DataTable _dtAllLocalDrivingLicenseApplications;

        private int _currentPage = 1;
        private const int PAGE_SIZE = 60;

        private void LoadCurrentPage()
        {
            clsUtil.LoadDataPage(_dtAllLocalDrivingLicenseApplications, dgvAllAplicationLocal, _currentPage, PAGE_SIZE,
               lblPageInfo, btnPrevious, btnNext);
        }

        public ManageApplicationDVL()
        {
            InitializeComponent();

        }

        private void ManageApplicationDVL_Load(object sender, EventArgs e)
        {
            _dtAllLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            LoadCurrentPage();

            lblRecords.Text = _dtAllLocalDrivingLicenseApplications.Rows.Count.ToString();

            if (dgvAllAplicationLocal.Rows.Count > 0)
            {
                dgvAllAplicationLocal.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            

                dgvAllAplicationLocal.Columns[0].HeaderText = "L.D.L.AppID";
                dgvAllAplicationLocal.Columns[0].Width = 100;
                dgvAllAplicationLocal.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllAplicationLocal.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllAplicationLocal.Columns[1].HeaderText = "Driving Class";
                dgvAllAplicationLocal.Columns[1].Width = 240;
                dgvAllAplicationLocal.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
               
                dgvAllAplicationLocal.Columns[2].HeaderText = "National No";
                dgvAllAplicationLocal.Columns[2].Width = 130;
                dgvAllAplicationLocal.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllAplicationLocal.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllAplicationLocal.Columns[3].HeaderText = "Full Name";
                dgvAllAplicationLocal.Columns[3].Width = 250;
                dgvAllAplicationLocal.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllAplicationLocal.Columns[4].HeaderText = "Application Date";
                dgvAllAplicationLocal.Columns[4].Width = 145;
                dgvAllAplicationLocal.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllAplicationLocal.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllAplicationLocal.Columns[5].HeaderText = "Passed Tests";
                dgvAllAplicationLocal.Columns[5].Width = 120;
                dgvAllAplicationLocal.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllAplicationLocal.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllAplicationLocal.Columns[6].HeaderText = "Status";
                dgvAllAplicationLocal.Columns[6].Width = 90;
                dgvAllAplicationLocal.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllAplicationLocal.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            CbFilter.SelectedIndex = 0;
        }

        private void BtnClose2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmAddEditApplicationDVL frmAddEditApplicationDVL = new FrmAddEditApplicationDVL();
            frmAddEditApplicationDVL.ShowDialog();
            ManageApplicationDVL_Load(null, null);
        }

        private void CbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbFilter.SelectedItem.ToString() == "None")
            {
                txtFilter.Visible = false;
                panel1.Visible = false;
            }
            else
            {
                txtFilter.Visible = true;
                panel1.Visible = true;
                txtFilter.Focus();
            }
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CbFilter.SelectedItem.ToString() == "L.D.L.AppID")
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                    errorProvider1.SetError(txtFilter, "this TextBox is accepted Numiric or Number.");
                    txtFilter.Focus();
                }
                else
                {
                    errorProvider1.SetError(txtFilter, "");
                }
            }
            else
            {
                e.Handled = false;

            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {

            string FilterColumn = "";
            string TextValue = txtFilter.Text.Trim();
            //Map Selected Filter to real Column name 
            switch (CbFilter.Text)
            {

                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

               
                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (TextValue == "" || FilterColumn == "None")
            {
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";


                LoadCurrentPage();
                btnPrevious.Visible = true;
                btnNext.Visible = true;
                lblRecords.Text = _dtAllLocalDrivingLicenseApplications.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "LocalDrivingLicenseApplicationID")
                //in this case we deal with integer not string.
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format($"[{FilterColumn}] = {TextValue}");
            else
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format($"[{FilterColumn}] LIKE '{TextValue}%'");

            dgvAllAplicationLocal.DataSource = _dtAllLocalDrivingLicenseApplications.DefaultView;

            btnPrevious.Visible = false;
            btnNext.Visible = false;
            lblPageInfo.Text = "Filtered";
            lblRecords.Text = dgvAllAplicationLocal.Rows.Count.ToString();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void canceledApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvAllAplicationLocal.CurrentRow.Cells[0].Value;
            short StatusApp = Convert.ToInt16(canceledApplicationToolStripMenuItem.Tag.ToString());     
            DateTime LastStatueAppDate = DateTime.Now;


            if (clsApplication.HasPassedAllTest(LocalDrivingLicenseApplicationID))
            {
                MessageBox.Show("Does not canceled this Application because it is All Test Passed his.", "Not Canceled", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure do want to cancel this application?", "Canceled", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            if(LocalDrivingLicenseApplication != null)
            { 

                if(LocalDrivingLicenseApplication.Cancel())
                {
                    MessageBox.Show("Application Canceled Successfully", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ManageApplicationDVL_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not cancel applicatoin, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void _ScheduleTest(clsTestTypes.enTestType TestType)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvAllAplicationLocal.CurrentRow.Cells[0].Value;
            ManageTestApplication frm = new ManageTestApplication(LocalDrivingLicenseApplicationID, TestType);
            frm.ShowDialog();
            //refresh
            ManageApplicationDVL_Load(null, null);
        }

        private void sechduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestTypes.enTestType.VisionTest);
            ManageApplicationDVL_Load(null, null);
        }

        private void sechduleWritingTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestTypes.enTestType.WrittenTest);

        }

        private void sehduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestTypes.enTestType.StreetTest);

        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvAllAplicationLocal.CurrentRow.Cells[0].Value;
            FrmIssueDriverLicenseFirstTime frmIssueDriverLicenseFirstTime = new FrmIssueDriverLicenseFirstTime(LocalDrivingLicenseApplicationID);
            frmIssueDriverLicenseFirstTime.ShowDialog();
            //refresh the form again.
            ManageApplicationDVL_Load(null, null);
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvAllAplicationLocal.CurrentRow.Cells[0].Value;

            FrmAddEditApplicationDVL frm = new FrmAddEditApplicationDVL(LocalDrivingLicenseApplicationID);
            frm.ShowDialog();
            ManageApplicationDVL_Load(null, null);
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvAllAplicationLocal.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    ManageApplicationDVL_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not delete applicatoin, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvAllAplicationLocal.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                    clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID
                                                    (LocalDrivingLicenseApplicationID);

            int TotalPassedTests = (int)dgvAllAplicationLocal.CurrentRow.Cells[5].Value;

            bool LicenseExists = LocalDrivingLicenseApplication.IsLicenseIssued();

            //Enabled only if person passed all tests and Does not have license. 
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (TotalPassedTests == 3) && !LicenseExists;

           showLicenseToolStripMenuItem.Enabled = LicenseExists;
            editApplicationToolStripMenuItem.Enabled = !LicenseExists && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);
            sechduleTestsToolStripMenu.Enabled = !LicenseExists;

            //Enable/Disable Cancel Menue Item
            //We only canel the applications with status=new.
            canceledApplicationToolStripMenuItem.Enabled = (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);

            //Enable/Disable Delete Menue Item
            //We only allow delete incase the application status is new not complete or Cancelled.
            deleteApplicationToolStripMenuItem.Enabled =
                (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);



            //Enable Disable Schedule menue and it's sub menue
            bool PassedVisionTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestTypes.enTestType.VisionTest); ;
            bool PassedWrittenTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestTypes.enTestType.WrittenTest);
            bool PassedStreetTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestTypes.enTestType.StreetTest);

            sechduleTestsToolStripMenu.Enabled = (!PassedVisionTest || !PassedWrittenTest || !PassedStreetTest) && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);

            if (sechduleTestsToolStripMenu.Enabled)
            {
                //To Allow Schdule vision test, Person must not passed the same test before.
                sechduleVisionTestToolStripMenuItem.Enabled = !PassedVisionTest;

                //To Allow Schdule written test, Person must pass the vision test and must not passed the same test before.
                sechduleWritingTestToolStripMenuItem.Enabled = PassedVisionTest && !PassedWrittenTest;

                //To Allow Schdule steet test, Person must pass the vision * written tests, and must not passed the same test before.
                sehduleStreetTestToolStripMenuItem.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;
            }
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLocalDrivingLicenseApplicationInfo frmLocalDrivingLicenseApplicationInfo = new FrmLocalDrivingLicenseApplicationInfo((int)dgvAllAplicationLocal.CurrentRow.Cells[0].Value);
            frmLocalDrivingLicenseApplicationInfo.ShowDialog();
            ManageApplicationDVL_Load(null, null);
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvAllAplicationLocal.CurrentRow.Cells[0].Value;
            int LicenseID = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID).GetActiveLicenseID();
            if (LicenseID == -1)
            {
                MessageBox.Show("This person does not have a license.", "No License", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                FrmShowLicenseDetails frmShowLicenseDetails = new FrmShowLicenseDetails(LicenseID);
                frmShowLicenseDetails.ShowDialog();
            }

            ManageApplicationDVL_Load(null, null);
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvAllAplicationLocal.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);
            FrmShowPersonLIcenseHistory frmShowPersonLIcenseHistory = new FrmShowPersonLIcenseHistory(LocalDrivingLicenseApplication.ApplicantPersonID);
            frmShowPersonLIcenseHistory.ShowDialog();

            ManageApplicationDVL_Load(null, null);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            _currentPage--;
            LoadCurrentPage();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _currentPage++;
            LoadCurrentPage();
        }
    }
}

