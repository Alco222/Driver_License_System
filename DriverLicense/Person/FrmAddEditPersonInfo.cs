using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DriverLicenseBusinessLayer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DriverLicense
{
    public partial class FrmAddEditPersonInfo : Form
    {

        public delegate void DataBackEventHandler(object sender, int PersonID);

        //Declare an events using the delegate.
        public event DataBackEventHandler DataBack;
          
        
        int _PersonID;

        public FrmAddEditPersonInfo(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            controlAddEditPersoneInfo1.SetPersonID(_PersonID);
        }

        private void controlAddEditPersoneInfo1_OnSendPersonInformationComplete(object sender, ControlAddEditPersoneInfo.sendPersonInformationCompleteEventArgs e)
        {
            DataBack?.Invoke(this, e.PersonID);
            lblPersonID.Text = e.PersonID.ToString();
            ctrCustormerHeaderForm1.TitleText = e.ModeString.ToString();
        }


    }
}
