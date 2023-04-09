using ScottPlot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ScottPlot.Generate;
using static SolarPlot.XYDataSet;

namespace SolarPlot
{
    public class XYDataSet : IEnumerable
    {
        public class XYPoint<T>
        {
            public double x;
            public T y;

            public XYPoint(double x, T y)
            {
                this.x = x;
                this.y = y;
            }

            public XYPoint(System.DateTime dateTime, T y)
            {
                this.x = dateTime.ToOADate();
                this.y = y;
            }
        }

        public class XYData<T>
        {
            public double[] x;
            public T[] y;
            public int count;

            public XYData() : this(1000) { }
            public XYData(int initialSize)
            {
                this.x = new double[initialSize];
                this.y = new T[initialSize];
                count = 0;
            }

            public static XYData<T> operator +(XYData<T> xyData, XYPoint<T> point)
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

            public double Xmin { get => x[0]; }

            public double Xmax { get => x[this.count - 1]; }

            public double Ymin
            {
                get
                {
                    if (typeof(T) == typeof(double))
                    {
                        double[] yAsDouble = (double[])Convert.ChangeType(y, typeof(double[]));
                        return yAsDouble.Min();
                    }
                    else
                    {
                        return 0;
                    }
                }
            }

            public double Ymax
            {
                get
                {
                    if (typeof(T) == typeof(double))
                    {
                        double[] yAsDouble = (double[])Convert.ChangeType(y, typeof(double[]));
                        return yAsDouble.Max();
                    }
                    else
                    {
                        return 0;
                    }
                }
            }


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

        protected Dictionary<string, XYData<double>> dataSet;

        public XYDataSet()
        {
            this.Clear();
        }

        public static XYDataSet operator +(XYDataSet dataSet, string name)
        {
            dataSet.dataSet.Add(name, new XYData<double>());
            return dataSet;
        }

        public XYData<double> this[string index]
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

        public bool ContainsKey(string key) 
        {
            return this.dataSet.ContainsKey(key);
        }

        //IEnumerator and IEnumerable require these methods.
        public IEnumerator GetEnumerator()
        {
            return new EnumeratorImplementation(this);
        }

        private class EnumeratorImplementation : IEnumerator
        {
            private XYDataSet dataSet;
            private int position = -1;


            public EnumeratorImplementation(XYDataSet dataset)
            {
                this.dataSet = dataset;
            }

            //IEnumerator
            public bool MoveNext()
            {
                position++;
                return (position < this.dataSet.dataSet.Count);
            }

            //IEnumerable
            public void Reset()
            {
                position = -1;
            }

            //IEnumerable
            public object Current
            {
                get
                {
                    KeyValuePair<string, XYData<double>> kvp = dataSet.dataSet.ElementAtOrDefault(position);
                    return kvp;
                }
            }
        }

        public void FillRemainingXAxis()
        {
            foreach (KeyValuePair<string, XYData<double>> kvp in dataSet)
            {
                kvp.Value.FillRemainingXAxis();
            }
        }

        public void Clear()
        {
            this.dataSet = new Dictionary<string, XYData<double>>();
        }
    }
}
