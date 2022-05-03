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
    public partial class users : Form
    {
        MongoCRUD db;
        string id;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public users(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void users_Load(object sender, EventArgs e)
        {
            db = new MongoCRUD("PetShop");
            
            var theuser = db.LoadRecordById<UserModel>("user", id);
            this.TransparencyKey = System.Drawing.Color.Cyan;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Text = "Hello " + theuser.contact.fname + "! Welcome to Wild House petshop." + Environment.NewLine + "Here you can adopt a pet or purchase items that you need." + Environment.NewLine + " We are happy to see you here!";
            label2.Text = "BALANCE: " + theuser.balance;
            if(theuser.type!="admin")
            {
                button6.Hide();
                button7.Hide();
                button6.Enabled = false;
                button7.Enabled = false;
                button8.Hide();
                button8.Enabled = false;
            }
            string all_pets_id = theuser.adopted.apets;
            int number = Convert.ToInt32(theuser.adopted.nr);
            if (number > 0)
            { 
                if(number>1)
                {
                    string[] separate_id = all_pets_id.Split(',');
                   
                    for (int i = 0; i < number; i++)
                    {
                        var pets = db.LoadRecordById<PetModel>("pets", separate_id[i]);
                        listBox1.Items.Add(pets.name);
                        
                    }
                }
                else
                {
                    var pets = db.LoadRecordById<PetModel>("pets", theuser.adopted.apets);
                    listBox1.Items.Add(pets.name);
                }
                
            }
            else
            {
                listBox1.Items.Add("No pets adopted");
            }
            label2.BackColor = System.Drawing.Color.Transparent;
            label3.BackColor = System.Drawing.Color.Transparent;
            listBox1.BackColor = System.Drawing.Color.SandyBrown;
            button2.BackColor = System.Drawing.Color.Transparent;
            button3.BackColor = System.Drawing.Color.Transparent;
            button4.BackColor = System.Drawing.Color.Transparent;
            button6.BackColor = System.Drawing.Color.Transparent;
            button7.BackColor = System.Drawing.Color.Transparent;
            button8.BackColor = System.Drawing.Color.Transparent;
            button2.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.BorderSize = 0;
            button4.FlatAppearance.BorderSize = 0;
            button6.FlatAppearance.BorderSize = 0;
            button7.FlatAppearance.BorderSize = 0;
            button8.FlatAppearance.BorderSize = 0;
        }

        private void users_MouseDown(object sender, MouseEventArgs e)
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

        private void button2_MouseHover(object sender, EventArgs e)
        {
            
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

        private void button4_Click(object sender, EventArgs e)
        {
            Form adaugare = new addbalance(id);
            adaugare.Show();
            this.Close();
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            button4.FlatAppearance.BorderSize = 1;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.FlatAppearance.BorderSize = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form adoptie = new adoption(id);
            adoptie.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            Form cumparaturi = new shop(id);
            cumparaturi.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form quiz = new quiz(id);
            quiz.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form adrec = new addrec();
            adrec.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form modify = new ModifyRecord();
            modify.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form adconf = new adoptionconfirm();
            adconf.Show();
        }
    }
}
