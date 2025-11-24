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
    public partial class ControlDriverLicense : UserControl
    {

        private int _DriverID ;
        private clsDriver _Driver;
        private DataTable _dtDriverLocalLicenseHistory;
        private DataTable _dtDriverInternationLicenseHistory;
        public ControlDriverLicense()
        {
            InitializeComponent();
            
        }

        private void _DesignForHeaderdgvInternationalLicenseHistory()
        {
            // Design for Header Style to dgvInternationalLicenseHistory.
            //-------------------------------------------------------------
            dgvInternationalLicenseHistory.EnableHeadersVisualStyles = false;

            dgvInternationalLicenseHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.Gainsboro;
            dgvInternationalLicenseHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvInternationalLicenseHistory.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dgvInternationalLicenseHistory.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvInternationalLicenseHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            //---------------------------------------------------------------
        }

        private void _DesignForHeaderdgvLocalLicenseHistory()
        {
            // Design for Header Style dgvLocalLicenseHistory.
            //-------------------------------------------------------------
            dgvLocalLicenseHistory.EnableHeadersVisualStyles = false;
            dgvLocalLicenseHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.Gainsboro;
            dgvLocalLicenseHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvLocalLicenseHistory.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            dgvLocalLicenseHistory.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLocalLicenseHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            //-------------------------------------------------------------
        }

        private void _LoadLocalLicense()
        {
            _dtDriverLocalLicenseHistory = clsDriver.GetLicenses(_DriverID);

            dgvLocalLicenseHistory.DataSource = _dtDriverLocalLicenseHistory;
            lblRecordLocal.Text = _dtDriverLocalLicenseHistory.Rows.Count.ToString();

            if (dgvLocalLicenseHistory.Rows.Count > 0)
            {
                // Design for Header Style dgvLocalLicenseHistory.
                _DesignForHeaderdgvLocalLicenseHistory();

                dgvLocalLicenseHistory.Columns[0].HeaderText = "License.ID";
                dgvLocalLicenseHistory.Columns[0].Width = 100;
                dgvLocalLicenseHistory.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvLocalLicenseHistory.Columns[1].HeaderText = "App.ID";
                dgvLocalLicenseHistory.Columns[1].Width = 70;
                dgvLocalLicenseHistory.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvLocalLicenseHistory.Columns[2].HeaderText = "Class Name";
                dgvLocalLicenseHistory.Columns[2].Width = 250;

                dgvLocalLicenseHistory.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicenseHistory.Columns[3].Width = 150;

                dgvLocalLicenseHistory.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicenseHistory.Columns[4].Width = 150;

                dgvLocalLicenseHistory.Columns[5].HeaderText = "Is Active";
                dgvLocalLicenseHistory.Columns[5].Width = 80;
                dgvLocalLicenseHistory.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            else
            {
                lblNoDataLocalLicense.Visible = true;
            }
        }

        private void _LoadInternationalLicense()
        {
            _dtDriverInternationLicenseHistory = clsDriver.GetInternationalLicenses(_DriverID);
            dgvInternationalLicenseHistory.DataSource = _dtDriverInternationLicenseHistory;
            lblRecordInter.Text = _dtDriverInternationLicenseHistory.Rows.Count.ToString();
            if (dgvInternationalLicenseHistory.Rows.Count > 0)
            {
                // Design for Header Style to dgvInternationalLicenseHistory.
                _DesignForHeaderdgvInternationalLicenseHistory();
               
                dgvInternationalLicenseHistory.Columns[0].HeaderText = "Int.License.ID";
                dgvInternationalLicenseHistory.Columns[0].Width = 120;
                dgvInternationalLicenseHistory.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                
                dgvInternationalLicenseHistory.Columns[1].HeaderText = "App.ID";
                dgvInternationalLicenseHistory.Columns[1].Width = 80;
                dgvInternationalLicenseHistory.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvInternationalLicenseHistory.Columns[2].HeaderText = "License.ID";
                dgvInternationalLicenseHistory.Columns[2].Width = 100;
                dgvInternationalLicenseHistory.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                
                dgvInternationalLicenseHistory.Columns[3].HeaderText = "Issued Date";
                dgvInternationalLicenseHistory.Columns[3].Width = 160;

                dgvInternationalLicenseHistory.Columns[4].HeaderText = "Expiration Date";
                dgvInternationalLicenseHistory.Columns[4].Width = 160;

                dgvInternationalLicenseHistory.Columns[5].HeaderText = "Is Active";
                dgvInternationalLicenseHistory.Columns[5].Width = 90;
                dgvInternationalLicenseHistory.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            else
            {
                lblNoDataInternationalLicense.Visible = true;
            }
           
        }

        public void LoadInfo(int DriverID)
        {
            _DriverID = DriverID;
            _Driver = clsDriver.FindByDriverID(_DriverID);

            if(_Driver == null)
            {
                MessageBox.Show($"There is no Driver with ID = {_DriverID}","Not Allowed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            _LoadLocalLicense();
            _LoadInternationalLicense();
        }

        public void LoadInfoByPersonID(int PersonID)
        {
            _Driver = clsDriver.FindByPersonID(PersonID);

            if (_Driver == null)
            {
                MessageBox.Show($"There is no Driver Linked with PersonID = {PersonID}", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _DriverID = _Driver.DriverID;

            _LoadLocalLicense();
            _LoadInternationalLicense();
        }

        public void Clear()
        {
            _dtDriverLocalLicenseHistory.Clear();
            _dtDriverInternationLicenseHistory.Clear();
        }

        private void showLocalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int licenseID = (int)dgvLocalLicenseHistory.CurrentRow.Cells[0].Value;
            FrmShowLicenseDetails frmShow = new FrmShowLicenseDetails(licenseID);
            frmShow.ShowDialog();
        }

        private void showInternationalInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternationallicenseID = (int)dgvInternationalLicenseHistory.CurrentRow.Cells[0].Value;
            FrmShowDriverInternationalLicenseInfo frmShow = new FrmShowDriverInternationalLicenseInfo(InternationallicenseID);
            frmShow.ShowDialog();
        }

        private void LocalLicenseMune_Opening(object sender, CancelEventArgs e)
        {
            if (dgvLocalLicenseHistory.Rows.Count <= 0)
            {
                showLocalLicenseToolStripMenuItem.Enabled = false;
            }
         
        }

        private void InternationalMune_Opening(object sender, CancelEventArgs e)
        {
            if(dgvInternationalLicenseHistory.Rows.Count <= 0)
            {
                showInternationalInfoToolStripMenuItem.Enabled = false;
            }
        }
    }
}
