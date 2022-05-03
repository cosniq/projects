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
    public partial class addbalance : Form
    {
        MongoCRUD db;
        string idu;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public addbalance(string id)
        {
            InitializeComponent();
            this.idu = id;
        }

        private void addbalance_Load(object sender, EventArgs e)
        {
            this.TransparencyKey = System.Drawing.Color.Cyan;
            button2.BackColor = System.Drawing.Color.Transparent;
            button2.FlatAppearance.BorderSize = 0;
            button3.BackColor = System.Drawing.Color.Transparent;
            button3.FlatAppearance.BorderSize = 0;
            db = new MongoCRUD("PetShop");
            var user = db.LoadRecordById<UserModel>("user", idu);
            label2.Text = "BALANCE: " + user.balance + " RON";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var user = db.LoadRecordById<UserModel>("user", idu);
            db.UpdateGeneral<UserModel>("user", idu, "balance", (Convert.ToInt32(user.balance) + Convert.ToInt32(textBox1.Text)).ToString());
            Form UI = new users(idu);
            UI.Show();
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

        private void addbalance_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form back = new users(idu);
            back.Show();
            this.Close();
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.FlatAppearance.BorderSize = 0;
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.FlatAppearance.BorderSize = 1;
        }
    }
}
