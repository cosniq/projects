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
    public partial class appointmentMaker : Form
    {
        int idDoctor, idPatient, timeSlotLength = 0;
        int[] idTimeSlot = new int[100];
        Form previousForm;
        String type;
        bool appointmentToItself;
        MySqlConnection connection = new MySqlConnection("server=127.0.0.1;port=3306;user id=admin;password=industrialInformaticsProjectPassword;persistsecurityinfo=True;database=industrialinformaticsproject;SslMode=none");

        public appointmentMaker(int idDoctor, int idPatient, Form previousForm, string type)
        {
            InitializeComponent();
            this.idDoctor = idDoctor;
            this.idPatient = idPatient;
            this.previousForm = previousForm;
            this.type = type;
        }

        public appointmentMaker(int idDoctor, int idPatient, Form previousForm, string type, bool appointmentToItself)
        {
            InitializeComponent();
            this.idDoctor = idDoctor;
            this.idPatient = idPatient;
            this.previousForm = previousForm;
            this.type = type;
            this.appointmentToItself = appointmentToItself;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            previousForm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                MySqlCommand command = new MySqlCommand("UPDATE available_appointments SET availability=false WHERE id_time_slot=" + idTimeSlot[listBox1.SelectedIndex].ToString(), connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                command = new MySqlCommand("INSERT INTO appointments (id_time_slot, id_patient) values (" + idTimeSlot[listBox1.SelectedIndex].ToString() + ", " + idPatient.ToString() + ")", connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                MessageBox.Show("Appointment Reserved");
            }
            else
                MessageBox.Show("Please select an appointment", "Warning");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                button1.Enabled = true;
            } else
            {
                button1.Enabled = false;
            }
        }

        private void labelFiller()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM patients_info WHERE id_patient = " + idPatient.ToString(), connection);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            label2.Text = "Patient's name: " + reader.GetString("first_name") + " " + reader.GetString("last_name");
            connection.Close();
            command = new MySqlCommand("SELECT * FROM medical_staff JOIN departments ON medical_staff.id_department=departments.id_department WHERE id_staff = " + idDoctor.ToString(), connection);
            connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            label3.Text = "Doctor's name: " + reader.GetString("first_name") + " " + reader.GetString("last_name") + " (" + reader.GetString("name") + ")";
            connection.Close();

            label2.BackColor = System.Drawing.Color.Transparent;
            label2.Font = new System.Drawing.Font("Times New Roman", 12);
            label3.BackColor = System.Drawing.Color.Transparent;
            label3.Font = new System.Drawing.Font("Times New Roman", 12);
        }

        private void appointmentMaker_Load(object sender, EventArgs e)
        {
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Font = new System.Drawing.Font("Times New Roman", 20);
            this.CenterToScreen();
            ControlBox = false;
            labelFiller();

            MySqlCommand command = null;
            if (type != "Specialist" || (type=="Specialist" && !appointmentToItself))
            {
               command = new MySqlCommand("SELECT * FROM available_appointments WHERE id_doctor=" + this.idDoctor.ToString() + " AND availability=true AND appointment_type='Consult'", connection);    
            }
            else
            {
                command = new MySqlCommand("SELECT * FROM available_appointments WHERE id_doctor=" + idDoctor.ToString() + " AND availability = true", connection);
            }
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                string row = "";
                while (reader.Read())
                {
                    idTimeSlot[timeSlotLength++] = Convert.ToInt32(reader.GetString("id_time_slot"));
                    row = reader.GetString("date_time_start") + " - " + reader.GetString("date_time_stop") + " Type: " + reader.GetString("appointment_type");
                    listBox1.Items.Add(row);
                }
            }
            connection.Close();
        }
    }
}
