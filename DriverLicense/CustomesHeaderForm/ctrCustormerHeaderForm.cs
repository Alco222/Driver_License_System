using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriverLicense.CustomesHeaderForm
{
    public partial class ctrCustormerHeaderForm : UserControl
    {
        public string TitleText
        {
            get => lblTitle.Text;
            set => lblTitle.Text = value;
        }

        public Image IconImage
        {
            get => pictureBox.Image;
            set => pictureBox.Image = value;
        }

        public Image IconImage2
        {
            get => pictureBox2.Image;
            set => pictureBox2.Image = value;
        }

        public Color HeaderBackColor
        {
            get => panelHeader.BackColor;
            set => panelHeader.BackColor = value;
        }

        public ctrCustormerHeaderForm()
        {
            InitializeComponent();

            panelHeader.MouseDown += panelHeader_MouseDown;
            lblTitle.MouseDown += panelHeader_MouseDown;
            pictureBox.MouseDown += panelHeader_MouseDown;
            pictureBox2.MouseDown += panelHeader_MouseDown;
            btnClose.Click += BtnClose_Click;
            this.Resize += ctrCustormerHeaderForm_Resize;
        }

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public Form ParentFormRef { get; set; }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            ParentFormRef?.Close(); // يغلق الفورم الأب
        }

        private void ctrCustormerHeaderForm_Resize(object sender, EventArgs e)
        {
            btnClose.Location = new Point(this.Width - btnClose.Width - 5, btnClose.Location.Y);
        }

       

        private void panelHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && ParentFormRef != null)
            {
                ReleaseCapture();
                SendMessage(ParentFormRef.Handle, 0xA1, 0x2, 0);
            }
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            ParentFormRef = this.FindForm();
        }
    }
}
