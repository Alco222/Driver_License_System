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
    public partial class MangeTestTypes : Form
    {
        private DataTable _dtAllTestTypes;
        public MangeTestTypes()
        {
            InitializeComponent();
        }

        private void _RefreshListTest()
        {
            _dtAllTestTypes = clsTestTypes.GetAllTestTypes();
            dgvTestTypes.DataSource = _dtAllTestTypes;
            lblRecords.Text = dgvTestTypes.Rows.Count.ToString();

            if(dgvTestTypes.Rows.Count > 0)
            {
                dgvTestTypes.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);

                dgvTestTypes.Columns[0].HeaderText = "ID";
                dgvTestTypes.Columns[0].Width = 50;
                dgvTestTypes.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvTestTypes.Columns[1].HeaderText = "Title";
                dgvTestTypes.Columns[1].Width = 120;
                dgvTestTypes.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvTestTypes.Columns[2].HeaderText = "Description";
                dgvTestTypes.Columns[2].Width = 330;
                dgvTestTypes.Columns[3].HeaderText = "Fees";
                dgvTestTypes.Columns[3].Width = 80;
                dgvTestTypes.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }


        }

        private void MangeTest_Load(object sender, EventArgs e)
        {
            _RefreshListTest();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsTestTypes.enTestType TestTypesID = (clsTestTypes.enTestType)dgvTestTypes.CurrentRow.Cells[0].Value;
            FrmEditTestTypes UpdateTestTypes = new FrmEditTestTypes(TestTypesID);
            UpdateTestTypes.ShowDialog();
            _RefreshListTest();
        }
    }


}
