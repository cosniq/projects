using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace Tester
{
    public partial class Form1 : Form
    {
        int instance = 0;
        string line;
        string[] lines = new string[1000];
        int contor = 1;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            label13.BackColor = System.Drawing.Color.Transparent;
            string msgdel = "Index you want to be"+Environment.NewLine+"deleted";
            label13.Text = msgdel;
            label13.Hide();
            textBox4.Hide();
            button3.Hide();
            button4.Hide();
            string Path = AppDomain.CurrentDomain.BaseDirectory + "Tests_Data.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(Path);
            while ((line = file.ReadLine()) != null)
            {
                lines[contor] = line;
                contor = contor + 1;

            }
            file.Close();
            string input1="";
            for(int j=1;j<contor;j++)
            {
                 input1 = input1 + lines[j]+Environment.NewLine;
               // MessageBox.Show(j+" and: "+lines[j]);
            }
            //  int no_richText = 0;
            //  int no_checkList = 0;
            // MessageBox.Show(input1);
           
            for(int j=1;j<contor;j=j+7)
            {
                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
              
                tableLayoutPanel1.Controls.Add(new Label() { Text = lines[j] }, 0, tableLayoutPanel1.RowCount-1);
                tableLayoutPanel1.Controls.Add(new RichTextBox() { Text = lines[j + 1], Size = new Size(135,50) }, 1, tableLayoutPanel1.RowCount - 1) ;
               
                tableLayoutPanel1.Controls.Add(new RichTextBox() { Text = lines[j + 2], Size = new Size(135, 50) }, 2, tableLayoutPanel1.RowCount-1);
                
                tableLayoutPanel1.Controls.Add(new RichTextBox() { Text = lines[j + 3], Size = new Size(135, 50) }, 3, tableLayoutPanel1.RowCount-1);
                
                tableLayoutPanel1.Controls.Add(new RichTextBox() { Text = lines[j + 4], Size = new Size(135, 50) }, 4, tableLayoutPanel1.RowCount-1);
                
                tableLayoutPanel1.Controls.Add(new RichTextBox() { Text = lines[j + 5], Size = new Size(135, 50) }, 5, tableLayoutPanel1.RowCount-1);
          
                //  tableLayoutPanel1.Controls.Add(new CheckedListBox(), no_checkList + 1, tableLayoutPanel1.RowCount - 1);
                //  no_checkList++;
                tableLayoutPanel1.Controls.Add(new RichTextBox() { Text = lines[j + 6], Size = new Size(135, 50) }, 6, tableLayoutPanel1.RowCount-1);
               

            }
           
            label1.BackColor = System.Drawing.Color.Transparent;
            label2.BackColor = System.Drawing.Color.Transparent;
            label3.BackColor = System.Drawing.Color.Transparent;
            label4.BackColor = System.Drawing.Color.Transparent;
            label5.BackColor = System.Drawing.Color.Transparent;
            label6.BackColor = System.Drawing.Color.Transparent;
            label7.BackColor = System.Drawing.Color.Transparent;
            label8.BackColor = System.Drawing.Color.Transparent;
            label9.BackColor = System.Drawing.Color.Transparent;
            label10.BackColor = System.Drawing.Color.Transparent;
            label11.BackColor = System.Drawing.Color.Transparent;
            label12.BackColor = System.Drawing.Color.Transparent;
         
            tableLayoutPanel1.Hide();
            label6.Text = "Test Index";
            label7.Text = "Description";
            label8.Text = "Watching this application state";
            label9.Text = "Input for test";
            label10.Text = "Expected result";
            label11.Text = "Result recieved";
            label12.Text = "Passed or failed";
            textBox1.Hide();
            textBox2.Hide();
            label1.Hide();
            label2.Hide();
            label5.Hide();

            label5.Text = "These are the tests scheduled";
            button1.Text = "Content";
            label1.Text = "Username";
            label2.Text = "Password";
            label3.Text = "DANCK Tester";
            //      string msg = "Please log in with the teste user and password." + Environment.NewLine+"This side app was made for testing the principal application only." ;
            string msg = "Press the Content button to display the tests.";
            label4.Text = msg;

            label1.Font = new System.Drawing.Font("Times New Roman", 20);
            label2.Font = new System.Drawing.Font("Times New Roman", 20);
            label3.Font = new System.Drawing.Font("Times New Roman", 30);
            label4.Font = new System.Drawing.Font("Times New Roman", 13);
            label5.Font = new System.Drawing.Font("Times New Roman", 13);
            label13.Font = new System.Drawing.Font("Times New Roman", 13);
            //MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory);

            // label2.Font = new Font("Times New Roman", label2.Size;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "onbutton_click_w.wav";
            player.Play();
            if (instance == 0)
            {
                label4.Hide();
              //  label13.Show();
              //  textBox4.Show();
               button3.Show();
             //   button4.Show();
                instance++;
                button1.Text = "Add";
              
                label5.Show();
                tableLayoutPanel1.Show();
            } else if(instance==1)
            {
                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
                tableLayoutPanel1.Controls.Add(new Label() { Text = Convert.ToString(tableLayoutPanel1.RowCount-1) }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new RichTextBox() { Text = "Insert Description", Size = new Size(135, 50) }, 1, tableLayoutPanel1.RowCount - 1);

                tableLayoutPanel1.Controls.Add(new RichTextBox() { Text = "Specify the instance", Size = new Size(135, 50) }, 2, tableLayoutPanel1.RowCount - 1);

                tableLayoutPanel1.Controls.Add(new RichTextBox() { Text ="Insert input", Size = new Size(135, 50) }, 3, tableLayoutPanel1.RowCount - 1);

                tableLayoutPanel1.Controls.Add(new RichTextBox() { Text = "Insert expectations", Size = new Size(135, 50) }, 4, tableLayoutPanel1.RowCount - 1);

                tableLayoutPanel1.Controls.Add(new RichTextBox() { Text = "Yet to be tested", Size = new Size(135, 50) }, 5, tableLayoutPanel1.RowCount - 1);

                //  tableLayoutPanel1.Controls.Add(new CheckedListBox(), no_checkList + 1, tableLayoutPanel1.RowCount - 1);
                //  no_checkList++;
                tableLayoutPanel1.Controls.Add(new RichTextBox() { Text = "Passed/Failed", Size = new Size(135, 50) }, 6, tableLayoutPanel1.RowCount - 1);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "onbutton_click_w.wav";
            player.Play();
            MessageBox.Show("Thank you for your visit!");
            System.Environment.Exit(0);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "onbutton_click_w.wav";
            player.Play();
            string salvare = "";
            //MessageBox.Show(Convert.ToString(tableLayoutPanel1.RowCount));
              for (int j = 1; j < tableLayoutPanel1.RowCount; j++)
              {
                  if (j == tableLayoutPanel1.RowCount -1)
                  {
                      salvare = salvare + tableLayoutPanel1.GetControlFromPosition(0, j).Text + Environment.NewLine;
                      salvare = salvare + tableLayoutPanel1.GetControlFromPosition(1, j).Text + Environment.NewLine;
                      salvare = salvare + tableLayoutPanel1.GetControlFromPosition(2, j).Text + Environment.NewLine;
                      salvare = salvare + tableLayoutPanel1.GetControlFromPosition(3, j).Text + Environment.NewLine;
                      salvare = salvare + tableLayoutPanel1.GetControlFromPosition(4, j).Text + Environment.NewLine;
                      salvare = salvare + tableLayoutPanel1.GetControlFromPosition(5, j).Text + Environment.NewLine;
                      salvare = salvare + tableLayoutPanel1.GetControlFromPosition(6, j).Text;
                  }else
                  {
                      salvare = salvare + tableLayoutPanel1.GetControlFromPosition(0, j).Text + Environment.NewLine;
                      salvare = salvare + tableLayoutPanel1.GetControlFromPosition(1, j).Text + Environment.NewLine;
                      salvare = salvare + tableLayoutPanel1.GetControlFromPosition(2, j).Text + Environment.NewLine;
                      salvare = salvare + tableLayoutPanel1.GetControlFromPosition(3, j).Text + Environment.NewLine;
                      salvare = salvare + tableLayoutPanel1.GetControlFromPosition(4, j).Text + Environment.NewLine;
                      salvare = salvare + tableLayoutPanel1.GetControlFromPosition(5, j).Text + Environment.NewLine;
                      salvare = salvare + tableLayoutPanel1.GetControlFromPosition(6, j).Text + Environment.NewLine;
                }

              }
            string Path = AppDomain.CurrentDomain.BaseDirectory + "Tests_Data.txt";
            StreamWriter outputFile = new StreamWriter(Path);
            outputFile.WriteLine(salvare);
            outputFile.Close();
            MessageBox.Show("Table saved!");
           // MessageBox.Show(salvare); 
          /*  Control c = tableLayoutPanel1.GetControlFromPosition(1, 2);
            string caca = tableLayoutPanel1.GetControlFromPosition(5, 1).Text;
            MessageBox.Show(caca); */
        }

    /*    Point? GetRowColIndex(TableLayoutPanel tlp, Point point)
        {
            if (point.X > tlp.Width || point.Y > tlp.Height)
                return null;

            int w = tlp.Width;
            int h = tlp.Height;
            int[] widths = tlp.GetColumnWidths();

            int i;
            for (i = widths.Length - 1; i > 0 && point.X < w; i--)
                w -= widths[i];
            int col = i + 1;

            int[] heights = tlp.GetRowHeights();
            for (i = heights.Length - 1; i >= 0 && point.Y < h; i--)
                h -= heights[i];

            int row = i + 1;

            return new Point(col, row);
        } */

        private void tableLayoutPanel1_Click(object sender, EventArgs e)
        {
          /*  var cellPos = GetRowColIndex(
            tableLayoutPanel1,
            tableLayoutPanel1.PointToClient(Cursor.Position));
            MessageBox.Show(Convert.ToString(cellPos.Value)); */
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.RemoveAt(Convert.ToInt32(textBox4.Text) + 1);
        }
    }
}
