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
    public partial class ModifyRecord : Form
    {
        bool addpet;
        int contor=0;
        Helper h = new Helper();
        string[] allStuffId = new string[500];
        MongoCRUD db;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public ModifyRecord()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ModifyRecord_Load(object sender, EventArgs e)
        {
            this.TransparencyKey = System.Drawing.Color.Cyan;
            db = new MongoCRUD("PetShop");
            button2.BackColor = System.Drawing.Color.Transparent;
            button2.FlatAppearance.BorderSize = 0;
        }

        private void ModifyRecord_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (checkedListBox1.SelectedIndex == 0)
            {
                addpet = true;
                listBox1.Items.Clear();
                checkedListBox1.SetItemChecked(1, false);
                var dbpets = db.LoadRecords<PetModel>("pets");
                allStuffId = h.clearString(allStuffId);
                contor = 0;
                foreach(var rec in dbpets)
                {
                    if (rec.breed != null) listBox1.Items.Add(rec.name + " - " + rec.breed+" - "+rec.age+" - "+ rec.health+" - "+rec.questid); 
                    else
                    listBox1.Items.Add(rec.name + " - " + rec.species + " - " + rec.age + " - " + rec.health + " - " + rec.questid);
                    allStuffId[contor] = rec.id;
                    contor++;
                }


            }
            if (checkedListBox1.SelectedIndex == 1)
            {
                addpet = false;
                listBox1.Items.Clear();
                checkedListBox1.SetItemChecked(0, false);
                var dbitems = db.LoadRecords<ItemModel>("items");
                allStuffId = h.clearString(allStuffId);
                contor = 0;
                foreach (var rec in dbitems)
                {
                    listBox1.Items.Add(rec.iName+" - "+rec.quantity+" - "+rec.price+" RON");
                    allStuffId[contor] = rec.id;
                    contor++;
                }




            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (addpet == true)
                {
                    db.UpdateGeneral<PetModel>("pets", allStuffId[listBox1.SelectedIndex], textBox1.Text, textBox2.Text);
                    MessageBox.Show("Pet details have been updated succsesfully!");
                    listBox1.Items.Clear();
                    checkedListBox1.SetItemChecked(0, false);
                    checkedListBox1.SetItemChecked(1, false);
                }

                if (addpet == false)
                {
                    db.UpdateGeneral<ItemModel>("items", allStuffId[listBox1.SelectedIndex], textBox1.Text, textBox2.Text);
                    MessageBox.Show("Item details have been updated succsesfully!");
                    listBox1.Items.Clear();
                    checkedListBox1.SetItemChecked(0, false);
                    checkedListBox1.SetItemChecked(1, false);
                }
            }
            catch
            {
                MessageBox.Show("Please select an entry from the list box!");
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
    }
}
