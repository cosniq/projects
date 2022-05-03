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
    public partial class adoption : Form
    {
        string idu;
        MongoCRUD db;
        Helper aux = new Helper();
        string[] allPetsid = new string[500];
        string[] allCategories = new string[500];
        string[] allSpecies = new string[500];
        string[] allBreeds = new string[500];
        string[] filteredNames = new string[500];
        string[] filteredId = new string[500];
        int filterednumber = 0;
        bool male, female;
        int contCat, contSpec, contBreed;
        int pcounter;
        string petname;
        int petindex;
        string selcategory;
        string selspecies;
        string selbreed;
        bool nofilter = true;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public adoption(string id)
        {
            InitializeComponent();
            this.idu = id;
            db = new MongoCRUD("PetShop");
        }

        private void adoption_Load(object sender, EventArgs e)
        {
            male = false;
            female = false;
            this.TransparencyKey = System.Drawing.Color.Cyan;
            label1.BackColor = System.Drawing.Color.Transparent;
            label2.BackColor = System.Drawing.Color.Transparent;
            label3.BackColor = System.Drawing.Color.Transparent;
            button2.BackColor = System.Drawing.Color.Transparent;
            button3.BackColor = System.Drawing.Color.Transparent;
            button4.BackColor = System.Drawing.Color.Transparent;
            button5.BackColor = System.Drawing.Color.Transparent;
            button6.BackColor = System.Drawing.Color.Transparent;
            button7.BackColor = System.Drawing.Color.Transparent;
            button2.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.BorderSize = 0;
            button4.FlatAppearance.BorderSize = 0;
            button5.FlatAppearance.BorderSize = 0;
            button6.FlatAppearance.BorderSize = 0;
            button7.FlatAppearance.BorderSize = 0;
            var user = db.LoadRecordById<UserModel>("user", idu);
            var dbpets = db.LoadRecords<PetModel>("pets");
            pcounter = 0;
            contBreed = 0;
            contCat = 0;
            contSpec = 0;
            int j = 0;
            bool k = false;
            foreach (var rec in dbpets)
            {
                k = false;
                listBox1.Items.Add(rec.name);
                allPetsid[pcounter] = rec.id;
                if (contBreed == 0) {
                    allBreeds[contBreed] = rec.breed;
                    contBreed++;
                }
                else
                {

                    
                    for (j = 0; j < contBreed; j++)
                    {
                        if ((allBreeds[j] == rec.breed) && (rec.breed != "") && (rec.breed != null) && (allBreeds[j] != "")) k = true;
                    }
                    if (k == false && (rec.breed != null))
                    {
                        allBreeds[contBreed] = rec.breed;
                        contBreed++;
                        k = true;
                    }
                    k = false;
                }
                k = false;
                if (contCat == 0)
                {
                    allCategories[contCat] = rec.category;
                    contCat++;
                }
                else
                {
                    for (j = 0; j < contCat; j++)
                    {
                        if ((allCategories[j] == rec.category) && (rec.category != "") && (rec.category != null) && (allCategories[j] != ""))
                        {
                            k = true;

                        }
                    }
                    if (k == false)
                    {
                        allCategories[contCat] = rec.category;
                        contCat++;
                        k = true;
                    }
                    k = false;
                }
                k = false;
                if (contSpec == 0)
                {
                    allSpecies[contSpec] = rec.species;
                    contSpec++;
                }
                else
                {
                    for (j = 0; j < contSpec; j++)
                    {
                        if ((allSpecies[j] == rec.species) && (rec.species != "") && (rec.category != null)) k = true;
                    }
                    if (k == false)
                    {
                        allSpecies[contSpec] = rec.species;
                        contSpec++;
                        k = true;
                    }
                    k = false;
                }
                pcounter = pcounter + 1;
            }

            for (int a = 0; a < contBreed; a++)
            {
                comboBox3.Items.Add(allBreeds[a]);
            }
            for (int a = 0; a < contCat; a++)
            {
                comboBox1.Items.Add(allCategories[a]);
            }
            for (int a = 0; a < contSpec; a++)
            {
                comboBox2.Items.Add(allSpecies[a]);
            }
        }

        private void adoption_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            petname = listBox1.GetItemText(listBox1.SelectedItem);
            petindex = listBox1.SelectedIndex;
            PetModel pet;
            if (nofilter == true)
            {

                pet = db.LoadRecordById<PetModel>("pets", allPetsid[petindex]);

            }
            else
            {

                pet = db.LoadRecordById<PetModel>("pets", filteredId[petindex]);

            }
            if(pet.adoption=="Adopt Now")
            {
                db.UpdateGeneral<PetModel>("pets", pet.id, "adoption", idu);
                MessageBox.Show("An adoption request has been sent!" + Environment.NewLine + "        Stay tuned!");
            }
            else
            {
                MessageBox.Show("This pet is already adopted or is undergoing adoption! Please choose another pet!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);

        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.FlatAppearance.BorderSize = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            filterednumber = 0;
            nofilter = false;
            listBox1.Items.Clear();
            if (checkBox1.Checked == true) male = true; else male = false;
            if (checkBox2.Checked == true) female = true; else female = false;
            if (selcategory != null)
            {
                //MessageBox.Show("Category " + selcategory);
                foreach (string oneid in allPetsid)
                {
                    if (oneid != null)
                    {
                        var individual = db.LoadRecordById<PetModel>("pets", oneid);
                        if (male == false && female == false)
                        {
                            if (individual.category == selcategory)
                            {
                                filteredId[filterednumber] = individual.id;
                                filteredNames[filterednumber] = individual.name;
                                filterednumber++;

                            }
                        }
                        else
                        {
                            if (male == true)
                            {
                                if (individual.category == selcategory)
                                {
                                    if (individual.gender == "M")
                                    {
                                        filteredId[filterednumber] = individual.id;
                                        filteredNames[filterednumber] = individual.name;
                                        filterednumber++;
                                    }
                                }
                            }
                            if (female == true)
                            {
                                if (individual.category == selcategory)
                                {
                                    if (individual.gender == "F")
                                    {
                                        filteredId[filterednumber] = individual.id;
                                        filteredNames[filterednumber] = individual.name;
                                        filterednumber++;
                                    }
                                }
                            }
                        }

                    }


                }
                for (int j = 0; j < filterednumber; j++)
                {
                    listBox1.Items.Add(filteredNames[j]);
                }
            }
            if (selspecies != null)
            {
                //MessageBox.Show("Species " + selspecies);
                foreach (string oneid in allPetsid)
                {
                    if (oneid != null)
                    {
                        var individual = db.LoadRecordById<PetModel>("pets", oneid);
                        if (male == false && female == false)
                        {
                            if (individual.species == selspecies)
                            {
                                filteredId[filterednumber] = individual.id;
                                filteredNames[filterednumber] = individual.name;
                                filterednumber++;

                            }
                        }
                        else
                        {
                            if (male == true)
                            {
                                if (individual.species == selspecies)
                                {
                                    if (individual.gender == "M")
                                    {
                                        filteredId[filterednumber] = individual.id;
                                        filteredNames[filterednumber] = individual.name;
                                        filterednumber++;
                                    }
                                }
                            }
                            if (female == true)
                            {
                                if (individual.species == selspecies)
                                {
                                    if (individual.gender == "F")
                                    {
                                        filteredId[filterednumber] = individual.id;
                                        filteredNames[filterednumber] = individual.name;
                                        filterednumber++;
                                    }
                                }
                            }
                        }

                    }


                }
                for (int j = 0; j < filterednumber; j++)
                {
                    listBox1.Items.Add(filteredNames[j]);
                }
            }
            if (selbreed != null)
            {
                //MessageBox.Show("Breed " + selbreed);
                foreach (string oneid in allPetsid)
                {
                    if (oneid != null)
                    {
                        var individual = db.LoadRecordById<PetModel>("pets", oneid);
                        if (male == false && female == false)
                        {
                            if (individual.breed == selbreed)
                            {
                                filteredId[filterednumber] = individual.id;
                                filteredNames[filterednumber] = individual.name;
                                filterednumber++;

                            }
                        }
                        else
                        {
                            if (male == true)
                            {
                                if (individual.breed == selbreed)
                                {
                                    if (individual.gender == "M")
                                    {
                                        filteredId[filterednumber] = individual.id;
                                        filteredNames[filterednumber] = individual.name;
                                        filterednumber++;
                                    }
                                }
                            }
                            if (female == true)
                            {
                                if (individual.breed == selbreed)
                                {
                                    if (individual.gender == "F")
                                    {
                                        filteredId[filterednumber] = individual.id;
                                        filteredNames[filterednumber] = individual.name;
                                        filterednumber++;
                                    }
                                }
                            }
                        }

                    }


                }
                for (int j = 0; j < filterednumber; j++)
                {
                    listBox1.Items.Add(filteredNames[j]);
                }
            }


        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.FlatAppearance.BorderSize = 1;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.FlatAppearance.BorderSize = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.GetItemText(comboBox1.SelectedItem) != null)
            {
                //comboBox2.Enabled = false;
                //comboBox3.Enabled = false;
                comboBox2.ResetText();
                comboBox3.ResetText();
                selcategory = comboBox1.GetItemText(comboBox1.SelectedItem);
                selspecies = null;
                selbreed = null;
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.GetItemText(comboBox2.SelectedItem) != null)
            {

                comboBox1.ResetText();
                comboBox3.ResetText();
                selcategory = null;
                selspecies = comboBox2.GetItemText(comboBox2.SelectedItem);
                selbreed = null;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.GetItemText(comboBox3.SelectedItem) != null)
            {

                comboBox2.ResetText();
                comboBox1.ResetText();
                selcategory = null;
                selspecies = null;
                selbreed = comboBox3.GetItemText(comboBox3.SelectedItem);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            nofilter = true;
            foreach (string idp in allPetsid)
            {
                if (idp != null)
                {
                    var pet = db.LoadRecordById<PetModel>("pets", idp);
                    listBox1.Items.Add(pet.name);
                }

            }
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            button5.FlatAppearance.BorderSize = 1;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.FlatAppearance.BorderSize = 0;
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            button4.FlatAppearance.BorderSize = 1;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.FlatAppearance.BorderSize = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox1.Text!="")
            {
                listBox1.Items.Clear();
                nofilter = false;
                filteredId = aux.clearString(filteredId);
                filteredNames = aux.clearString(filteredNames);
                filterednumber = 0;
                foreach (string oneid in allPetsid)
                {
                    if (oneid != null)
                    {
                        var individual = db.LoadRecordById<PetModel>("pets", oneid);

                        if (individual.name == textBox1.Text)
                        {
                            filteredId[filterednumber] = individual.id;
                            filteredNames[filterednumber] = individual.name;
                            filterednumber++;

                        }

                    }
                }
                if(filterednumber>0)
                {
                    for (int j = 0; j < filterednumber; j++)
                    {
                        listBox1.Items.Add(filteredNames[j]);
                    }
                }else MessageBox.Show("No name found! Please write the desired name in the box!");

            }
            else MessageBox.Show("No name found! Please write the desired name in the box!");

        }

        private void button6_MouseEnter(object sender, EventArgs e)
        {
            button6.FlatAppearance.BorderSize = 1;
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            button6.FlatAppearance.BorderSize = 0;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var user = db.LoadRecordById<UserModel>("user", idu);
            bool viable;
            if (user.quest.Length == 4) viable = true; else viable = false;
            if (viable == true)
            {
                filteredId = aux.clearString(filteredId);
                filteredNames = aux.clearString(filteredNames);
                filterednumber = 0;
                int quid = Convert.ToInt32(user.quest);
                listBox1.Items.Clear();
                nofilter = false;
                bool viable2;
                foreach (string oneid in allPetsid)
                {
                    if (oneid != null)
                    {
                        var individual = db.LoadRecordById<PetModel>("pets", oneid);
                        int qpid = Convert.ToInt32(individual.questid);
                        viable2 = aux.questVerify(quid, qpid);

                        if (viable2 == true)
                        {
                            filteredId[filterednumber] = individual.id;
                            filteredNames[filterednumber] = individual.name;
                            filterednumber++;

                        }

                    }
                }
                for (int j = 0; j < filterednumber; j++)
                {
                    listBox1.Items.Add(filteredNames[j]);
                }
            }
            else MessageBox.Show("Please take the Matching Test first!");

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form back = new users(idu);
            back.Show();
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
            }
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.FlatAppearance.BorderSize = 0;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            petname = listBox1.GetItemText(listBox1.SelectedItem);
            petindex = listBox1.SelectedIndex;
            PetModel pet;
            if (nofilter == true)
            {

                pet = db.LoadRecordById<PetModel>("pets", allPetsid[petindex]);

            }
            else
            {

                pet = db.LoadRecordById<PetModel>("pets", filteredId[petindex]);

            }
            label5.Text = "Name: " + Environment.NewLine + pet.name;
            label6.Text = "Gender: " + Environment.NewLine + pet.gender;
            label7.Text = "Category: " + Environment.NewLine + pet.category;
            label8.Text = "Species: " + Environment.NewLine + pet.species;
            label9.Text = "Breed: " + Environment.NewLine + pet.breed;
            label10.Text = "Age: " + Environment.NewLine + pet.age + " months";
            if(pet.adoption=="Adopted" || pet.adoption=="Adopt Now") label11.Text = "Adoption: " + Environment.NewLine + pet.adoption;
                else label11.Text = "Adoption: " + Environment.NewLine + " Undergoing";
            label12.Text = "Health: " + Environment.NewLine + pet.health;
            label13.Text = "Description: " + Environment.NewLine + pet.description;
            //label13.Size = new Size(320, 70);


        }
    }
}
