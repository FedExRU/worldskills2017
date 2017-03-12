using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marathon_Skills_2017___Program
{
    public partial class FormInteractiveMap : Form
    {
        public FormInteractiveMap()
        {
            InitializeComponent();
        }

        private void FormInteractiveMap_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormInformationAboutMarathonSkills fm = new FormInformationAboutMarathonSkills();
            fm.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime marathonTime = new DateTime(2017, 3, 5, 6, 0, 0);
            TimeSpan totalTime = marathonTime - DateTime.Now;

            label3.Text = totalTime.Days + " дней " + totalTime.Hours + " часов и " + totalTime.Minutes + " минут до старта марафона!";
        }

        private void FormInteractiveMap_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            FormCheckPointStart2 fm = new FormCheckPointStart2();
            fm.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            FormCheckPointStart1 fm = new FormCheckPointStart1();
            fm.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            FormCheckPointStart3 fm = new FormCheckPointStart3();
            fm.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            FormCheckPoint1 fm = new FormCheckPoint1();
            fm.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            FormCheckPoint2 fm = new FormCheckPoint2();
            fm.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            FormCheckPoint3 fm = new FormCheckPoint3();
            fm.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            FormCheckPoint4 fm = new FormCheckPoint4();
            fm.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            FormCheckPoint5 fm = new FormCheckPoint5();
            fm.Show();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            FormCheckPoint6 fm = new FormCheckPoint6();
            fm.Show();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            FormCheckPoint7 fm = new FormCheckPoint7();
            fm.Show();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            FormCheckPoint8 fm = new FormCheckPoint8();
            fm.Show();
        }
    }
}
