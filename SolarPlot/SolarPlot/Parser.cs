using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Globalization;
using static ScottPlot.Plottable.PopulationPlot;
using static SolarPlot.Worker.LoadGoodweCSV;
using System.Data.Common;
using System.Xml.Linq;
using ScottPlot;
using System.Windows.Forms;
using System.Runtime.Remoting.Channels;
using static SolarPlot.XYDataSet;

namespace SolarPlot
{
    internal partial class Worker
    {
        private Dictionary<string, ParsersBase> commands;

        private void InitializeParser()
        {
            this.commands = new Dictionary<string, ParsersBase>
            {
                ["LOADGOODWECSV"] = new LoadGoodweCSV(this.form),
                ["LOADOPENDTUCSV"] = new LoadOpenDTUCSV(this.form),
                ["CALCULATEENERGYPERPERIOD"] = new CalculateEnergyPerPeriod(this.form),
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
            protected Dictionary<string, Inverter> inverter;

            public ParsersBase(MainForm form)
            {
                this.form = form;
                this.inverter = form.inverter;
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

        public class LoadGoodweCSV : ParsersBase
        {
            public LoadGoodweCSV(MainForm form) : base(form) { }

            override public void Parse(string[] commandItems)
            {
                if (File.Exists(commandItems[1]))
                {
                    if (this.inverter.ContainsKey("Goodwe"))
                    {
                        this.inverter["Goodwe"] = new Inverter("Goodwe");
                    }
                    else
                    {
                        this.inverter.Add("Goodwe", new Inverter("Goodwe"));
                    }
                    XYDataSet dataSet = this.inverter["Goodwe"].dataSet;

                    FileInfo fileInfo = new FileInfo(commandItems[1]);
                    Int64 fileSize = fileInfo.Length;

                    using (StreamReader file = new StreamReader(commandItems[1]))
                    {
                        //first read heading
                        string[] heading = file.ReadLine().Split(',');

                        int columnIndexTimeStamp = FindInStringArray(heading, "TimeStamp");
                        string[] columnNames = {"Power", "Iac1", "Iac2", "Iac3", "Vac1", "Vac2", "Vac3", "Freq1", "Freq2", "Freq3", "Ipv1", "Ipv2", "Vpv1", "Vpv2", "Temperature" };
                        Dictionary<string, int> columnIndex = new Dictionary<string, int>();

                        foreach (string name in columnNames)
                        {
                            int column = FindInStringArray(heading, name);
                            if (column >= 0)
                            {
                                columnIndex.Add(name, column);
                                dataSet += name;
                            }
                        }

                        if ((columnIndexTimeStamp >= 0) && (columnIndex["Power"] >=0))
                        {
                            DateTime previousDateTime = new DateTime();
                            while (!file.EndOfStream)
                            {
                                string[] line = file.ReadLine().Split(',');

                                Int64 position = file.BaseStream.Position;
                                this.form.SetProgress((int)((position * 100) / fileSize));

                                DateTime dateTime = DateTime.Parse(line[columnIndexTimeStamp], CultureInfo.InvariantCulture);
                                if (dateTime > previousDateTime) // make sure we only have ascending date times
                                { 
                                    foreach (KeyValuePair<string,int> kvp in columnIndex)
                                    {
                                        dataSet[kvp.Key] += new XYDataSet.XYPoint<double>(dateTime, double.Parse(line[kvp.Value], CultureInfo.InvariantCulture));
                                    }
                                    previousDateTime = dateTime;
                                }
                            }
                            dataSet.FillRemainingXAxis();
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

            public class LoadOpenDTUCSV : ParsersBase
            {
                private class InverterNameTranslator
                {
                    public string inverterName;
                    public Dictionary<int, string> channelNameTranslator;

                    public InverterNameTranslator(string name)
                    {
                        inverterName = name;
                        channelNameTranslator = new Dictionary<int, string>();
                    }
                }
                private Dictionary<string, InverterNameTranslator> inverterNameTranslator;
                private SortedDictionary<int, XYData<double>> columnGraphTranslator;

                private string[] channelColumnsToSelect = { "Current", "Voltage", "Power" };
                private string[] inverterColumnsToSelect = { "CurrentAC", "VoltageAC", "Power", "Frequency", "PowerDC", "Temperature" };

                public LoadOpenDTUCSV(MainForm form) : base(form) { }

                override public void Parse(string[] commandItems)
                {
                    inverterNameTranslator = new Dictionary<string, InverterNameTranslator>();
                    columnGraphTranslator = new SortedDictionary<int, XYData<double>>();

                    if (File.Exists(commandItems[1]))
                    {
                        FileInfo fileInfo = new FileInfo(commandItems[1]);
                        Int64 fileSize = fileInfo.Length;

                        using (StreamReader file = new StreamReader(commandItems[1]))
                        {
                            //first read heading
                            string[] heading = file.ReadLine().Split(',');
                            // also read first line of data (to extract the name of the inverter)
                            string[] firstLine = file.ReadLine().Split(',');
                            for (int i = 0; i < heading.Length; i++)
                            {
                                heading[i] = heading[i].Trim();
                            }
                            for (int i = 0; i < firstLine.Length; i++)
                            {
                                firstLine[i] = firstLine[i].Trim();
                            }

                            int columnIndexTimeStamp = FindInStringArray(heading, "TimeStamp");
                            for (int columnIndex = 0; columnIndex < heading.Length; columnIndex++)
                            {
                                if (heading[columnIndex].Contains(":"))
                                {
                                    string[] subheading = heading[columnIndex].Split(':');

                                    string serialNumber = subheading[0].Split('[')[0]; // Split('[') to cut off the channel number, if present
                                    if (!inverterNameTranslator.ContainsKey(serialNumber))
                                    { // this is a new inverter
                                        string name = firstLine[columnIndex];

                                        if (subheading[1].Equals("Name"))
                                        {   // we have found the name of the interter!
                                            inverterNameTranslator.Add(serialNumber, new InverterNameTranslator(name));

                                            // we should check if this inverter is already in the dataset. If so, we need to delete it.
                                            if (this.inverter.ContainsKey(name))
                                            {
                                                this.inverter[name] = new Inverter(name);
                                            }
                                            else
                                            { // we dont have it in the dataset yet, so we should make it.
                                                this.inverter.Add(name, new Inverter(name));
                                            }
                                        }
                                    }
                                    else
                                    { // we know the name of the inverter, so it is also in the list of inverters of the dataset.
                                        string inverterName = inverterNameTranslator[serialNumber].inverterName;

                                        // check if it is a column about a channel
                                        if (heading[columnIndex].Contains("["))
                                        { // yes it is a column about a channel
                                            int channelIndex = heading[columnIndex].IndexOf("[") + 1;
                                            int channel = Int32.Parse(heading[columnIndex].Substring(channelIndex, 1));

                                            //Check if this is a newly discovered channel.
                                            if (!inverterNameTranslator[serialNumber].channelNameTranslator.ContainsKey(channel))
                                            { // we need to add it!
                                                if (subheading[1].Equals("Name"))
                                                {
                                                    string channelName = firstLine[columnIndex];

                                                    inverterNameTranslator[serialNumber].channelNameTranslator.Add(channel, channelName);
                                                    // see if we have it already in the dataset. If so, we need to delete it
                                                    if (inverter[inverterName].channel.ContainsKey(channelName))
                                                    { // yes it exists. We need to delete it
                                                        inverter[inverterName].channel[channelName] = new InverterChannel(channelName);
                                                    }
                                                    else
                                                    { // no it does not exist, we need to add it.
                                                        inverter[inverterName].channel.Add(channelName, new InverterChannel(channelName));
                                                    }
                                                }
                                            }
                                            else
                                            { // this is a known channel
                                                string channelName = inverterNameTranslator[serialNumber].channelNameTranslator[channel];

                                                foreach (string columnName in channelColumnsToSelect)
                                                {
                                                    if (columnName.Equals(subheading[1]))
                                                    {
                                                        string graphName = subheading[1];
                                                        inverter[inverterName].channel[channelName].dataSet += graphName;
                                                        this.columnGraphTranslator.Add(columnIndex, this.inverter[inverterName].channel[channelName].dataSet[graphName]);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        { // no we are reading a general inverter information
                                            foreach (string columnName in inverterColumnsToSelect)
                                            {
                                                if (columnName.Equals(subheading[1]))
                                                {
                                                    string graphName = subheading[1];
                                                    inverter[inverterName].dataSet += graphName;
                                                    this.columnGraphTranslator.Add(columnIndex, this.inverter[inverterName].dataSet[graphName]);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            DateTime previousDateTime = new DateTime();
                            while (!file.EndOfStream)
                            {
                                string[] line = file.ReadLine().Split(',');

                                Int64 position = file.BaseStream.Position;
                                this.form.SetProgress((int)((position * 100) / fileSize));

                                DateTime dateTime = DateTime.Parse(line[columnIndexTimeStamp], CultureInfo.InvariantCulture);
                                if (dateTime > previousDateTime) // make sure we only have ascending date times
                                {
                                    for (int columnIndex = 0; columnIndex < line.Length; columnIndex++)
                                    {
                                        if (columnGraphTranslator.ContainsKey(columnIndex))
                                        {
                                            columnGraphTranslator[columnIndex] += new XYDataSet.XYPoint<double>(dateTime, double.Parse(line[columnIndex], CultureInfo.InvariantCulture));
                                        }
                                    }
                                    previousDateTime = dateTime;
                                }
                            }


                            foreach (KeyValuePair<int, XYData<double>> kvp in columnGraphTranslator)
                            {
                                kvp.Value.FillRemainingXAxis();
                            }
                            this.form.SetProgress(0);
                        }
                    }
                    else
                    {
                        this.form.SetErrorStatus("File not found.");
                    }
                }
            }


            public class CalculateEnergyPerPeriod : ParsersBase
            {
                public CalculateEnergyPerPeriod(MainForm form) : base (form) {  }

                public override void Parse(string[] commandItems)
                {
                    foreach (KeyValuePair<string, Inverter> kvpInverter in inverter)
                    {
                        kvpInverter.Value.dataSet.CalculateEnergyFromPower();
                        foreach(KeyValuePair<string, InverterChannel> kvpChannel in kvpInverter.Value.channel)
                        {
                            kvpChannel.Value.dataSet.CalculateEnergyFromPower();
                        }
                    }
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
