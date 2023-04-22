using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace SolarPlot
{
    internal class YearPlot
    {
        class WeekEntry
        {
            public string name;
            public Color color;

            public WeekEntry(string name, Color color)
            {
                this.name = name;
                this.color = color;
            }
        }


        XYDataSet dataSet;
        System.Windows.Forms.DataVisualization.Charting.Chart plot;

        // orginal example code copy & paste from: https://social.msdn.microsoft.com/Forums/vstudio/en-US/9bd4beee-8546-4f17-aeb4-c9262f2d4a05/create-3d-surface?forum=vbgeneral
        // ported to C# by OpenAI's ChatGPT
        // a few tiny manual changes to get ChatGPT's work compiling & running

        public YearPlot(XYDataSet dataSet, System.Windows.Forms.DataVisualization.Charting.Chart plot)
        {
            this.dataSet = dataSet;
            this.plot = plot;

            this.CalcultePoints();

            //setup the chart
            ChartArea chartArea = plot.ChartAreas[0];
            chartArea.AxisX.Title = "Time of Day";

            for (int i = 0; i < 48; i++)
            {
                string labelString = "";
                if ((i%2)==0) labelString += (i/2).ToString()+":00";
                CustomLabel t = new CustomLabel(i, i+1, labelString, 0, LabelMarkStyle.None);
                chartArea.AxisX.CustomLabels.Add(t);
            }
            chartArea.AxisX.MajorGrid.LineColor = Color.LightBlue;
            chartArea.AxisX.Minimum = 0;
            chartArea.AxisX.Maximum = 48;
            chartArea.AxisX.Interval = 2;

            chartArea.AxisY.Title = "Average kWh";
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



            Dictionary<int,WeekEntry> weekData= new Dictionary<int, WeekEntry>()
            {
                [0] = new WeekEntry("January", Color.FromArgb(255,128,128)),
                [13] = new WeekEntry("April",  Color.FromArgb(192, 192, 128)),
                [26] = new WeekEntry("July",   Color.FromArgb(128, 255, 128)),
                [39] = new WeekEntry("October", Color.FromArgb(128, 192, 192))
            };


            //draw the chart
            plot.Series.Clear();
            Color color = Color.Black;
            for (int i = 0; i < 52; i++)
            {
                bool inLegend = false;
                string seriesName = "w" + i.ToString();
                if (weekData.ContainsKey(i))
                {
                    seriesName = weekData[i].name;
                    color = weekData[i].color;
                    inLegend = true;
                }

                plot.Series.Add(seriesName);
                plot.Series[i].ChartType = SeriesChartType.Area;
                //plot.Series[i].IsValueShownAsLabel = true;
                //plot.Series[i].AxisLabel = i.ToString();
                plot.Series[i].BorderWidth = 1;
                plot.Series[i].Color = color;
                plot.Series[i].IsVisibleInLegend = inLegend;
                // Set series strip width
                plot.Series[i]["PointWidth"] = "1";
                // Set series points gap to 1 pixels
                plot.Series[i]["PixelPointGapDepth"] = "1";

                for (int x = 0; x < 48; x++)
                {
                    plot.Series[i].Points.AddXY(x, data[2023][i, x]);
                }
            }
            DrawChart(0);
        }

        public void DrawChart(int angle)
        {

            plot.ChartAreas[0].Area3DStyle.Rotation = angle;
        }

        private Dictionary<int, double[,]> data;


        private void CalcultePoints()
        {
            data = new Dictionary<int, double[,]>();
            double[] x = this.dataSet["EnergyPerHalfHour"].x;
            double[] y = this.dataSet["EnergyPerHalfHour"].y;

            int year = DateTime.FromOADate(this.dataSet["EnergyPerHalfHour"].Xmin).Year;
            data.Add(year, new double[52, 48]);
            int end = this.dataSet["EnergyPerHalfHour"].count;
            for (int index =0; index< end; index ++)
            {
                DateTime t = DateTime.FromOADate(x[index]);
                if (t.Year != year) 
                {
                    year = t.Year;
                    data.Add(year, new double[52, 48]);

                }
                int week = (int)(t.DayOfYear / 7);
                int hr = t.Hour * 2 + (int)(t.Minute / 30);
                data[year][week, hr] += y[index] /7; // divide by 7 because we add up over a week.
            }
        }
    }
}

