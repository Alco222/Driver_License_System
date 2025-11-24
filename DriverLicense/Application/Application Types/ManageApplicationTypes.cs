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
    public partial class ManageApplicationTypes : Form
    {

        private void _RefreshListApplication()
        {
            dgvAllApplication.Font = new Font("Tahoma", 11);
            dgvAllApplication.DataSource = clsApplicationTypes.GetAllApplicationTypes();
            lblRecords.Text = dgvAllApplication.Rows.Count.ToString();

            if(dgvAllApplication.Rows.Count > 0)
            {
                dgvAllApplication.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);

                dgvAllApplication.Columns[0].HeaderText = "ID";
                dgvAllApplication.Columns[0].Width = 80;
                dgvAllApplication.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllApplication.Columns[1].HeaderText = "Title";
                dgvAllApplication.Columns[1].Width = 350;
                dgvAllApplication.Columns[2].HeaderText = "Fees";
                dgvAllApplication.Columns[2].Width = 120;
                dgvAllApplication.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        public ManageApplicationTypes()
        {
            InitializeComponent();
        }

        private void FrmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            _RefreshListApplication();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUpdateApplicationTypes updateApplicationTypes = new FrmUpdateApplicationTypes((int)dgvAllApplication.CurrentRow.Cells[0].Value);
            updateApplicationTypes.ShowDialog();
            _RefreshListApplication();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
