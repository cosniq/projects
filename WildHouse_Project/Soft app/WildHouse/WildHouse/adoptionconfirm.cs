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
    public partial class adoptionconfirm : Form
    {
        int contor = 0;
        Helper h = new Helper();
        string[] allPetsId = new string[500];
        MongoCRUD db;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public adoptionconfirm()
        {
            InitializeComponent();
        }

        private void adoptionconfirm_Load(object sender, EventArgs e)
        {
             button2.BackColor = System.Drawing.Color.Transparent;
            button3.BackColor = System.Drawing.Color.Transparent;
            button2.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.BorderSize = 0;
            contor = 0;
            db = new MongoCRUD("PetShop");
            var dbpets=db.LoadRecords<PetModel>("pets");
            foreach(var pet in dbpets)
            {
                if(pet.adoption!="Adopt Now" && pet.adoption!="Adopted")
                {
                    if (pet.breed != null) listBox1.Items.Add(pet.name + " - " + pet.breed);
                    else
                        listBox1.Items.Add(pet.name + " - " + pet.species);
                    allPetsId[contor] = pet.id;
                    contor++;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void adoptionconfirm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var pet = db.LoadRecordById<PetModel>("pets", allPetsId[listBox1.SelectedIndex]);
            var user = db.LoadRecordById<UserModel>("user", pet.adoption);
            string userpets = user.adopted.apets+","+pet.id;
            string ad = "adoption";
            db.UpdateGeneral<PetModel>("pets", pet.id, ad, "Adopted");
            db.UpdateGeneral<UserModel>("user", user.id, "adopted.apets", userpets);
            db.UpdateGeneral<UserModel>("user", user.id, "adopted.nr", (Convert.ToInt32(user.adopted.nr)+1).ToString());
            MessageBox.Show("Adoption completed!");
            contor = 0;
            allPetsId = h.clearString(allPetsId);
            listBox1.Items.Clear();
            var dbpets = db.LoadRecords<PetModel>("pets");
            foreach (var pet1 in dbpets)
            {
                if (pet1.adoption != "Adopt Now" && pet1.adoption != "Adopted")
                {
                    if (pet1.breed != null) listBox1.Items.Add(pet1.name + " - " + pet1.breed);
                    else
                        listBox1.Items.Add(pet1.name + " - " + pet1.species);
                    allPetsId[contor] = pet1.id;
                    contor++;
                }
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var pet = db.LoadRecordById<PetModel>("pets", allPetsId[listBox1.SelectedIndex]);
            var user = db.LoadRecordById<UserModel>("user", pet.adoption);
            label2.Text = "Name of user:" + Environment.NewLine + user.contact.fname + " " + user.contact.lname + Environment.NewLine + Environment.NewLine+Environment.NewLine+"Phone: " + user.contact.phone;
            label3.Text = "User quest: " + user.quest;
            label4.Text = "Pet quest: " + pet.questid;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var pet = db.LoadRecordById<PetModel>("pets", allPetsId[listBox1.SelectedIndex]);
            db.UpdateGeneral<PetModel>("pets", pet.id, "adoption", "Adopt Now");
            MessageBox.Show("Pet is free to be adopted by other user!");
            contor = 0;
            allPetsId = h.clearString(allPetsId);
            listBox1.Items.Clear();
            var dbpets = db.LoadRecords<PetModel>("pets");
            foreach (var pet1 in dbpets)
            {
                if (pet1.adoption != "Adopt Now" && pet1.adoption != "Adopted")
                {
                    if (pet1.breed != null) listBox1.Items.Add(pet1.name + " - " + pet1.breed);
                    else
                        listBox1.Items.Add(pet1.name + " - " + pet1.species);
                    allPetsId[contor] = pet1.id;
                    contor++;
                }
            }
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.FlatAppearance.BorderSize = 1;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.FlatAppearance.BorderSize = 0;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.FlatAppearance.BorderSize = 1;

        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.FlatAppearance.BorderSize = 0;
        }
    }
}
