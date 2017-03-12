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
    public partial class FormBMRCalculator : Form
    {
        public FormBMRCalculator()
        {
            InitializeComponent();
        }

        private void FormBMRCalculator_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormMoreAboutMarathonSkills fm = new FormMoreAboutMarathonSkills();
            fm.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime marathonTime = new DateTime(2017, 3, 5, 6, 0, 0);
            TimeSpan totalTime = marathonTime - DateTime.Now;

            label3.Text = totalTime.Days + " дней " + totalTime.Hours + " часов и " + totalTime.Minutes + " минут до старта марафона!";
        }

        private void FormBMRCalculator_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.BorderStyle = BorderStyle.Fixed3D;
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox3.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.BorderStyle = BorderStyle.Fixed3D;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormMoreAboutMarathonSkills fm = new FormMoreAboutMarathonSkills();
            fm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox3.BorderStyle == BorderStyle.Fixed3D)
                {
                    double bmr = 66 + (13.7 * Convert.ToDouble(numericUpDown2.Value)) + (5 * Convert.ToDouble(numericUpDown1.Value)) - (6.8 * Convert.ToDouble(numericUpDown3.Value));

                    bmr = bmr / 1000;
                    bmr = Math.Round(bmr, 3);
                    label14.Text = bmr.ToString();

                    panel1.Visible = true;

                    label22.Text = " " + (Math.Round(bmr * 1.2, 2)).ToString();
                    label23.Text = " " + (Math.Round(bmr * 1.375, 2)).ToString();
                    label24.Text = " " + (Math.Round(bmr * 1.55, 2)).ToString();
                    label25.Text = " " + (Math.Round(bmr * 1.725, 2)).ToString();
                    label26.Text = " " + (Math.Round(bmr * 1.9, 2)).ToString();

                }

                if (pictureBox2.BorderStyle == BorderStyle.Fixed3D)
                {
                    double bmr = 655 + (9.6 * Convert.ToDouble(numericUpDown2.Value)) + (1.8 * Convert.ToDouble(numericUpDown1.Value)) - (4.7 * Convert.ToDouble(numericUpDown3.Value));

                    bmr = bmr / 1000;
                    bmr = Math.Round(bmr, 3);
                    label14.Text = bmr.ToString();

                    panel1.Visible = true;

                    label22.Text = " " + (Math.Round(bmr * 1.2, 2)).ToString();
                    label23.Text = " " + (Math.Round(bmr * 1.375, 2)).ToString();
                    label24.Text = " " + (Math.Round(bmr * 1.55, 2)).ToString();
                    label25.Text = " " + (Math.Round(bmr * 1.725, 2)).ToString();
                    label26.Text = " " + (Math.Round(bmr * 1.9, 2)).ToString();

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Деление значений на ноль невозможно!", "Опомещение системы");
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {
            FormBMRCalculatorInfo fm = new FormBMRCalculatorInfo();
            fm.Show();
        }
    }
}
