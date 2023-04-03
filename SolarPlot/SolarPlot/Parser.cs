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
                    form.data = new MainForm.DataSet(form);

                    FileInfo fileInfo = new FileInfo(commandItems[1]);
                    Int64 fileSize = fileInfo.Length;

                    using (StreamReader file = new StreamReader(commandItems[1]))
                    {
                        //first read heading
                        string[] heading = file.ReadLine().Split(',');

                        int indexDateTime = FindInStringArray(heading, "TimeStamp");
                        int indexPower = FindInStringArray(heading, "Power");
                        int indexIac1 = FindInStringArray(heading, "Iac1");
                        int indexIac2 = FindInStringArray(heading, "Iac2");
                        int indexIac3 = FindInStringArray(heading, "Iac3");
                        int indexVac1 = FindInStringArray(heading, "Vac1");
                        int indexVac2 = FindInStringArray(heading, "Vac2");
                        int indexVac3 = FindInStringArray(heading, "Vac3");
                        int indexFreq1 = FindInStringArray(heading, "Freq1");
                        int indexFreq2 = FindInStringArray(heading, "Freq2");
                        int indexFreq3 = FindInStringArray(heading, "Freq3");
                        int indexIpv1 = FindInStringArray(heading, "Ipv1");
                        int indexIpv2 = FindInStringArray(heading, "Ipv2");
                        int indexVpv1 = FindInStringArray(heading, "Vpv1");
                        int indexVpv2 = FindInStringArray(heading, "Vpv2");
                        int indexTemperature = FindInStringArray(heading, "Temperature");

                        if ((indexDateTime >=0) && (indexPower>=0))
                        {

                            DateTime previousDateTime = new DateTime();
                            while (!file.EndOfStream)
                            {
                                string[] line = file.ReadLine().Split(',');

                                Int64 position = file.BaseStream.Position;
                                this.form.SetProgress((int)((position * 100) / fileSize));

                                DateTime datetime = DateTime.Parse(line[indexDateTime], CultureInfo.InvariantCulture);
                                double power = double.Parse(line[indexPower], CultureInfo.InvariantCulture);

                                double Iac1 = (indexIac1 >= 0)? double.Parse(line[indexIac1], CultureInfo.InvariantCulture) : 0;
                                double Iac2 = (indexIac2 >= 0) ? double.Parse(line[indexIac2], CultureInfo.InvariantCulture) : 0;
                                double Iac3 = (indexIac3 >= 0) ? double.Parse(line[indexIac3], CultureInfo.InvariantCulture) : 0;
                                double Vac1 = (indexVac1 >= 0) ? double.Parse(line[indexVac1], CultureInfo.InvariantCulture) : 0;
                                double Vac2 = (indexVac2 >= 0) ? double.Parse(line[indexVac2], CultureInfo.InvariantCulture) : 0;
                                double Vac3 = (indexVac3 >= 0) ? double.Parse(line[indexVac3], CultureInfo.InvariantCulture) : 0;
                                double freq1 = (indexFreq1 >= 0) ? double.Parse(line[indexFreq1], CultureInfo.InvariantCulture) : 0;
                                double freq2 = (indexFreq2 >= 0) ? double.Parse(line[indexFreq2], CultureInfo.InvariantCulture) : 0;
                                double freq3 = (indexFreq3 >= 0) ? double.Parse(line[indexFreq3], CultureInfo.InvariantCulture) : 0;
                                double Ipv1 = (indexIpv1 >= 0) ? double.Parse(line[indexIpv1], CultureInfo.InvariantCulture) : 0;
                                double Ipv2 = (indexIpv2 >= 0) ? double.Parse(line[indexIpv2], CultureInfo.InvariantCulture) : 0;
                                double Vpv1 = (indexVpv1 >= 0) ? double.Parse(line[indexVpv1], CultureInfo.InvariantCulture) : 0;
                                double Vpv2 = (indexVpv2 >= 0) ? double.Parse(line[indexVpv2], CultureInfo.InvariantCulture) : 0;
                                double temperature = (indexTemperature >= 0) ? double.Parse(line[indexTemperature], CultureInfo.InvariantCulture) : 0;



                                if (datetime > previousDateTime) // make sure we only have ascending date times
                                {
                                    this.form.data.Add(datetime, power, Iac1, Iac2, Iac3, Vac1, Vac2, Vac3, freq1, freq2, freq3, Ipv1, Ipv2, Vpv1, Vpv2, temperature);
                                    previousDateTime = datetime;
                                }
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
