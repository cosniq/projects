using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DANCK
{
    public partial class PasswordChanger : Form
    {
        Form previous;
        string type;
        int[] ids = new int[1000];
        int idLength = 0;
        MySqlConnection connection = new MySqlConnection("server=127.0.0.1;port=3306;user id=admin;password=industrialInformaticsProjectPassword;persistsecurityinfo=True;database=industrialinformaticsproject;SslMode=none");

        public PasswordChanger(string type, Form previous)
        {
            InitializeComponent();
            this.type = type;
            this.previous = previous;
        }

        private void PasswordChanger_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Font = new System.Drawing.Font("Times New Roman", 10);
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.Font = new System.Drawing.Font("Times New Roman", 10);

            MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE type='" + type + "'", connection);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetString("username"));
                    ids[idLength++] = Convert.ToInt32(reader.GetString("id"));
                }
            }
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            previous.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1 && textBox1.Text.Length != 0)
            {
                MySqlCommand command = new MySqlCommand("UPDATE users SET password='" + textBox1.Text + "' WHERE id=" + ids[comboBox1.SelectedIndex].ToString(), connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Password changed!", "Success");
            }
            else
                MessageBox.Show("Error!");
        }
    }
}
