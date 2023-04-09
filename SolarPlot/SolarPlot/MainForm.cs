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

                public void SetVisibilty(bool enabled)
                {
                    this.line.IsVisible = enabled;
                }
            }

            private class LineProperty
            {
                public Color color;
                public Color fillColor;
                public bool fill;
                public int axisIndex;

                public LineProperty(Color color, Color fillColor, bool fill, int axisIndex)
                {
                    this.color = color;
                    this.fillColor = fillColor;
                    this.fill = fill;
                    this.axisIndex = axisIndex;
                }
            }

            XYDataSet dataSet;
            ScottPlot.FormsPlot plot;
            public Dictionary<string, Line> lines;
            private ScottPlot.Renderable.Legend legend;
            private Axis Yaxis2;

            private Dictionary<string, LineProperty> lineProperties = new Dictionary<string, LineProperty>()
            {
                ["Power"] = new LineProperty(Color.Blue, Color.Blue, true, 0),
                ["Iac1"] = new LineProperty(Color.Red, Color.Red, false, 1),
                ["Iac2"] = new LineProperty(Color.Green, Color.Green, false, 1),
                ["Iac3"] = new LineProperty(Color.LightBlue, Color.LightBlue, false, 1),
                ["Vac1"] = new LineProperty(Color.Red, Color.Red, false, 1),
                ["Vac2"] = new LineProperty(Color.Green, Color.Green, false, 1),
                ["Vac3"] = new LineProperty(Color.LightBlue, Color.LightBlue, false, 1),
                ["Freq1"] = new LineProperty(Color.Red, Color.Red, false, 1),
                ["Freq2"] = new LineProperty(Color.Green, Color.Green, false, 1),
                ["Freq3"] = new LineProperty(Color.LightBlue, Color.LightBlue, false, 1),
                ["Ipv1"] = new LineProperty(Color.Red, Color.Red, false, 1),
                ["Ipv2"] = new LineProperty(Color.Green, Color.Green, false, 1),
                ["Vpv1"] = new LineProperty(Color.Red, Color.Red, false, 1),
                ["Vpv2"] = new LineProperty(Color.Green, Color.Green, false, 1),
                ["Temperature"] = new LineProperty(Color.Purple, Color.Purple, false, 1),

            };

            public DayPlot(XYDataSet dataSet, ScottPlot.FormsPlot plot)
            {
                this.dataSet = dataSet;
                this.plot = plot;

                this.lines = new Dictionary<string, Line>();

                foreach (KeyValuePair<string, XYData<double>> kvp in dataSet)
                {
                    string name = kvp.Key;
                    LineProperty prop = lineProperties[name];
                    lines.Add(name, new Line(plot, name, kvp.Value.x, kvp.Value.y, prop.color, prop.fillColor, prop.fill, prop.axisIndex));

                }
                this.plot.Plot.XAxis.DateTimeFormat(true);
                this.legend = this.plot.Plot.Legend();
                this.legend.Orientation = ScottPlot.Orientation.Vertical;
                this.legend.Location = ScottPlot.Alignment.UpperLeft;
                this.legend.FontSize = 9;
                this.plot.Plot.XLabel("Date/Time");


                this.AutoRangeXAxis();
                this.AutoRangeYAxis(new string[] { "Power" }, 0);
                this.AutoRangeYAxis(new string[] { "Vac1", "Vac2", "Vac3" }, 1);
                this.plot.Configuration.LockVerticalAxis = true;
                this.plot.Refresh();
            }


            public void AutoRangeXAxis()
            {
                double min = Double.MaxValue;
                double max = Double.MinValue;
                foreach (KeyValuePair<string, XYData<double>> kvp in this.dataSet)
                {
                    double t;
                    t = kvp.Value.Xmin;
                    min = (t < min) ? t : min;
                    t = kvp.Value.Xmax;
                    max = (t > max) ? t : max;
                }
                this.plot.Plot.SetAxisLimits(xMin: min, xMax: max);
            }
            public void AutoRangeYAxis(string[] names, int axisIndex)
            {
                double min = Double.MaxValue;
                double max = Double.MinValue;
                string axisName = "";
                foreach (string name in names)
                {
                    if (this.dataSet.ContainsKey(name))
                    {
                        double t;
                        t = this.dataSet[name].Ymin;
                        min = (t < min) ? t : min;
                        t = this.dataSet[name].Ymax;
                        max = (t > max) ? t : max;
                        axisName += name + " ";
                    }
                }
                this.plot.Plot.SetAxisLimits(yMin: min, yMax: max * 1.1, yAxisIndex: axisIndex);

                if (axisIndex == 0)
                {
                    this.plot.Plot.YAxis.Label(axisName);
                    this.plot.Plot.YAxis.Ticks(true);
                }
                else
                {
                    this.plot.Plot.YAxis2.Label(axisName);
                    this.plot.Plot.YAxis2.Ticks(true);
                }
            }

            public void EnableLine(string[] names, bool enable)
            {
                foreach (string name in names)
                {
                    if (this.lines.ContainsKey(name))
                    {
                        this.lines[name].SetVisibilty(enable);
                    }
                }
            }
        }

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


        private volatile bool dayPlotSelectionChanging = false;
        private Dictionary<string, string[]> selectionNames = new Dictionary<string, string[]>() 
        {
            ["Vpv"] = new string[] { "Vpv1", "Vpv2" },
            ["Ipv"] = new string[] { "Ipv1", "Ipv2" },
            ["Vac"] = new string[] { "Vac1", "Vac2", "Vac3" },
            ["Iac"] = new string[] { "Iac1", "Iac2", "Iac3" },
            ["Fac"] = new string[] { "Freq1", "Freq2", "Freq3" },
            ["Temp"] = new string[] { "Temperature" },

        };

        private void ProcessDayPlotSelectionChanged(object sender, EventArgs e)
        {
            if (!dayPlotSelectionChanging) // when we are going to change the selection of the other boxes, they will generate this event as well. So we need to ignore those events
            {
                dayPlotSelectionChanging = true;
                System.Windows.Forms.CheckBox[] dayPlotSelectionBoxes = { this.DayCheckBoxVpv, this.DayCheckBoxIpv, this.DayCheckBoxVac, this.DayCheckBoxIac, this.DayCheckBoxFac, this.DayCheckBoxTemp };
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
    }
}

