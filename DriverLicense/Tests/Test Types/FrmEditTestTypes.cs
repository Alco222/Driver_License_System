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
    public partial class FrmEditTestTypes : Form
    {   
        clsTestTypes.enTestType _TestTesID;
        clsTestTypes _TestTypes;

        public FrmEditTestTypes(clsTestTypes.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestTesID = TestTypeID;
        }

        private void _LoadData()
        {
            // If you are editing, retrieve the contact by ID
            _TestTypes = clsTestTypes.Find(_TestTesID);


            // If contact not found, show a message and close the form
            if (_TestTypes == null)
            {
                MessageBox.Show($"This form will be closed, because could not Test Types with ID = {_TestTesID}");
                this.Close();
                return;
            }
            else
            {
                float Fess = (float)_TestTypes.Fees;

                // Update labels and form fields with contact's data
                lblTesID.Text = ((int)_TestTesID).ToString();
                txtTitleTest.Text = _TestTypes.Title.ToString();
                txtDescriptionTest.Text = _TestTypes.Description.ToString();
                txtFessTest.Text = Fess.ToString("N2");
            }
            
        }

        private void FrmEditTest_Load(object sender, EventArgs e)
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
            _TestTypes.Title = txtTitleTest.Text;
            _TestTypes.Fees = Convert.ToSingle(txtFessTest.Text);


            if (_TestTypes.Save())
                MessageBox.Show("Data Saved Successfully.", "Seccess", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
