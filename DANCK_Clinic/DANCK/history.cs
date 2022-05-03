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
    public partial class history : Form
    {
        int patientId, entriesLength = 0, doctorId;
        int[] entriesId = new int[1000];
        Form previous;
        MySqlConnection connection = new MySqlConnection("server=127.0.0.1;port=3306;user id=admin;password=industrialInformaticsProjectPassword;persistsecurityinfo=True;database=industrialinformaticsproject;SslMode=none");

        public history(Form previous, int patientId, int doctorId)
        {
            InitializeComponent();
            this.previous = previous;
            this.patientId = patientId;
            this.doctorId = doctorId;
        }

        private void history_Load(object sender, EventArgs e)
        {
            ControlBox = false;
            this.CenterToScreen();
            MySqlCommand command = new MySqlCommand("SELECT * FROM patients_info WHERE id_patient=" + patientId.ToString(), connection);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            string patientName = reader.GetString("first_name") + " " + reader.GetString("last_name");
            connection.Close();
            label1.Text = "Patient: " + patientName;
            string query = "SELECT * FROM medical_history JOIN medical_staff ON medical_history.id_staff=medical_staff.id_staff WHERE medical_history.id_patient=" + patientId.ToString();
            command = new MySqlCommand(query, connection);
            connection.Open();
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                string row = "";
                while (reader.Read())
                {
                    row = reader.GetString("entry_date_time") + " by " + reader.GetString("first_name") + " " + reader.GetString("last_name");
                    listBox1.Items.Add(row);
                    entriesId[entriesLength++] = Convert.ToInt32(reader.GetString("id_entry"));
                }
            }
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            previous.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length == 0)
                MessageBox.Show("No data to insert");
            else
            {
                DateTime currentTime = DateTime.Now;
                string dateTime = currentTime.Year.ToString() + '-' + currentTime.Month.ToString() + '-' + currentTime.Day.ToString() + ' ' + currentTime.Hour.ToString() + ':' + currentTime.Minute.ToString() + ':' + currentTime.Second.ToString();

                connection.Open();
                string query = "INSERT INTO medical_history (id_patient, id_staff, entry_date_time, data) VALUES (" + patientId.ToString() + ", " + doctorId.ToString() + ", '" + dateTime + "', '" + richTextBox1.Text + "')";
                MySqlCommand commmand = new MySqlCommand(query, connection);
                commmand.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Entry inserted!", "Success");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                MySqlCommand command = new MySqlCommand("SELECT data FROM medical_history WHERE id_entry=" + entriesId[listBox1.SelectedIndex].ToString(), connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                reader.Read();
                string entry = reader.GetString("data");
                connection.Close();

                richTextBox1.Text = entry;
            }
        }
    }
}
