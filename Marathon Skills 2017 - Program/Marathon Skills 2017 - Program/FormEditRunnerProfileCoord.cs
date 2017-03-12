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
    public partial class FormEditRunnerProfileCoord : Form
    {
        public string email;
        public string runnerEmail;
        public string registrationId;
        public FormEditRunnerProfileCoord(string email, string runnerEmail, string registrationId)
        {
            this.email = email;
            this.runnerEmail = runnerEmail;
            this.registrationId = registrationId;
            InitializeComponent();
        }

        private void FormEditRunnerProfileCoord_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime marathonTime = new DateTime(2017, 3, 5, 6, 0, 0);
            TimeSpan totalTime = marathonTime - DateTime.Now;

            label3.Text = totalTime.Days + " дней " + totalTime.Hours + " часов и " + totalTime.Minutes + " минут до старта марафона!";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormRunnerProfileCoord fm = new FormRunnerProfileCoord(email, runnerEmail, registrationId);
            fm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Hide();
        }

        private void FormEditRunnerProfileCoord_Load(object sender, EventArgs e)
        {
            timer1.Start();

            openFileDialog1.Filter = "Image Files |*.jpg;*.png;*gif;";

            SqlConnection conn = new SqlConnection(Connection.GetString());
            conn.Open();

            SqlCommand commandGender = new SqlCommand("SELECT * FROM Gender", conn);

            using (SqlDataReader reader = commandGender.ExecuteReader())
            {
                while (reader.Read())
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

            SqlCommand commandStatus = new SqlCommand("SELECT * FROM RegistrationStatus ORDER BY RegistrationStatus", conn);

            using (SqlDataReader reader = commandStatus.ExecuteReader())
            {
                while (reader.Read())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.text = reader["RegistrationStatus"].ToString();
                    item.value = reader["RegistrationStatusId"].ToString();
                    comboBox3.Items.Add(item);
                }
            }

            SqlCommand command = new SqlCommand("SELECT RegistrationStatus.RegistrationStatus, Users.FirstName, Users.LastName, Country.CountryName, Runner.Gender, Runner.DateOfBirth, Users.Picture FROM RegistrationStatus, Registration, Users, Country, Runner WHERE Users.Email = Runner.Email AND Runner.CountryCode = Country.CountryCode AND Users.Email = '" + runnerEmail + "' AND Registration.RegistrationId = '"+registrationId+ "' AND Registration.RegistrationStatusId = RegistrationStatus.RegistrationStatusId", conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    label5.Text = email;
                    textBox4.Text = reader["LastName"].ToString();
                    textBox5.Text = reader["FirstName"].ToString();
                    comboBox1.Text = reader["Gender"].ToString();
                    comboBox2.Text = reader["CountryName"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(reader["DateOfBirth"]);
                    textBox6.Text = reader["Picture"].ToString();
                    comboBox3.Text = reader["RegistrationStatus"].ToString();
                    try
                    {
                        pictureBox2.Image = Image.FromFile("picture/" + reader["Picture"]);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormRunnerProfileCoord fm = new FormRunnerProfileCoord(email, runnerEmail, registrationId);
            fm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bool changePass = false;

            if (textBox2.Text != "" || textBox3.Text != "")
            {
                changePass = true;
            }

            if (textBox4.Text == "" || textBox5.Text == "" || comboBox1.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("Одно или несколько обязательных полей для ввода (отмеченных знаком *) не были заполнены!", "Оповещение системы");
            }
            else
            {
                bool digit = false;
                bool spec = false;
                bool lowChar = false;
                bool pass = false;
                bool cpass = false;

                if (changePass)
                {


                    for (int i = 0; i < textBox2.TextLength; i++)
                    {
                        if (Char.IsDigit(textBox2.Text[i]))
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

                    if (pass)
                    {
                        if (textBox2.Text != textBox3.Text)
                        {
                            MessageBox.Show("Пароли не совпадают!", "Оповещение системы");
                        }
                        else
                        {
                            changePass = false;
                            cpass = true;
                        }
                    }
                }

                if(changePass == false)
                {
                    DateTime dateOfBirth = Convert.ToDateTime(dateTimePicker1.Value);

                    if (DateTime.Now.Year - dateOfBirth.Year < 10)
                    {
                        MessageBox.Show("Возраст бегуна на момент регистрации должен быть не менее 10ти лет!", "Оповещение системы");
                    }
                    else
                    {



                        SqlConnection conn = new SqlConnection(Connection.GetString());
                        conn.Open();

                        SqlCommand command = new SqlCommand("UPDATE Users Set FirstName = '" + textBox5.Text + "', LastName = '" + textBox4.Text + "', Picture = '" + textBox6.Text + "' WHERE Email = '" + runnerEmail + "'", conn);

                        command.ExecuteNonQuery();

                        SqlCommand command2 = new SqlCommand("UPDATE Runner Set Gender = '" + (comboBox1.SelectedItem as ComboBoxItem).value + "', CountryCode = '" + (comboBox2.SelectedItem as ComboBoxItem).value + "' WHERE Email = '" + runnerEmail + "'", conn);

                        command2.ExecuteNonQuery();

                        SqlCommand command3 = new SqlCommand("UPDATE Registration Set RegistrationStatusId = '" + (comboBox3.SelectedItem as ComboBoxItem).value + "' WHERE RegistrationId = '" + registrationId + "'", conn);

                        command3.ExecuteNonQuery();

                        if (cpass)
                        {
                            SqlCommand command4 = new SqlCommand("UPDATE Users Set Password = '" + textBox2.Text + "' WHERE Email = '" + email + "'", conn);

                            command4.ExecuteNonQuery();
                        }

                        try
                        {
                            Bitmap bmp = new Bitmap(openFileDialog1.FileName);
                            bmp.Save("picture/" + openFileDialog1.SafeFileName);
                        }
                        catch (Exception ex)
                        {

                        }

                        MessageBox.Show("Профиль бегуна успешно изменен!", "Оповещение системы");

                        FormRunnerProfileCoord fm = new FormRunnerProfileCoord(email, runnerEmail, registrationId);
                        fm.Show();
                        this.Hide();

                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox6.Text = openFileDialog1.SafeFileName;
                pictureBox2.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }
    }
}
