using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DANCK
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ControlBox = false;
            this.CenterToScreen();
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Font = new System.Drawing.Font("Times New Roman", 35);
            label2.Font = new System.Drawing.Font("Times New Roman", 17);
            label3.Font = new System.Drawing.Font("Times New Roman", 17);
            label1.Text = "DANCK Clinic";
            pictureBox2.BackColor = System.Drawing.Color.Transparent;
            pictureBox1.BackColor = System.Drawing.Color.Transparent;
            pictureBox3.BackColor = System.Drawing.Color.Transparent;
            
            button2.Location = new Point(65, 365);
            button1.Location = new Point(65, 430);
            button3.Location = new Point(65, 500);
            button4.Location = new Point(65, 570);

            pictureBox2.Size = new Size(40,40);

            label2.BackColor = System.Drawing.Color.Transparent;
            label3.BackColor = System.Drawing.Color.Transparent;

            string fakenumbers, orar;
            fakenumbers = "Contact"+Environment.NewLine+Environment.NewLine+"+40-7055574-178" + Environment.NewLine + "+40-7115559-401";
            orar = "Schedule"+Environment.NewLine  + Environment.NewLine + "Monday - Friday: 7am-17pm" + Environment.NewLine + "Saturday: 7am-14pm" + Environment.NewLine + "Sunday: 10am-12pm";

            label2.Location = new Point(465, 375);
            label3.Location = new Point(800, 375);
            label2.Text = fakenumbers;
            label3.Text = orar;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form pacient = new Login("Patient");
            this.Hide();
            pacient.Show();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form login = new Login("Specialist Doctor");
            login.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form login = new Login("Administrator");
            login.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form login = new Login("Doctor");
            login.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
