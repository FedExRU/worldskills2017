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
using System.Text.RegularExpressions;

namespace Marathon_Skills_2017___Program
{
    public partial class FormRegistration : Form
    {
        public FormRegistration()
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

        private void FormRegistration_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Hide();
        }

        private void FormRegistration_Load(object sender, EventArgs e)
        {
            timer1.Start();

            openFileDialog1.Filter = "Image Files |*.jpg;*.png;*gif;";

            SqlConnection conn = new SqlConnection(Connection.GetString());
            conn.Open();

            SqlCommand commandGender = new SqlCommand("SELECT * FROM Gender", conn);

            using (SqlDataReader reader = commandGender.ExecuteReader())
            {
                while(reader.Read())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.text = reader["Gender"].ToString();
                    item.value = reader["Gender"].ToString();
                    comboBox1.Items.Add(item);
                }
            }

            SqlCommand commandCountry = new SqlCommand("SELECT * FROM Country ORDER BY CountryName", conn);

            using (SqlDataReader reader = commandCountry.ExecuteReader())
            {
                while (reader.Read())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.text = reader["CountryName"].ToString();
                    item.value = reader["CountryCode"].ToString();
                    comboBox2.Items.Add(item);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox6.Text = openFileDialog1.SafeFileName;
                pictureBox2.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || comboBox1.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("Одно или несколько обязательных полей для ввода (отмеченных знаком *) не были заполнены!", "Оповещение системы");
            }
            else
            {
                Regex emailRegex = new Regex(@"\w{2,10}@\w{2,10}.\w{2,10}");
                Match emailMatch = emailRegex.Match(textBox1.Text);

                if (emailMatch.Value == "")
                    MessageBox.Show("Некорректный формат email!", "Оповещение системы");
                else
                {
                    bool digit = false;
                    bool spec = false;
                    bool lowChar = false;
                    bool pass = false;

                    for(int i = 0; i < textBox2.TextLength; i++)
                    {
                        if(Char.IsDigit(textBox2.Text[i]))
                        {
                            digit = true;
                            break;
                        }
                    }

                    for (int i = 0; i < textBox2.TextLength; i++)
                    {
                        if (Char.IsLower(textBox2.Text[i]))
                        {
                            lowChar = true;
                            break;
                        }
                    }

                    for (int i = 0; i < textBox2.TextLength; i++)
                    {
                        if (textBox2.Text[i] == '#' || textBox2.Text[i] == '!' || textBox2.Text[i] == '@' || textBox2.Text[i] == '$' || textBox2.Text[i] == '%' || textBox2.Text[i] == '^')
                        {
                            spec = true;
                            break;
                        }
                    }

                    if (textBox2.TextLength < 6 || !spec || !digit || !lowChar)
                        MessageBox.Show("Некорректный формат пароля! Длина пароля должна быть не менее шести символов, из которых должна быть, как минимум, одна буква нижнего регистра, одна цифра и один из следующих символов: !,#,%,^,@", "Оповещение системы");
                    else
                    {
                        pass = true;
                    }

                    if(pass)
                    {
                        if(textBox2.Text != textBox3.Text)
                        {
                            MessageBox.Show("Пароли не совпадают!", "Оповещение системы");
                        }
                        else
                        {
                            DateTime dateOfBirth = Convert.ToDateTime(dateTimePicker1.Value);

                            if(DateTime.Now.Year - dateOfBirth.Year < 10)
                            {
                                MessageBox.Show("Возраст бегуна на момент регистрации должен быть не менее 10ти лет!", "Оповещение системы");
                            }
                            else
                            {

                              
                                    SqlConnection conn = new SqlConnection(Connection.GetString());
                                    conn.Open();

                                    SqlCommand command = new SqlCommand("INSERT INTO Users VALUES(@e,@p,@l,@f,@r,@pp)", conn);

                                    command.Parameters.Add("@e", textBox1.Text);
                                    command.Parameters.Add("@p", textBox2.Text);
                                    command.Parameters.Add("@l", textBox4.Text);
                                    command.Parameters.Add("@f", textBox5.Text);
                                    command.Parameters.Add("@r", "R");
                                    command.Parameters.Add("@pp", textBox6.Text);

                                    command.ExecuteNonQuery();

                                    SqlCommand command2 = new SqlCommand("INSERT INTO Runner(Email, Gender, DateOfBirth, CountryCode) VALUES(@e, @g, @d, @c)", conn);

                                    command2.Parameters.Add("@e", textBox1.Text);
                                    command2.Parameters.Add("@g", (comboBox1.SelectedItem as ComboBoxItem).value);
                                    command2.Parameters.Add("@d", dateTimePicker1.Value);
                                    command2.Parameters.Add("@c", (comboBox2.SelectedItem as ComboBoxItem).value);

                                    command2.ExecuteNonQuery();

                                    try
                                    {
                                        Bitmap bmp = new Bitmap(openFileDialog1.FileName);
                                        bmp.Save("picture/" + openFileDialog1.SafeFileName);
                                    }
                                    catch(Exception ex)
                                    {

                                    }

                                    FormRegistrationMarathon fm = new FormRegistrationMarathon(textBox1.Text);
                                    fm.Show();
                                    this.Hide();
                             
                               


                            }
                        }
                    }
                }
            }
        }
    }
}
