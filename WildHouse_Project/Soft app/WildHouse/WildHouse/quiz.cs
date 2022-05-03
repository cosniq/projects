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
    public partial class quiz : Form
    {
        MongoCRUD db;
        string idu;
        int contor = 1;
        int answers = 0;
        int helpanswer = 0;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public quiz(string id)
        {
            InitializeComponent();
            this.idu = id;
            db = new MongoCRUD("PetShop");
        }

        private void quiz_Load(object sender, EventArgs e)
        {
            this.TransparencyKey = System.Drawing.Color.Cyan;
            label3.Text = "How much free time do you have during a day?";
            button2.BackColor = System.Drawing.Color.Transparent;
            button2.FlatAppearance.BorderSize = 0;
            var user = db.LoadRecordById<UserModel>("user", idu);
            label2.Text = "Hello " + user.contact.fname + "! Please answer to the following questions!";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void quiz_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true || checkBox2.Checked==true || checkBox3.Checked==true)
            {
                if (contor == 1)
                {
                    if (checkBox1.Checked == true) helpanswer = 1;
                    if (checkBox2.Checked == true) helpanswer = 2;
                    if (checkBox3.Checked == true) helpanswer = 3;
                    answers = answers * 10 + helpanswer;
                    label3.Text = "Where are you living?";
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;
                    checkBox1.Text = "small flat";
                    checkBox2.Text = "large flat";
                    checkBox3.Text = "house";
                    helpanswer = 0;
                }
                if (contor == 2)
                {
                    if (checkBox1.Checked == true) helpanswer = 1;
                    if (checkBox2.Checked == true) helpanswer = 2;
                    if (checkBox3.Checked == true) helpanswer = 3;
                    answers = answers * 10 + helpanswer;
                    label3.Text = "How many times a day are you willing to take care of your pet?";
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;
                    checkBox1.Text = "1";
                    checkBox2.Text = "2";
                    checkBox3.Text = "3 or more";
                    helpanswer = 0;

                }
                if (contor == 3)
                {
                    if (checkBox1.Checked == true) helpanswer = 1;
                    if (checkBox2.Checked == true) helpanswer = 2;
                    if (checkBox3.Checked == true) helpanswer = 3;
                    answers = answers * 10 + helpanswer;
                    label3.Text = "How much money are you willing to spend monthly on your pet?";
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;
                    checkBox1.Text = "50 RON";
                    checkBox2.Text = "100 RON";
                    checkBox3.Text = "150 RON or more";
                    button2.Text = "Submit";
                    helpanswer = 0;

                }
                if (contor == 4)
                {
                    if (checkBox1.Checked == true) helpanswer = 1;
                    if (checkBox2.Checked == true) helpanswer = 2;
                    if (checkBox3.Checked == true) helpanswer = 3;
                    answers = answers * 10 + helpanswer;
                    
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;
                    helpanswer = 0;
                    contor = 0;
                    var user = db.LoadRecordById<UserModel>("user", idu);
                    db.UpdateGeneral<UserModel>("user", idu, "quest", answers.ToString());
                    MessageBox.Show("Thank you for taking our quizz!");
                    Form back = new users(idu);
                    back.Show();
                    this.Close();
                }
                contor++;
            }
            else
            {
                MessageBox.Show("Please select an answer!");
            }
            
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.FlatAppearance.BorderSize = 1;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.FlatAppearance.BorderSize = 0;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;
               
            }
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox3.Checked = false;
               
            }
           
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                checkBox2.Checked = false;
                checkBox1.Checked = false;
                
            }
            
        }
    }
}
