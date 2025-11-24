using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using DriverLicense.Classes;
using DriverLicenseBusinessLayer;

namespace DriverLicense  
{
    public partial class FrmAddEditApplicationDVL : Form
    {
       public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;

        int _SelectedPersonID= -1;
        int _LocalDrivingLicenseApplicationID=-1;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        public FrmAddEditApplicationDVL()
        {
            InitializeComponent();
            _Mode = enMode.AddNew; // Set the mode to AddNew by default
        }

        public FrmAddEditApplicationDVL(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _Mode = enMode.Update; // Set the mode to AddNew by default
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID; // Set the LocalDrivingLicenseApplicationID
        }

        private bool _HandlingAgePersonAgeMatchedMinimumAgeLicenseClass(int LicenseClassID)
        {
           

            int Age = clsPerson.GetAgePerson(_SelectedPersonID);
            bool IsValidAge = clsClassLicense.DoesPersonAgeMatchedTehMinimumAgeInclass(LicenseClassID, Age);

            if (!IsValidAge)
            {
                MessageBox.Show("This Person is not allowed to apply for the selected driving class, because his age is " + Age + " and the minimum age required is " + clsClassLicense.Find(LicenseClassID).MinimuuAllwoedAge, "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool _HandlingActiveApplication(int LicenseClassID)
        {
            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(_SelectedPersonID, clsApplication.enApplicationType.NewDrivingLicense, LicenseClassID);

            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbClass.Focus();
                return false;
            }
            return true;
        }

        private bool _HandlingLicenseExistByPerson(int LicenseClassID)
        {
            if (clsLicense.IsLicenseExistByPersonID(ctrlPersonCardWithFilter11.PersonID, LicenseClassID))
            {

                MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        
        private void _FillClassesLicenseInComeboBox()
        {
            // Create a DataTable and load all Class License from the database into it
            DataTable dtClassLicense = clsClassLicense.GetAllClassLicense();

            // Loop through each country and add it to the ComboBox
            foreach (DataRow row in dtClassLicense.Rows)
            {
                // Add the country name to the ComboBox list
                cbClass.Items.Add(row["ClassName"]);
            }
        }

        private void _ResetDefualtValues()
        {
            //this will initialize the reset the defaule values
            _FillClassesLicenseInComeboBox();


            if (_Mode == enMode.AddNew)
            {

                ctrCustormerHeaderForm1.TitleText = "New Local Driving License Application";
               
                _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
               
                tpApplicationInfo.Enabled = false;

                cbClass.SelectedIndex = 2;
                lblFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewDrivingLicense).Fees.ToString();
                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
                lblCreatedByUser.Text = LoggedInUser.CurrentUser.UserName;
            }
            else
            {
                ctrCustormerHeaderForm1.TitleText = "Update Local Driving License Application";
              

                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
            } 
        }

        private void _LoadData()
        {
            ctrlPersonCardWithFilter11.FilterEnabled = false;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Application with ID = " + _LocalDrivingLicenseApplicationID, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            ctrlPersonCardWithFilter11.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);
            lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort(_LocalDrivingLicenseApplication.ApplicationDate);
            cbClass.SelectedIndex = cbClass.FindString(_LocalDrivingLicenseApplication.LicenseClassInfo.Name);
            lblFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString();
            lblCreatedByUser.Text = LoggedInUser.CurrentUser.UserName;
            _SelectedPersonID = ctrlPersonCardWithFilter11.PersonID;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
                return;
            }
            else
            {
                //incase of add new mode.
                if (_SelectedPersonID != -1)
                {

                    btnSave.Enabled = true;
                    tpApplicationInfo.Enabled = true;
                    tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];

                }

                else

                {
                    MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter11.FilterFocus();
                }
            }

           
        }

        private void FrmAddEditApplicationDVL_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int LicenseClassID = clsClassLicense.Find(cbClass.Text).LicenseClassID;

            if(!_HandlingAgePersonAgeMatchedMinimumAgeLicenseClass(LicenseClassID))
               return;

            if (!_HandlingActiveApplication(LicenseClassID))
                return;

            //check if user already have issued license of the same driving  class.
            if(!_HandlingLicenseExistByPerson(LicenseClassID))
                return;

            _LocalDrivingLicenseApplication.ApplicantPersonID = ctrlPersonCardWithFilter11.PersonID;
            _LocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseApplication.ApplicationTypeID = 1;
            _LocalDrivingLicenseApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
            _LocalDrivingLicenseApplication.PaidFees = Convert.ToSingle(lblFees.Text);
            _LocalDrivingLicenseApplication.CreatedByUserID = LoggedInUser.CurrentUser.UserID;
            _LocalDrivingLicenseApplication.LicenseClassID = LicenseClassID;


            if (_LocalDrivingLicenseApplication.Save())
            {
                lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                ctrCustormerHeaderForm1.TitleText = "Update Local Driving License Application";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlPersonCardWithFilter11_OnPersonSelected(int obj)
        {
            _SelectedPersonID = obj;
        }

        private void FrmAddEditApplicationDVL_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter11.FilterFocus();
        }
    }
}
