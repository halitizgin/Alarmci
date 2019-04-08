using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alarm
{
    public partial class frmAlarm : Form
    {
        public frmAlarm()
        {
            InitializeComponent();
        }

        private void frmAlarm_Load(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = (Form1)Application.OpenForms["Form1"];
            form1.wplayer.controls.stop();
            form1.alarm = false;
            form1.button1.Text = "Alarm Kur";
            form1.label6.Text = "Durum: Alarm Kapalı";
            form1.Text = "Alarmcı v5.6 | Durum: Alarm Kapalı";
        }
    }
}
