using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriverLicense
{
    public partial class FrmShowPersonLIcenseHistory : Form
    {
        int _PersonID = -1;

        public FrmShowPersonLIcenseHistory()
        {
            InitializeComponent();
        }


        public FrmShowPersonLIcenseHistory(int personID)
        {
            InitializeComponent();
            _PersonID = personID;
        }

        private void FrmShowPersonLIcenseHistory_Load(object sender, EventArgs e)
        {
            if(_PersonID !=-1)
            {
                ctrlPersonCardWithFilter11.LoadPersonInfo(_PersonID);
                ctrlPersonCardWithFilter11.FilterEnabled = false;
                controlDriverLicense1.LoadInfoByPersonID(_PersonID);
            }
            else
            {
                ctrlPersonCardWithFilter11.FilterEnabled = true;
                ctrlPersonCardWithFilter11.FilterFocus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlPersonCardWithFilter11_OnPersonSelected(int obj)
        {
            _PersonID = obj;

            if(_PersonID == -1)
            {
                controlDriverLicense1.Clear();
            }
            else
             controlDriverLicense1.LoadInfoByPersonID(_PersonID);
        }
    }
}
