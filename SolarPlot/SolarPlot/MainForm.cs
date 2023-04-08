﻿using ScottPlot;
using ScottPlot.Styles;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static ScottPlot.Generate;
using static SolarPlot.XYDataSet;

namespace SolarPlot
{
    public partial class MainForm : Form
    {

        internal class DayPlot
        {
            internal class Line
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

                public Line(ScottPlot.FormsPlot plot, string name, double[] xData, double[] yData, Color lineColor, Color fillColor, bool fill, int axisIndex)
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



            XYDataSet dataSet;
            ScottPlot.FormsPlot plot;
            public Dictionary<string, Line> lines;
            private ScottPlot.Renderable.Legend legend;

            public DayPlot(XYDataSet dataSet, ScottPlot.FormsPlot plot)
            {
                this.dataSet = dataSet;
                this.plot = plot;

                this.lines = new Dictionary<string, Line>();

                foreach (KeyValuePair<string, XYData> kvp in dataSet)
                {
                    string name = kvp.Key;


                    lines.Add(name, new Line(plot, name, kvp.Value.x, kvp.Value.y, Color.Blue, Color.Blue, false, 0));


                }
                this.plot.Plot.XAxis.DateTimeFormat(true);
                this.legend = this.plot.Plot.Legend();
                this.legend.Orientation = ScottPlot.Orientation.Vertical;
                this.legend.Location = ScottPlot.Alignment.UpperLeft;
                this.legend.FontSize = 9;
                this.plot.Plot.XLabel("Date/Time");
                //this.data.AutoRangeXAxis();
                //this.data.AutoRangeY0Axis();
                //this.data.AutoRangeY1Axis(new string[] { "Vac1", "Vac1", "Vac3" });
                this.plot.Configuration.LockVerticalAxis = true;
                this.plot.Refresh();
            }
        }

        private DayPlot dayPlot;
            /*

            

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
                double min = Double.MaxValue;
                double max = Double.MinValue;
                foreach (KeyValuePair<string, Line> kvp in dayLines)
                {
                    kvp.Value.FillRemainingXAxis();

                    double t;
                    t = kvp.Value.GetMinX();
                    min = (t<min)? t : min;
                    t = kvp.Value.GetMaxX();
                    max = (t>max)? t : max;
                }
                this.form.PlotDay.Plot.SetAxisLimits(xMin: min, xMax: max);
            }

            public void AutoRangeY0Axis()
            {
                double min = dayLines["Power"].GetMinY();
                double max = dayLines["Power"].GetMaxY();
                this.form.PlotDay.Plot.SetAxisLimits(yMin: min, yMax: max);
            }

            public void AutoRangeY1Axis(string[] items)
            {
                double min = Double.MaxValue;
                double max = Double.MinValue;
                foreach (string item in items)
                {
                    double t;
                    t = dayLines[item].GetMinY();
                    min = (t < min) ? t : min;
                    t = dayLines[item].GetMaxY();
                    max = (t > max) ? t : max;
                }
                this.form.PlotDay.Plot.SetAxisLimits(yMin: min, yMax: max, yAxisIndex: 1);
            }

        */

        /*
        public class DayPlot
        {
            public DayPlot() { }

        

        */

        internal XYDataSet dataSet;

        private Worker worker;

        public MainForm()
        {
            InitializeComponent();

            this.dataSet = new XYDataSet();
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


        private void ProcessDayPlotSelectionChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.CheckBox[] dayPlotSelectionBoxes = { this.DayCheckBoxVpv, this.DayCheckBoxIpv, this.DayCheckBoxVac, this.DayCheckBoxIac, this.DayCheckBoxFac, this.DayCheckBoxTemp };

            foreach (CheckBox checkbox in dayPlotSelectionBoxes)
            {
                if (checkbox != sender) checkbox.Checked = false;
                else checkbox.Checked = true;
            }
        }
    }
}

