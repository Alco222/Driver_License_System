using DriverLicenseBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriverLicense
{



    public partial class FormTests : Form
    {
       
        public FormTests()
        {
            InitializeComponent();
        }

        private async void btnTest_Click(object sender, EventArgs e)
        {
        
            lblStatus.Text = "⏳ Checking login...";

            // تنفيذ العملية في الخلفية
            var loginTask = Task.Run(async () =>
            {
                await Task.Delay(5000); // نحاكي عملية تستغرق 5 ثواني (مثلاً التحقق من قاعدة البيانات)
                return "✅ Login completed!";
            });

            // أثناء الانتظار نعمل أشياء أخرى 👇
            for (int i = 0; i < 5; i++)
            {
                lblStatus.Text = $"Working... {i + 1}";
                await Task.Delay(1000); // تحديث كل ثانية
            }

            // بعد انتهاء العملية الحقيقية
            string result = await loginTask;
            lblStatus.Text = result;
        }
        

        private void btnTest2_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "⏳ Checking login...";
            lblStatus.Refresh();

            // العملية العادية (تحاكي عملية login بقاعدة بيانات)
            Thread.Sleep(5000); // توقف 5 ثوانٍ

            lblStatus.Text = "✅ Login completed!";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int PersonID =Convert.ToInt32(textBox1.Text);
            bool result = clsPerson.IsPersonExists(PersonID);

            if(result)
            {
                MessageBox.Show("Person exists.");
            }
            else
            {
                MessageBox.Show("Person does not exist.");
            }

        }
    }
}
