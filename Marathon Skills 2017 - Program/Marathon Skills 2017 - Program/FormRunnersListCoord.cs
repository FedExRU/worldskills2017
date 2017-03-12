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
    public partial class FormRunnersListCoord : Form
    {
        public string email;
        public FormRunnersListCoord(string email)
        {
            this.email = email;
            InitializeComponent();
        }

        private void FormRunnersListCoord_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormCoordinator fm = new FormCoordinator(email);
            fm.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime marathonTime = new DateTime(2017, 3, 5, 6, 0, 0);
            TimeSpan totalTime = marathonTime - DateTime.Now;

            label3.Text = totalTime.Days + " дней " + totalTime.Hours + " часов и " + totalTime.Minutes + " минут до старта марафона!";
        }

        private void FormRunnersListCoord_Load(object sender, EventArgs e)
        {
            timer1.Start();
            comboBox3.Text = "Any";
            SqlConnection conn = new SqlConnection(Connection.GetString());
            conn.Open();

            SqlCommand commandCount = new SqlCommand("SELECT COUNT(Email) as c FROM Users WHERE RoleId = 'R'", conn);

            using (SqlDataReader reader = commandCount.ExecuteReader())
            {
                while(reader.Read())
                {
                    label10.Text = reader["c"].ToString();
                }
            }

            SqlCommand commandStatus = new SqlCommand("SELECT * FROM RegistrationStatus", conn);

            using (SqlDataReader reader = commandStatus.ExecuteReader())
            {
                while(reader.Read())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.text = reader["RegistrationStatus"].ToString();
                    item.value = reader["RegistrationStatusId"].ToString();
                    comboBox1.Items.Add(item);
                    comboBox1.Text = "Any";
                }
            }

            SqlCommand commandDistance = new SqlCommand("SELECT * FROM EventType", conn);

            using (SqlDataReader reader = commandDistance.ExecuteReader())
            {
                while (reader.Read())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.text = reader["EventTypeName"].ToString();
                    item.value = reader["EventTypeId"].ToString();
                    comboBox2.Items.Add(item);
                    comboBox2.Text = "Any";
                }
            }


            SqlCommand command = new SqlCommand("SELECT TOP(400) Registration.RegistrationId, Users.FirstName, Users.LastName, Users.Email,RegistrationStatus.RegistrationStatus FROM RegistrationStatus, Registration, Runner, Users WHERE Users.Email = Runner.Email AND Users.RoleId = 'R' AND Runner.RunnerId = Registration.RunnerId AND Registration.RegistrationStatusId = RegistrationStatus.RegistrationStatusId", conn);

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
                    tx4.Text = reader["RegistrationStatus"].ToString();
                    tx4.ReadOnly = true;

                    panel1.Controls.Add(tx4);

                    Button bn = new Button();
                    bn.Size = button6.Size;
                    bn.Location = button6.Location;
                    bn.Text = "Edit";
                    bn.Font = button6.Font;
                    bn.Tag = reader["Email"].ToString();
                    bn.TabIndex = Convert.ToInt32(reader["RegistrationId"]);
                    bn.Location = new Point(button6.Location.X, button6.Location.Y + z);
                    bn.Click += (ee, aa) => {
                        //MessageBox.Show(bn.Tag.ToString() + " " + bn.TabIndex);

                        FormRunnerProfileCoord fm = new FormRunnerProfileCoord(email, bn.Tag.ToString(), bn.TabIndex.ToString());
                        fm.Show();
                        this.Hide();
                    };

                    panel1.Controls.Add(bn);
                    z += 26;
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string status = "";

            if (comboBox1.Text != "" && comboBox1.Text != "Any")
                status = " AND Registration.RegistrationStatusId = '" + (comboBox1.SelectedItem as ComboBoxItem).value + "' ";

            string distance = "";
            string from = "";
            if (comboBox2.Text != "" && comboBox2.Text != "Any")
            {
                from = " RegistrationEvent, Event, EventType, ";
                distance = " AND Registration.RegistrationId = RegistrationEvent.RegistrationId AND RegistrationEvent.EventId = Event.EventId AND Event.EventTypeId = EventType.EventTypeId AND EventType.EventTypeId = '" + (comboBox2.SelectedItem as ComboBoxItem).value + "' ";
            }

            panel1.Controls.Clear();

            SqlConnection conn = new SqlConnection(Connection.GetString());
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT TOP(400) Users.FirstName, Users.LastName, Users.Email,RegistrationStatus.RegistrationStatus FROM "+from+" RegistrationStatus, Registration, Runner, Users WHERE Users.Email = Runner.Email AND Users.RoleId = 'R' AND Runner.RunnerId = Registration.RunnerId AND Registration.RegistrationStatusId = RegistrationStatus.RegistrationStatusId" + status + distance, conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                int z = 0;
                while (reader.Read())
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
                    tx4.Text = reader["RegistrationStatus"].ToString();
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
                        MessageBox.Show(bn.Tag.ToString());

                        //FormAddEditCharity fm = new FormAddEditCharity(email, bn.Tag.ToString());
                        //fm.Show();
                        //this.Hide();
                    };

                    panel1.Controls.Add(bn);
                    z += 26;
                }
            }
        }
    }
}
