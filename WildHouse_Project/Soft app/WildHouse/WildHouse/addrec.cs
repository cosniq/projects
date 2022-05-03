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
    public partial class addrec : Form
    {
        bool addpet;
        MongoCRUD db;
        Helper h = new Helper();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public addrec()
        {
            InitializeComponent();
            db = new MongoCRUD("PetShop");
        }

        private void addrec_Load(object sender, EventArgs e)
        {
            this.TransparencyKey = System.Drawing.Color.Cyan;
            checkedListBox1.BackColor = System.Drawing.Color.SaddleBrown;
            checkedListBox1.ForeColor = System.Drawing.Color.White;
            button2.BackColor = System.Drawing.Color.Transparent;
            button2.FlatAppearance.BorderSize = 0;
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            label8.Hide();
            label9.Hide();
            label10.Hide();
            label11.Hide();
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();
            textBox5.Hide();
            textBox6.Hide();
            textBox7.Hide();
            textBox8.Hide();
            textBox9.Hide();



        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex == 0)
            {
                addpet = true;
                checkedListBox1.SetItemChecked(1, false);
                label3.Text = "Name: ";
                label4.Text = "Gender: ";
                label5.Text = "Category: ";
                label6.Text = "Species: ";
                label7.Text = "Breed: ";
                label8.Text = "Age: ";
                label9.Text = "Health: ";
                label10.Text = "Questid: ";
                label11.Text = "Description: ";

                label3.Show();
                label4.Show();
                label5.Show();
                label6.Show();
                label7.Show();
                label8.Show();
                label9.Show();
                label10.Show();
                label11.Show();
                textBox1.Show();
                textBox2.Show();
                textBox3.Show();
                textBox4.Show();
                textBox5.Show();
                textBox6.Show();
                textBox7.Show();
                textBox8.Show();
                textBox9.Show();


            }
            if (checkedListBox1.SelectedIndex == 1)
            {
                addpet = false;
                checkedListBox1.SetItemChecked(0, false);
                label3.Text = "iName: ";
                label4.Text = "Quantity: ";
                label5.Text = "Price: ";
                label11.Text = "Description: ";
               

                label3.Show();
                label4.Show();
                label5.Show();
                label11.Show();
             
                textBox1.Show();
                textBox2.Show();
                textBox3.Show();
                textBox9.Show();

                label6.Hide();
                label7.Hide();
                label8.Hide();
                label9.Hide();
                label10.Hide();
                textBox4.Hide();
                textBox5.Hide();
                textBox6.Hide();
                textBox7.Hide();
                textBox8.Hide();
                


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(addpet==true)
            {
                bool k = h.checkTextToInt(textBox6.Text);
                if(k==false)
                {
                    MessageBox.Show("Please insert a valid number of months in the age field!");
                }
                else
                {
                    PetModel NewPet = new PetModel
                    {
                        name = textBox1.Text,
                        gender = textBox2.Text,
                        category = textBox3.Text,
                        species = textBox4.Text,
                        breed = textBox5.Text,
                        age = textBox6.Text,
                        adoption = "Adopt Now",
                        health = textBox7.Text,
                        questid = textBox8.Text,
                        description = textBox9.Text
                    };
                    db.InsertRecord<PetModel>("pets", NewPet);
                    MessageBox.Show("New pet added succsefully!");
                    this.Close();
                }
            }
            if (addpet == false)
            {
                bool k = h.checkTextToInt(textBox3.Text);
                if (k == false)
                {
                    MessageBox.Show("Please insert a valid price!");
                }
                else
                {
                    ItemModel NewItem = new ItemModel
                    {
                        iName = textBox1.Text,
                        quantity = textBox2.Text,
                        price = textBox3.Text,
                        description = textBox9.Text
                    };
                    db.InsertRecord<ItemModel>("items", NewItem);
                    MessageBox.Show("New item added succsefully!");
                    this.Close();
                }
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

        private void addrec_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
