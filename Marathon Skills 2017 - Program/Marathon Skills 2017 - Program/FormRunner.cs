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
    public partial class FormRunner : Form
    {
        public string email;
        public FormRunner(string email)
        {
            this.email = email;
            InitializeComponent();
        }

        private void FormRunner_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime marathonTime = new DateTime(2017, 3, 5, 6, 0, 0);
            TimeSpan totalTime = marathonTime - DateTime.Now;

            label3.Text = totalTime.Days + " дней " + totalTime.Hours + " часов и " + totalTime.Minutes + " минут до старта марафона!";
        }

        private void FormRunner_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormRegistrationMarathon fm = new FormRegistrationMarathon(email);
            fm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormEditRunnerProfile fm = new FormEditRunnerProfile(email);
            fm.Show();
            this.Hide();
        }
    }
}
