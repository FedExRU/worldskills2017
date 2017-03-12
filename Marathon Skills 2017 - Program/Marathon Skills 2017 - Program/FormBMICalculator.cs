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
    public partial class FormBMICalculator : Form
    {
        public FormBMICalculator()
        {
            InitializeComponent();
        }

        private void FormBMICalculator_FormClosing(object sender, FormClosingEventArgs e)
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

        private void FormBMICalculator_Load(object sender, EventArgs e)
        {
            timer1.Start();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormMoreAboutMarathonSkills fm = new FormMoreAboutMarathonSkills();
            fm.Show();
            this.Hide();
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

        private void button1_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel11.Visible = false;

            decimal bmi = 0;

            try
            {
                bmi = numericUpDown2.Value / (numericUpDown1.Value * numericUpDown1.Value) * 10000;

                bmi = Math.Round(bmi, 2);
                //MessageBox.Show(bmi.ToString());

                if (Convert.ToDouble(bmi) < 18.5)
                {
                    panel8.Visible = true;
                    label11.Text = bmi.ToString();
                    pictureBox4.Image = Properties.Resources.bmi_underweight_icon;
                    textBox1.Text = "Недостаточный вес";

                }
                if (Convert.ToDouble(bmi) >= 18.5 && Convert.ToDouble(bmi) <= 24.9)
                {
                    panel9.Visible = true;
                    label12.Text = bmi.ToString();
                    pictureBox4.Image = Properties.Resources.bmi_healthy_icon;
                    textBox1.Text = "Здоровый вес";

                }
                if (Convert.ToDouble(bmi) >= 25 && Convert.ToDouble(bmi) <= 29.9)
                {
                    panel10.Visible = true;
                    label14.Text = bmi.ToString();
                    pictureBox4.Image = Properties.Resources.bmi_obese_icon;
                    textBox1.Text = "Избыточный вес";

                }
                if (Convert.ToDouble(bmi) >= 30)
                {
                    panel11.Visible = true;
                    label16.Text = bmi.ToString();
                    pictureBox4.Image = Properties.Resources.bmi_overweight_icon;
                    textBox1.Text = "Ожирение";

                }
            }
            catch
            {
                MessageBox.Show("Деление значений на ноль невозможно!", "Опомещение системы");
            }
        }
    }
}
