using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ScottPlot.Generate;

namespace SolarPlot
{
    public class XYDataSet : IEnumerator, IEnumerable
    {
        public class XYPoint
        {
            public double x;
            public double y;

            public XYPoint(double x, double y)
            {
                this.x = x;
                this.y = y;
            }

            public XYPoint(System.DateTime dateTime, double y)
            {
                this.x = dateTime.ToOADate();
                this.y = y;
            }
        }

        public class XYData
        {
            public double[] x;
            public double[] y;
            public int count;

            public XYData() : this(1000) { }
            public XYData(int initialSize)
            {
                this.x = new double[initialSize];
                this.y = new double[initialSize];
                count = 0;
            }

            public static XYData operator +(XYData xyData, XYPoint point)
            {
                if (xyData.count == xyData.x.Length)
                {
                    int count2 = xyData.count * 2;
                    Array.Resize(ref xyData.x, count2);
                    Array.Resize(ref xyData.y, count2);
                }
                xyData.x[xyData.count] = point.x;
                xyData.y[xyData.count] = point.y;
                xyData.count++;

                return xyData;
            }
            public static XYData operator +(XYPoint point, XYData xyData)
            {
                return xyData + point;
            }

            public double Xmin { get => x[0]; }

            public double Xmax { get => x[this.count - 1]; }

            public double Ymin { get => y.Min(); }

            public double Ymax { get => y.Max(); }

            // fill remaining x-as with reasonable data, to prevent ScottPlot giving an error
            public void FillRemainingXAxis()
            {
                double step = (x[this.count - 1] - x[0]) / (count - 1);
                for (int i = count; i < x.Length; i++)
                {
                    x[i] = x[0] + i * step;
                }
            }
        }

        private Dictionary<string, XYData> dataSet;
        private int position = -1; // this position is needed for IEnumerable

        public XYDataSet()
        {
            this.Clear();
        }

        public static XYDataSet operator +(XYDataSet dataSet, string name)
        {
            dataSet.dataSet.Add(name, new XYData());
            return dataSet;
        }
        public static XYDataSet operator +(string name, XYDataSet dataSet)
        {
            return dataSet + name;
        }

        public XYData this[string index]
        {
            get
            {
                return this.dataSet[index];
            }
            set
            {
                this.dataSet[index] = value;
            }
        }

        //IEnumerator and IEnumerable require these methods.
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        //IEnumerator
        public bool MoveNext()
        {
            position++;
            return (position < dataSet.Count);
        }
        //IEnumerable
        public void Reset()
        {
            position = -1;
        }
        //IEnumerable
        public object Current
        {
            get { KeyValuePair<string, XYData> kvp = dataSet.ElementAtOrDefault(position);
                return kvp; }
        }

        public void FillRemainingXAxis()
        {
            foreach (KeyValuePair<string, XYData> kvp in dataSet)
            {
                kvp.Value.FillRemainingXAxis();
            }
        }

        public void Clear()
        {
            this.dataSet = new Dictionary<string, XYData>();
        }
    }
}
