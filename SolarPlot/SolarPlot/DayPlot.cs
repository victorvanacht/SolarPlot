using ScottPlot;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;
using static SolarPlot.XYDataSet;

namespace SolarPlot
{
    internal class DayPlot
    {
        internal class Line
        {
            private FormsPlot plot;
            private double[] xData;
            private double[] yData;
            private string name;
            private string propertyName;
            private Color color;
            private Color fillColor;
            private bool fill;
            private bool bar;
            private double barThickness;
            private int axisIndex;

            private ScottPlot.Plottable.SignalPlotXY line;
            private ScottPlot.Plottable.BarPlot bars;

            public double Ymin {
                get => yData.Min();
            }
            public double Ymax
            {
                get => yData.Max();
            }

            public Line(ScottPlot.FormsPlot plot, string name, string propertyName, double[] xData, double[] yData, Color lineColor, Color fillColor, bool fill, bool bar, double barThickness,
                int axisIndex)
            {
                this.plot = plot;
                this.name = name;
                this.propertyName = propertyName;
                this.xData = xData;
                this.yData = yData;
                this.color = lineColor;
                this.fillColor = fillColor;
                this.fill = fill;
                this.bar = bar;
                this.barThickness = barThickness;
                this.axisIndex = axisIndex;

                if (!bar)
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
                else
                {
                    this.bars = this.plot.Plot.AddBar(this.yData, this.xData);
                    this.bars.BarWidth = this.barThickness;
                    this.bars.YAxisIndex = this.axisIndex;

                }
            }

            public void SetVisibilty(bool enabled)
            {
                if (!bar)
                {
                    this.line.IsVisible = enabled;
                }
                else
                {
                    this.bars.IsVisible = enabled;
                }
            }

            public string GetName()
            {
                return this.propertyName;
            }

        }

        private class LineProperty
        {
            public string name;
            public Color color;
            public Color fillColor;
            public bool fill;
            public bool bar;
            public double barThickness;
            public int axisIndex;

            public LineProperty(string name, Color color, Color fillColor, bool fill, bool bar, double barThickness, int axisIndex)
            {
                this.name = name;
                this.color = color;
                this.fillColor = fillColor;
                this.fill = fill;
                this.bar = bar;
                this.barThickness= barThickness;
                this.axisIndex = axisIndex;
            }
        }

        XYDataSet dataSet;
        ScottPlot.FormsPlot plot;
        public Dictionary<string, Line> lines;
        private ScottPlot.Renderable.Legend legend;
        private ScottPlot.Renderable.Axis Yaxis2;

        private const string name_Power = "Power";
        private const string name_Iac1Iac2Iac3 = "Iac1 Iac2 Iac3";
        private const string name_Vac1Vac2Vac3 = "Vac1 Vac2 Vac3";
        private const string name_Freq1Freq2Freqc3 = "Freq1 Freq2 Freq3";
        private const string name_Ipv1Ipv2 = "Ipv1 Ipv2";
        private const string name_Vpv1Vpv2 = "Vpv1 Vpv2";
        private const string name_Temperature = "Temperature";
        private const string name_kWhr = "kWhr";
        private const string name_Iac = "Iac";
        private const string name_Vac = "Vac";
        private const string name_Freq = "Freq";
        private const string name_PowerDC = "PowerDC";
        private const string name_Idc = "Idc";
        private const string name_Vdc = "Vdc";

        private Dictionary<string, LineProperty> lineProperties = new Dictionary<string, LineProperty>()
        {
            ["Power"] = new LineProperty(name_Power, Color.Blue, Color.Blue, true, false, 0, 0),
            ["Iac1"] = new LineProperty(name_Iac1Iac2Iac3, Color.Red, Color.Red, false, false, 0, 0),
            ["Iac2"] = new LineProperty(name_Iac1Iac2Iac3, Color.Green, Color.Green, false, false, 0, 0),
            ["Iac3"] = new LineProperty(name_Iac1Iac2Iac3, Color.LightBlue, Color.LightBlue, false, false,0, 0),
            ["Vac1"] = new LineProperty(name_Vac1Vac2Vac3, Color.Red, Color.Red, false, false, 0, 0),
            ["Vac2"] = new LineProperty(name_Vac1Vac2Vac3, Color.Green, Color.Green, false, false, 0, 0),
            ["Vac3"] = new LineProperty(name_Vac1Vac2Vac3, Color.LightBlue, Color.LightBlue, false, false, 0, 0),
            ["Freq1"] = new LineProperty(name_Freq1Freq2Freqc3,Color.Red, Color.Red, false, false, 0, 0),
            ["Freq2"] = new LineProperty(name_Freq1Freq2Freqc3, Color.Green, Color.Green, false, false, 0, 0),
            ["Freq3"] = new LineProperty(name_Freq1Freq2Freqc3, Color.LightBlue, Color.LightBlue, false, false, 0, 0),
            ["Ipv1"] = new LineProperty(name_Ipv1Ipv2, Color.Red, Color.Red, false, false, 0, 0),
            ["Ipv2"] = new LineProperty(name_Ipv1Ipv2, Color.Green, Color.Green, false, false, 0, 0),
            ["Vpv1"] = new LineProperty(name_Vpv1Vpv2, Color.Red, Color.Red, false, false, 0, 0),
            ["Vpv2"] = new LineProperty(name_Vpv1Vpv2, Color.Green, Color.Green, false, false, 0, 0),
            ["Temperature"] = new LineProperty(name_Temperature, Color.Purple, Color.Purple, false, false, 0, 0),
            ["EnergyPerDay"] = new LineProperty(name_kWhr, Color.Purple, Color.Purple, false, true, 0.8, 1),
            ["EnergyPerHalfHour"] = new LineProperty(name_kWhr, Color.Purple, Color.Purple, false, true, 0.8/48, 1),

            ["CurrentAC"] = new LineProperty(name_Iac, Color.Red, Color.Red, false, false, 0, 0),
            ["VoltageAC"] = new LineProperty(name_Vac, Color.Blue, Color.Blue, false, false, 0, 0),
            ["Frequency"] = new LineProperty(name_Freq, Color.Green, Color.Green, false, false, 0, 0),
            ["PowerDC"] = new LineProperty(name_PowerDC, Color.Blue, Color.Blue, false, false, 0, 0),

            ["Current"] = new LineProperty(name_Idc, Color.Red, Color.Red, false, false, 0, 0),
            ["Voltage"] = new LineProperty(name_Vdc, Color.Blue, Color.Blue, false, false, 0, 0),
        };



        public DayPlot(XYDataSet dataSet, ScottPlot.FormsPlot plot)
        {
            this.dataSet = dataSet;
            this.plot = plot;
            this.lines = new Dictionary<string, Line>();

            this.plot.Plot.Clear();

            // first add the bar graphs, so they will be in the back ground
            string[] barNames = { "EnergyPerDay", "EnergyPerHalfHour" };
            foreach (string barName in barNames)
            {
                lines.Add(barName, new Line(plot, barName, lineProperties[barName].name, dataSet[barName].x, dataSet[barName].y, lineProperties[barName].color, lineProperties[barName].fillColor, lineProperties[barName].fill, lineProperties[barName].bar, lineProperties[barName].barThickness, lineProperties[barName].axisIndex));
            }


            foreach (KeyValuePair<string, XYData<double>> kvp in dataSet)
            {
                bool isBar = false;
                foreach (string barName in barNames)
                {
                    if (barName.Equals(kvp.Key))
                    {
                        isBar = true;
                    }
                }
                if (!isBar)
                {
                    string name = kvp.Key;
                    LineProperty prop = lineProperties[name];
                    lines.Add(name, new Line(plot, name, prop.name, kvp.Value.x, kvp.Value.y, prop.color, prop.fillColor, prop.fill, prop.bar, prop.barThickness, prop.axisIndex));
                }
            }
            this.plot.Plot.XAxis.DateTimeFormat(true);
            this.legend = this.plot.Plot.Legend();
            this.legend.Orientation = ScottPlot.Orientation.Vertical;
            this.legend.Location = ScottPlot.Alignment.UpperLeft;
            this.legend.FontSize = 9;
            this.plot.Plot.XLabel("Date/Time");

            this.AutoRangeXAxis();
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


        public string[] GetLineNames()
        {
            List<string> r = new List<string>();
            foreach (KeyValuePair<string, Line> kvp in this.lines)
            {
                string name = kvp.Value.GetName();
                if ((!r.Contains<string>(name)) && (name!=name_kWhr))
                {
                    r.Add(name);
                }
            }
            r.Sort();
            return r.ToArray();
        }

        public void EnableLine(string selectedLine)
        {
            double min = Double.MaxValue;
            double max = Double.MinValue;
            foreach (KeyValuePair<string, Line> kvp in this.lines)
            {
                Line line = kvp.Value;
                string name = line.GetName();
                if (name == name_kWhr)
                {
                    this.plot.Plot.SetAxisLimits(yMin: 0, yMax: this.lines["EnergyPerDay"].Ymax * 1.1, yAxisIndex: 1);
                    this.plot.Plot.YAxis.Label(selectedLine);
                    this.plot.Plot.YAxis.Ticks(true);

                    line.SetVisibilty(true);
                }
                else
                {
                    bool enable = (name == selectedLine);
                    if (enable)
                    {
                        double t;
                        t = line.Ymin;
                        min = (t < min) ? t : min;
                        t = line.Ymax;
                        max = (t > max) ? t : max;
                        this.plot.Plot.SetAxisLimits(yMin: min, yMax: max * 1.1, yAxisIndex: 0);
                        this.plot.Plot.YAxis.Label(selectedLine);
                        this.plot.Plot.YAxis.Ticks(true);
                    }
                    line.SetVisibilty(enable);
                }
            }
            this.plot.Refresh();
        }
    }
}
