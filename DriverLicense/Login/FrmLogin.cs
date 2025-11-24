using System;
using System.Windows.Forms;
using DriverLicenseBusinessLayer;
using Microsoft.Win32;
using System.Threading.Tasks;
using System.Threading;
using DriverLicense.Login;
using System.Windows.Documents;
using System.Diagnostics;

namespace DriverLicense
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void LoggedInUserToUserRegester(clsUsers user)
        {
            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD-Login-RegisterDate";
            string ValuerUserName = "UserName";
            string ValueDate = "RegisterDate";
            string RegisterDate = DateTime.Now.ToString("dd/MM/yyy HH:mm");

            try
            {
               
                Registry.SetValue(KeyPath, ValueDate, RegisterDate, RegistryValueKind.String);
                Registry.SetValue(KeyPath, ValuerUserName,user.UserName, RegistryValueKind.String);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An occurred: {ex.Message}");
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            string UserName = "", Pssword = "";

            if(LoggedInUser.GetStoredCredential(ref UserName, ref Pssword))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Pssword;
                tsRemamber.Checked = true;
            }
            else
                tsRemamber.Checked = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*        private void btnLogin_Click(object sender, EventArgs e)
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    //Task task1;
                    //show loading form
                    frmLoading loading = new frmLoading();
                    loading.Show();

                    clsUsers user = clsUsers.LoginUser(txtUserName.Text.Trim(), txtPassword.Text.Trim());

                    LoggedInUser Filelogger = new LoggedInUser(LoggedInUserToUserRegester);

                    if (user != null)
                    {
                        //incase the user is not active
                        if (!user.IsActive)
                        {
                            txtUserName.Focus();
                            MessageBox.Show("Your accound is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (tsRemamber.Checked)
                            //store username and password
                            // task1= Task.Run(()=> LoggedInUser.RememberUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim()));
                            LoggedInUser.RememberUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                        else
                            //store empty username and password
                            //task1 = Task.Run(()=> LoggedInUser.RememberUsernameAndPassword(null,null));
                            LoggedInUser.RememberUsernameAndPassword(null, null);

                        Filelogger.Logger(user);

                        //await Task.WhenAll(task1);

                        LoggedInUser.CurrentUser = user;

                        //Do your work here
                        //await Task.Delay(3000);
                        //Close the loading form
                        loading.Close();
                        stopwatch.Stop();
                        MessageBox.Show($"Login Successful! Time taken: {stopwatch.ElapsedMilliseconds} ms", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Hide();
                        FrmMainDVL frm = new FrmMainDVL(this);
                        frm.ShowDialog();

                    }
                    else
                    {
                        txtUserName.Focus();
                        MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
        */
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            
            //show loading form
            frmLoading loading = new frmLoading();
            loading.Show();

            clsUsers user = await clsUsers.LoginUserAsync(txtUserName.Text.Trim(), txtPassword.Text.Trim());

            LoggedInUser Filelogger = new LoggedInUser(LoggedInUserToUserRegester);

            if (user != null)
            {
                //incase the user is not active
                if (!user.IsActive)
                {
                    txtUserName.Focus();
                    MessageBox.Show("Your accound is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (tsRemamber.Checked)
                    //store username and password
                    LoggedInUser.RememberUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                else
                    //store empty username and password
                    LoggedInUser.RememberUsernameAndPassword(null, null);

                Filelogger.Logger(user);

                LoggedInUser.CurrentUser = user;

                //Do your work here
                await Task.Delay(2500);

                //Close the loading form
                loading.Close();
                stopwatch.Stop();
               
                this.Hide();
                FrmMainDVL frm = new FrmMainDVL(this);
                frm.ShowDialog();

            }
            else
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void chbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if(chbShowPassword.Checked)
                txtPassword.PasswordChar = char.MinValue;
            else
                txtPassword.PasswordChar = '*';

        }
    }
}
