using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace Thesis
{
    
    
    public partial class Form1 : Form
    {
        static string RecievedPublishes, RP2;
        static string[] RP1=new string[24400];
        static string[] RPAcc = new string[24400];
        static string[] RPGyr = new string[24400];
        static int contor=1;
        static int contorAccelerometer = 1;
        static int contorGyroscope = 1;
        static int[] DataSet = new int[3];
        static int[] iAccX = new int[24400];
        static int[] iAccY = new int[24400];
        static int[] iAccZ = new int[24400];
        static int iAcc = 0;
        static int[] iGyrX = new int[24400];
        static int[] iGyrY = new int[24400];
        static int[] iGyrZ = new int[24400];
        static int iGyr = 0;
        static string allAccX, allAccY, allAccZ, allGyrX, allGyrY, allGyrZ, VeloX, VeloY, VeloZ, DispX, DispY, DispZ,timeA,timeG;
        static double[] VelocitiesX = new double[24400];
        static double[] VelocitiesY = new double[24400];
        static double[] VelocitiesZ = new double[24400];
        static double[] DisplacementX = new double[24400];
        static double[] DisplacementY = new double[24400];
        static double[] DisplacementZ = new double[24400];
        static int iteratii = 0;
        static int timpRulare = 0;
        static float[] samplingTimeA = new float[24400];
        static int contorA = 1;
        static float[] samplingTimeG = new float[24400];
        static int contorG = 1;


        public Form1()
        {
            InitializeComponent();
            label1.Text = "Motion Based Application: IoT Experimental Implementation"+Environment.NewLine+"                       for Testing Conceptual Development";
            label2.Text = Convert.ToChar(169) + " Costea Nicolae-Viorel UTCN";
            label7.Text = "Data display selection";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button4.Hide();
            MqttClient mqttClient = new MqttClient("192.168.100.5");
            mqttClient.Connect("ClientComputerUTCN", "admin", "hivemq");
            samplingTimeA[0] = 0;
            samplingTimeG[0] = 0;
            timeA = "All sampling time instances for accelerometer: "+samplingTimeA[0]+" ";
            timeG = "All sampling time instances for gyroscope: "+samplingTimeG[0]+" ";
            //from_6AF3EF42-8A06-4E42-A7CC-8692399E7581  --- iPhone
            //from_9d6956a3ac2ccd0f   ---Oppo
            mqttClient.MqttMsgPublishReceived += MqttPostProperty_MqttMsgPublishReceived;
            mqttClient.Subscribe(new string[] { "from_9d6956a3ac2ccd0f" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });

            timer1.Start();
            timer2.Start();
            
        }

       
        private static void MqttPostProperty_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            
            iteratii++;
            Console.WriteLine("reply topic  :" + e.Topic);
            Console.WriteLine("reply payload:" + e.Message.ToString());
            Console.WriteLine(System.Text.Encoding.UTF8.GetString(e.Message));
            RecievedPublishes = RecievedPublishes +Environment.NewLine + System.Text.Encoding.UTF8.GetString(e.Message);
            string onePublished = System.Text.Encoding.UTF8.GetString(e.Message);
            JObject json = JObject.Parse(onePublished);
            bool Accelerometer = false;
            bool Gyroscope = false;
            
            foreach (var ej in json)
            {
                if (ej.Key == "cmd_id" || ej.Key == "cmdVal") 
                {
                    RP2 = RP2 + ej.Key + " : " + ej.Value + Environment.NewLine;
                   // RP1[contor] = ej.Key + " : " + ej.Value;
                    
                    if (ej.Value != null)
                    {


                        if (ej.Value.ToString() == "A" || ej.Value.ToString() == "G")
                        {
                            RP1[contor] = ej.Value + " : ";
                        }
                        else
                        {
                            RP1[contor] = RP1[contor] + ej.Value;
                            contor++;
                        }

                        if(ej.Value.ToString() == "A")
                        {
                            Accelerometer = true;

                            samplingTimeA[contorA] = timpRulare;
                            timeA = timeA + (samplingTimeA[contorA]/100).ToString("n2") + " ";
                            contorA++;
                        }
                        else
                        {
                            if (Accelerometer == true)
                            {
                                //For saving the entire data set of 3 values 
                                RPAcc[contorAccelerometer] = ej.Value+" | ";
                                contorAccelerometer++;
                                Accelerometer = false;
                                //Breaking the data in 3 sets for each axis(X,Y,Z)
                                string InitialSet = ej.Value.ToString();
                                int incrementSet = 0;
                                foreach (var s in InitialSet.Split(','))
                                {
                                    int num;
                                    if (int.TryParse(s, out num))
                                    {
                                        DataSet[incrementSet] = num;
                                        incrementSet++;
                                    }
                                }
                                iAccX[iAcc] = DataSet[0];
                                iAccY[iAcc] = DataSet[1];
                                iAccZ[iAcc] = DataSet[2]+9;
                                iAcc++;

                            }
                        }
                        if (ej.Value.ToString() == "G")
                        {
                            Gyroscope = true;
                            samplingTimeG[contorG] = timpRulare;
                            timeG = timeG + (samplingTimeG[contorG]/100).ToString("n2") + " ";
                            contorG++;
                        }
                        else
                        {
                            if (Gyroscope == true)
                            {
                                //For saving the entire data set of 3 values
                                RPGyr[contorGyroscope] = ej.Value + " | ";
                                contorGyroscope++;
                                Gyroscope = false;
                                //Breaking the data in 3 sets for each axis(X,Y,Z)
                                string InitialSet2 = ej.Value.ToString();
                                int incrementSet2 = 0;
                                foreach (var s in InitialSet2.Split(','))
                                {
                                    int num2;
                                    if (int.TryParse(s, out num2))
                                    {
                                        DataSet[incrementSet2] = num2;
                                        incrementSet2++;
                                    }
                                }
                                iGyrX[iGyr] = DataSet[0];
                                iGyrY[iGyr] = DataSet[1];
                                iGyrZ[iGyr] = DataSet[2];
                                iGyr++;
                            }
                        }


                    }
                }

                    
            }
            // MessageBox.Show(System.Text.Encoding.UTF8.GetString(e.Message));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            /*
            */
            //System.Diagnostics.Process.Start(@"D:\Thesis C# client\Thesis\Cuburi\Cuburi.exe");
            MessageBox.Show(timeA+Environment.NewLine+timeG);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InitializeVelocitiesAndDisplacements();
            InitializeAccAndGyr();
            string printingData = "";
            string FinalDataSetAcc = string.Join("", RPAcc);
            string FinalDataSetGyr = string.Join("", RPGyr);

            var listOfChecks = checkedListBox1.CheckedItems;
            foreach(var check in listOfChecks)
            {
                if (check.ToString() == "Unformatted Data") printingData = printingData + "Accelerometer data sets: " + FinalDataSetAcc + Environment.NewLine + Environment.NewLine + "Gyroscope data sets: " + FinalDataSetGyr + Environment.NewLine + Environment.NewLine;
                if (check.ToString() == "Accelerometer Axis X") printingData=printingData+"Accelerometer values on axis X: "+allAccX+Environment.NewLine+Environment.NewLine;
                if (check.ToString() == "Accelerometer Axis Y") printingData = printingData + "Accelerometer values on axis Y: " + allAccY + Environment.NewLine + Environment.NewLine;
                if (check.ToString() == "Accelerometer Axis Z") printingData = printingData + "Accelerometer values on axis Z: " + allAccZ + Environment.NewLine + Environment.NewLine;
                if (check.ToString() == "Accelerometer Time Vector") printingData = printingData + timeA + Environment.NewLine + Environment.NewLine;
                if (check.ToString() == "Velocity Axis X") printingData = printingData + "Velocities on axis X: " + VeloX + Environment.NewLine + Environment.NewLine;
                if (check.ToString() == "Velocity Axis Y") printingData = printingData + "Velocities on axis Y: " + VeloY + Environment.NewLine + Environment.NewLine;
                if (check.ToString() == "Velocity Axis Z") printingData = printingData + "Velocities on axis Z: " + VeloZ + Environment.NewLine + Environment.NewLine;
                if (check.ToString() == "Displacement Axis X") printingData = printingData + "Displacement vector on axis X is: " + DispX + Environment.NewLine + Environment.NewLine;
                if (check.ToString() == "Displacement Axis Y") printingData = printingData + "Displacement vector on axis Y is: " + DispY + Environment.NewLine + Environment.NewLine;
                if (check.ToString() == "Displacement Axis Z") printingData = printingData + "Displacement vector on axis Z is: " + DispZ + Environment.NewLine + Environment.NewLine;
                if (check.ToString() == "Gyroscope Axis X") printingData = printingData + "Gyroscope values on axis X: " + allGyrX + Environment.NewLine + Environment.NewLine;
                if (check.ToString() == "Gyroscope Axis Y") printingData = printingData + "Gyroscope values on axis Y: " + allGyrY + Environment.NewLine + Environment.NewLine;
                if (check.ToString() == "Gyroscope Axis Z") printingData = printingData + "Gyroscope values on axis Z: " + allGyrZ + Environment.NewLine + Environment.NewLine;
                if (check.ToString() == "Gyroscope Time Vector") printingData = printingData  + timeG + Environment.NewLine + Environment.NewLine;

            }
            if (printingData == "") MessageBox.Show("Please select at least a data set for viewing");
            else MessageBox.Show(printingData);
                

            
            //string stuff=checkedListBox1.CheckedIndices+" ";
            //MessageBox.Show(stuff);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            Environment.Exit(0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string pathAcc = Environment.CurrentDirectory + @"\AccelerometerData.txt";
            string pathGyr = Environment.CurrentDirectory + @"\GyroscopeData.txt";
            string pathAccX = Environment.CurrentDirectory + @"\AccelerometerDataX.txt";
            string pathAccY = Environment.CurrentDirectory + @"\AccelerometerDataY.txt";
            string pathAccZ = Environment.CurrentDirectory + @"\AccelerometerDataZ.txt";
            string pathGyrX = Environment.CurrentDirectory + @"\GyroscopeDataX.txt";
            string pathGyrY = Environment.CurrentDirectory + @"\GyroscopeDataY.txt";
            string pathGyrZ = Environment.CurrentDirectory + @"\GyroscopeDataZ.txt";
            

            try
            {
                var f_acc = File.Create(pathAcc);
                f_acc.Close();
                string FinalDataSetAcc = string.Join("", RPAcc);
                //MessageBox.Show(FinalDataSetAcc);
                File.WriteAllText(pathAcc, FinalDataSetAcc);
                //System.Threading.Thread.Sleep(5000);


                var f_gyr = File.Create(pathGyr);
                f_gyr.Close();
                string FinalDataSetGyr = string.Join("", RPGyr);
                //MessageBox.Show(FinalDataSetGyr);
                File.WriteAllText(pathGyr, FinalDataSetGyr);


                //System.Threading.Thread.Sleep(5000);
                /*allAccX = iAccX[0] + " ";
                allAccY = iAccY[0] + " ";
                allAccZ = iAccZ[0] + " ";
                allGyrX = iGyrX[0] + " ";
                allGyrY = iGyrY[0] + " ";
                allGyrZ = iGyrZ[0] + " ";
                for (int a = 1; a < iAcc; a++)
                {
                    allAccX = allAccX + iAccX[a] + " ";
                    allAccY = allAccY + iAccY[a] + " ";
                    allAccZ = allAccZ + iAccZ[a] + " ";
                    allGyrX = allGyrX + iGyrX[a] + " ";
                    allGyrY = allGyrY + iGyrY[a] + " ";
                    allGyrZ = allGyrZ + iGyrZ[a] + " ";

                } */
                InitializeAccAndGyr();
                var f_accX = File.Create(pathAccX);
                f_accX.Close();
                File.WriteAllText(pathAccX, allAccX);
                
                var f_accY = File.Create(pathAccY);
                f_accY.Close();
                File.WriteAllText(pathAccY, allAccY);

                var f_accZ = File.Create(pathAccZ);
                f_accZ.Close();
                File.WriteAllText(pathAccZ, allAccZ);

                var f_gyrX = File.Create(pathGyrX);
                f_gyrX.Close();
                File.WriteAllText(pathGyrX, allGyrX);

                var f_gyrY = File.Create(pathGyrY);
                f_gyrY.Close();
                File.WriteAllText(pathGyrY, allGyrY);

                var f_gyrZ = File.Create(pathGyrZ);
                f_gyrZ.Close();
                File.WriteAllText(pathGyrZ, allGyrZ);

                InitializeVelocitiesAndDisplacements();
                DispX = iAcc + " " + DispX;
                DispY = iAcc + " " + DispY;
                DispZ = iAcc + " " + DispZ;
                //Create file for axis X of Displacement
                string pathDisplacementX = @"D:\Thesis C# client\Thesis\Cuburi\DisplacementX.txt";
                var f_dispX = File.Create(pathDisplacementX);
                f_dispX.Close();
                File.WriteAllText(pathDisplacementX, DispX);
                //Create file for axis Y of Displacement
                string pathDisplacementY = @"D:\Thesis C# client\Thesis\Cuburi\DisplacementY.txt";
                var f_dispY = File.Create(pathDisplacementY);
                f_dispY.Close();
                File.WriteAllText(pathDisplacementY, DispY);
                //Create file for axis Z of Displacement
                string pathDisplacementZ = @"D:\Thesis C# client\Thesis\Cuburi\DisplacementZ.txt";
                var f_dispZ = File.Create(pathDisplacementZ);
                f_dispZ.Close();
                File.WriteAllText(pathDisplacementZ, DispZ);
                //Push to cubes code
                InitializeAccAndGyr();
                allGyrX = iGyr + " " + allGyrX;
                allGyrY = iGyr + " " + allGyrY;
                allGyrZ = iGyr + " " + allGyrZ;
                string pathAllGyrX = @"D:\Thesis C# client\Thesis\Cuburi\AllGyrX.txt";
                var f_allgyrX = File.Create(pathAllGyrX);
                f_allgyrX.Close();
                File.WriteAllText(pathAllGyrX, allGyrX);

                string pathAllGyrY = @"D:\Thesis C# client\Thesis\Cuburi\AllGyrY.txt";
                var f_allgyrY = File.Create(pathAllGyrY);
                f_allgyrY.Close();
                File.WriteAllText(pathAllGyrY, allGyrY);

                string pathAllGyrZ = @"D:\Thesis C# client\Thesis\Cuburi\AllGyrZ.txt";
                var f_allgyrZ = File.Create(pathAllGyrZ);
                f_allgyrZ.Close();
                File.WriteAllText(pathAllGyrZ, allGyrZ);

                string pathTimeA = @"D:\Thesis C# client\Thesis\Cuburi\TimeA.txt";
                var f_TimeA = File.Create(pathTimeA);
                f_TimeA.Close();
                File.WriteAllText(pathTimeA, timeA);

                string pathTimeG = @"D:\Thesis C# client\Thesis\Cuburi\TimeG.txt";
                var f_TimeG = File.Create(pathTimeG);
                f_TimeG.Close();
                File.WriteAllText(pathTimeG, timeG);



                MessageBox.Show("All data was saved succssefully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            var GyrChart = chart2.ChartAreas[0];
            GyrChart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            //time axis
            GyrChart.AxisX.Minimum = 0;
            GyrChart.AxisX.Maximum = 10;// (iGyr + 1) / 2;
            GyrChart.AxisX.Title = "Time";
            //sensor axis
            GyrChart.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            GyrChart.AxisY.Minimum = 10;//ValueSecurityCheck(LowestValue(iGyrY.Min(), iGyrX.Min(), iGyrZ.Min()));
            GyrChart.AxisY.Maximum = 10;// ValueSecurityCheck(HighestValue(iGyrX.Max(), iGyrY.Max(), iGyrZ.Max()));
            GyrChart.AxisY.Title = "Gyroscope" + Environment.NewLine + " Sensor Values";
            //clear
            chart2.Series.Clear();

            chart2.Series.Add("X");
            chart2.Series["X"].Color = Color.FromArgb(255, 0, 0);
            chart2.Series["X"].Legend = "Legend1";
            chart2.Series["X"].ChartArea = "ChartArea1";
            chart2.Series["X"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            chart2.Series.Add("Y");
            chart2.Series["Y"].Color = Color.FromArgb(0, 0, 255);
            chart2.Series["Y"].Legend = "Legend1";
            chart2.Series["Y"].ChartArea = "ChartArea1";
            chart2.Series["Y"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            chart2.Series.Add("Z");
            chart2.Series["Z"].Color = Color.FromArgb(255, 0, 255);
            chart2.Series["Z"].Legend = "Legend1";
            chart2.Series["Z"].ChartArea = "ChartArea1";
            chart2.Series["Z"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            double timp2 = 0;
            for (int j = 0; j < iAcc; j++)
            {
                chart2.Series["X"].Points.AddXY(timp2, iGyrX[j]);
                chart2.Series["Y"].Points.AddXY(timp2, iGyrY[j]);
                chart2.Series["Z"].Points.AddXY(timp2, iGyrZ[j]);
                timp2 = timp2 + 0.5;
            }
        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }
       
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            label6.Text = "Time since application started running(seconds/100):  "+ timpRulare;
            
            timpRulare++;


        }

        private void timer2_Tick_1(object sender, EventArgs e)
        {
            label3.Text = "Number of iterations from HiveMQ publishes: "+ iteratii.ToString();
            var AccChart = chart1.ChartAreas[0];
            AccChart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            //time axis
            AccChart.AxisX.Minimum = 0;
            //AccChart.AxisX.Maximum = (iAcc + 1) / 2;
            AccChart.AxisX.Maximum = samplingTimeA.Max()/100+1;
            AccChart.AxisX.Title = "Time (S)";
            //sensor axis
            AccChart.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            AccChart.AxisY.Minimum = ValueSecurityCheck(LowestValue(iAccY.Min(), iAccX.Min(), iAccZ.Min()), '<');
            AccChart.AxisY.Maximum = ValueSecurityCheck(HighestValue(iAccX.Max(), iAccY.Max(), iAccZ.Max()), '>');
            AccChart.AxisY.Title = "Accelerometer" + Environment.NewLine + " Sensor Values";
            //clear
            chart1.Series.Clear();

            chart1.Series.Add("X");
            chart1.Series["X"].Color = Color.FromArgb(255, 0, 0);
            chart1.Series["X"].Legend = "Legend1";
            chart1.Series["X"].ChartArea = "ChartArea1";
            chart1.Series["X"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            chart1.Series.Add("Y");
            chart1.Series["Y"].Color = Color.FromArgb(0, 0, 255);
            chart1.Series["Y"].Legend = "Legend1";
            chart1.Series["Y"].ChartArea = "ChartArea1";
            chart1.Series["Y"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            chart1.Series.Add("Z");
            chart1.Series["Z"].Color = Color.FromArgb(255, 0, 255);
            chart1.Series["Z"].Legend = "Legend1";
            chart1.Series["Z"].ChartArea = "ChartArea1";
            chart1.Series["Z"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            //double timp = 0;
            for (int i = 0; i < iAcc; i++)
            {
                chart1.Series["X"].Points.AddXY(samplingTimeA[i]/100, iAccX[i]);
                chart1.Series["Y"].Points.AddXY(samplingTimeA[i]/100, iAccY[i]);
                chart1.Series["Z"].Points.AddXY(samplingTimeA[i]/100, iAccZ[i]);
                //timp = timp + 0.1;
            }

            var GyrChart = chart2.ChartAreas[0];
            GyrChart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            //time axis
            GyrChart.AxisX.Minimum = 0;
            //GyrChart.AxisX.Maximum = (iGyr + 1) / 2;
            GyrChart.AxisX.Maximum = samplingTimeG.Max()/100+1;
            GyrChart.AxisX.Title = "Time (S)";
            //sensor axis
            GyrChart.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            GyrChart.AxisY.Minimum = ValueSecurityCheck(LowestValue(iGyrY.Min(), iGyrX.Min(), iGyrZ.Min()), '<');
            GyrChart.AxisY.Maximum = ValueSecurityCheck(HighestValue(iGyrX.Max(), iGyrY.Max(), iGyrZ.Max()), '>');
            GyrChart.AxisY.Title = "Gyroscope" + Environment.NewLine + " Sensor Values";
            //clear
            chart2.Series.Clear();

            chart2.Series.Add("X");
            chart2.Series["X"].Color = Color.FromArgb(255, 0, 0);
            chart2.Series["X"].Legend = "Legend1";
            chart2.Series["X"].ChartArea = "ChartArea1";
            chart2.Series["X"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            chart2.Series.Add("Y");
            chart2.Series["Y"].Color = Color.FromArgb(0, 0, 255);
            chart2.Series["Y"].Legend = "Legend1";
            chart2.Series["Y"].ChartArea = "ChartArea1";
            chart2.Series["Y"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            chart2.Series.Add("Z");
            chart2.Series["Z"].Color = Color.FromArgb(255, 0, 255);
            chart2.Series["Z"].Legend = "Legend1";
            chart2.Series["Z"].ChartArea = "ChartArea1";
            chart2.Series["Z"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            double timp2 = 0;
            for (int j = 0; j < iGyr; j++)
            {
                chart2.Series["X"].Points.AddXY(samplingTimeG[j]/100, iGyrX[j]);
                chart2.Series["Y"].Points.AddXY(samplingTimeG[j]/100, iGyrY[j]);
                chart2.Series["Z"].Points.AddXY(samplingTimeG[j]/100, iGyrZ[j]);
                timp2 = timp2 + 0.1;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            InitializeVelocitiesAndDisplacements();
            DispX = iAcc + " " + DispX;
            DispY = iAcc + " " + DispY;
            DispZ = iAcc + " " + DispZ;
            //Create file for axis X of Displacement
            string pathDisplacementX = @"D:\Thesis C# client\Thesis\Cuburi\DisplacementX.txt";
            var f_dispX = File.Create(pathDisplacementX);
            f_dispX.Close();
            File.WriteAllText(pathDisplacementX, DispX);
            //Create file for axis Y of Displacement
            string pathDisplacementY = @"D:\Thesis C# client\Thesis\Cuburi\DisplacementY.txt";
            var f_dispY = File.Create(pathDisplacementY);
            f_dispY.Close();
            File.WriteAllText(pathDisplacementY, DispY);
            //Create file for axis Z of Displacement
            string pathDisplacementZ = @"D:\Thesis C# client\Thesis\Cuburi\DisplacementZ.txt";
            var f_dispZ = File.Create(pathDisplacementZ);
            f_dispZ.Close();
            File.WriteAllText(pathDisplacementZ, DispZ);
            //System.Diagnostics.Process.Start(@"D:\Thesis C# client\Thesis\x64\Debug\Cuburi.exe");
            MessageBox.Show("Velocities on X axis are: " + VeloX+Environment.NewLine+"Displcements are: "+DispX);
        }

        private int LowestValue(int a, int b, int c)
        {
            if (a <= b && a <= c) return a;
            if (b <= a && b <= c) return b;
            if (c <= a && c <= b) return c;
            return 0;
        }

        private int HighestValue(int a, int b, int c)
        {
            if (a >= b && a >= c) return a;
            if (b >= a && b >= c) return b;
            if (c >= a && c >= b) return c;
            return 0;
        }

        private double ValueSecurityCheck(double a, char b)
        {
            if (b == '<')
            {
                if (a < 2 && a > -2) return -2; else return a;
            }
            if(b=='>')
            {
                if (a < 2 && a > -2) return 2; else return a;
            }
            
            return 0;
        }

        private double GetVelocity(int acceleration, double Vinitial, double time)
        {
            /* acceleration = (Vfinal-Vinitial)/timp
             * Vfinal-Vinitial=acceleration*timp
             * Vfinal-acceleration*timp+Vinitial */
            double Vfinal;
            //if(acceleration>PreAcc) Vfinal = Vinitial+ acceleration*time;
            //if(acceleration<=PreAcc) Vfinal = Vinitial -acceleration * time;
            Vfinal = Vinitial + acceleration * time;
            return Vfinal;
        }

        private double GetDisplacement(int acceleration, double velocity, double time)
        {
            double displacement = velocity * time + 0.5 * acceleration * time * time;
            return displacement;
        }

        private void InitializeVelocitiesAndDisplacements()
        {
            VelocitiesX[0] = 0;
            DisplacementX[0] = 0;
            VelocitiesY[0] = 0;
            DisplacementY[0] = 0;
            VelocitiesZ[0] = 0;
            DisplacementZ[0] = 0;
            VeloX = VelocitiesX[0].ToString() + " ";
            DispX = DisplacementX[0].ToString() + " ";
            VeloY = VelocitiesY[0].ToString() + " ";
            DispY = DisplacementY[0].ToString() + " ";
            VeloZ = VelocitiesZ[0].ToString() + " ";
            DispZ = DisplacementZ[0].ToString() + " ";
            //double timpV = 0.1;
            int a;
            for (a = 1; a < iAcc; a++)
            {
                //Axis X
                VelocitiesX[a] = GetVelocity(iAccX[a], VelocitiesX[a - 1], (samplingTimeA[a] - samplingTimeA[a - 1]) / 100);
                VeloX = VeloX + VelocitiesX[a] + " ";
                DisplacementX[a] = GetDisplacement(iAccX[a], VelocitiesX[a - 1], (samplingTimeA[a] - samplingTimeA[a - 1]) / 100);
                DispX = DispX + DisplacementX[a] + " ";
                //Axis Y
                VelocitiesY[a] = GetVelocity(iAccY[a], VelocitiesY[a - 1], (samplingTimeA[a] - samplingTimeA[a - 1]) / 100);
                VeloY = VeloY + VelocitiesY[a] + " ";
                DisplacementY[a] = GetDisplacement(iAccY[a], VelocitiesY[a - 1], (samplingTimeA[a] - samplingTimeA[a - 1]) / 100);
                DispY = DispY + DisplacementY[a] + " ";
                //Axis Z
                VelocitiesZ[a] = GetVelocity(iAccZ[a], VelocitiesZ[a - 1], (samplingTimeA[a] - samplingTimeA[a - 1]) / 100);
                VeloZ = VeloZ + VelocitiesZ[a] + " ";
                DisplacementZ[a] = GetDisplacement(iAccZ[a], VelocitiesZ[a - 1], (samplingTimeA[a] - samplingTimeA[a - 1]) / 100);
                DispZ = DispZ + DisplacementZ[a] + " ";
            }
           
        }
        private void InitializeAccAndGyr()
        {
            allAccX = iAccX[0] + " ";
            allAccY = iAccY[0] + " ";
            allAccZ = iAccZ[0] + " ";
            allGyrX = iGyrX[0] + " ";
            allGyrY = iGyrY[0] + " ";
            allGyrZ = iGyrZ[0] + " ";
            for (int a = 1; a < iAcc; a++)
            {
                allAccX = allAccX + iAccX[a] + " ";
                allAccY = allAccY + iAccY[a] + " ";
                allAccZ = allAccZ + iAccZ[a] + " ";
                allGyrX = allGyrX + iGyrX[a] + " ";
                allGyrY = allGyrY + iGyrY[a] + " ";
                allGyrZ = allGyrZ + iGyrZ[a] + " ";

            }
        }
        
    }
}
/*
 *******************************************************  button2_Click - when it was used for testing purposes **************************************
 //label3.Text= RecievedPublishes;
            //timer1.Stop();
            //MessageBox.Show(string.Join("", iAccX));
            /*allAccX = iAccX[0] + " ";
            allAccY = iAccY[0] + " ";
            allAccZ = iAccZ[0] + " ";
            for(int a=1; a<iAcc; a++)
            {
                allAccX = allAccX + iAccX[a] + " ";
                allAccY = allAccY + iAccY[a] + " ";
                allAccZ = allAccZ + iAccZ[a] + " ";

            }
            MessageBox.Show("X: " + allAccX + Environment.NewLine + "Y: " + allAccY + Environment.NewLine + "Z: " + allAccZ); 

VelocitiesX[0] = 0;
DisplacementX[0] = 0;
VelocitiesY[0] = 0;
DisplacementY[0] = 0;
VelocitiesZ[0] = 0;
DisplacementZ[0] = 0;
VeloX = VelocitiesX[0].ToString()+" ";
DispX = DisplacementX[0].ToString() + " ";
VeloY = VelocitiesX[0].ToString() + " ";
DispY = DisplacementX[0].ToString() + " ";
VeloZ = VelocitiesX[0].ToString() + " ";
DispZ = DisplacementX[0].ToString() + " ";
//double timpV = 0.1;
int a;
int Displmax = iAcc;
for(a=1; a<iAcc; a++)
{
    //timpV += 1;
    //Axis X
    VelocitiesX[a] = GetVelocity(iAccX[a], VelocitiesX[a-1], (samplingTimeA[a]-samplingTimeA[a-1])/100);
    VeloX = VeloX + VelocitiesX[a] + " ";
    DisplacementX[a] = GetDisplacement(iAccX[a], VelocitiesX[a-1],(samplingTimeA[a] - samplingTimeA[a - 1])/100);
    DispX = DispX + DisplacementX[a] + " ";
    //Axis Y
    VelocitiesY[a] = GetVelocity(iAccY[a], VelocitiesY[a - 1], (samplingTimeA[a] - samplingTimeA[a - 1])/100);
    VeloY = VeloY + VelocitiesY[a] + " ";
    DisplacementY[a] = GetDisplacement(iAccY[a], VelocitiesY[a - 1], (samplingTimeA[a] - samplingTimeA[a - 1])/100);
    DispY = DispY + DisplacementY[a] + " ";
    //Axis Z
    VelocitiesZ[a] = GetVelocity(iAccZ[a], VelocitiesZ[a - 1], (samplingTimeA[a] - samplingTimeA[a - 1])/100);
    VeloZ = VeloZ + VelocitiesZ[a] + " ";
    DisplacementZ[a] = GetDisplacement(iAccZ[a], VelocitiesZ[a - 1], (samplingTimeA[a] - samplingTimeA[a - 1])/100);
    DispZ = DispZ + DisplacementZ[a] + " ";
}
**********************Timer1_Tick - when it was used for testing *******************************************
 //label3.Text = RP2;
            if (contor != 0)
            {
                listBox1.Items.Clear();
                for(int i=1;i<contor;i++)
                {
                    if(RP1[i]!=null) listBox1.Items.Add(RP1[i]);
                }
            }

if (DetTime)
 {
     //timer1.Stop();
     //MessageBox.Show(DetermineTime.ToString());
     Console.WriteLine(DetermineTime.ToString());
     DetTime = false;
     DetermineTime = 0;

 }
 DetermineTime++;
***********************button4_Click - when it was used for testing ******************************
*var AccChart = chart1.ChartAreas[0];
            AccChart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            //time axis
            AccChart.AxisX.Minimum = 0;
            AccChart.AxisX.Maximum = (iAcc+1)/2;
            AccChart.AxisX.Title = "Time";
            //sensor axis
            AccChart.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            AccChart.AxisY.Minimum = iAccY.Min();
            AccChart.AxisY.Maximum = iAccX.Max();
            AccChart.AxisY.Title = "Sensor Values";
            //clear
            chart1.Series.Clear();
            Random random = new Random();
            chart1.Series.Add("X");
            chart1.Series["X"].Color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            chart1.Series["X"].Legend = "Legend1";
            chart1.Series["X"].ChartArea = "ChartArea1";
            chart1.Series["X"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            double timp = 0;
            for(int i=0;i<iAcc; i++)
            {
                chart1.Series["X"].Points.AddXY(timp, iAccX[i]);
                timp = timp + 0.5;
            }*/
/*var GyrChart = chart2.ChartAreas[0];
GyrChart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
//time axis
GyrChart.AxisX.Minimum = 0;
GyrChart.AxisX.Maximum = (iGyr + 1) / 2;
GyrChart.AxisX.Title = "Time";
//sensor axis
GyrChart.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
GyrChart.AxisY.Minimum = ValueSecurityCheck(LowestValue(iGyrY.Min(), iGyrX.Min(), iGyrZ.Min()),'<');
GyrChart.AxisY.Maximum = ValueSecurityCheck(HighestValue(iGyrX.Max(), iGyrY.Max(), iGyrZ.Max()),'>');
GyrChart.AxisY.Title = "Gyroscope" + Environment.NewLine + " Sensor Values";
//clear
chart2.Series.Clear();

chart2.Series.Add("X");
chart2.Series["X"].Color = Color.FromArgb(255, 0, 0);
chart2.Series["X"].Legend = "Legend1";
chart2.Series["X"].ChartArea = "ChartArea1";
chart2.Series["X"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

chart2.Series.Add("Y");
chart2.Series["Y"].Color = Color.FromArgb(0, 0, 255);
chart2.Series["Y"].Legend = "Legend1";
chart2.Series["Y"].ChartArea = "ChartArea1";
chart2.Series["Y"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

chart2.Series.Add("Z");
chart2.Series["Z"].Color = Color.FromArgb(255, 0, 255);
chart2.Series["Z"].Legend = "Legend1";
chart2.Series["Z"].ChartArea = "ChartArea1";
chart2.Series["Z"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
double timp2 = 0;
for (int j = 0; j < iGyr; j++)
{
    chart2.Series["X"].Points.AddXY(timp2, iGyrX[j]);
    chart2.Series["Y"].Points.AddXY(timp2, iGyrY[j]);
    chart2.Series["Z"].Points.AddXY(timp2, iGyrZ[j]);
    timp2 = timp2 + 0.5;
}

*/
