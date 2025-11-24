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
using DVLD.Classes;

namespace DriverLicense
{
    public partial class FrmListInternationalLicenseApplication : Form
    {

        DataTable _dtAllInternational;
        private int _currentPage = 1;
        private const int PAGE_SIZE = 60;

        private void LoadCurrentPage()
        {
            clsUtil.LoadDataPage(_dtAllInternational, dgvInternationalLIcense, _currentPage, PAGE_SIZE,
               lblPageInfo, btnPrevious, btnNext);
        }

        public FrmListInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void FrmListInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            _dtAllInternational = clsInternationalLicense.GetAllInternationalLicense();
            LoadCurrentPage();
            CbFilter.SelectedIndex = 0; // Default to "All" filter
            lblRecords.Text = _dtAllInternational.Rows.Count.ToString();
            if(dgvInternationalLIcense.Rows.Count >0)
            {
                dgvInternationalLIcense.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);

                dgvInternationalLIcense.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLIcense.Columns[0].Width = 80;
                dgvInternationalLIcense.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvInternationalLIcense.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvInternationalLIcense.Columns[1].HeaderText = "Application ID";
                dgvInternationalLIcense.Columns[1].Width = 80;
                dgvInternationalLIcense.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvInternationalLIcense.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvInternationalLIcense.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLIcense.Columns[2].Width = 80;
                dgvInternationalLIcense.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvInternationalLIcense.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvInternationalLIcense.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLIcense.Columns[3].Width = 80;
                dgvInternationalLIcense.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvInternationalLIcense.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLIcense.Columns[4].Width = 80;
                dgvInternationalLIcense.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvInternationalLIcense.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvInternationalLIcense.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLIcense.Columns[5].Width = 80;
                dgvInternationalLIcense.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvInternationalLIcense.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvInternationalLIcense.Columns[6].HeaderText = "Is Active";
                dgvInternationalLIcense.Columns[6].Width = 80;
                dgvInternationalLIcense.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvInternationalLIcense.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmNewInternationalLicenseApplication frmNewInternationalLicenseApplication = new FrmNewInternationalLicenseApplication();
            frmNewInternationalLicenseApplication.ShowDialog();
            FrmListInternationalLicenseApplication_Load(null, null);
        }

        private void CbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbFilter.Text == "Is Active")
            {

                txtFilter.Visible = false;
                cbIsActive.Visible = true;
                panel1.Visible = false;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }
            else

            {
                cbIsActive.Visible = false;

                if (CbFilter.SelectedItem.ToString() == "None")
                {
                    txtFilter.Visible = false;
                    panel1.Visible = false;
                }
                else
                {
                    cbIsActive.Visible = false;
                    txtFilter.Visible = true;
                    panel1.Visible = true;
                    txtFilter.Focus();
                }

            }
        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            string TextValue = txtFilter.Text.Trim();

            switch (CbFilter.Text)
            {
                case "Int.License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":
                    FilterColumn = "ApplicationID";
                    break;
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;
                case "Local License ID":
                    FilterColumn = "IssuedUsedLocalLicenseID";
                    break;
                case "Is Active":
                    FilterColumn = "IsActive";
                    break;

                default:
                    FilterColumn = "None"; // Default to InternationalLicenseID if no match
                    break;
            }

            if (TextValue == "" || FilterColumn == "None")
            {
                _dtAllInternational.DefaultView.RowFilter = ""; // Show all records
                LoadCurrentPage();
                btnPrevious.Visible = true;
                btnNext.Visible = true;
                lblRecords.Text = _dtAllInternational.Rows.Count.ToString();
                return;
            }
            if (FilterColumn == "InternationalLicenseID" || FilterColumn == "ApplicationID" || FilterColumn == "DriverID" || FilterColumn == "IssuedUsedLocalLicenseID")
                _dtAllInternational.DefaultView.RowFilter = string.Format($"[{FilterColumn}] = {TextValue}");
            else
                _dtAllInternational.DefaultView.RowFilter = string.Format($"[{FilterColumn}] LIKE '{TextValue}%'");

            dgvInternationalLIcense.DataSource = _dtAllInternational.DefaultView;

            btnPrevious.Visible = false;
            btnNext.Visible = false;
            lblPageInfo.Text = "Filtered";
            lblRecords.Text = dgvInternationalLIcense.Rows.Count.ToString();
        }
        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FiltertValue = cbIsActive.Text.Trim();

            switch (FiltertValue)
            {
                case "All":
                    break;

                case "Yes":
                    FiltertValue = "1";
                    break;

                case "No":
                    FiltertValue = "0";
                    break;
                default:
                    FiltertValue = ""; // No filter
                    break;
            }

            if (FiltertValue == "All")
                _dtAllInternational.DefaultView.RowFilter = ""; // Show all records

            else
                _dtAllInternational.DefaultView.RowFilter = string.Format($"[{FilterColumn}] = {FiltertValue}");

            dgvInternationalLIcense.DataSource = _dtAllInternational.DefaultView;

            btnPrevious.Visible = false;
            btnNext.Visible = false;
            lblPageInfo.Text = "Filtered";
            lblRecords.Text =dgvInternationalLIcense.Rows.Count.ToString();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = Convert.ToInt32(dgvInternationalLIcense.CurrentRow.Cells[2].Value);
            int PersonID = clsDriver.FindByDriverID(DriverID).PersonID;
            FrmPersonInformation frmPersonInformation = new FrmPersonInformation(PersonID);
            frmPersonInformation.ShowDialog();
            FrmListInternationalLicenseApplication_Load(null,null);
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = Convert.ToInt32(dgvInternationalLIcense.CurrentRow.Cells[0].Value);
            FrmShowDriverInternationalLicenseInfo frmShowDriverInternationalLicenseInfo = new FrmShowDriverInternationalLicenseInfo(InternationalLicenseID);
           frmShowDriverInternationalLicenseInfo.ShowDialog();
            FrmListInternationalLicenseApplication_Load(null, null);
        }

        private void showPersonHiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = Convert.ToInt32(dgvInternationalLIcense.CurrentRow.Cells[2].Value);
            int PersonID = clsDriver.FindByDriverID(DriverID).PersonID;
            FrmShowPersonLIcenseHistory frmShowPersonLIcenseHistory = new FrmShowPersonLIcenseHistory(PersonID);
            frmShowPersonLIcenseHistory.ShowDialog();
            FrmListInternationalLicenseApplication_Load(null,null);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
