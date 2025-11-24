using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DriverLicense.Application.Release_Detain_LIcense;
using DriverLicenseBusinessLayer;
using DVLD.Classes;

namespace DriverLicense.Application.Renew_Local_Drinving_LIcense
{
    public partial class ManageDetainLicenses : Form
    {
        DataTable _dtDetainLicense;
        private int _currentPage = 1;
        private const int PAGE_SIZE = 60;

        private void LoadCurrentPage()
        {
            clsUtil.LoadDataPage(_dtDetainLicense, dgvAllDetainLicense, _currentPage, PAGE_SIZE,
               lblPageInfo, btnPrevious,btnNext);
        }

        public ManageDetainLicenses()
        {
            InitializeComponent();
        }

        private void ManageDetainLicenses_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0; // Set default filter to "All Detain Licenses"

            _dtDetainLicense = clsDetainedLicense.GetAllDetainLicense();
            LoadCurrentPage();
            lblRecords.Text = _dtDetainLicense.Rows.Count.ToString();

            if (dgvAllDetainLicense.Columns.Count > 0)
            {
                dgvAllDetainLicense.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                
      

                dgvAllDetainLicense.Columns[0].HeaderText = "D.ID";
                dgvAllDetainLicense.Columns[0].Width = 55;
                dgvAllDetainLicense.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllDetainLicense.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllDetainLicense.Columns[1].HeaderText = "L.ID";
                dgvAllDetainLicense.Columns[1].Width= 55;
                dgvAllDetainLicense.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllDetainLicense.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllDetainLicense.Columns[2].HeaderText = "Detain Date";
                dgvAllDetainLicense.Columns[2].Width = 160;
                dgvAllDetainLicense.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllDetainLicense.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllDetainLicense.Columns[3].HeaderText = "Is Release";
                dgvAllDetainLicense.Columns[3].Width = 90;
                dgvAllDetainLicense.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllDetainLicense.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllDetainLicense.Columns[4].HeaderText = "Fine Fess";
                dgvAllDetainLicense.Columns[4].Width = 120;
                dgvAllDetainLicense.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllDetainLicense.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllDetainLicense.Columns[5].HeaderText = "Release Date";
                dgvAllDetainLicense.Columns[5].Width = 160;
                dgvAllDetainLicense.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllDetainLicense.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllDetainLicense.Columns[6].HeaderText = "National.No";
                dgvAllDetainLicense.Columns[6].Width = 100;
                dgvAllDetainLicense.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllDetainLicense.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllDetainLicense.Columns[7].HeaderText = "Full Name";
                dgvAllDetainLicense.Columns[7].Width = 140;
                dgvAllDetainLicense.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllDetainLicense.Columns[8].HeaderText = "Release App.ID";
                dgvAllDetainLicense.Columns[8].Width = 100;
                dgvAllDetainLicense.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllDetainLicense.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            string TextValue = txtFilter.Text.Trim();
            //"None\r\nDetain ID\r\nNational No\r\nFull Name\r\nIs Released\r\nRelease Application ID";

            switch (cbFilter.Text)
            {
                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;
                case "National No":
                    FilterColumn = "NationalNo";
                  
                    break;
                case "Is Relased":
                    FilterColumn = "IsReleased";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Release Application ID":
                    FilterColumn = "ReleaseApplicationID";
                    break;
                default:
                    FilterColumn = "None"; // Default to UserName if no match
                    break;
            }

            if (txtFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtDetainLicense.DefaultView.RowFilter = "";

                LoadCurrentPage();
                btnPrevious.Visible = true;
                btnNext.Visible = true;
                lblRecords.Text = _dtDetainLicense.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "DetainID" || FilterColumn == "ReleaseApplicationID")
                _dtDetainLicense.DefaultView.RowFilter = string.Format($"[{FilterColumn}] = {TextValue}");

            else
                _dtDetainLicense.DefaultView.RowFilter = string.Format($"[{FilterColumn}] LIKE '{TextValue}%'");
            
            dgvAllDetainLicense.DataSource = _dtDetainLicense.DefaultView;

            btnPrevious.Visible = false;
            btnNext.Visible = false;
            lblPageInfo.Text = "Filtered";
            lblRecords.Text = dgvAllDetainLicense.Rows.Count.ToString();
           
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "Detain ID" || cbFilter.Text == "Release Application ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.Text == "Is Released")
            {
                txtFilter.Visible = false;
                cbIDetain.Visible = true;
                panel1.Visible = false;
                cbIDetain.Focus();
                cbIDetain.SelectedIndex = 0;
            }

            else

            {

                cbIDetain.Visible = false;

                if (cbFilter.SelectedItem.ToString() == "None")
                {
                    txtFilter.Visible = false;
                    panel1.Visible = false;
                }
                else
                {
                    cbIDetain.Visible = false;
                    txtFilter.Visible = true;
                    panel1.Visible = true;
                    txtFilter.Focus();
                }
            }
        }

        private void cbDetain_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsReleased";
            string FilterValue = cbIDetain.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }


            if (FilterValue == "All")
                _dtDetainLicense.DefaultView.RowFilter = "";
            else //in this case we deal with numbers not string.
                _dtDetainLicense.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            dgvAllDetainLicense.DataSource = _dtDetainLicense.DefaultView;

            btnPrevious.Visible = false;
            btnNext.Visible = false;
            lblPageInfo.Text = "Filtered";
            lblRecords.Text = dgvAllDetainLicense.Rows.Count.ToString();
        }

        private void btnRelaseDetainLicense_Click(object sender, EventArgs e)
        {
            FrmReleaseDetainLicenseApp frmReleaseDetainLicenseApp = new FrmReleaseDetainLicenseApp();
            frmReleaseDetainLicenseApp.ShowDialog();
            ManageDetainLicenses_Load(null, null);
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            FrmDetainLIcense frmDetainLIcense = new FrmDetainLIcense();
            frmDetainLIcense.ShowDialog();
            ManageDetainLicenses_Load(null, null);
        }

        private void releaseDetainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvAllDetainLicense.CurrentRow.Cells["LicenseID"].Value;
            FrmReleaseDetainLicenseApp frmReleaseDetainLicenseApp = new FrmReleaseDetainLicenseApp(LicenseID);
            frmReleaseDetainLicenseApp.ShowDialog();
            ManageDetainLicenses_Load(null, null);
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NationalNo = (string)dgvAllDetainLicense.CurrentRow.Cells["NationalNo"].Value;
            int PersonID = clsPerson.Find(NationalNo).PersonID;
            FrmPersonInformation frmPersonInformation = new FrmPersonInformation(PersonID);
            frmPersonInformation.ShowDialog();
            ManageDetainLicenses_Load(null, null);
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvAllDetainLicense.CurrentRow.Cells["LicenseID"].Value;
            FrmShowLicenseDetails frmShowLicenseDetails = new FrmShowLicenseDetails(LicenseID);
            frmShowLicenseDetails.ShowDialog();
            ManageDetainLicenses_Load(null, null);

        }

        private void showPersonHiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NationalNo = (string)dgvAllDetainLicense.CurrentRow.Cells["NationalNo"].Value;
            int PersonID = clsPerson.Find(NationalNo).PersonID;
            FrmShowPersonLIcenseHistory frmShowPersonLIcenseHistory = new FrmShowPersonLIcenseHistory(PersonID);
            frmShowPersonLIcenseHistory.ShowDialog();
            ManageDetainLicenses_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmsDetainMenu_Opening(object sender, CancelEventArgs e)
        {
            bool IsReleased = (bool)dgvAllDetainLicense.CurrentRow.Cells[3].Value;
            if (IsReleased)
            {
                releaseDetainLicenseToolStripMenuItem.Enabled = false;
            }
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
