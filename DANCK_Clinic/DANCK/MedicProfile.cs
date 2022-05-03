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
    public partial class MedicProfile : Form
    {
        int doctorId, patientsIdLength = 0, departmentLength = 0, doctorsLength = 0, appointmentLength = 0;
        int[] patientsId = new int[1000], departmentId = new int[100], doctorsId = new int[100], appointmentId = new int[100];
        string name;
        bool admin;
        MedicProfile medicProfile;
        MySqlConnection connection = new MySqlConnection("server=127.0.0.1;port=3306;user id=admin;password=industrialInformaticsProjectPassword;persistsecurityinfo=True;database=industrialinformaticsproject;SslMode=none");

        public MedicProfile(int doctorId, bool admin, MedicProfile medicProfile)
        {
            InitializeComponent();
            this.doctorId = doctorId;
            this.admin = admin;
            this.medicProfile = medicProfile;
        }

        private void Medic_Load(object sender, EventArgs e)
        {
            button8.Show();
            if (medicProfile != null)
            {
                medicProfile.Close();
            }
            if (admin == true)
            {
                button5.Text = "Back to Admin Page";
                button5.Size = new Size(190, 46);
            }

            MySqlCommand command = new MySqlCommand("SELECT first_name, last_name FROM medical_staff WHERE id_staff=" + doctorId.ToString(), connection);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            name = reader.GetString("first_name") + " " + reader.GetString("last_name");
            connection.Close();
            label2.Text = "Welcome to your profile " + name;


            button7.Hide();
            ControlBox = false;
            this.CenterToScreen();
            button6.Hide();
            listBox1.Hide();
            label1.Text = "Your profile";
            label1.BackColor = System.Drawing.Color.Transparent;
            label2.BackColor = System.Drawing.Color.Transparent;
            label3.BackColor = System.Drawing.Color.Transparent;
            label6.BackColor = System.Drawing.Color.Transparent;
            label7.BackColor = System.Drawing.Color.Transparent;
            label1.Font = new System.Drawing.Font("Times New Roman", 35);
            label2.Font = new System.Drawing.Font("Times New Roman", 15);
            label3.Font = new System.Drawing.Font("Times New Roman", 10);
            label6.Font = new System.Drawing.Font("Times New Roman", 10);
            label7.Font = new System.Drawing.Font("Times New Roman", 10);



            button3.Hide();
            comboBox1.Hide();
            comboBox2.Hide();
            comboBox3.Hide();
            label3.Hide();
            label6.Hide();
            label7.Hide();
            button4.Hide();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            button8.Hide();
            button1.Hide();
            button2.Hide();
            label4.Hide();
            label5.Hide();
            label8.Hide();
            listBox1.Items.Clear();
            button3.Show();
            listBox1.Show();
            button7.Show();
            button6.Show();
            appointmentLength = 0;
            patientsIdLength = 0;

            MySqlCommand command = new MySqlCommand("SELECT * FROM available_appointments JOIN appointments ON available_appointments.id_time_slot = appointments.id_time_slot JOIN patients_info ON patients_info.id_patient = appointments.id_patient WHERE available_appointments.id_doctor=" + doctorId.ToString(), connection);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                string row = "";
                while (reader.Read())
                {
                    row = "Time slot: " + reader.GetString("date_time_start") + " - " + reader.GetString("date_time_stop") + " Patient: " + reader.GetString("first_name") + " " + reader.GetString("last_name");
                    listBox1.Items.Add(row);
                    appointmentId[appointmentLength++] = Convert.ToInt32(reader.GetString("id_appointment"));
                    patientsId[patientsIdLength++] = Convert.ToInt32(reader.GetString("id_patient"));
                }
            }
            connection.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button8.Hide();
            button6.Hide();
            listBox1.Hide();
            button1.Hide();
            button3.Hide();
            button4.Show();
            button7.Show();
            label4.Hide();
            label5.Hide();
            label8.Hide();
            comboBox1.Show();
            comboBox2.Show();
            comboBox3.Show();
            label3.Show();
            label6.Show();
            label7.Show();
            button2.Hide();

            label3.Text = "Patient name";
            label6.Text = "Department";
            label7.Text = "Specialist Doctor Name";

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

        private void button4_Click(object sender, EventArgs e)
        {
            
            if (comboBox2.SelectedIndex != -1 && comboBox3.SelectedIndex != -1 && comboBox1.SelectedIndex != -1)
            {
                comboBox1.Hide();
                comboBox2.Hide();
                comboBox3.Hide();
                label3.Hide();
                label6.Hide();
                label7.Hide();
                button2.Show();
                button1.Show();
                button4.Hide();
                button7.Hide();
                button8.Show();
                label4.Show();
                label5.Show();
                label6.Show();

                appointmentMaker appointmentMaker = new appointmentMaker(doctorsId[comboBox2.SelectedIndex], patientsId[comboBox3.SelectedIndex], this, "Doctor");
                appointmentMaker.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("There are empty fields!", "Warning");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            newAvailableAppointment form1 = new newAvailableAppointment(doctorId, this);
            this.Hide();
            form1.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MedicProfile medicProfile = new MedicProfile(doctorId, admin, this);
            medicProfile.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (admin == false)
            {
                this.Close();
                Login frm = new Login("Doctor");
                frm.Show();
            }else
            {
                this.Close();
                Form adm = new Administrator("Administrator");
                adm.Show();
            }
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
                    string doctorName = "";
                    while (reader.Read())
                    {
                        doctorName = reader.GetString("first_name") + " " + reader.GetString("last_name");
                        comboBox2.Items.Add(doctorName);
                        doctorsId[doctorsLength++] = Convert.ToInt32(reader.GetString("id_staff"));
                    }
                }
                connection.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button8.Hide();
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an appointment!");
            }
            else
            {
                MySqlCommand command = new MySqlCommand("SELECT id_time_slot FROM appointments WHERE id_appointment=" + appointmentId[listBox1.SelectedIndex].ToString(), connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int idTimeSlot = Convert.ToInt32(reader.GetString("id_time_slot"));
                connection.Close();
                string query = "UPDATE available_appointments SET availability=true WHERE id_time_slot=" + idTimeSlot.ToString();
                Console.WriteLine(query);
                command = new MySqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                command = new MySqlCommand("DELETE FROM appointments WHERE id_appointment=" + appointmentId[listBox1.SelectedIndex].ToString(), connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                MessageBox.Show("Appointment Deleted!", "Success");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select an element in the list!");
                return;
            }
            history form1 = new history(this, patientsId[listBox1.SelectedIndex], doctorId);
            form1.Show();
            this.Hide();
            button8.Hide();
        }
    }
}
