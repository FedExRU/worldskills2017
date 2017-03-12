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
    public partial class FormSponsorshipConfirmation : Form
    {
        public string runnerId;
        public string sponsorshipTarget;
        public string charityName;
        public FormSponsorshipConfirmation(string runnerId, string charityName, string sponsorshipTarget)
        {
            this.runnerId = runnerId;
            this.sponsorshipTarget = sponsorshipTarget;
            this.charityName = charityName;
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

        private void FormSponsorshipConfirmation_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();

        }

        private void FormSponsorshipConfirmation_Load(object sender, EventArgs e)
        {
            timer1.Start();

            textBox2.ForeColor = Color.Gray;

            SqlConnection conn = new SqlConnection(Connection.GetString());
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT Users.FirstName, Users.LastName, Country.CountryName, RegistrationEvent.BibNumber FROM Users, Runner, Country, Registration, RegistrationEvent WHERE Country.CountryCode = Runner.CountryCode AND Runner.Email = Users.Email AND Runner.RunnerId = '" + runnerId + "' AND Registration.RunnerId = Runner.RunnerId AND Registration.RegistrationId = RegistrationEvent.RegistrationId", conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    textBox1.Text = reader["FirstName"].ToString() + " " + reader["LastName"].ToString() + " (" + reader["BibNumber"].ToString() + ") " + "из " + reader["CountryName"].ToString();

                    textBox2.Text = charityName;

                    label20.Text = "$" + sponsorshipTarget;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Hide();
        }
    }
}
