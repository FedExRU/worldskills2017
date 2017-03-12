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
    public partial class FormRunnerProfileCoord : Form
    {
        public string email;
        public string runnerEmail;
        public string registrationId;
        public FormRunnerProfileCoord(string email, string runnerEmail, string registrationId)
        {
            this.email = email;
            this.runnerEmail = runnerEmail;
            this.registrationId = registrationId;
            InitializeComponent();
        }

        private void FormRunnerProfileCoord_FormClosing(object sender, FormClosingEventArgs e)
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
            FormRunnersListCoord fm = new FormRunnersListCoord(email);
            fm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Hide();
        }

        private void FormRunnerProfileCoord_Load(object sender, EventArgs e)
        {
            timer1.Start();

            SqlConnection conn = new SqlConnection(Connection.GetString());
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT Users.Picture, Users.FirstName, Users.LastName, Users.Email, Runner.Gender, Runner.DateOfBirth, Country.CountryName, Charity.CharityName, Registration.SponsorshipTarget, RaceKitOption.RaceKitOption, EventType.EventTypeName, RegistrationStatus.RegistrationStatusId FROM RaceKitOption, Charity, EventType, Event, RegistrationEvent, Registration, RegistrationStatus, Runner, Country, Users WHERE Users.Email = Runner.Email AND Runner.RunnerId = Registration.RunnerId AND Registration.RegistrationId = RegistrationEvent.RegistrationId AND Registration.RaceKitOptionId = RaceKitOption.RaceKitOptionId AND Registration.RegistrationStatusId = RegistrationStatus.RegistrationStatusId AND RegistrationEvent.EventId = Event.EventId AND Event.EventTypeId = EventType.EventTypeID AND Country.CountryCode = Runner.CountryCode AND Users.Email = '" + runnerEmail+"' AND Registration.RegistrationId = '"+registrationId+"' AND Charity.CharityId = Registration.CharityId", conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    label19.Text = reader["Email"].ToString();
                    label20.Text = reader["LastName"].ToString();
                    label21.Text = reader["FirstName"].ToString();
                    label22.Text = reader["Gender"].ToString();
                    label23.Text = reader["DateOfBirth"].ToString();
                    label24.Text = reader["CountryName"].ToString();
                    label25.Text = reader["CharityName"].ToString();
                    label26.Text = "$"+reader["SponsorshipTarget"].ToString();
                    label27.Text = reader["RaceKitOption"].ToString();
                    label28.Text = reader["EventTypeName"].ToString();

                    try
                    {
                        pictureBox2.Image = Image.FromFile("picture/" + reader["Picture"].ToString());
                    }
                    catch(Exception ex)
                    {

                    }

                    if(reader["RegistrationStatusId"].ToString() == "1")
                    {
                        pictureBox3.Image = Properties.Resources.tick_icon;
                    }
                    if (reader["RegistrationStatusId"].ToString() == "2")
                    {
                        pictureBox3.Image = Properties.Resources.tick_icon;
                        pictureBox4.Image = Properties.Resources.tick_icon;
                    }
                    if (reader["RegistrationStatusId"].ToString() == "3")
                    {
                        pictureBox3.Image = Properties.Resources.tick_icon;
                        pictureBox4.Image = Properties.Resources.tick_icon;
                        pictureBox5.Image = Properties.Resources.tick_icon;
                    }
                    if (reader["RegistrationStatusId"].ToString() == "4")
                    {
                        pictureBox3.Image = Properties.Resources.tick_icon;
                        pictureBox4.Image = Properties.Resources.tick_icon;
                        pictureBox5.Image = Properties.Resources.tick_icon;
                        pictureBox6.Image = Properties.Resources.tick_icon;
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormEditRunnerProfileCoord fm = new FormEditRunnerProfileCoord(email, runnerEmail, registrationId);
            fm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormPrintSert fm = new FormPrintSert(email, runnerEmail, registrationId);
            fm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormPrintSert fm = new FormPrintSert(email, runnerEmail, registrationId);
            fm.Show();
            this.Hide();
        }
    }
}
