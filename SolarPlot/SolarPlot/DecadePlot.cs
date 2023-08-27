using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using static ScottPlot.Plottable.PopulationPlot;
using static SolarPlot.XYDataSet;

namespace SolarPlot
{
    internal class DecadePlot
    {
        XYDataSet dataSet;
        System.Windows.Forms.DataVisualization.Charting.Chart plot;

        private double[,] data;
        private int firstYear;
        private int lastYear;


        // orginal example code copy & paste from: https://social.msdn.microsoft.com/Forums/vstudio/en-US/9bd4beee-8546-4f17-aeb4-c9262f2d4a05/create-3d-surface?forum=vbgeneral
        // ported to C# by OpenAI's ChatGPT
        // a few tiny manual changes to get ChatGPT's work compiling & running

        public DecadePlot(Dictionary<string, Inverter> inverter, System.Windows.Forms.DataVisualization.Charting.Chart plot)
        {
            this.plot = plot;

            // Calculate data
            data = new double[100, 52]; // 100 years
            foreach (KeyValuePair<string, Inverter> kvpInverter in inverter)
            {
                Inverter inverterSelected = kvpInverter.Value;
                XYDataSet dataSet = inverterSelected.dataSet;

                double[] x = dataSet["EnergyPerHalfHour"].x;
                double[] y = dataSet["EnergyPerHalfHour"].y;

                this.firstYear = DateTime.FromOADate(dataSet["EnergyPerHalfHour"].Xmin).Year;
                this.lastYear = DateTime.FromOADate(dataSet["EnergyPerHalfHour"].Xmax).Year;
                int end = dataSet["EnergyPerHalfHour"].count;
                for (int index = 0; index<end; index++)
                {
                    DateTime t = DateTime.FromOADate(x[index]);
                    if ((t.Year - this.firstYear) >= 100) break;
                    int week = (int)(t.DayOfYear / 7);
                    data[t.Year - this.firstYear, week] += y[index] /7; // divide by 7 because we want it per day.
                }
            }

            //setup the chart
            ChartArea chartArea = plot.ChartAreas[0];
            chartArea.AxisX.Title = "Week of Year";

            for (int i = 0; i < 52; i++)
            {
                CustomLabel t = new CustomLabel(i, i+1, i.ToString(), 0, LabelMarkStyle.None);
                chartArea.AxisX.CustomLabels.Add(t);
            }
            chartArea.AxisX.MajorGrid.LineColor = Color.LightBlue;
            chartArea.AxisX.Minimum = 0;
            chartArea.AxisX.Maximum = 52;
            chartArea.AxisX.Interval = 2;

            chartArea.AxisY.Title = "Average kWh per day";
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

            LoadYearDataInChart();
            DrawChart(0);
        }

        public void LoadYearDataInChart()
        {
            plot.Series.Clear();
            int colorStep = 255 / (this.lastYear - this.firstYear + 1);
            for (int i = 0; i <= (this.lastYear - this.firstYear); i++)
            {
                plot.Series.Add((this.firstYear + i).ToString());
                plot.Series[i].ChartType = SeriesChartType.Area;
                plot.Series[i].BorderWidth = 1;
                plot.Series[i].Color = Color.FromArgb(i*colorStep, 255-i*colorStep, 128);
                plot.Series[i].IsVisibleInLegend = ((i % 5) == 0);
                // Set series strip width
                plot.Series[i]["PointWidth"] = "1";
                // Set series points gap to 1 pixels
                plot.Series[i]["PixelPointGapDepth"] = "1";

                for (int x = 0; x < 52; x++)
                {
                    plot.Series[i].Points.AddXY(x, data[i, x]);
                }
            }
        }

        public void DrawChart(int angle)
        {

            plot.ChartAreas[0].Area3DStyle.Rotation = angle;
        }
    }
}

