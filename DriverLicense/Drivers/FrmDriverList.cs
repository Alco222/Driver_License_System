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
    public partial class FrmDriverList : Form
    {
        DataTable _dtAllDrivers = clsDriver.GetAllDrivers();
        private int _currentPage = 1;
        private const int PAGE_SIZE = 60;

        private void LoadCurrentPage()
        {
            clsUtil.LoadDataPage(_dtAllDrivers, dgvDrivers, _currentPage, PAGE_SIZE,
               lblPageInfo, btnPrevious, btnNext);
        }

        public FrmDriverList()
        {
            InitializeComponent();
        }

        private void FrmDriverList_Load(object sender, EventArgs e)
        {
            CbFilter.SelectedIndex = 0; // Set default filter to "All Drivers"
            LoadCurrentPage();
            lblRecords.Text = _dtAllDrivers.Rows.Count.ToString();
            if (dgvDrivers.Rows.Count > 0)
            {
                dgvDrivers.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);

                dgvDrivers.Columns[0].HeaderText = "Driver ID";
                dgvDrivers.Columns[0].Width = 70;
                dgvDrivers.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDrivers.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvDrivers.Columns[1].HeaderText = "Person ID";
                dgvDrivers.Columns[1].Width = 80;
                dgvDrivers.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDrivers.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvDrivers.Columns[2].HeaderText = "National No";
                dgvDrivers.Columns[2].Width = 120;
                dgvDrivers.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDrivers.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvDrivers.Columns[3].HeaderText = "Full Name";
                dgvDrivers.Columns[3].Width = 180;
                dgvDrivers.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvDrivers.Columns[4].HeaderText = "Date";
                dgvDrivers.Columns[4].Width = 150;
                dgvDrivers.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDrivers.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvDrivers.Columns[5].HeaderText = "Active Licenses";
                dgvDrivers.Columns[5].Width = 100;
                dgvDrivers.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDrivers.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (CbFilter.Text)
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No":
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
            if (txtFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllDrivers.DefaultView.RowFilter = "";
                
                LoadCurrentPage();
                btnPrevious.Visible = true;
                btnNext.Visible = true;
                lblRecords.Text = _dtAllDrivers.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "DriverID" || FilterColumn == "PersonID")
                //in this case we deal with numbers not string.
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());
            else
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilter.Text.Trim());

            dgvDrivers.DataSource = _dtAllDrivers.DefaultView;

            btnPrevious.Visible = false;
            btnNext.Visible = false;
            lblPageInfo.Text = "Filtered";
            lblRecords.Text = dgvDrivers.Rows.Count.ToString();

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
                txtFilter.Text = "";
                panel1.Visible = true;
                txtFilter.Focus();
            }
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CbFilter.SelectedItem.ToString() == "PersonID" && CbFilter.SelectedItem.ToString()=="DriverID")
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

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID =(int)dgvDrivers.CurrentRow.Cells[1].Value;
            FrmPersonInformation frmPersonInformation = new FrmPersonInformation(PersonID);
            frmPersonInformation.ShowDialog();
            FrmDriverList_Load(null, null);
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvDrivers.CurrentRow.Cells[1].Value;
            FrmShowPersonLIcenseHistory frmShowPersonLIcenseHistory = new FrmShowPersonLIcenseHistory(PersonID);
            frmShowPersonLIcenseHistory.ShowDialog();
            FrmDriverList_Load(null, null);

        }

        private void issueInternationalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
