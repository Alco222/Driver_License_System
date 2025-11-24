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
    public partial class ControleUserInfo : UserControl
    {
        int _UserID = -1;
        clsUsers _User;

        public int UserID
        {
            get { return _UserID; }
        }

        public ControleUserInfo()
        {
            InitializeComponent();
        }

        public void LoadDataUser(int UserID)
        {
            _UserID = UserID;

             _User = clsUsers.Find(UserID);
            if (_User == null)
            {
                _ResetPersonInfo();
                MessageBox.Show($"Not Found User With UserID = {UserID}", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillUserInfo();
        }

        private void _ResetPersonInfo()
        {
            controlePersonDetails1.ResetPersonInfo();
            lblUserName.Text = "[???]";
            lblUserID.Text = "[???]";
            lblIsActive.Text = "[???]";

           
        }

        private void _FillUserInfo()
        {
            controlePersonDetails1.LoadPersonInfo(_User.PersonID);
            lblUserID.Text = UserID.ToString();
            lblUserName.Text = _User.UserName;

            lblIsActive.Text = _User.IsActive ? "Yes" : "No";
        }

    }
}
