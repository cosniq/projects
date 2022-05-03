using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WildHouse
{
    public partial class createacc : Form
    {
        MongoCRUD db;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public createacc()
        {
            InitializeComponent();
            db = new MongoCRUD("PetShop");
        }

        private void createacc_Load(object sender, EventArgs e)
        {
            this.TransparencyKey = System.Drawing.Color.Cyan;
            button2.BackColor = System.Drawing.Color.Transparent;
            button3.BackColor = System.Drawing.Color.Transparent;
            button2.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.BorderSize = 0;
        }

        private void createacc_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form inapoi = new Form1();
            inapoi.Show();
            this.Close();
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.FlatAppearance.BorderSize = 1;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.FlatAppearance.BorderSize = 0;
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.FlatAppearance.BorderSize = 1;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.FlatAppearance.BorderSize = 0;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserModel userNou = new UserModel
            {
                username = textBox1.Text,
                password = textBox2.Text,
                quest = "notdefined",
                balance = 0,
                contact = new ContactModel
                {
                    fname = textBox3.Text,
                    lname = textBox4.Text,
                    phone = textBox5.Text,
                    email = textBox6.Text
                },
                adopted = new AdoptedModel
                {
                    nr = 0,
                    apets = ""
                },
                type="normal"
            };
            var recs = db.LoadRecords<UserModel>("user");
            bool ok = true;
            foreach (var rec in recs)
            {
                if (textBox1.Text == rec.username)
                {
                    MessageBox.Show("This username is already used!");
                    textBox1.Text = "";
                    ok = false;
                }
            }
            if (ok == true) {
                db.InsertRecord<UserModel>("user", userNou);
                MessageBox.Show("Account created succsesfully!" + Environment.NewLine + "You can login now!");
            }
            
          
        }
    }
}
