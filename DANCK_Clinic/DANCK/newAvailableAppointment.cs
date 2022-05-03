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
    public partial class newAvailableAppointment : Form
    {
        int doctorId;
        Form previous;
        MySqlConnection connection = new MySqlConnection("server=127.0.0.1;port=3306;user id=admin;password=industrialInformaticsProjectPassword;persistsecurityinfo=True;database=industrialinformaticsproject;SslMode=none");

        public newAvailableAppointment(int doctorId, Form previous)
        {
            InitializeComponent();
            this.doctorId = doctorId;
            this.previous = previous;
        }

        private void newAvailableAppointment_Load(object sender, EventArgs e)
        {
            label4.BackColor = System.Drawing.Color.Transparent;
            label4.Font = new System.Drawing.Font("Times New Roman", 20);
            this.CenterToScreen();
            ControlBox = false;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Font = new System.Drawing.Font("Times New Roman", 10);
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.Font = new System.Drawing.Font("Times New Roman", 10);
            label3.BackColor = System.Drawing.Color.Transparent;
            label3.Font = new System.Drawing.Font("Times New Roman", 10);

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "yyyy-MM-dd HH:mm:ss";

            MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE id=" + doctorId.ToString(), connection);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            string type = reader.GetString("type");
            connection.Close();
            if (type == "Doctor")
            {
                textBox1.Text = "Consult";
                textBox1.ReadOnly = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            previous.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string type = textBox1.Text;

            MySqlCommand command = new MySqlCommand("INSERT INTO available_appointments (id_doctor, date_time_start, date_time_stop, availability, appointment_type) VALUES (" + doctorId.ToString() + ", '" + this.dateTimePicker1.Text + "', '" + this.dateTimePicker2.Text + "', true, '" + type + "')", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            if (!textBox1.ReadOnly)
                textBox1.Clear();
            MessageBox.Show("Inserted!");


        }
    }
}
