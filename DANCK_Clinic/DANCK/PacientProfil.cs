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
    public partial class PacientProfil : Form
    {
        int id, departmentLength = 0, doctorsLength = 0;
        string name, type;
        bool admin;
        int[] departmentId = new int[100], doctorsId = new int[100];
        string[] departmentsName = new string[100];
        MySqlConnection connection = new MySqlConnection("server=127.0.0.1;port=3306;user id=admin;password=industrialInformaticsProjectPassword;persistsecurityinfo=True;database=industrialinformaticsproject;SslMode=none");

        public PacientProfil(int id, bool admin)
        {
            InitializeComponent();
            this.id = id;
            this.admin = admin;
        }

        private void PacientProfil_Load(object sender, EventArgs e)
        {
            if (!admin)
            {
                type = "Patient";
            }
            MySqlCommand command = new MySqlCommand("SELECT first_name, last_name FROM patients_info WHERE id_patient = '" + id + "'", connection);
            command.CommandTimeout = 60;
            MySqlDataReader reader;
            connection.Open();
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                string row = "";
                while(reader.Read())
                {
                    row = reader.GetString("first_name") + " " + reader.GetString("last_name");
                }
                name = row;
            }
            label2.Text = "Welcome to your profile " + name;
            connection.Close();

            

            tableLayoutPanel1.Hide();
            ControlBox = false;
            this.CenterToScreen();
            if (admin == true)
            {
                button3.Text = "Back to Admin Page";
                button3.Size = new Size(190, 46);
            }
            label1.Text = "Your profile";
            label1.BackColor = System.Drawing.Color.Transparent;
            label2.BackColor = System.Drawing.Color.Transparent;
            label1.Font = new System.Drawing.Font("Times New Roman", 35);
            label2.Font = new System.Drawing.Font("Times New Roman", 15);

            label6.BackColor = System.Drawing.Color.Transparent;
            label6.Font = new System.Drawing.Font("Times New Roman", 10);
            label9.BackColor = System.Drawing.Color.Transparent;
            label9.Font = new System.Drawing.Font("Times New Roman", 10);
            label10.BackColor = System.Drawing.Color.Transparent;
            label10.Font = new System.Drawing.Font("Times New Roman", 10);
            label6.Hide();
            label9.Hide();
            label10.Hide();
            textBox1.Hide();
            comboBox1.Hide();
            comboBox2.Hide();
            button5.Hide();
            button4.Hide();


            label3.BackColor = System.Drawing.Color.Transparent;
            label3.Font = new System.Drawing.Font("Times New Roman", 12);
            label4.BackColor = System.Drawing.Color.Transparent;
            label4.Font = new System.Drawing.Font("Times New Roman", 12);
            label5.BackColor = System.Drawing.Color.Transparent;
            label5.Font = new System.Drawing.Font("Times New Roman", 12);
            label3.Hide();
            label4.Hide();
            label5.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            departmentLength = 0;
            comboBox1.Items.Clear();
            string query = "";
            if (admin)
            {
                query = "SELECT * FROM departments";
            }
            else
            {
                query = "SELECT * FROM departments WHERE name = 'Family'";
            }
            MySqlCommand command = new MySqlCommand(query, connection);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    departmentId[departmentLength] = Convert.ToInt32(reader.GetString("id_department"));
                    departmentsName[departmentLength] = reader.GetString("name");
                    departmentLength++;
                }
            }

            connection.Close();

            for (int i = 0; i < departmentLength; i++)
            {
                comboBox1.Items.Add(departmentsName[i]);
            }

            tableLayoutPanel1.Hide();
            label6.Show();
            label9.Show();
            label10.Show();
            textBox1.Show();
            comboBox1.Show();
            comboBox2.Show();
            button1.Hide();
            button2.Hide();
            label7.Hide();
            label8.Hide();
            button3.Show();
            button5.Show();
            button4.Show();

            if (!admin)
            {
                textBox1.ReadOnly = true;
                textBox1.Text = name;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.Show();
            label6.Hide();
            label9.Hide();
            label10.Hide();
            textBox1.Hide();
            comboBox1.Hide();
            comboBox2.Hide();
            button1.Hide();
            button2.Hide();
            label7.Hide();
            label8.Hide();
            button3.Show();
            button5.Show();
            button4.Hide();
            label3.Show();
            label4.Show();
            label5.Show();

            tableLayoutPanel1.RowCount = 1;
            MySqlCommand command = new MySqlCommand("SELECT * FROM available_appointments JOIN appointments ON available_appointments.id_time_slot=appointments.id_time_slot JOIN medical_staff ON medical_staff.id_staff=available_appointments.id_doctor WHERE appointments.id_patient=" + id.ToString(), connection);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                string type = "";
                string doctor = "";
                string time = "";

                while (reader.Read())
                {
                    type = reader.GetString("appointment_type");
                    doctor = reader.GetString("first_name") + " " + reader.GetString("last_name");
                    time = reader.GetString("date_time_start") + " - " + reader.GetString("date_time_stop");
                    tableLayoutPanel1.RowCount++;
                    tableLayoutPanel1.Controls.Add(new Label() { Text = type, Width = 230 }, 0, tableLayoutPanel1.RowCount - 1) ;
                    tableLayoutPanel1.Controls.Add(new Label() { Text = doctor, Width = 800 }, 1, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = time, Width = 1260 }, 2, tableLayoutPanel1.RowCount - 1);

                }
            }
            connection.Close();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex != -1)
            {
                appointmentMaker appointmentMakerForm = new appointmentMaker(doctorsId[comboBox2.SelectedIndex], id, this, type);
                appointmentMakerForm.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Please select a doctor!", "Warning");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            doctorsLength = 0;
            comboBox2.Items.Clear();
            if (comboBox1.SelectedIndex != -1)
            {
                MySqlCommand command = new MySqlCommand("SELECT id_staff, first_name, last_name FROM medical_staff WHERE id_department = " + departmentId[comboBox1.SelectedIndex], connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    string name = "";
                    while(reader.Read())
                    {
                        doctorsId[doctorsLength] = Convert.ToInt32(reader.GetString("id_staff"));
                        name = reader.GetString("first_name") + " " + reader.GetString("last_name");
                        doctorsLength++;
                        comboBox2.Items.Add(name);
                    }
                }
                connection.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Hide();
            label6.Hide();
            label9.Hide();
            label10.Hide();
            textBox1.Hide();
            comboBox1.Hide();
            comboBox2.Hide();
            button1.Show();
            button2.Show();
            label7.Show();
            label8.Show();
            button3.Show();
            button4.Hide();
            button5.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (admin == false)
            {
                this.Close();
                Login frm = new Login("Patient");
                frm.Show();
            }else
            {
                this.Close();
                Form adm = new Administrator("Administrator");
                adm.Show();
            }
        }
    }
}
