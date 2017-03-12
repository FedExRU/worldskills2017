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
    public partial class FormRegistrationMarathon : Form
    {
        public string email;
        public int value;
        public string runnerId;
        public FormRegistrationMarathon(string email)
        {
            this.email = email;
            InitializeComponent();
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

        private void button4_Click(object sender, EventArgs e)
        {
            FormRunner fm = new FormRunner(email);
            fm.Show();
            this.Hide();
        }

        private void FormRegistrationMarathon_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        private void FormRegistrationMarathon_Load(object sender, EventArgs e)
        {
            timer1.Start();

            SqlConnection conn = new SqlConnection(Connection.GetString());
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Charity", conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.text = reader["CharityName"].ToString();
                    item.value = reader["CharityId"].ToString();

                    comboBox1.Items.Add(item);
                }
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar !=8)
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormRunner fm = new FormRunner(email);
            fm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox5.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Пожалуйста, выберете спонсора из числа благотворительных организаций и внесите сумму спонсорского взноса", "Оповещение системы");
            }
            else
            {
                if(!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked)
                {
                    MessageBox.Show("Пожалуйста, выберете, как минимум, один из представленных марафонов");
                }
                else
                {
                    SqlConnection conn = new SqlConnection(Connection.GetString());
                    conn.Open();

                    SqlCommand command = new SqlCommand("SELECT * FROM Runner WHERE Email = '" + email + "'", conn);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            runnerId = reader["RunnerId"].ToString();
                        }
                    }

                    string ko = "";
                    if (checkBox1.Checked)
                        ko = "A";
                    if (checkBox2.Checked)
                        ko = "B";
                    if (checkBox3.Checked)
                        ko = "C";

                    SqlCommand command2 = new SqlCommand("INSERT INTO Registration Values ("+runnerId+",'','"+ko+"','1','"+textBox5.Text+"','"+(comboBox1.SelectedItem as ComboBoxItem).value+"',"+value+")",conn);
     
                    command2.ExecuteNonQuery();

                    FormRegistrationMarathonConfirmation fm = new FormRegistrationMarathonConfirmation(email);
                    fm.Show();
                    this.Hide();

                }
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {
            FormSponsorshipCharityInfo fm = new FormSponsorshipCharityInfo(comboBox1.Text);
            fm.Show();
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            value = Convert.ToInt32(label9.Text);
            value += Convert.ToInt32(textBox5.Text);

            label9.Text = value.ToString();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
                value = Convert.ToInt32(label9.Text);
                value += 0;

                label9.Text = value.ToString();
            
          
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
           
                value = Convert.ToInt32(label9.Text);
                value += 20;

                label9.Text = value.ToString();
            
           
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
           
                value = Convert.ToInt32(label9.Text);
                value += 45;

                label9.Text = value.ToString();
            
          
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            value = Convert.ToInt32(label9.Text);
            value += 145;

            label9.Text = value.ToString();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            value = Convert.ToInt32(label9.Text);
            value += 75;

            label9.Text = value.ToString();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            value = Convert.ToInt32(label9.Text);
            value += 20;

            label9.Text = value.ToString();
        }
    }
}
