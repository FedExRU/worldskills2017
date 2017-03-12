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
    public partial class FormSignup : Form
    {
        public FormSignup()
        {
            InitializeComponent();
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

        private void FormSignup_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        private void FormSignup_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Одно или несколько обязательных полей для ввода (отмеченных знаком *) не были заполнены", "Оповещение системы");
            }
            else
            {
                SqlConnection conn = new SqlConnection(Connection.GetString());
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE Email = '" + textBox1.Text + "' AND Password = '" + textBox2.Text + "'", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        MessageBox.Show("Пользователь с таким email и паролем не найден!. Удостоверьтесь в корректности введенных данных.", "Оповещение системы");
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            if (reader["RoleId"].ToString() == "R")
                            {
                                //MessageBox.Show("Runner");
                                FormRunner fm = new FormRunner(reader["Email"].ToString());
                                fm.Show();
                                this.Hide();
                            }
                            if (reader["RoleId"].ToString() == "A")
                            {
                                //MessageBox.Show("Runner");
                                FormAdmin fm = new FormAdmin(reader["Email"].ToString());
                                fm.Show();
                                this.Hide();
                            }
                            if (reader["RoleId"].ToString() == "C")
                            {
                                //MessageBox.Show("Runner");
                                FormCoordinator fm = new FormCoordinator(reader["Email"].ToString());
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
