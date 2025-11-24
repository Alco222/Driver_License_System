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
using DriverLicenseBusinessLayer;
using DVLD.Classes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DriverLicense
{
    public partial class MangerPerson : Form
    {
        private static DataTable _dtAllPersons = clsPerson.GetAllPeople();

        private DataTable _dtPeople = _dtAllPersons.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                  "FirstName", "SecondName", "ThirdName", "LastName",
                                                  "Gendercaption", "BirthDate", "CountryName",
                                                  "Phone", "Email");
       
        private int _currentPage = 1;
        private const int _PAGE_SIZE = 60;
        
        private void LoadCurrentPage()
        {
            clsUtil.LoadDataPage(_dtPeople, dgvAllPerson,_currentPage,_PAGE_SIZE,
               lblPageInfo, btnPrevious, btnNext);
        }

        private void _PerpareComboBox()
        {
            CbFilter.Items.AddRange(new string[] { "None", "Person ID", "First Name", "Second Name", "Third Name", "Last Name", "National No", "Email",  "Nationality", "Phone", "Gender" });
            CbFilter.SelectedIndex = 0;
        }

        private void _RefreshPersonList()
        {
            _dtAllPersons = clsPerson.GetAllPeople();
            _dtPeople = _dtAllPersons.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                  "FirstName", "SecondName", "ThirdName", "LastName",
                                                  "Gendercaption", "BirthDate", "CountryName",
                                                  "Phone", "Email");

            dgvAllPerson.DataSource = _dtPeople;
            lblRecords.Text = dgvAllPerson.Rows.Count.ToString();

        }

        public MangerPerson()
        {
            InitializeComponent();
        }

        private void MangerPerson_Load(object sender, EventArgs e)
        {
            _PerpareComboBox();

            LoadCurrentPage();
            CbFilter.SelectedIndex = 0; // Set default filter to "Non"
            lblRecords.Text = _dtAllPersons.Rows.Count.ToString();
            if (dgvAllPerson.Rows.Count > 0)
            {
                dgvAllPerson.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);

                dgvAllPerson.Columns[0].HeaderText = "Person ID";
                dgvAllPerson.Columns[0].Width = 70;
                dgvAllPerson.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllPerson.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllPerson.Columns[1].HeaderText = "National No";
                dgvAllPerson.Columns[1].Width = 75;
                dgvAllPerson.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllPerson.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllPerson.Columns[2].HeaderText = "First Name";
                dgvAllPerson.Columns[2].Width = 100;
                dgvAllPerson.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllPerson.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllPerson.Columns[3].HeaderText = "Second Name";
                dgvAllPerson.Columns[3].Width = 110;
                dgvAllPerson.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllPerson.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllPerson.Columns[4].HeaderText = "Third Name";
                dgvAllPerson.Columns[4].Width = 100;
                dgvAllPerson.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllPerson.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllPerson.Columns[5].HeaderText = "Last Name";
                dgvAllPerson.Columns[5].Width = 100;
                dgvAllPerson.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllPerson.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllPerson.Columns[6].HeaderText = "Gender";
                dgvAllPerson.Columns[6].Width = 70;
                dgvAllPerson.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllPerson.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllPerson.Columns[7].HeaderText = "Date Of Birth";
                dgvAllPerson.Columns[7].Width = 100;
                dgvAllPerson.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllPerson.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllPerson.Columns[8].HeaderText = "Nationality";
                dgvAllPerson.Columns[8].Width = 90;
                dgvAllPerson.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllPerson.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllPerson.Columns[9].HeaderText = "Phone";
                dgvAllPerson.Columns[9].Width = 110;
                dgvAllPerson.Columns[9].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAllPerson.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvAllPerson.Columns[10].HeaderText = "Email";
                dgvAllPerson.Columns[10].Width = 125;
                dgvAllPerson.Columns[10].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

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

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmAddEditPersonInfo frm = new FrmAddEditPersonInfo(-1);
            frm.ShowDialog();
            _RefreshPersonList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this code is create object with parametrized constructor.
            //and  Return the actual value (ID) in that field. This value is returns of type object
            //إرجاع القيمة الفعلية (المعرف) في تلك الخانة. هذه القيمة ترجع من نوع الكائن.
            FrmAddEditPersonInfo frm = new FrmAddEditPersonInfo((int)dgvAllPerson.CurrentRow.Cells[0].Value);

            // and Show Form 'FrmAddEditContact'.
            frm.ShowDialog();

            //execute this Method after close 'FrmAddEditContact' and Load data in
            //'FrmListContacts'
            _RefreshPersonList();

        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAddEditPersonInfo frm = new FrmAddEditPersonInfo(-1);
            frm.ShowDialog();
            _RefreshPersonList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("are you sure want to deleted Person [" + dgvAllPerson.CurrentRow.Cells[0].Value + "]", "Confirm Deleted", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (clsPerson.DeletePerson((int)dgvAllPerson.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfuly.", "Deleted Seccessed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshPersonList();
                }
                else
                    MessageBox.Show("Person was not deleted, because it has data linked to it.", "Deleted Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPersonInformation frm = new FrmPersonInformation((int)dgvAllPerson.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPersonList();
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
            if (CbFilter.SelectedItem.ToString() == "Person ID")
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
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gender":
                    FilterColumn = "Gendercaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (TextValue == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";

                LoadCurrentPage(); 
                btnPrevious.Visible = true;
                btnNext.Visible = true;
                lblRecords.Text = _dtPeople.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "PersonID") //in this case we deal with integer not string.
                _dtPeople.DefaultView.RowFilter = string.Format($"[{FilterColumn}] = {TextValue}");
            else
                _dtPeople.DefaultView.RowFilter = string.Format($"[{FilterColumn}] LIKE '{TextValue}%'");

            dgvAllPerson.DataSource = _dtPeople.DefaultView;

            btnPrevious.Visible = false;
            btnNext.Visible = false;
            lblPageInfo.Text = "Filtered";
            lblRecords.Text = dgvAllPerson.Rows.Count.ToString();
           
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("this feature is not implemented yet.", "Not Implemented", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("this feature is not implemented yet.", "Not Implemented", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

    }
}
