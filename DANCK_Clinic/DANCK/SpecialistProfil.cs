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
    public partial class SpecialistProfil : Form
    {
        int specialistId, patientsIdLength = 0, departmentLength = 0, doctorsLength = 0, appointmentLength = 0;
        int[] patientsId = new int[1000], departmentId = new int[100], doctorsId = new int[100], appointmentId = new int[100];
        string name;
        bool admin;
        MySqlConnection connection = new MySqlConnection("server=127.0.0.1;port=3306;user id=admin;password=industrialInformaticsProjectPassword;persistsecurityinfo=True;database=industrialinformaticsproject;SslMode=none");


        SpecialistProfil specialistProfil;
        public SpecialistProfil(int specialistId, bool admin, SpecialistProfil specialistProfil)
        {
            InitializeComponent();
            this.specialistId = specialistId;
            this.admin = admin;
            this.specialistProfil = specialistProfil;
        }

        private void SpecialistProfil_Load(object sender, EventArgs e)
        {
            button9.Hide();
            ControlBox = false;
            this.CenterToScreen();
            if (specialistProfil != null)
            {
                specialistProfil.Close();
            }
            if (admin == true)
            {
                button5.Text = "Back to Admin Page";
                button5.Size = new Size(190, 46);
            }

            MySqlCommand command = new MySqlCommand("SELECT first_name, last_name FROM medical_staff WHERE id_staff=" + specialistId.ToString(), connection);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            name = reader.GetString("first_name") + " " + reader.GetString("last_name");
            connection.Close();
            label2.Text = "Welcome to your profile " + name;


            label1.Text = "Your profile";
            label1.BackColor = System.Drawing.Color.Transparent;
            label2.Text = "Welcome to your profile " + name;
            label2.BackColor = System.Drawing.Color.Transparent;

            listBox1.Hide();
            label3.Hide();

            label3.Text = "Records";
            label3.BackColor = System.Drawing.Color.Transparent;

            label4.Text = "Medical history";
            label4.BackColor = System.Drawing.Color.Transparent;

            label1.Font = new System.Drawing.Font("Times New Roman", 35);
            label2.Font = new System.Drawing.Font("Times New Roman", 15);
            label3.Font = new System.Drawing.Font("Times New Roman", 15);
            label4.Font = new System.Drawing.Font("Times New Roman", 15);
            label4.Hide();
            button1.Hide();
            button2.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            comboBox1.Hide();
            comboBox2.Hide();
            comboBox3.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                Form medical_history = new history(this, patientsId[listBox1.SelectedIndex], specialistId);
                medical_history.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please select an entry!", "Warning");
            }
        }

        

        private void button5_Click(object sender, EventArgs e)
        {
            if (admin == false)
            {
                this.Close();
                Login frm = new Login("Specialist Doctor");
                frm.Show();
            }else
            {
                this.Close();
                Form adm = new Administrator("Administrator");
                adm.Show();

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SpecialistProfil specialistProfil = new SpecialistProfil(specialistId, admin, this); 
            specialistProfil.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                doctorsLength = 0;
                comboBox2.Items.Clear();
                MySqlCommand command = new MySqlCommand("SELECT id_staff, first_name, last_name FROM medical_staff WHERE id_department=" + departmentId[comboBox1.SelectedIndex].ToString(), connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    string doctorname = "";
                    while (reader.Read())
                    {
                        doctorname = reader.GetString("first_name") + " " + reader.GetString("last_name");
                        comboBox2.Items.Add(doctorname);
                        doctorsId[doctorsLength++] = Convert.ToInt32(reader.GetString("id_staff"));
                    }
                }
                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Delete Selected")
            {
                if (listBox1.SelectedIndex != -1)
                {
                    int idTimeSlot = -1;
                    MySqlCommand command = new MySqlCommand("SELECT id_time_slot FROM appointments WHERE id_appointment=" + appointmentId[listBox1.SelectedIndex].ToString(), connection);
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        idTimeSlot = Convert.ToInt32(reader.GetString("id_time_slot"));
                    }
                    connection.Close();
                    command = new MySqlCommand("DELETE FROM appointments WHERE id_appointment=" + appointmentId[listBox1.SelectedIndex].ToString(), connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    command = new MySqlCommand("UPDATE available_appointments SET availability = true WHERE id_time_slot=" + idTimeSlot.ToString(), connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Appointment deleted!", "Success");
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                }
                else
                {
                    MessageBox.Show("Select an appointment!");
                }
            }
            else if (button2.Text == "Done")
            {
               
                Form newAppointment = new appointmentMaker(doctorsId[comboBox2.SelectedIndex], patientsId[comboBox3.SelectedIndex], this, "Specialist", (doctorsId[comboBox2.SelectedIndex] == specialistId ? true : false));
                newAppointment.Show();
                this.Hide();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button9.Show();
            button3.Hide();
            label8.Hide();
            label9.Hide();
            label10.Hide();
            button1.Show();
            button5.Hide();
            button7.Hide();
            button8.Hide();
            listBox1.Items.Clear();
            listBox1.Show();
            button9.Show();
            button2.Text = "Delete Selected";
            button2.Show();
            appointmentLength = 0;
            patientsIdLength = 0;

            MySqlCommand command = new MySqlCommand("SELECT * FROM available_appointments JOIN appointments ON available_appointments.id_time_slot = appointments.id_time_slot JOIN patients_info ON patients_info.id_patient = appointments.id_patient WHERE available_appointments.id_doctor=" + specialistId.ToString(), connection);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                string row = "";
                while (reader.Read())
                {
                    row = "Time slot: " + reader.GetString("date_time_start") + " - " + reader.GetString("date_time_stop") + " Patient: " + reader.GetString("first_name") + " " + reader.GetString("last_name") + " Type: " + reader.GetString("appointment_type");
                    listBox1.Items.Add(row);
                    appointmentId[appointmentLength++] = Convert.ToInt32(reader.GetString("id_appointment"));
                    patientsId[patientsIdLength++] = Convert.ToInt32(reader.GetString("id_patient"));
                }
            }
            connection.Close();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            button9.Show();
            button3.Hide();
            label8.Hide();
            label9.Hide();
            label10.Hide();
            button2.Show();
            button7.Hide();
            button8.Hide();
            button9.Show();
            label5.Show();
            label6.Show();
            label7.Show();
            comboBox1.Show();
            comboBox2.Show();
            comboBox3.Show();
            label5.Text = "Patient name";
            label6.Text = "Department";
            label7.Text = "Specialist Doctor Name";
            patientsIdLength = 0;
            departmentLength = 0;

            MySqlCommand command = new MySqlCommand("SELECT id_patient, first_name, last_name FROM patients_info", connection);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                string patientName = "";
                while (reader.Read())
                {
                    patientName = reader.GetString("first_name") + " " + reader.GetString("last_name");
                    comboBox3.Items.Add(patientName);
                    patientsId[patientsIdLength++] = Convert.ToInt32(reader.GetString("id_patient"));
                }
            }
            connection.Close();
            command = new MySqlCommand("SELECT * FROM departments", connection);
            connection.Open();
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                string departmentName = "";
                while (reader.Read())
                {
                    departmentName = reader.GetString("name");
                    departmentId[departmentLength++] = Convert.ToInt32(reader.GetString("id_department"));
                    comboBox1.Items.Add(departmentName);
                }
            }
            connection.Close();


        }

        private void button8_Click(object sender, EventArgs e)
        {
            newAvailableAppointment form1 = new newAvailableAppointment(specialistId, this);
            this.Hide();
            form1.Show();
        }
    }
}
