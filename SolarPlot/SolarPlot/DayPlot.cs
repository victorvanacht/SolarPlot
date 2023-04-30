using ScottPlot;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            private Color color;
            private Color fillColor;
            private bool fill;
            private bool bar;
            private double barThickness;
            private int axisIndex;

            private ScottPlot.Plottable.SignalPlotXY line;
            private ScottPlot.Plottable.BarPlot bars;

            public Line(ScottPlot.FormsPlot plot, string name, double[] xData, double[] yData, Color lineColor, Color fillColor, bool fill, bool bar, double barThickness,
                int axisIndex)
            {
                this.plot = plot;
                this.name = name;
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
        }

        private class LineProperty
        {
            public Color color;
            public Color fillColor;
            public bool fill;
            public bool bar;
            public double barThickness;
            public int axisIndex;

            public LineProperty(Color color, Color fillColor, bool fill, bool bar, double barThickness, int axisIndex)
            {
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

        private Dictionary<string, LineProperty> lineProperties = new Dictionary<string, LineProperty>()
        {
            ["Power"] = new LineProperty(Color.Blue, Color.Blue, true, false, 0, 0),
            ["Iac1"] = new LineProperty(Color.Red, Color.Red, false, false, 0, 1),
            ["Iac2"] = new LineProperty(Color.Green, Color.Green, false, false, 0, 1),
            ["Iac3"] = new LineProperty(Color.LightBlue, Color.LightBlue, false, false,0, 1),
            ["Vac1"] = new LineProperty(Color.Red, Color.Red, false, false, 0, 1),
            ["Vac2"] = new LineProperty(Color.Green, Color.Green, false, false, 0, 1),
            ["Vac3"] = new LineProperty(Color.LightBlue, Color.LightBlue, false, false, 0, 1),
            ["Freq1"] = new LineProperty(Color.Red, Color.Red, false, false, 0, 1),
            ["Freq2"] = new LineProperty(Color.Green, Color.Green, false, false, 0, 1),
            ["Freq3"] = new LineProperty(Color.LightBlue, Color.LightBlue, false, false, 0, 1),
            ["Ipv1"] = new LineProperty(Color.Red, Color.Red, false, false, 0, 1),
            ["Ipv2"] = new LineProperty(Color.Green, Color.Green, false, false, 0, 1),
            ["Vpv1"] = new LineProperty(Color.Red, Color.Red, false, false, 0, 1),
            ["Vpv2"] = new LineProperty(Color.Green, Color.Green, false, false, 0, 1),
            ["Temperature"] = new LineProperty(Color.Purple, Color.Purple, false, false, 0, 1),
            ["EnergyPerDay"] = new LineProperty(Color.Purple, Color.Purple, false, true, 0.8, 1),
            ["EnergyPerHalfHour"] = new LineProperty(Color.Purple, Color.Purple, false, true, 0.8/48, 1),
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
                lines.Add(barName, new Line(plot, barName, dataSet[barName].x, dataSet[barName].y, lineProperties[barName].color, lineProperties[barName].fillColor, lineProperties[barName].fill, lineProperties[barName].bar, lineProperties[barName].barThickness, lineProperties[barName].axisIndex));
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
                    lines.Add(name, new Line(plot, name, kvp.Value.x, kvp.Value.y, prop.color, prop.fillColor, prop.fill, prop.bar, prop.barThickness, prop.axisIndex));
                }
            }
            this.plot.Plot.XAxis.DateTimeFormat(true);
            this.legend = this.plot.Plot.Legend();
            this.legend.Orientation = ScottPlot.Orientation.Vertical;
            this.legend.Location = ScottPlot.Alignment.UpperLeft;
            this.legend.FontSize = 9;
            this.plot.Plot.XLabel("Date/Time");

            this.AutoRangeXAxis();
            this.AutoRangeYAxis(new string[] { "Power" }, 0);
            this.AutoRangeYAxis(new string[] { "EnergyPerDay" }, 1);
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
}
