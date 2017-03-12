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
    public partial class FormUsersList : Form
    {
        public string email;
        public FormUsersList(string email)
        {
            this.email = email;
            InitializeComponent();
        }

        private void FormUsersList_FormClosing(object sender, FormClosingEventArgs e)
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
            FormAdmin fm = new FormAdmin(email);
            fm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Hide();
        }

        private void FormUsersList_Load(object sender, EventArgs e)
        {
            timer1.Start();

            

            SqlConnection conn = new SqlConnection(Connection.GetString());
            conn.Open();

            SqlCommand commandCount = new SqlCommand("SELECT COUNT(Email) as c FROM Users", conn);

            using (SqlDataReader reader = commandCount.ExecuteReader())
            {
                while(reader.Read())
                {
                    label10.Text = reader["c"].ToString();
                }
            }

           SqlCommand commandRole = new SqlCommand("SELECT * FROM Role", conn);

            using (SqlDataReader reader = commandRole.ExecuteReader())
            {
                while (reader.Read())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.text = reader["RoleName"].ToString();
                    item.value = reader["RoleId"].ToString();
                    comboBox1.Items.Add(item);
                    comboBox1.Text = "Any";
                    comboBox2.Text = "Any";
                }
            }


            SqlCommand command = new SqlCommand("SELECT TOP(500) Users.FirstName, Users.LastName, Users.Email, Role.RoleName FROM Users, Role WHERE Users.RoleId = Role.RoleId ORDER BY Role.RoleId", conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                int z = 0;
                while(reader.Read())
                {
                    TextBox tx1 = new TextBox();
                    tx1.Size = textBox1.Size;
                    tx1.Font = textBox1.Font;
                    tx1.Location = new Point(textBox1.Location.X, textBox1.Location.Y + z);
                    tx1.Text = reader["LastName"].ToString();
                    tx1.ReadOnly = true;

                    panel1.Controls.Add(tx1);

                    TextBox tx2 = new TextBox();
                    tx2.Size = textBox2.Size;
                    tx2.Font = textBox2.Font;
                    tx2.Location = new Point(textBox2.Location.X, textBox2.Location.Y + z);
                    tx2.Text = reader["FirstName"].ToString();
                    tx2.ReadOnly = true;

                    panel1.Controls.Add(tx2);

                    TextBox tx3 = new TextBox();
                    tx3.Size = textBox3.Size;
                    tx3.Font = textBox3.Font;
                    tx3.Location = new Point(textBox3.Location.X, textBox3.Location.Y + z);
                    tx3.Text = reader["Email"].ToString();
                    tx3.ReadOnly = true;

                    panel1.Controls.Add(tx3);

                    TextBox tx4 = new TextBox();
                    tx4.Size = textBox4.Size;
                    tx4.Font = textBox4.Font;
                    tx4.Location = new Point(textBox4.Location.X, textBox4.Location.Y + z);
                    tx4.Text = reader["RoleName"].ToString();
                    tx4.ReadOnly = true;

                    panel1.Controls.Add(tx4);

                    Button bn = new Button();
                    bn.Size = button6.Size;
                    bn.Location = button6.Location;
                    bn.Text = "Edit";
                    bn.Font = button6.Font;
                    bn.Tag = reader["Email"].ToString();
                    bn.Location = new Point(button6.Location.X, button6.Location.Y + z);
                    bn.Click += (ee, aa) => {
                        //MessageBox.Show(bn.Tag.ToString() + " " + bn.TabIndex);

                        FormEditUser fm = new FormEditUser(email, bn.Tag.ToString());
                        fm.Show();
                        this.Hide();
                    };

                    panel1.Controls.Add(bn);
                    z += 26;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormAddUser fm = new FormAddUser(email);
            fm.Show();
            this.Hide();
        }
    }
}
