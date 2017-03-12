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
    public partial class FormInformationAboutMarathonSkills : Form
    {
        public FormInformationAboutMarathonSkills()
        {
            InitializeComponent();
        }

        private void FormInformationAboutMarathonSkills_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime marathonTime = new DateTime(2017, 3, 5, 6, 0, 0);
            TimeSpan totalTime = marathonTime - DateTime.Now;

            label3.Text = totalTime.Days + " дней " + totalTime.Hours + " часов и " + totalTime.Minutes + " минут до старта марафона!";
        }

        private void FormInformationAboutMarathonSkills_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormMoreAboutMarathonSkills fm = new FormMoreAboutMarathonSkills();
            fm.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FormInteractiveMap fm = new FormInteractiveMap();
            fm.Show();
            this.Hide();
        }
    }
}
