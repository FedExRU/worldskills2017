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
using System.IO;

namespace Marathon_Skills_2017___Program
{
    public partial class FormVolunteersLoading : Form
    {
        public string email;
        public FormVolunteersLoading(string email)
        {
            this.email = email;
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormVolunteers fm = new FormVolunteers(email);
            fm.Show();
            this.Hide();

        }

        private void FormVolunteersLoading_Load(object sender, EventArgs e)
        {
            timer1.Start();

            openFileDialog1.Filter = "CSV files|*.csv;";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Hide();
        }

        private void FormVolunteersLoading_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormVolunteers fm = new FormVolunteers(email);
            fm.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime marathonTime = new DateTime(2017, 3, 5, 6, 0, 0);
            TimeSpan totalTime = marathonTime - DateTime.Now;

            label3.Text = totalTime.Days + " дней " + totalTime.Hours + " часов и " + totalTime.Minutes + " минут до старта марафона!";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.SafeFileName;
                button3.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(Connection.GetString());
            conn.Open();
            string[] data = File.ReadAllLines(openFileDialog1.FileName);
            
            for(int i = 1; i < data.Length; i++)
            {
                
                String[] word = data[i].Split(new char[] {','});

                for(int j = 0; j < word.Length; j++)
                {
                    //MessageBox.Show(word[j]);
                    SqlCommand commanSearchId = new SqlCommand("SELECT * FROM Volunteer WHERE VolunteerId = '" + word[0] + "'", conn);
                    bool t = true;
                    using (SqlDataReader reader = commanSearchId.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            t = false;
                        }
                       
                    }

                    if(!t)
                    {
                        try
                        {
                            if (word[4] == "F")
                                word[4] = "Female";
                            if (word[4] == "M")
                                word[4] = "Male";

                            SqlCommand command1 = new SqlCommand("UPDATE Volunteer Set FirstName = '" + word[2] + "', LastName = '" + word[1] + "', CountryCode = '" + word[3] + "', Gender = '" + word[4] + "' WHERE VolunteerId = '" + word[0] + "'", conn);
                            command1.ExecuteNonQuery();
                        }
                        catch ( Exception ex)
                        {

                        }
                    }


                   if(t)
                    {
                        try
                        {
                            SqlCommand insert = new SqlCommand("SET IDENTITY_INSERT Volunteer ON", conn);
                            insert.ExecuteNonQuery();
                            SqlCommand command2 = new SqlCommand("SET IDENTITY_INSERT Volunteer ON; INSERT INTO Volunteer(VolunteerId, FirstName, LastName, CountryCode, Gender) Values(@id, @f, @l, @c, @g)", conn);

                            if (word[4] == "F")
                                word[4] = "Female";
                            if (word[4] == "M")
                                word[4] = "Male";

                            command2.Parameters.Add("@id", word[0]);
                            command2.Parameters.Add("@f", word[2]);
                            command2.Parameters.Add("@l", word[1]);
                            command2.Parameters.Add("@c", word[3]);
                            command2.Parameters.Add("@g", word[4]);

                            command2.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            
                        }
                    }
                }
            }

            MessageBox.Show("Данные о волонтерах успешно добавлены!", "Оповещение системы");
            FormVolunteers fm = new FormVolunteers(email);
            fm.Show();
            this.Hide();

        }
    }
}
