using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Globalization;
using static ScottPlot.Plottable.PopulationPlot;
using static SolarPlot.Worker.LoadCSV;

namespace SolarPlot
{
    internal partial class Worker
    {
        private Dictionary<string, ParsersBase> commands;

        private void InitializeParser()
        {
            this.commands = new Dictionary<string, ParsersBase>
            {
                ["LOADCSV"] = new LoadCSV(this.form),
                ["PLOTINIT"] = new PlotInit(this.form),
            };
        }


        private void Parse(string command)
        {
            var commandElements = command.Split(' ');
            commandElements[0] = commandElements[0].ToUpper();
            if (commands.ContainsKey(commandElements[0]))
            {
                SetStatus(commandElements[0]);
                commands[commandElements[0]].Parse(commandElements);
            }
            else
            {
                this.form.SetErrorStatus("Syntax error");
            }

        }

        public class ParsersBase
        {
            protected MainForm form;

            public ParsersBase(MainForm form)
            {
                this.form = form;
            }

            virtual public void Parse(string[] commandItems) { }

            protected static int FindInStringArray(string[] array, string searchString)
            {
                int r;
                for (r=0; r < array.Length; r++)
                {
                    if (array[r].Trim().Equals(searchString)) return r;
                }
                return -1;
            }

        }

        public class LoadCSV : ParsersBase
        {
            public LoadCSV(MainForm form) : base(form) { }

            override public void Parse(string[] commandItems)
            {
                if (File.Exists(commandItems[1]))
                {
                    FileInfo fileInfo = new FileInfo(commandItems[1]);
                    Int64 fileSize = fileInfo.Length;

                    using (StreamReader file = new StreamReader(commandItems[1]))
                    {
                        //first read heading
                        string[] heading = file.ReadLine().Split(',');

                        int indexDateTime = FindInStringArray(heading, "TimeStamp");
                        int indexPower = FindInStringArray(heading, "Power");

                        if ((indexDateTime >=0) && (indexPower>=0))
                        {

                            while (!file.EndOfStream)
                            {
                                string[] line = file.ReadLine().Split(',');

                                Int64 position = file.BaseStream.Position;
                                this.form.SetProgress((int)((position * 100) / fileSize));

                                DateTime datetime = DateTime.Parse(line[indexDateTime], CultureInfo.InvariantCulture);
                                double power = double.Parse(line[indexPower], CultureInfo.InvariantCulture);

                                this.form.data.Add(datetime, power);
                            }
                            this.form.SetProgress(0);
                        }
                        else
                        {
                            this.form.SetErrorStatus("File format error.");
                        }
                    }
                }
                else
                {
                    this.form.SetErrorStatus("File not found.");
                }
            }

            public class PlotInit : ParsersBase
            {
                public PlotInit(MainForm form) : base(form) { }

                override public void Parse(string[] commandItems)
                {
                    this.form.PlotInit();
                }
            }

        }
    }
}
