using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace Marathon_Skills_2017___Program
{
    public partial class FormAddUser : Form
    {
        public string email;
        public FormAddUser(string email)
        {
            this.email = email;
            InitializeComponent();
        }

        private void FormAddUser_FormClosing(object sender, FormClosingEventArgs e)
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
            FormUsersList fm = new FormUsersList(email);
            fm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Hide();
        }

        private void FormAddUser_Load(object sender, EventArgs e)
        {
            timer1.Start();

            SqlConnection conn = new SqlConnection(Connection.GetString());
            conn.Open();

            SqlCommand commandRole = new SqlCommand("SELECT * FROM Role", conn);

            using (SqlDataReader reader = commandRole.ExecuteReader())
            {
                while (reader.Read())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.text = reader["RoleName"].ToString();
                    item.value = reader["RoleId"].ToString();
                    comboBox1.Items.Add(item);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormUsersList fm = new FormUsersList(email);
            fm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || comboBox1.Text == "")
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
                    
                    if(pass)
                    {
                        if (textBox2.Text != textBox3.Text)
                        {
                            MessageBox.Show("Пароли не совпадают!", "Оповещение системы");
                        }
                        else
                        {
                            SqlConnection conn = new SqlConnection(Connection.GetString());
                            conn.Open();
                            SqlCommand command = new SqlCommand("INSERT INTO Users (Email, Password, FirstName, LastName, RoleId) Values (@e, @p, @f, @l, @r)", conn);

                            command.Parameters.Add("@e", textBox1.Text);
                            command.Parameters.Add("@p", textBox2.Text);
                            command.Parameters.Add("@f", textBox5.Text);
                            command.Parameters.Add("@l", textBox4.Text);
                            command.Parameters.Add("@r", (comboBox1.SelectedItem as ComboBoxItem).value);

                            command.ExecuteNonQuery();

                            MessageBox.Show("Пользователь успешно добавлен в Систему!", "Оповещение системы");

                            FormUsersList fm = new FormUsersList(email);
                            fm.Show();
                            this.Hide();
                        }
                    }
                }
            }
        }
    }
}
