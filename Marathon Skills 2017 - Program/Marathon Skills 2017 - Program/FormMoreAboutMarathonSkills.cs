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
    public partial class FormMoreAboutMarathonSkills : Form
    {
        public FormMoreAboutMarathonSkills()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime marathonTime = new DateTime(2017, 3, 5, 6, 0, 0);
            TimeSpan totalTime = marathonTime - DateTime.Now;

            label3.Text = totalTime.Days + " дней " + totalTime.Hours + " часов и " + totalTime.Minutes + " минут до старта марафона!";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Hide();
        }

        private void FormMoreAboutMarathonSkills_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        private void FormMoreAboutMarathonSkills_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FormCharityList fm = new FormCharityList();
            fm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormInformationAboutMarathonSkills fm = new FormInformationAboutMarathonSkills();
            fm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormBMICalculator fm = new FormBMICalculator();
            fm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormBMRCalculator fm = new FormBMRCalculator();
            fm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormAllRaces fm = new FormAllRaces();
            fm.Show();
            this.Hide();
        }
    }
}
