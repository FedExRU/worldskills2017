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
    public partial class FormAddEditCharity : Form
    {
        public string email;
        public string charityId;
        public FormAddEditCharity(string email, string charityId = null)
        {
            this.email = email;
            this.charityId = charityId;
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormCharityListAdmin fm = new FormCharityListAdmin(email);
            fm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Hide();
        }

        private void FormAddEditCharity_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime marathonTime = new DateTime(2017, 3, 5, 6, 0, 0);
            TimeSpan totalTime = marathonTime - DateTime.Now;

            label3.Text = totalTime.Days + " дней " + totalTime.Hours + " часов и " + totalTime.Minutes + " минут до старта марафона!";
        }

        private void FormAddEditCharity_Load(object sender, EventArgs e)
        {
            timer1.Start();

            openFileDialog1.Filter = "Image files | *.jpg;*.jpeg;*.png;*.gif;*.bmp;";

            if(charityId != null)
            {
                SqlConnection conn = new SqlConnection(Connection.GetString());
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Charity WHERE CharityId = '" + charityId + "'", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        if(reader["CharityLogo"].ToString() != "")
                        {
                            pictureBox2.Image = Image.FromFile("logo/" + reader["CharityLogo"].ToString());
                            textBox2.Text = reader["CharityLogo"].ToString();
                        }
                        textBox1.Text = reader["CharityName"].ToString();
                        richTextBox1.Text = reader["CharityDescription"].ToString();
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormCharityListAdmin fm = new FormCharityListAdmin(email);
            fm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openFileDialog1.SafeFileName;
                pictureBox2.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("Поле 'Наименование' благотворительной организации обязательно для заполнения!", "Оповещение системы");

            }
            else
            {
                if(charityId != null)
                {
                    SqlConnection conn = new SqlConnection(Connection.GetString());
                    conn.Open();

                    SqlCommand command = new SqlCommand("UPDATE Charity Set CharityName = @name, CharityDescription = @desc,  CharityLogo = @logo WHERE CharityId = '"+charityId+"'", conn);
                    command.Parameters.Add("@name", textBox1.Text);
                    command.Parameters.Add("@desc", richTextBox1.Text);
                    command.Parameters.Add("@logo", textBox2.Text);

                    command.ExecuteNonQuery();

                   try
                    {
                        if (textBox2.Text != "")
                        {
                            Bitmap bmp = new Bitmap(openFileDialog1.FileName);
                            bmp.Save("logo/" + openFileDialog1.SafeFileName);
                        }
                    }
                    catch(Exception ex)
                    {

                    }

                    MessageBox.Show("Благотворительная организация " + textBox1.Text + " успешно обновлена!", "Оповещение системы");

                    FormCharityListAdmin fm = new FormCharityListAdmin(email);
                    fm.Show();
                    this.Hide();
                }
                else
                {
                    SqlConnection conn = new SqlConnection(Connection.GetString());
                    conn.Open();

                    SqlCommand command = new SqlCommand("INSERT INTO Charity Values(@name, @desc, @logo)", conn);
                    command.Parameters.Add("@name", textBox1.Text);
                    command.Parameters.Add("@desc", richTextBox1.Text);
                    command.Parameters.Add("@logo", textBox2.Text);

                    command.ExecuteNonQuery();

                   if(textBox2.Text != "")
                    {
                        Bitmap bmp = new Bitmap(openFileDialog1.FileName);
                        bmp.Save("logo/" + openFileDialog1.SafeFileName);
                    }

                    MessageBox.Show("Благотворительная организация " + textBox1.Text + " успешно добавлена!", "Оповещение системы");

                    FormCharityListAdmin fm = new FormCharityListAdmin(email);
                    fm.Show();
                    this.Hide();
                }
            }
        }
    }
}
