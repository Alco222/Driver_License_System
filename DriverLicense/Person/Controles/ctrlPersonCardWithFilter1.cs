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
    public partial class ctrlPersonCardWithFilter1 : UserControl
    {
        public event Action< int> OnPersonSelected;

        protected virtual void GetPesonIDCompleted(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(PersonID);
            }
        }

        //int _PersonID = -1;
        public int PersonID
        {
            get { return controlePersonDetails1.PersonID; }
        }

        public ctrlPersonCardWithFilter1()
        {
            InitializeComponent();
        }


        public clsPerson SelectedPersonInfo
        {
            get { return controlePersonDetails1.SelectedPersonInfo; }
        }
  
        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson = value;
                btnAddNewPerson.Visible = _ShowAddPerson;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }

        public void FilterFocus()
        {
            txtFilter.Focus();
        }

        public void LoadPersonInfo(int PersonID)
        {
            cbFilter.SelectedIndex = 1;
            txtFilter.Text = PersonID.ToString();
            FindNow();
        }

        private void FindNow()
        {
            switch (cbFilter.Text)

            {
                case "PersonID":
                    controlePersonDetails1.LoadPersonInfo(int.Parse(txtFilter.Text));

                    break;

                case "NationalNo":
                    controlePersonDetails1.LoadPersonInfo(txtFilter.Text);
                    break;

                default:
                    break;
            }

            if (OnPersonSelected != null && FilterEnabled)
                // Raise the event with a parameter
                OnPersonSelected(controlePersonDetails1.PersonID);
        }

        private void btnSearch_Click(object sender, EventArgs e)    
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            FindNow();

        }

        private void ctrlPersonCardWithFilter1_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;
            txtFilter.Focus();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmAddEditPersonInfo frm1 = new FrmAddEditPersonInfo(-1);
            frm1.DataBack += DataBackEvent; // Subscribe to the event
            frm1.ShowDialog();


        }

        private void DataBackEvent(object sender, int PersonID)
        {
            // Handle the data received
            cbFilter.SelectedIndex = 1;
            txtFilter.Text = PersonID.ToString();
            controlePersonDetails1.LoadPersonInfo(PersonID);
            gbFilters.Enabled = false;
        }

        private void txtFilter_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilter.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilter, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtFilter, null);
            }
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {
                btnSearch.PerformClick();
            }

            //this will allow only digits if person id is selected
            if (cbFilter.Text == "PersonID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Text = "";
            txtFilter.Focus();
        }

    }
}
