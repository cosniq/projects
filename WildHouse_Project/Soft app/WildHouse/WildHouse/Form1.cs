using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WildHouse
{
    public partial class Form1 : Form
    {
        MongoCRUD db;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public Form1()
        {
            InitializeComponent();
            db = new MongoCRUD("PetShop");
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.TransparencyKey = System.Drawing.Color.Cyan;
            button1.BackColor = System.Drawing.Color.Transparent;
            button2.BackColor= System.Drawing.Color.Transparent;
            button3.BackColor = System.Drawing.Color.Transparent;
            label1.BackColor = System.Drawing.Color.Transparent;
            label2.BackColor = System.Drawing.Color.Transparent;
            label3.BackColor = System.Drawing.Color.Transparent;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
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

        private void button1_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = System.Drawing.Color.Red;
            button1.FlatAppearance.BorderSize = 1;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = System.Drawing.Color.Transparent;
            button1.FlatAppearance.BorderSize = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool existence = false;
            var recs = db.LoadRecords<UserModel>("user");

            foreach(var rec in recs)
            {
                if(textBox1.Text==rec.username && textBox2.Text==rec.password)
                {
                    existence = true;
                    string usedid = rec.id.ToString();
                    Form log = new users(usedid);
                    log.Show();
                    this.Hide();
                }      
            }
            if(existence==false) MessageBox.Show("Inccorect username or password!");
           
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            
            
            button2.BackColor = System.Drawing.Color.SaddleBrown;
            button2.FlatAppearance.BorderSize = 1;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            
           
            button2.BackColor = System.Drawing.Color.Transparent;
            button2.FlatAppearance.BorderSize = 0;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
           /* button2.BackColor = System.Drawing.Color.SaddleBrown;
            button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent; */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form creare = new createacc();
            creare.Show();
            this.Hide();
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.BackColor = System.Drawing.Color.SaddleBrown;
            button3.FlatAppearance.BorderSize = 1;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = System.Drawing.Color.Transparent;
            button3.FlatAppearance.BorderSize = 0;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2.PerformClick();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2.PerformClick();
            }
        }
    }
}
