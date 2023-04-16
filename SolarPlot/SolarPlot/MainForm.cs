using ScottPlot;
using ScottPlot.Renderable;
using ScottPlot.Styles;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;
using static ScottPlot.Generate;
using static SolarPlot.XYDataSet;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SolarPlot
{
    public partial class MainForm : Form
    {
        internal XYDataSet dataSet;
        private DayPlot dayPlot;

        private Worker worker;

        public MainForm()
        {
            InitializeComponent();

            this.dataSet = new XYDataSet();
            this.worker = new Worker(this);

            if (Properties.Settings.Default.OpenFile != "")
            {
                this.worker.Command("LoadCSV " + Properties.Settings.Default.OpenFile);
                this.worker.Command("CalculateEnergyPerPeriod");
                this.worker.Command("PlotInit");
            }

            // orginal example code copy & paste from: https://social.msdn.microsoft.com/Forums/vstudio/en-US/9bd4beee-8546-4f17-aeb4-c9262f2d4a05/create-3d-surface?forum=vbgeneral
            // ported to C# by OpenAI's ChatGPT
            // a few tiny manual changes to get ChatGPT's work compiling & running


            gPoints = new double[48,52];
            //create the surface data
            Random rand = new Random();
            for (int y = 0; y < 52; y++)
            {
                for (int x = 0; x < 48; x++)
                {
                    gPoints[x,y] = -(x-24)*(x-24) * (((double)y)/50) + 100;
                }
            }

            //setup the chart
            ChartArea chartArea = chart2.ChartAreas[0];
            chartArea.AxisX.Title = "X";
            chartArea.AxisX.MajorGrid.LineColor = Color.LightBlue;
            chartArea.AxisX.Minimum = 0;
            chartArea.AxisX.Maximum = 48;
            chartArea.AxisX.Interval = 2;

            chartArea.AxisY.Title = "Y";
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.Minimum = 0;

            chartArea.BackColor = Color.FloralWhite; //AntiqueWhite //LightSkyBlue
            chartArea.BackSecondaryColor = Color.White;
            chartArea.BackGradientStyle = GradientStyle.HorizontalCenter;
            chartArea.BorderColor = Color.Blue;
            chartArea.BorderDashStyle = ChartDashStyle.Solid;
            chartArea.BorderWidth = 1;
            chartArea.ShadowOffset = 2;

            // Enable 3D charts
            chartArea.Area3DStyle.Enable3D = true;
            chartArea.Area3DStyle.Perspective = 2;
  
            DrawChart();
            //draw the chart
        }

        public double[,] gPoints;


        public void DrawChart()
        {
            chart2.Series.Clear();
            chart2.ChartAreas[0].Area3DStyle.Rotation = this.YearTrackBarAngle.Value;

            for (int i = 0; i < 52; i++)
            {
                chart2.Series.Add("z" + i.ToString());
                chart2.Series[i].ChartType = SeriesChartType.Area;
                chart2.Series[i].BorderWidth = 1;
                chart2.Series[i].Color = Color.SteelBlue;
                chart2.Series[i].IsVisibleInLegend = false;
                // Set series strip width
                chart2.Series[i]["PointWidth"] = "1";
                // Set series points gap to 1 pixels
                chart2.Series[i]["PixelPointGapDepth"] = "1";

                for (int x = 0; x < 48; x++)
                {
                    chart2.Series[i].Points.AddXY(x, gPoints[x, i]);
                }
            }
        }


public void SetStatus(string status)
        {
            if (this.statusStrip.InvokeRequired)
            {
                this.statusStrip.Invoke((Action)delegate { SetStatus(status); });
            }
            else
            {
                this.toolStripStatusLabel1.Text = status;
            }
        }

        public void SetErrorStatus(string error)
        {
            if (this.statusStrip.InvokeRequired)
            {
                this.statusStrip.Invoke((Action)delegate { SetStatus(error); });
            }
            else
            {
                this.toolStripStatusLabel2.Text = error;
            }
        }

        public void SetProgress(int progress)
        {
            if (this.statusStrip.InvokeRequired)
            {
                this.statusStrip.Invoke((Action)delegate { SetProgress(progress); });
            }
            else
            {
                this.toolStripProgressBar.Value = progress;
            }
        }

        public void PlotInit()
        {
            if (this.PlotDayGraph.InvokeRequired)
            {
                this.PlotDayGraph.Invoke((Action)delegate { PlotInit(); });
            }
            else
            {
                this.dayPlot = new DayPlot(this.dataSet, this.PlotDayGraph);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("SolarPlot\nProgrammed by Victor van Acht\n\nhttps://github.com/victorvanacht/SolarPlot");
        }

        private void openCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "csv files (*.csv)|*.csv|txt files (*.txt)|*.txt|all files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.OpenFile = openFileDialog.FileName;
                this.worker.Command("LoadCSV " + openFileDialog.FileName);
                this.worker.Command("CalculateEnergyPerPeriod");
                this.worker.Command("PlotInit");
            }
        }

        private void FormExit_Click(object sender, FormClosingEventArgs e)
        {
            /*
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                OTGWButtonDisconnect_Click(sender, e);
                MaxButtonDisconnect_Click(sender, e);
            }
            */
            Exit(sender, e);
        }

        private void Exit(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            Application.Exit();
        }


        private volatile bool dayPlotSelectionChanging = false;
        private Dictionary<string, string[]> selectionNames = new Dictionary<string, string[]>() 
        {
            ["Vpv"] = new string[] { "Vpv1", "Vpv2" },
            ["Ipv"] = new string[] { "Ipv1", "Ipv2" },
            ["Vac"] = new string[] { "Vac1", "Vac2", "Vac3" },
            ["Iac"] = new string[] { "Iac1", "Iac2", "Iac3" },
            ["Fac"] = new string[] { "Freq1", "Freq2", "Freq3" },
            ["Temp"] = new string[] { "Temperature" },
            ["kWHr"] = new string[] { "EnergyPerDay", "EnergyPerHalfHour" },

        };

        private void ProcessDayPlotSelectionChanged(object sender, EventArgs e)
        {
            if (!dayPlotSelectionChanging) // when we are going to change the selection of the other boxes, they will generate this event as well. So we need to ignore those events
            {
                dayPlotSelectionChanging = true;
                System.Windows.Forms.CheckBox[] dayPlotSelectionBoxes = { this.DayCheckBoxVpv, this.DayCheckBoxIpv, this.DayCheckBoxVac, this.DayCheckBoxIac, this.DayCheckBoxFac, this.DayCheckBoxTemp, this.DayCheckBoxKWHr};
                foreach (CheckBox checkbox in dayPlotSelectionBoxes)
                {
                    bool visibility = (checkbox == sender);
                    checkbox.Checked = visibility;

                    string[] selection = selectionNames[checkbox.Text];
                    this.dayPlot.EnableLine(selection, visibility);
                    if (visibility)
                    {
                        this.dayPlot.AutoRangeYAxis(selection, 1);
                    }

                    this.PlotDayGraph.Refresh();

                }
                dayPlotSelectionChanging = false;
            }
        }

        private void YearTrackBarAngle_ValueChanged(object sender, EventArgs e)
        {
            DrawChart();
        }
    }
}

