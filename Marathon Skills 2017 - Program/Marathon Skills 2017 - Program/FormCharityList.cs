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
    public partial class FormCharityList : Form
    {
        public FormCharityList()
        {
            InitializeComponent();
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

        private void FormCharityList_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        private void FormCharityList_Load(object sender, EventArgs e)
        {
            timer1.Start();

            SqlConnection conn = new SqlConnection(Connection.GetString());
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Charity", conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                int z = 0;

                while (reader.Read())
                {
                    PictureBox pb = new PictureBox();
                    pb.Size = pictureBox2.Size;
                    pb.Location = new Point(pictureBox2.Location.X, pictureBox2.Location.Y + z);
                    pb.BorderStyle = BorderStyle.Fixed3D;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;

                    if (reader["CharityLogo"].ToString() != "")
                        pb.Image = Image.FromFile("logo/" + reader["CharityLogo"].ToString());


                    Label lb = new Label();
                    lb.Size = label2.Size;
                    lb.Location = new Point(label2.Location.X, label2.Location.Y + z);
                    lb.Font = label2.Font;
                    lb.ForeColor = label2.ForeColor;
                    lb.Text = reader["CharityName"].ToString();

                    RichTextBox rb = new RichTextBox();
                    rb.Size = richTextBox1.Size;
                    rb.Location = new Point(richTextBox1.Location.X, richTextBox1.Location.Y + z);
                    rb.Font = richTextBox1.Font;
                    rb.ForeColor = richTextBox1.ForeColor;
                    rb.Text = reader["CharityDescription"].ToString();
                    rb.ReadOnly = true;

                    panel1.Controls.Add(pb);
                    panel1.Controls.Add(lb);
                    panel1.Controls.Add(rb);
                    z += 156;
                }
            }

               
        }
    }
}
