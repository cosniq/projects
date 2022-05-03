using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WildHouse
{

    public partial class shop : Form
    {
        MongoCRUD db;
        string idu;
        Control c,c1,c2;
        int selectedrow;
        bool selected = false;
        int price = 0;
        string selCart = null;
        int selIndex;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public shop(string id)
        {
            InitializeComponent();
            this.idu = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void shop_Load(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.RowCount = 0;
            db = new MongoCRUD("PetShop");
            var theuser = db.LoadRecordById<UserModel>("user", idu);
            label6.Text = "BALANCE: " + theuser.balance+" RON";
            this.TransparencyKey = System.Drawing.Color.Cyan;
            button2.BackColor = System.Drawing.Color.Transparent;
            button3.BackColor = System.Drawing.Color.Transparent;
            button4.BackColor = System.Drawing.Color.Transparent;
            button5.BackColor = System.Drawing.Color.Transparent;
            button2.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.BorderSize = 0;
            button4.FlatAppearance.BorderSize = 0;
            button5.FlatAppearance.BorderSize = 0;


            var recs = db.LoadRecords<ItemModel>("items");
            foreach(var rec in recs)
            {
                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));

                tableLayoutPanel1.Controls.Add(new Label() { Text = rec.iName }, 0, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = rec.quantity }, 1, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = rec.price + " RON" }, 2, tableLayoutPanel1.RowCount - 1);
                tableLayoutPanel1.Controls.Add(new Label() { Text = rec.description, Size=new Size(353,40) }, 3, tableLayoutPanel1.RowCount - 1);
            }
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.FlatAppearance.BorderSize = 1;
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.FlatAppearance.BorderSize = 1;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.FlatAppearance.BorderSize = 0;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.FlatAppearance.BorderSize = 0;
        }

        private void shop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            var cellPos = GetRowColIndex(tableLayoutPanel1,tableLayoutPanel1.PointToClient(Cursor.Position));
            int row = 0;
            int verticalOffset = 0;
            foreach (int h in tableLayoutPanel1.GetRowHeights())
            {
                int column = 0;
                int horizontalOffset = 0;
                foreach (int w in tableLayoutPanel1.GetColumnWidths())
                {
                    Rectangle rectangle = new Rectangle(horizontalOffset, verticalOffset, w, h);
                    if (rectangle.Contains(e.Location))
                    {
                        c = this.tableLayoutPanel1.GetControlFromPosition(0, row);
                        c1 = this.tableLayoutPanel1.GetControlFromPosition(1, row);
                        c2 = this.tableLayoutPanel1.GetControlFromPosition(2, row);
                        label8.Text = "You have selected: " + c.Text;
                        selectedrow = row;
                        selected = true;
                        return;
                    }
                    horizontalOffset += w;
                    column++;
                }
                verticalOffset += h;
                row++;
            }

            
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.FlatAppearance.BorderSize = 0;
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            button4.FlatAppearance.BorderSize = 1;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selCart = listBox1.GetItemText(listBox1.SelectedItem);
            //label5.Text = selCart;
            selIndex = listBox1.SelectedIndex;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (selCart != null)
            {
                
                var forupdate = db.LoadRecordByiName<ItemModel>("items", selCart);
                db.UpdateGeneral<ItemModel>("items", forupdate.id, "quantity", (Convert.ToInt32(forupdate.quantity)+1).ToString());
                price = price - Convert.ToInt32(forupdate.price);
                
                tableLayoutPanel1.Controls.Clear();
                tableLayoutPanel1.RowStyles.Clear();
                tableLayoutPanel1.RowCount = 0;
                var recs = db.LoadRecords<ItemModel>("items");
                foreach (var rec in recs)
                {
                    tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));

                    tableLayoutPanel1.Controls.Add(new Label() { Text = rec.iName }, 0, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = rec.quantity }, 1, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = rec.price + " RON" }, 2, tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.Controls.Add(new Label() { Text = rec.description, Size = new Size(353, 40) }, 3, tableLayoutPanel1.RowCount - 1);
                    // MessageBox.Show(rec.description);

                }
                label9.Text = "Total: " + price+" RON";
                listBox1.Items.RemoveAt(selIndex);
            }
            else MessageBox.Show("Please select an item from your cart!");
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int newbal;
            var acc = db.LoadRecordById<UserModel>("user", idu);
            if(Convert.ToInt32(acc.balance)>price)
            {
                newbal = Convert.ToInt32(acc.balance) - price;
                db.UpdateGeneral<UserModel>("user", idu, "balance", newbal.ToString());
                label6.Text = "Balance: " + newbal + " RON";
                listBox1.Items.Clear();
                MessageBox.Show("Thank you for your purchase!");
                Form user = new users(idu);
                user.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Not enough currency!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form back = new users(idu);
            back.Show();
            this.Close();
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            button5.FlatAppearance.BorderSize = 1;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.FlatAppearance.BorderSize = 0;
        }

        Point? GetRowColIndex(TableLayoutPanel tlp, Point point)
        {
            if (point.X > tlp.Width || point.Y > tlp.Height)
                return null;

            int w = tlp.Width;
            int h = tlp.Height;
            int[] widths = tlp.GetColumnWidths();

            int i;
            for (i = widths.Length - 1; i >= 0 && point.X < w; i--)
                w -= widths[i];
            int col = i + 1;

            int[] heights = tlp.GetRowHeights();
            for (i = heights.Length - 1; i >= 0 && point.Y < h; i--)
                h -= heights[i];

            int row = i + 1;

            return new Point(col, row);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (selected == true)
            {
                if (Convert.ToInt32(c1.Text) > 0)
                {
                    listBox1.Items.Add(c.Text);
                    string pret = "";

                    for (int i = 0; i < c2.Text.Length - 4; i++)
                    {
                        pret = String.Concat(pret, c2.Text[i]);
                    }
                    price = price + Convert.ToInt32(pret);
                    label9.Text = "Total: " + price + " RON";

                    var forupdate = db.LoadRecordByiName<ItemModel>("items", c.Text);
                    ItemModel updatedItem = new ItemModel
                    {
                        //id = MongoDB.Bson.ObjectId.GenerateNewId(forupdate.id).ToString(),
                        id = forupdate.id,
                        iName = forupdate.iName,
                        quantity = (Convert.ToInt32(forupdate.quantity) - 1).ToString(),
                        price = forupdate.price,
                        description = forupdate.description
                    };



                    db.UpdateGeneral<ItemModel>("items", forupdate.id, "quantity", updatedItem.quantity);
                    //db.UpsertRecord<ItemModel>("items", ObjectId.Parse(forupdate.id), updatedItem);
                    tableLayoutPanel1.Controls.Clear();
                    tableLayoutPanel1.RowStyles.Clear();
                    tableLayoutPanel1.RowCount = 0;
                    var recs = db.LoadRecords<ItemModel>("items");
                    foreach (var rec in recs)
                    {
                        tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));

                        tableLayoutPanel1.Controls.Add(new Label() { Text = rec.iName }, 0, tableLayoutPanel1.RowCount - 1);
                        tableLayoutPanel1.Controls.Add(new Label() { Text = rec.quantity }, 1, tableLayoutPanel1.RowCount - 1);
                        tableLayoutPanel1.Controls.Add(new Label() { Text = rec.price + " RON" }, 2, tableLayoutPanel1.RowCount - 1);
                        tableLayoutPanel1.Controls.Add(new Label() { Text = rec.description, Size = new Size(353, 40) }, 3, tableLayoutPanel1.RowCount - 1);
                        // MessageBox.Show(rec.description);

                    }
                    c = this.tableLayoutPanel1.GetControlFromPosition(0, selectedrow);
                    c1 = this.tableLayoutPanel1.GetControlFromPosition(1, selectedrow);
                    c2 = this.tableLayoutPanel1.GetControlFromPosition(2, selectedrow);
                }
                else
                {
                    MessageBox.Show("                         WildHouse" + Environment.NewLine + Environment.NewLine + Environment.NewLine + "    We are out of this item! Please check later.");
                }
            }
            else MessageBox.Show("        WildHouse" + Environment.NewLine + Environment.NewLine + "Please select an item!");
        }
    }
}
