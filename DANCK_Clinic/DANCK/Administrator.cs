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
    public partial class Administrator : Form
    {
        bool admin = true;
        string type;
        public Administrator(string type)
        {
            InitializeComponent();
            this.type = type;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            Login frm = new Login(type);
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form newAccount = new NewAccount("Patient", this);
            this.Hide();
            newAccount.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form newAccount = new NewAccount("Doctor", this);
            newAccount.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form newAccount = new NewAccount("Specialist", this);
            newAccount.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form passChange = new PasswordChanger("Patient", this);
            this.Hide();
            passChange.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form passChange = new PasswordChanger("Doctor", this);
            this.Hide();
            passChange.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form passChange = new PasswordChanger("Specialist Doctor", this);
            this.Hide();
            passChange.Show();
        }

        private void Administrator_Load(object sender, EventArgs e)
        {

        }
    }
}
