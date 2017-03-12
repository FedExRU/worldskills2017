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
    public partial class FormVolunteers : Form
    {
        public string email;
        public FormVolunteers(string email)
        {
            this.email = email;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormAdmin fm = new FormAdmin(email);
            fm.Show();
            this.Hide();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime marathonTime = new DateTime(2017, 3, 5, 6, 0, 0);
            TimeSpan totalTime = marathonTime - DateTime.Now;

            label3.Text = totalTime.Days + " дней " + totalTime.Hours + " часов и " + totalTime.Minutes + " минут до старта марафона!";
        }

        private void FormVolunteers_Load(object sender, EventArgs e)
        {
            timer1.Start();

            SqlConnection conn = new SqlConnection(Connection.GetString());

            conn.Open();
            int k = 0;

            SqlCommand command = new SqlCommand("SELECT Volunteer.FirstName, Volunteer.LastName, Volunteer.Gender, Country.CountryName FROM Volunteer, Country WHERE Volunteer.CountryCode = Country.CountryCode", conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                int z = 0;
                
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        k += 1;
                        TextBox tx1 = new TextBox();
                        tx1.Size = textBox1.Size;
                        tx1.Location = new Point(textBox1.Location.X, textBox1.Location.Y + z);
                        tx1.Font = textBox1.Font;
                        tx1.ReadOnly = true;
                        tx1.Text = reader["LastName"].ToString();
                        panel1.Controls.Add(tx1);

                        TextBox tx3 = new TextBox();
                        tx3.Size = textBox3.Size;
                        tx3.Location = new Point(textBox3.Location.X, textBox3.Location.Y + z);
                        tx3.Font = textBox3.Font;
                        tx3.ReadOnly = true;
                        tx3.Text = reader["FirstName"].ToString();
                        panel1.Controls.Add(tx3);

                        TextBox tx2 = new TextBox();
                        tx2.Size = textBox2.Size;
                        tx2.Location = new Point(textBox2.Location.X, textBox2.Location.Y + z);
                        tx2.Font = textBox2.Font;
                        tx2.ReadOnly = true;
                        tx2.Text = reader["CountryName"].ToString();
                        panel1.Controls.Add(tx2);

                        TextBox tx4 = new TextBox();
                        tx4.Size = textBox4.Size;
                        tx4.Location = new Point(textBox4.Location.X, textBox4.Location.Y + z);
                        tx4.Font = textBox4.Font;
                        tx4.ReadOnly = true;
                        tx4.Text = reader["Gender"].ToString();
                        panel1.Controls.Add(tx4);

                        z += 26;
                    }
                }
                else
                {
                    label12.Visible = true;
                }
            }


            label7.Text = k.ToString();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormVolunteersLoading fm = new FormVolunteersLoading(email);
            fm.Show();
            this.Hide();
        }

        private void FormVolunteers_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }
    }
}
