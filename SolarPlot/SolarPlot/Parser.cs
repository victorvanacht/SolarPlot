﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Globalization;
using static ScottPlot.Plottable.PopulationPlot;
using static SolarPlot.Worker.LoadCSV;
using System.Data.Common;
using System.Xml.Linq;
using ScottPlot;
using System.Windows.Forms;

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
            protected XYDataSet dataSet;

            public ParsersBase(MainForm form)
            {
                this.form = form;
                this.dataSet = form.dataSet;
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
                    this.dataSet.Clear();

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
                                this.dataSet += name;
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
                                        this.dataSet[kvp.Key] += new XYDataSet.XYPoint<double>(dateTime, double.Parse(line[kvp.Value], CultureInfo.InvariantCulture));
                                    }
                                    previousDateTime = dateTime;
                                }
                            }
                            this.dataSet.FillRemainingXAxis();
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

            public class CalculateEnergyPerPeriod : ParsersBase
            {
                public CalculateEnergyPerPeriod(MainForm form) : base (form) {  }

                public override void Parse(string[] commandItems)
                {
                    this.dataSet.CalculateEnergyFromPower();
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
