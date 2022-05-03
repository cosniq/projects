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
    public partial class NewAccount : Form
    {
        string type;
        Form previous;
        int[] departmentId = new int[100];
        int departmentLength = 0;
        MySqlConnection connection = new MySqlConnection("server=127.0.0.1;port=3306;user id=admin;password=industrialInformaticsProjectPassword;persistsecurityinfo=True;database=industrialinformaticsproject;SslMode=none");


        public NewAccount(string type)
        {
            InitializeComponent();
            this.type = type;
        }

        public NewAccount(string type, Form previous)
        {
            InitializeComponent();
            this.type = type;
            this.previous = previous;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void NewAccount_Load(object sender, EventArgs e)
        {

            ControlBox = false;
            this.CenterToScreen();
            label1.Visible=false;
            label2.Visible=false;
            label3.Visible=false;
            label4.Visible=false;
            label5.Visible=false;
            label6.Visible=false;
            label7.Visible=false;
            label8.Visible=false;
            label9.Visible=false;
            //label10.Visible=false;
            textBox1.Visible=false;
            textBox2.Visible=false;
            textBox3.Visible=false;
            textBox4.Visible=false;
            textBox5.Visible=false;
            textBox6.Visible=false;
            //textBox7.Visible=false;
            dateTimePicker1.Visible=false;
            radioButton1.Visible=false;
            radioButton2.Visible=false;

            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Font = new System.Drawing.Font("Times New Roman", 35);
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.Font = new System.Drawing.Font("Times New Roman", 10);
            label3.BackColor = System.Drawing.Color.Transparent;
            label3.Font = new System.Drawing.Font("Times New Roman", 10);
            label4.BackColor = System.Drawing.Color.Transparent;
            label4.Font = new System.Drawing.Font("Times New Roman", 10);
            label5.BackColor = System.Drawing.Color.Transparent;
            label5.Font = new System.Drawing.Font("Times New Roman", 10);
            label6.BackColor = System.Drawing.Color.Transparent;
            label6.Font = new System.Drawing.Font("Times New Roman", 10);
            label7.BackColor = System.Drawing.Color.Transparent;
            label7.Font = new System.Drawing.Font("Times New Roman", 10);
            label8.BackColor = System.Drawing.Color.Transparent;
            label8.Font = new System.Drawing.Font("Times New Roman", 10);
            label9.BackColor = System.Drawing.Color.Transparent;
            label9.Font = new System.Drawing.Font("Times New Roman", 10);
            label10.BackColor = System.Drawing.Color.Transparent;
            label10.Font = new System.Drawing.Font("Times New Roman", 10);

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";

            if (type == "Patient")
            {
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
                dateTimePicker1.Visible = true;
                radioButton1.Visible = true;
                radioButton2.Visible = true;
            }
            else
                if (type == "Doctor")
            {
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = false;
                label8.Visible = true;
                //label10.Visible=true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
                //textBox7.Visible=true;
                dateTimePicker1.Hide();
                label7.Hide();
                label9.Hide();
                textBox6.Hide();
                
            }
            else
                if(type=="Specialist")
            {
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label9.Visible = true;
                label8.Visible = true;
                //label10.Visible=true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
                textBox6.Visible = true;
                //textBox7.Visible=true;
                dateTimePicker1.Hide();
                label7.Hide();
                comboBox1.Show();
                label10.Show();

                MySqlCommand command = new MySqlCommand("SELECT * FROM departments WHERE name != 'Family'", connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader.GetString("name"));
                        departmentId[departmentLength++] = Convert.ToInt32(reader.GetString("id_department"));
                    }
                }

            }
            else
                if (type == "Administrator")
            {
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (previous == null)
            {
                this.Close();
                Login frm = new Login(type);
                frm.Show();
            }
            else
            {
                this.Close();
                previous.Show();
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Boolean toHide = true;
            if (type == "Patient")
            {
                string username = textBox1.Text;
                string password = textBox2.Text;
                string fname = textBox3.Text;
                string lname = textBox4.Text;
                string email = textBox5.Text;
                string sex = "";
                if (radioButton1.Checked)
                    sex = "Male";
                else
                    sex = "Female";
                //MySqlConnection connection = new MySqlConnection("server=127.0.0.1;port=3306;user id=admin;password=industrialInformaticsProjectPassword;persistsecurityinfo=True;database=industrialinformaticsproject;SslMode=none");
                MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE username='" + username + "'", connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    MessageBox.Show("Choose another username! (This is taken)", "Attention!");
                    toHide = false;
                    connection.Close();
                }
                else
                {
                    connection.Close();
                    try
                    {
                        command = new MySqlCommand("INSERT INTO users (username, password, type) VALUES ('" + username + "', '" + password + "', 'Patient')", connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();

                        int newId = -1;
                        command = new MySqlCommand("SELECT id FROM users WHERE username='" + username + "'", connection);
                        connection.Open();
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            newId = Convert.ToInt32(reader.GetString("id"));
                        }
                        connection.Close();

                        command = new MySqlCommand("INSERT INTO patients_info (id_patient, first_name, last_name, date_of_birth, sex, email) VALUES (" + newId.ToString() + ", '" + fname + "', '" + lname + "', '" + this.dateTimePicker1.Text + "', '" + sex + "', '" + email + "')", connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("User inserted!", "Success");
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }
            }
            else if (type == "Doctor")
            {
                string username = textBox1.Text;
                string password = textBox2.Text;
                string fname = textBox3.Text;
                string lname = textBox4.Text;
                string email = textBox5.Text;

                //MySqlConnection connection = new MySqlConnection("server=127.0.0.1;port=3306;user id=admin;password=industrialInformaticsProjectPassword;persistsecurityinfo=True;database=industrialinformaticsproject;SslMode=none");
                MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE username='" + username + "'", connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    MessageBox.Show("Choose another username! (This is taken)", "Attention!");
                    toHide = false;
                    connection.Close();
                }
                else
                {
                    connection.Close();
                    try
                    {
                        command = new MySqlCommand("INSERT INTO users (username, password, type) VALUES ('" + username + "', '" + password + "', 'Doctor')", connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();

                        int newId = -1;
                        command = new MySqlCommand("SELECT id FROM users WHERE username='" + username + "'", connection);
                        connection.Open();
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            newId = Convert.ToInt32(reader.GetString("id"));
                        }
                        connection.Close();

                        command = new MySqlCommand("INSERT INTO medical_staff (id_staff, id_department, first_name, last_name, specialization, email) VALUES (" + newId.ToString() + ", '1', '" + fname + "', '" + lname + "', 'Family', '" + email + "')", connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("User inserted!", "Success");
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }
            }
            else if (type == "Specialist")
            {
                string username = textBox1.Text;
                string password = textBox2.Text;
                string fname = textBox3.Text;
                string lname = textBox4.Text;
                string email = textBox5.Text;
                string specialization = textBox6.Text;
                int selectedDepartment = departmentId[comboBox1.SelectedIndex];

                //MySqlConnection connection = new MySqlConnection("server=127.0.0.1;port=3306;user id=admin;password=industrialInformaticsProjectPassword;persistsecurityinfo=True;database=industrialinformaticsproject;SslMode=none");
                MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE username='" + username + "'", connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    MessageBox.Show("Choose another username! (This is taken)", "Attention!");
                    toHide = false;
                    connection.Close();
                }
                else
                {
                    connection.Close();
                    try
                    {
                        command = new MySqlCommand("INSERT INTO users (username, password, type) VALUES ('" + username + "', '" + password + "', 'Specialist Doctor')", connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();

                        int newId = -1;
                        command = new MySqlCommand("SELECT id FROM users WHERE username='" + username + "'", connection);
                        connection.Open();
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            newId = Convert.ToInt32(reader.GetString("id"));
                        }
                        connection.Close();

                        command = new MySqlCommand("INSERT INTO medical_staff (id_staff, id_department, first_name, last_name, specialization, email) VALUES (" + newId.ToString() + ", '" + selectedDepartment.ToString() +"', '" + fname + "', '" + lname + "', '"+ specialization +"', '" + email + "')", connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("User inserted!", "Success");
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }
            }

            if (toHide)
            {
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                //label10.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                textBox3.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;
                textBox6.Visible = false;
                //textBox7.Visible = false;
                dateTimePicker1.Visible = false;
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                button2.Visible = false;
                label10.Hide();
                comboBox1.Hide();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
