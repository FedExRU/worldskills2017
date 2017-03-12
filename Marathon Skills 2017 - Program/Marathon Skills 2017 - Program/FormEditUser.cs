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
    public partial class FormEditUser : Form
    {
        public string email;
        public string userEmail;
        public FormEditUser(string email, string userEmail)
        {
            this.email = email;
            this.userEmail = userEmail;
            InitializeComponent();
        }

        private void FormEditUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
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

        private void button2_Click(object sender, EventArgs e)
        {
            FormUsersList fm = new FormUsersList(email);
            fm.Show();
            this.Hide();
        }

        private void FormEditUser_Load(object sender, EventArgs e)
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

            SqlCommand command = new SqlCommand("SELECT Users.FirstName, Users.LastName, Users.Email, Role.RoleName FROM Users, Role WHERE Users.RoleId = Role.RoleId AND Users.Email = '" + userEmail+ "'", conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    label6.Text = reader["Email"].ToString(); ;
                    textBox4.Text = reader["LastName"].ToString();
                    textBox5.Text = reader["FirstName"].ToString();
                    comboBox1.Text = reader["RoleName"].ToString();
                 
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool changePass = false;

            if (textBox2.Text != "" || textBox3.Text != "")
            {
                changePass = true;
            }

            if (textBox4.Text == "" || textBox5.Text == "" || comboBox1.Text == "")
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

                if (changePass == false)
                {
                   



                        SqlConnection conn = new SqlConnection(Connection.GetString());
                        conn.Open();

                        SqlCommand command = new SqlCommand("UPDATE Users Set FirstName = '" + textBox5.Text + "', LastName = '" + textBox4.Text + "', RoleId = '"+(comboBox1.SelectedItem as ComboBoxItem).value+"' WHERE Email = '" + userEmail + "'", conn);

                        command.ExecuteNonQuery();

                        if (cpass)
                        {
                            SqlCommand command4 = new SqlCommand("UPDATE Users Set Password = '" + textBox2.Text + "' WHERE Email = '" + email + "'", conn);

                            command4.ExecuteNonQuery();
                        }

                        MessageBox.Show("Профиль пользователя успешно изменен!", "Оповещение системы");

                        FormUsersList fm = new FormUsersList(email);
                        fm.Show();
                        this.Hide();

                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime marathonTime = new DateTime(2017, 3, 5, 6, 0, 0);
            TimeSpan totalTime = marathonTime - DateTime.Now;

            label3.Text = totalTime.Days + " дней " + totalTime.Hours + " часов и " + totalTime.Minutes + " минут до старта марафона!";
        }
    }
}
