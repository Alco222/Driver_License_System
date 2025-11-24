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
    public partial class FrmUpdateApplicationTypes : Form
    {
        int _AppID;
        clsApplicationTypes _ApplicationTypes;
        private void _LoadData()
        {
            // If you are editing, retrieve the contact by ID
            _ApplicationTypes = clsApplicationTypes.Find(_AppID);


            // If contact not found, show a message and close the form
            if (_ApplicationTypes == null)
            {
                MessageBox.Show($"This form will be closed because no Application Types with ID = {_AppID}");
                this.Close();
                return;
            }
            float Fess = (float)_ApplicationTypes.Fees; 

            // Update labels and form fields with contact's data
            lblAppID.Text = _AppID.ToString();
            txtTitleApp.Text = _ApplicationTypes.Title;
            
            txtFessApp.Text = Fess.ToString("N2");

        }

        public FrmUpdateApplicationTypes(int AppID)
        {
            InitializeComponent();
            _AppID = AppID;
        }

        private void UpdateApplicationTypes_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


            // Assign the form fields to the _Contact object's properties
            _ApplicationTypes.Title = txtTitleApp.Text;
            _ApplicationTypes.Fees = Convert.ToSingle(txtFessApp.Text);


            if (_ApplicationTypes.Save())
                MessageBox.Show("Data Saved Successfully.","Seccess",MessageBoxButtons.OK,MessageBoxIcon.Information);
            else
                MessageBox.Show("Error: Data Is not Saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TextBox_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBox, "This field cannot be empty.");
            }
            else
            {
                errorProvider1.SetError(textBox, null);
            }
        }

    }
}
