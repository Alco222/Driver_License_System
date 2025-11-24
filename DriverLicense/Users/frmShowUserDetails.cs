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
    public partial class frmShowUserDetails : Form
    {
        int _UserID;

        public frmShowUserDetails(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void frmShowUserDetails_Load(object sender, EventArgs e)
        {
            controleUserInfo1.LoadDataUser(_UserID);
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
