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
    public partial class FormPrintSert : Form
    {
        public string email;
        public string runnerEmail;
        public string registrationId;
        public FormPrintSert(string email, string runnerEmail, string registrationId)
        {
            this.email = email;
            this.runnerEmail = runnerEmail;
            this.registrationId = registrationId;
            InitializeComponent();
        }

        private void FormPrintSert_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        private void FormPrintSert_Load(object sender, EventArgs e)
        {
            timer1.Start();

            SqlConnection conn = new SqlConnection(Connection.GetString());
            conn.Open();

            SqlCommand commandRace = new SqlCommand("SELECT DISTINCT EventType.EventTypeName, EventType.EventTypeId FROM Users, Runner, Registration, RegistrationEvent, Event, EventType, Marathon WHERE Users.Email = Runner.Email AND Runner.RunnerId = Registration.RunnerId AND RegistrationEvent.RegistrationId = Registration.RegistrationId AND RegistrationEvent.EventId = Event.EventId AND Event.MarathonId = 4 AND EventType.EventTypeId = Event.EventTypeId AND Users.Email = '"+runnerEmail+"'", conn);

            using (SqlDataReader reader = commandRace.ExecuteReader())
            {
                while(reader.Read())
                {
                    if(reader.HasRows)
                    {
                        ComboBoxItem item = new ComboBoxItem();
                        item.text = reader["EventTypeName"].ToString();
                        item.value = reader["EventTypeId"].ToString();
                        comboBox1.Items.Add(item);
                        comboBox1.Text = reader["EventTypeName"].ToString();
                    }
                    else
                    {
                        ComboBoxItem item = new ComboBoxItem();
                        item.text = "Данные о типе марафона отсутствуют";
                        item.value = " ";
                        comboBox1.Items.Add(item);
                        comboBox1.Text = "Данные о типе марафона отсутствуют";
                    }
                }
            }

            

            try
            {
                SqlCommand command = new SqlCommand("SELECT DISTINCT EventType.EventTypeName, Users.FirstName, Users.LastName, RegistrationEvent.RaceTime, RegistrationEvent.RegistrationEventId, Registration.SponsorshipTarget, EventType.EventTypeName, Registration.RegistrationId FROM Users, Runner, Registration, RegistrationEvent, Event, EventType, Marathon WHERE Users.Email = Runner.Email AND Runner.RunnerId = Registration.RunnerId AND RegistrationEvent.RegistrationId = Registration.RegistrationId AND RegistrationEvent.EventId = Event.EventId AND Event.MarathonId = 4 AND EventType.EventTypeId = Event.EventTypeId AND Users.Email = '" + runnerEmail + "' AND EventType.EventTypeId = '" + (comboBox1.SelectedItem as ComboBoxItem).value + "' AND RegistrationEvent.RaceTime is not null", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            TimeSpan ts = TimeSpan.FromSeconds(Convert.ToDouble(reader["RaceTime"]));
                            label8.Text = "Поздравляем " + reader["LastName"].ToString() + " " + reader["FirstName"].ToString() + " с участием " + reader["EventTypeName"].ToString() + "! Ваши\n результаты " + ts.Hours + "hh " + ts.Minutes + "mm " + ts.Seconds + "ss и занятое место " + reader["RegistrationEventId"].ToString() + "!";
                            label7.Text = "Вы также заработали $" + reader["SponsorshipTarget"].ToString() + "\n для своей благотворительной организации!";
                        }
                        else
                        {
                            panel1.Visible = false;
                            MessageBox.Show("У бегуна отсутствуют результаты за Marathon Skills 2014, Osaka, Japan!", "Оповещение системы");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            if(comboBox1.Text == "")
            {
                panel1.Visible = false;
                MessageBox.Show("У бегуна отсутствуют результаты за Marathon Skills 2014, Osaka, Japan!", "Оповещение системы");
            }

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime marathonTime = new DateTime(2017, 3, 5, 6, 0, 0);
            TimeSpan totalTime = marathonTime - DateTime.Now;

            label3.Text = totalTime.Days + " дней " + totalTime.Hours + " часов и " + totalTime.Minutes + " минут до старта марафона!";
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(Connection.GetString());
            conn.Open();

            
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(Connection.GetString());
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("SELECT DISTINCT EventType.EventTypeName, Users.FirstName, Users.LastName, RegistrationEvent.RaceTime, RegistrationEvent.RegistrationEventId, Registration.SponsorshipTarget, EventType.EventTypeName, Registration.RegistrationId FROM Users, Runner, Registration, RegistrationEvent, Event, EventType, Marathon WHERE Users.Email = Runner.Email AND Runner.RunnerId = Registration.RunnerId AND RegistrationEvent.RegistrationId = Registration.RegistrationId AND RegistrationEvent.EventId = Event.EventId AND Event.MarathonId = 4 AND EventType.EventTypeId = Event.EventTypeId AND Users.Email = '" + runnerEmail + "' AND EventType.EventTypeId = '"+(comboBox1.SelectedItem as ComboBoxItem).value +"' AND RegistrationEvent.RaceTime is not null", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if(reader.HasRows)
                        {
                            TimeSpan ts = TimeSpan.FromSeconds(Convert.ToDouble(reader["RaceTime"]));
                            label8.Text = "Поздравляем " + reader["LastName"].ToString() + " " + reader["FirstName"].ToString() + " с участием " + reader["EventTypeName"].ToString() + "! Ваши\n результаты " + ts.Hours + "hh " + ts.Minutes + "mm " + ts.Seconds + "ss и занятое место " + reader["RegistrationEventId"].ToString() + "!";
                            label7.Text = "Вы также заработали $" + reader["SponsorshipTarget"].ToString() + "\n для своей благотворительной организации!";
                        }
                        else
                        {
                            panel1.Visible = false;
                            MessageBox.Show("У бегуна отсутствуют результаты за Marathon Skills 2014, Osaka, Japan!", "Оповещение системы");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
