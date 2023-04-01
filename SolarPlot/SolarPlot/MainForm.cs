using ScottPlot;
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

namespace SolarPlot
{
    public partial class MainForm : Form
    {
        public class DataSet
        {
            public class Line
            {
                private FormsPlot plot;
                private double[] xData;
                private double[] yData;
                private string name;
                private Color color;
                private int count;

                private ScottPlot.Plottable.SignalPlotXY line;

                public Line(FormsPlot plot, string name, double[] xData, double[] yData, Color color)
                {
                    this.plot = plot;
                    this.name = name;
                    this.xData = xData;
                    this.yData = yData;
                    this.color = color;
                    this.count = 0;

                    this.line = this.plot.Plot.AddSignalXY(this.xData, this.yData, this.color, this.name);
                    this.line.MarkerSize = 0;
                    this.line.Color = color;
                }

                public void AddPoint(DateTime dateTime, double value)
                {
                    if (this.count == xData.Length)
                    {
                        Array.Resize(ref this.xData, this.count * 2);
                        Array.Resize(ref this.yData, this.count * 2);
                    }
                    this.xData[count] = dateTime.ToOADate();
                    this.yData[count] = value;
                    this.count++;

                }

                public void AutoRangeXAxis()
                {
                    this.plot.Plot.SetAxisLimits(xMin: xData[0], xMax: xData[this.count - 1]);
                }

                public void AutoRangeYAxis()
                {
                    this.plot.Plot.SetAxisLimits(yMin: yData.Min(), yMax: yData.Max());
                }

            }

            public Dictionary<string, Line> dayLines;

            private MainForm form;

            public DataSet(MainForm form)
            {
                this.form = form;

                this.dayLines = new Dictionary<string, Line>() 
                {
                    ["power"] = new Line(this.form.PlotDay, "Power", new double[1000], new double[1000], Color.Blue)
                };
            }

            public void Add(DateTime dateTime, double power)
            {
                dayLines["power"].AddPoint(dateTime, power);
            }
            public void AutoRangeXAxis()
            {
                dayLines["power"].AutoRangeXAxis();
            }

            public void AutoRangeYAxis(string item)
            {
                dayLines[item].AutoRangeYAxis();
            }

        }

        public DataSet data;

        private Worker worker;
        private ScottPlot.Renderable.Legend PlotDaylegend;

        public MainForm()
        {
            InitializeComponent();

            this.data = new DataSet(this);
            this.worker = new Worker(this);

            if (Properties.Settings.Default.OpenFile != "")
            {
                this.worker.Command("LoadCSV " + Properties.Settings.Default.OpenFile);
                this.worker.Command("PlotInit");
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
            if (this.PlotDay.InvokeRequired)
            {
                this.PlotDay.Invoke((Action)delegate { PlotInit(); });
            }
            else
            {
                this.PlotDay.Plot.XAxis.DateTimeFormat(true);
                this.PlotDaylegend = this.PlotDay.Plot.Legend();
                this.PlotDaylegend.Orientation = ScottPlot.Orientation.Vertical;
                this.PlotDaylegend.Location = ScottPlot.Alignment.UpperLeft;
                this.PlotDaylegend.FontSize = 9;
                this.PlotDay.Plot.XLabel("Date/Time");
                this.data.AutoRangeXAxis();
                this.data.AutoRangeYAxis("power");
                this.PlotDay.Refresh();
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
    }
}

