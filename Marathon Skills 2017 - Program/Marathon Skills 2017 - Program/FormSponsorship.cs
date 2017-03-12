using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Marathon_Skills_2017___Program
{
    public partial class FormSponsorship : Form
    {
        public FormSponsorship()
        {
            InitializeComponent();
        }

        private void FormSponsorship_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime marathonTime = new DateTime(2017, 3, 5, 6, 0, 0);
            TimeSpan totalTime = marathonTime - DateTime.Now;

            label3.Text = totalTime.Days + " дней " + totalTime.Hours + " часов и " + totalTime.Minutes + " минут до старта марафона!";
        }

        private void FormSponsorship_Load(object sender, EventArgs e)
        {
            timer1.Start();
            SqlConnection conn = new SqlConnection(Connection.GetString());
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT RegistrationEvent.BibNumber, Runner.RunnerId, Users.FirstName, Users.LastName, Country.CountryName FROM Users,Runner, Country, RegistrationEvent, Registration WHERE Country.CountryCode = Runner.CountryCode AND Users.RoleId = 'R' AND Users.Email = Runner.Email AND Registration.RunnerId = Runner.RunnerId AND Registration.RegistrationId = RegistrationEvent.RegistrationId ORDER BY Users.FirstName", conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.text = reader["FirstName"].ToString() + " " + reader["LastName"].ToString() + " - " + reader["BibNumber"].ToString() + " (" + reader["CountryName"].ToString() + ")";
                    item.value = reader["RunnerId"].ToString();

                    comboBox1.Items.Add(item);
                }
            }

            conn.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Hide();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Одно или несколько обязательных полей для ввода (отмеченных знаком *) не были заполненны!", "Оповещение системы");
            }
            else
            {
                if(textBox3.TextLength < 16)
                {
                    MessageBox.Show("Некорретный номер карты! Длина номера банковской карточки должна составлять 16 символов!", "Оповещение системы");
                }
                else
                {
                    DateTime cardTime = new DateTime(Convert.ToInt32(textBox6.Text), Convert.ToInt32(textBox5.Text), DateTime.Now.Day);

                    if(cardTime <= DateTime.Now)
                    {
                        MessageBox.Show("Указанный срок действия карты считается истекшим!", "Оповещение системы");
                    }
                    else
                    {
                        if (textBox4.TextLength < 3)
                        {
                            MessageBox.Show("Некорректный CVC код! Длина кода должна составлять 3 символа!", "Оповещение системы");
                        }
                        else
                        {
                            if(textBox8.Text == "" || textBox8.Text == "0")
                            {
                                MessageBox.Show("Пожалуйста, укажите сумму спонсорского взноса более $0.", "Опомещение системы");
                            }
                            else
                            {
                                SqlConnection conn = new SqlConnection(Connection.GetString());
                                conn.Open();
                                SqlCommand command = new SqlCommand("UPDATE Registration Set SponsorshipTarget = SponsorshipTarget + " + textBox8.Text + " WHERE RunnerId = '" + (comboBox1.SelectedItem as ComboBoxItem).value + "'", conn);

                                command.ExecuteNonQuery();

                                FormSponsorshipConfirmation fm = new FormSponsorshipConfirmation((comboBox1.SelectedItem as ComboBoxItem).value, textBox7.Text, textBox8.Text);
                                fm.Show();
                                this.Hide();
                            }
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(textBox8.Text);
            value -= 10;
            textBox8.Text = value.ToString();

            if (value <= 0)
            {
                label20.Text = "$0";
                textBox8.Text = "0".ToString();
            }
            else
                label20.Text = "$" + value.ToString();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(textBox8.Text);
            value += 10;
            textBox8.Text = value.ToString();
            label20.Text = "$" + value.ToString();

            if (value <= 0)
            {
                label20.Text = "$0";
                textBox8.Text = "0".ToString();
            }
            else
                label20.Text = "$" + value.ToString();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (textBox8.Text == "")
            {
                label20.Text = "$0";
                textBox8.Text = "0".ToString();
            }
            else
                label20.Text = "$" + textBox8.Text.ToString();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(Connection.GetString());
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT TOP(1) Charity.CharityName, Charity.CharityId FROM Charity, Registration WHERE Charity.CharityId = Registration.CharityId AND Registration.RunnerId = "+(comboBox1.SelectedItem as ComboBoxItem).value+"", conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    textBox7.Text = reader["CharityName"].ToString();
                }
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {
            FormSponsorshipCharityInfo fm = new FormSponsorshipCharityInfo(textBox7.Text);
            fm.Show();
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
    }
}
