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
                private Color fillColor;
                private bool fill;
                private int axisIndex;
                private int count;

                private ScottPlot.Plottable.SignalPlotXY line;

                public Line(FormsPlot plot, string name, double[] xData, double[] yData, Color lineColor, Color fillColor, bool fill, int axisIndex)
                {
                    this.plot = plot;
                    this.name = name;
                    this.xData = xData;
                    this.yData = yData;
                    this.color = lineColor;
                    this.fillColor = fillColor;
                    this.fill = fill;
                    this.axisIndex = axisIndex;
                    this.count = 0;

                    AddLine();
                }

                public void AddPoint(DateTime dateTime, double value)
                {
                    if (this.count == xData.Length)
                    {
                        int count2 = count * 2;
                        Array.Resize(ref this.xData, count2);
                        Array.Resize(ref this.yData,count2);

                        // refreh plot by removing and re-adding the line.
                        // Because internal memory allocation of ScottPlot does not get updated when
                        // xData and yData are re-allocated by Resize
                        this.plot.Plot.Remove(this.line);
                        AddLine();
                    }
                    if ((this.count > 8550) && (this.count < 8560))
                    {
                        Console.WriteLine(count.ToString() + "  " + dateTime.ToString());
                    }
                    this.xData[count] = dateTime.ToOADate();
                    this.yData[count] = value;
                    this.count++;

                }

                public void AutoRangeXAxis()
                {
                    // fill remaining x-as with reasonable data, to prevent ScottPlot giving an error
                    double step = (xData[this.count - 1] - xData[0]) / (count-1);
                    for (int i = count; i < xData.Length; i++)
                    {
                    xData[i] = xData[0] + i * step;
                    }

                    this.plot.Plot.SetAxisLimits(xMin: xData[0], xMax: xData[this.count - 1]);
                }

                public void AutoRangeYAxis()
                {
                    this.plot.Plot.SetAxisLimits(yMin: yData.Min(), yMax: yData.Max());
                }

                private void AddLine()
                {
                    this.line = this.plot.Plot.AddSignalXY(this.xData, this.yData, this.color, this.name);
                    this.line.LineWidth = 2;
                    this.line.MarkerSize = 0;
                    this.line.Color = color;
                    if (this.fill) this.line.FillBelow(fillColor, 0.2);
                    this.line.Smooth = true;
                    this.line.XAxisIndex = 0;
                    this.line.YAxisIndex = this.axisIndex;
                }


            }

            public Dictionary<string, Line> dayLines;

            private MainForm form;

            public DataSet(MainForm form)
            {
                this.form = form;

                this.dayLines = new Dictionary<string, Line>() 
                {
                    ["Power"] = new Line(this.form.PlotDay, "Power", new double[1000], new double[1000], Color.Blue, Color.Blue, true, 0),
                    ["Iac1"] = new Line(this.form.PlotDay, "Iac1", new double[1000], new double[1000], Color.Red, Color.Red, false, 1),
                    ["Iac2"] = new Line(this.form.PlotDay, "Iac2", new double[1000], new double[1000], Color.Green, Color.Green, false, 1),
                    ["Iac3"] = new Line(this.form.PlotDay, "Iac3", new double[1000], new double[1000], Color.LightBlue, Color.LightBlue, false, 1),
                    ["Vac1"] = new Line(this.form.PlotDay, "Vac1", new double[1000], new double[1000], Color.Red, Color.Red, false, 1),
                    ["Vac2"] = new Line(this.form.PlotDay, "Vac2", new double[1000], new double[1000], Color.Green, Color.Green, false, 1),
                    ["Vac3"] = new Line(this.form.PlotDay, "Vac3", new double[1000], new double[1000], Color.LightBlue, Color.LightBlue, false, 1),
                    ["Freq1"] = new Line(this.form.PlotDay, "Freq1", new double[1000], new double[1000], Color.Red, Color.Red, false, 1),
                    ["Freq2"] = new Line(this.form.PlotDay, "Freq2", new double[1000], new double[1000], Color.Green, Color.Green, false, 1),
                    ["Freq3"] = new Line(this.form.PlotDay, "Freq3", new double[1000], new double[1000], Color.LightBlue, Color.LightBlue, false, 1),
                    ["Ipv1"] = new Line(this.form.PlotDay, "Ipv1", new double[1000], new double[1000], Color.Red, Color.Red, false, 1),
                    ["Ipv2"] = new Line(this.form.PlotDay, "Ipv2", new double[1000], new double[1000], Color.Green, Color.Green, false, 1),
                    ["Vpv1"] = new Line(this.form.PlotDay, "Vpv1", new double[1000], new double[1000], Color.Red, Color.Red, false, 1),
                    ["Vpv2"] = new Line(this.form.PlotDay, "Vpv2", new double[1000], new double[1000], Color.Green, Color.Green, false, 1),
                    ["Temperature"] = new Line(this.form.PlotDay, "Temperature", new double[1000], new double[1000], Color.Red, Color.Red, false, 1),
                };
            }

            public void Add(DateTime dateTime, 
                            double power,
                            double Iac1,
                            double Iac2,
                            double Iac3,
                            double Vac1,
                            double Vac2,
                            double Vac3,
                            double freq1,
                            double freq2,
                            double freq3,
                            double Ipv1,
                            double Ipv2,
                            double Vpv1,
                            double Vpv2,
                            double temperature
                            )
            {
                dayLines["Power"].AddPoint(dateTime, power);
                dayLines["Iac1"].AddPoint(dateTime, Iac1);
                dayLines["Iac2"].AddPoint(dateTime, Iac2);
                dayLines["Iac3"].AddPoint(dateTime, Iac3);
                dayLines["Vac1"].AddPoint(dateTime, Vac1);
                dayLines["Vac2"].AddPoint(dateTime, Vac2);
                dayLines["Vac3"].AddPoint(dateTime, Vac3);
                dayLines["Freq1"].AddPoint(dateTime, freq1);
                dayLines["Freq2"].AddPoint(dateTime, freq2);
                dayLines["Freq3"].AddPoint(dateTime, freq3);
                dayLines["Ipv1"].AddPoint(dateTime, Ipv1);
                dayLines["Ipv2"].AddPoint(dateTime, Ipv2);
                dayLines["Vpv1"].AddPoint(dateTime, Vpv1);
                dayLines["Vpv2"].AddPoint(dateTime, Vpv2);
                dayLines["Temperature"].AddPoint(dateTime, temperature);
            }
            public void AutoRangeXAxis()
            {
                foreach (KeyValuePair<string, Line> kvp in dayLines)
                {
                    kvp.Value.AutoRangeXAxis();
                }
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
                this.data.AutoRangeYAxis("Power");
                this.PlotDay.Configuration.LockVerticalAxis = true;
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

