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
    public partial class FormCharityListCoord : Form
    {
        public string email;
        public FormCharityListCoord(string email)
        {
            this.email = email;
            InitializeComponent();
        }

        private void FormCharityListCoord_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime marathonTime = new DateTime(2017, 3, 5, 6, 0, 0);
            TimeSpan totalTime = marathonTime - DateTime.Now;

            label3.Text = totalTime.Days + " дней " + totalTime.Hours + " часов и " + totalTime.Minutes + " минут до старта марафона!";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Hide();
        }

        private void FormCharityListCoord_Load(object sender, EventArgs e)
        {
            timer1.Start();
            comboBox1.Text = "Any";
            SqlConnection conn = new SqlConnection(Connection.GetString());
            conn.Open();

            SqlCommand cmd1 = new SqlCommand("SELECT COUNT(CharityId) as c from Charity", conn);

            using (SqlDataReader reader = cmd1.ExecuteReader())
            {
                while(reader.Read())
                {
                    label2.Text += reader["c"].ToString();
                }
            }

            SqlCommand cmd2 = new SqlCommand("SELECT SUM(SponsorshipTarget) as c from Registration", conn);

            using (SqlDataReader reader = cmd2.ExecuteReader())
            {
                while (reader.Read())
                {
                    label4.Text += "$" + reader["c"].ToString();
                }
            }

            SqlCommand command = new SqlCommand("SELECT SUM(Registration.SponsorshipTarget) as c, Charity.CharityName, Charity.CharityLogo FROM Charity, Registration WHERE Charity.CharityId = Registration.CharityId GROUP BY Charity.CharityName, Charity.CharityLogo", conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                int z = 0;
                while(reader.Read())
                {
                    PictureBox pb = new PictureBox();
                    pb.Size = pictureBox2.Size;
                    pb.Location = new Point(pictureBox2.Location.X, pictureBox2.Location.Y + z);
                    pb.BorderStyle = BorderStyle.Fixed3D;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;

                    if (reader["CharityLogo"].ToString() != "")
                        pb.Image = Image.FromFile("logo/" + reader["CharityLogo"].ToString());

                    RichTextBox rb = new RichTextBox();
                    rb.Size = richTextBox1.Size;
                    rb.Location = new Point(richTextBox1.Location.X, richTextBox1.Location.Y + z);
                    rb.Font = richTextBox1.Font;
                    rb.ForeColor = richTextBox1.ForeColor;
                    rb.Text = reader["CharityName"].ToString();
                    rb.ReadOnly = true;

                    RichTextBox rb2 = new RichTextBox();
                    rb2.Size = richTextBox2.Size;
                    rb2.Location = new Point(richTextBox2.Location.X, richTextBox2.Location.Y + z);
                    rb2.Font = richTextBox2.Font;
                    rb2.ForeColor = richTextBox2.ForeColor;
                    rb2.Text = "$" + reader["c"].ToString();
                    rb2.ReadOnly = true;

                    panel1.Controls.Add(pb);
                    panel1.Controls.Add(rb);
                    panel1.Controls.Add(rb2);
                    z += 105;
                }

                
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormCoordinator fm = new FormCoordinator(email);
            fm.Show();
            this.Hide();
        }
    }
}
