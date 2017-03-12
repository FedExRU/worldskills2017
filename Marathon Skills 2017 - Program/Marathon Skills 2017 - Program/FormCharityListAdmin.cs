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
    public partial class FormCharityListAdmin : Form
    {
        public string email;
        public FormCharityListAdmin(string email)
        {
            this.email = email;
            InitializeComponent();
        }

        private void FormCharityListAdmin_FormClosing(object sender, FormClosingEventArgs e)
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

        private void FormCharityListAdmin_Load(object sender, EventArgs e)
        {
            timer1.Start();

            SqlConnection conn = new SqlConnection(Connection.GetString());
            conn.Open();


            SqlCommand command = new SqlCommand("SELECT * FROM Charity ORDER BY CharityId DESC", conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                int z = 0;
                while(reader.Read())
                {
                    PictureBox pb = new PictureBox();
                    pb.Size = pictureBox2.Size;
                    pb.Location = new Point(pictureBox2.Location.X, pictureBox2.Location.Y + z);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    pb.BorderStyle = BorderStyle.Fixed3D;

                    if (reader["CharityLogo"].ToString() != "")
                    {
                        pb.Image = Image.FromFile("logo/" + reader["CharityLogo"].ToString());
                    }

                    RichTextBox rch1 = new RichTextBox();
                    rch1.Text = reader["CharityName"].ToString();
                    rch1.Size = richTextBox1.Size;
                    rch1.Location = new Point(richTextBox1.Location.X, richTextBox1.Location.Y + z);
                    rch1.Font = richTextBox1.Font;
                    rch1.ReadOnly = true;

                    RichTextBox rch2 = new RichTextBox();
                    rch2.Text = reader["CharityDescription"].ToString();
                    rch2.Size = richTextBox2.Size;
                    rch2.Location = new Point(richTextBox2.Location.X, richTextBox2.Location.Y + z);
                    rch2.Font = richTextBox2.Font;
                    rch2.ReadOnly = true;

                    Panel pn = new Panel();
                    pn.Size = panel6.Size;
                    pn.Location = new Point(panel6.Location.X, panel6.Location.Y + z);
                    pn.BorderStyle = BorderStyle.Fixed3D;

                    Button bn = new Button();
                    bn.Size = button2.Size;
                    bn.Location = button2.Location;
                    bn.Text = "Edit";
                    bn.Font = button2.Font;
                    bn.Tag = reader["CharityId"].ToString();
                    bn.Click += (ee, aa) => {
                        //MessageBox.Show(bn.Tag.ToString());

                        FormAddEditCharity fm = new FormAddEditCharity(email, bn.Tag.ToString());
                        fm.Show();
                        this.Hide();
                    };

                    pn.Controls.Add(bn);

                    panel1.Controls.Add(pn);
                    panel1.Controls.Add(pb);
                    panel1.Controls.Add(rch1);
                    panel1.Controls.Add(rch2);
                    z += 122;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormAddEditCharity fm = new FormAddEditCharity(email);
            fm.Show();
            this.Hide();
        }
    }
}
