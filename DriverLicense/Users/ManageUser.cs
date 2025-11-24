using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using DriverLicenseBusinessLayer;
using DVLD.Classes;

namespace DriverLicense
{
    // This form is used to manage users in the system.
    // It allows adding, editing, deleting, and filtering users.
    // The form interacts with the clsUsers class from the business layer.
    // It displays user information in a DataGridView and provides various user management functionalities.
    public partial class ManageUser : Form
    {
         DataTable _dtAllUser;

        private int _currentPage = 1;
        private const int PAGE_SIZE = 60;

        private void LoadCurrentPage()
        {
            clsUtil.LoadDataPage(_dtAllUser, dgvAllUser, _currentPage, PAGE_SIZE,
               lblPageInfo, btnPrevious,btnNext);
        }

        public ManageUser()
        {
            InitializeComponent();
        }

        private void ManageUser_Load(object sender, EventArgs e)
        {
            _dtAllUser = clsUsers.GetAllUser();
            LoadCurrentPage();
            cbFilter.SelectedIndex = 0;
            lblRecords.Text = _dtAllUser.Rows.Count.ToString();

            if(dgvAllUser.Rows.Count > 0)
            {
                dgvAllUser.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);

                dgvAllUser.Columns[0].HeaderText = "User ID";
                dgvAllUser.Columns[0].Width = 90;
                dgvAllUser.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllUser.Columns[1].HeaderText = "Person ID";
                dgvAllUser.Columns[1].Width = 95;
                dgvAllUser.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllUser.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllUser.Columns[2].HeaderText = "Full Name";
                dgvAllUser.Columns[2].Width = 250;
                dgvAllUser.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllUser.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllUser.Columns[3].HeaderText = "User Name";
                dgvAllUser.Columns[3].Width = 180;
                dgvAllUser.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllUser.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllUser.Columns[4].HeaderText = "Is Active";
                dgvAllUser.Columns[4].Width = 100;
                dgvAllUser.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllUser.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmAddEditUser frm = new FrmAddEditUser(-1);
            frm.ShowDialog();
            ManageUser_Load(null,null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAddEditUser frm = new FrmAddEditUser((int)dgvAllUser.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            ManageUser_Load(null,null);
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmChangePassword frm = new FrmChangePassword((int)dgvAllUser.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            ManageUser_Load(null,null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("are you sure want to deleted User [" + dgvAllUser.CurrentRow.Cells[0].Value + "]", "Confirm Deleted", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsUsers.DeleteUser((int)dgvAllUser.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("User Deleted Successfuly.", "Deleted Seccessed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ManageUser_Load(null, null);
                }
                else
                    MessageBox.Show("User was not deleted, because it has data linked to it.", "Deleted Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowUserDetails frmShow = new frmShowUserDetails((int)dgvAllUser.CurrentRow.Cells[0].Value);
            frmShow.ShowDialog();
            ManageUser_Load(null,null);
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAddEditUser frmAdd = new FrmAddEditUser(-1);
            frmAdd.ShowDialog();
            ManageUser_Load(null,null);
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            string TextValue = txtFilter.Text.Trim();

            // Determine the column to filter based on the selected filter type
            switch (cbFilter.Text)
           {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "UserName":
                    FilterColumn = "UserName";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Is Active":
                    FilterColumn = "IsActive";
                    break;
                default:
                    FilterColumn = "None"; // Default to UserName if no match
                    break;
            }

            if(txtFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllUser.DefaultView.RowFilter = "";
                lblRecords.Text = dgvAllUser.Rows.Count.ToString();
                LoadCurrentPage();
                btnPrevious.Visible = true;
                btnNext.Visible = true;
                lblRecords.Text = _dtAllUser.Rows.Count.ToString();
                return;

            }

            if (FilterColumn == "UserID" || FilterColumn == "PersonID")//in this case we deal with numbers not string.
                _dtAllUser.DefaultView.RowFilter = string.Format($"[{FilterColumn}] = {TextValue}");
            
            else //in this case we deal with string.
                _dtAllUser.DefaultView.RowFilter = string.Format($"[{FilterColumn}] LIKE '{TextValue}%'");

            dgvAllUser.DataSource = _dtAllUser.DefaultView;

            btnPrevious.Visible = false;
            btnNext.Visible = false;
            lblPageInfo.Text = "Filtered";
            lblRecords.Text = dgvAllUser.Rows.Count.ToString();
        } 

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.Text == "Is Active")
            {
                txtFilter.Visible = false;
                cbIsActive.Visible = true;
                panel1.Visible = false;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }
            else
            {
                txtFilter.Visible = (cbFilter.Text != "None");
                panel1.Visible = (cbFilter.Text != "None");

                cbIsActive.Visible = false;

                txtFilter.Text = "";
                txtFilter.Focus();
            }
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text;

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
                _dtAllUser.DefaultView.RowFilter = "";
            else//in this case we deal with numbers not string.
                _dtAllUser.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);
            
            dgvAllUser.DataSource = _dtAllUser.DefaultView;

            btnPrevious.Visible = false;
            btnNext.Visible = false;
            lblPageInfo.Text = "Filtered";
            lblRecords.Text = dgvAllUser.Rows.Count.ToString();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "Person ID" || cbFilter.Text == "User ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet.", "Not Implemented", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet.", "Not Implemented", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
