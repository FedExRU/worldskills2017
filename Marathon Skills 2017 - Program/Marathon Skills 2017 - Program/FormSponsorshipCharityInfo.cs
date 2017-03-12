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
    public partial class FormSponsorshipCharityInfo : Form
    {
        public string charityName;
        public FormSponsorshipCharityInfo(string charityName)
        {
            this.charityName = charityName;
            InitializeComponent();
        }

        private void FormSponsorshipCharityInfo_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(Connection.GetString()))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Charity WHERE CharityName = '" + charityName + "'", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            pictureBox1.Image = Image.FromFile("logo/" + reader["CharityLogo"].ToString());
                        }
                        catch (Exception ex){ }

                        richTextBox1.Text = reader["CharityDescription"].ToString();
                        textBox7.Text = reader["CharityName"].ToString();

                    }

                }
            }
                
        }
    }
}
