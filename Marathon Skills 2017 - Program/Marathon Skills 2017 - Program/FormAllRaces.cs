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
    public partial class FormAllRaces : Form
    {
        public FormAllRaces()
        {
            InitializeComponent();
        }

        private void FormAllRaces_FormClosing(object sender, FormClosingEventArgs e)
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
            FormMoreAboutMarathonSkills fm = new FormMoreAboutMarathonSkills();
            fm.Show();
            this.Hide();
        }

        private void FormAllRaces_Load(object sender, EventArgs e)
        {
            timer1.Start();

            SqlConnection conn = new SqlConnection(Connection.GetString());
            conn.Open();

            SqlCommand commandMarathon = new SqlCommand("SELECT Marathon.MarathonId, Marathon.YearHeld, Country.CountryName FROM Marathon, Country WHERE Marathon.CountryCode = Country.CountryCode", conn);

            using (SqlDataReader reader = commandMarathon.ExecuteReader())
            {
                while(reader.Read())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.text = reader["YearHeld"].ToString() + " - " + reader["CountryName"].ToString();
                    item.value = reader["MarathonId"].ToString();
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

            SqlCommand commandGender = new SqlCommand("SELECT * FROM Gender", conn);

            using (SqlDataReader reader = commandGender.ExecuteReader())
            {
                while (reader.Read())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.text = reader["Gender"].ToString();
                    item.value = reader["Gender"].ToString();
                    comboBox4.Items.Add(item);
                    comboBox4.Text = "Any";
                }
            }

            comboBox3.Text = "Any";

            SqlCommand commandCountRunners = new SqlCommand("SELECT Count(Email) as c FROM Users WHERE RoleId = 'R'", conn);

            using (SqlDataReader reader = commandCountRunners.ExecuteReader())
            {
                while (reader.Read())
                {
                    label11.Text = reader["c"].ToString();
                }
            }

            SqlCommand commandCountRunnersFinish = new SqlCommand("SELECT Count(Users.Email) as c FROM Users, Runner, Registration, RegistrationEvent WHERE RoleId = 'R' AND Users.Email = Runner.Email AND Registration.RunnerId = Runner.RunnerId AND Registration.RegistrationId = RegistrationEvent.RegistrationId AND RegistrationEvent.RaceTime is not null", conn);

            using (SqlDataReader reader = commandCountRunnersFinish.ExecuteReader())
            {
                while (reader.Read())
                {
                    label12.Text = reader["c"].ToString();
                }
            }

            SqlCommand commandAverageTime= new SqlCommand("SELECT AVG(RaceTime) as c FROM RegistrationEvent WHERE RaceTime is not null", conn);

            using (SqlDataReader reader = commandAverageTime.ExecuteReader())
            {
                while (reader.Read())
                {
                    TimeSpan sp = TimeSpan.FromSeconds(Convert.ToDouble(reader["c"].ToString()));
                    label10.Text = sp.Hours + "hh " + sp.Minutes + "mm " + sp.Seconds + "ss ";
                }
            }

            SqlCommand command = new SqlCommand("SELECT TOP(500) Users.FirstName, Users.LastName, Country.CountryName, RegistrationEvent.RaceTime FROM Users, RegistrationEvent, Registration, Runner, Country WHERE Users.Email = Runner.Email AND Runner.CountryCode = Country.CountryCode AND Registration.RunnerId = Runner.RunnerId AND RegistrationEvent.RegistrationId = Registration.RegistrationId AND RegistrationEvent.RaceTime is not null AND RegistrationEvent.RaceTime !=0 ORDER BY RegistrationEvent.RaceTime", conn);
            //, Users.FirstName, Users.LastName, Country.CountryName FROM Registration, RegistrationEvent, Users, Runner, Country WHERE  AND   ORDER BY RegistrationEvent.RaceTime
            using (SqlDataReader reader = command.ExecuteReader())
            {
                int z = 0;
                int i = 1;
                while(reader.Read())
                {
                    TextBox tx1 = new TextBox();
                    tx1.Size = textBox1.Size;
                    tx1.Font = textBox1.Font;
                    tx1.Location = new Point(textBox1.Location.X, textBox1.Location.Y + z);
                    tx1.Text = i.ToString();
                    tx1.ReadOnly = true;
                    
                    TextBox tx2 = new TextBox();
                    tx2.Size = textBox2.Size;
                    tx2.Font = textBox2.Font;
                    tx2.Location = new Point(textBox2.Location.X, textBox2.Location.Y + z);
                    
                    TimeSpan sp = TimeSpan.FromSeconds(Convert.ToDouble(reader["RaceTime"].ToString()));
                    tx2.Text = sp.Hours + "hh " + sp.Minutes + "mm " + sp.Seconds + "ss ";
                    tx2.ReadOnly = true;
                    
                    TextBox tx3 = new TextBox();
                    tx3.Size = textBox3.Size;
                    tx3.Font = textBox3.Font;
                    tx3.Location = new Point(textBox3.Location.X, textBox3.Location.Y + z);
                    tx3.Text = reader["LastName"].ToString() + " " + reader["FirstName"].ToString(); 
                    tx3.ReadOnly = true;
                    
                    TextBox tx4 = new TextBox();
                    tx4.Size = textBox4.Size;
                    tx4.Font = textBox4.Font;
                    tx4.Location = new Point(textBox4.Location.X, textBox4.Location.Y + z);
                    tx4.Text = reader["CountryName"].ToString();
                    tx4.ReadOnly = true;
                    
                    panel1.Controls.Add(tx1);
                    panel1.Controls.Add(tx2);
                    panel1.Controls.Add(tx3);
                    panel1.Controls.Add(tx4);

                    i++;
                    z += 30;
                }
            }
        }
    }
}
