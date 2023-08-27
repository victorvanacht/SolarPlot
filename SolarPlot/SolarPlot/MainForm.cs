using ScottPlot;
using ScottPlot.Renderable;
using ScottPlot.Styles;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;
using static ScottPlot.Generate;
using static SolarPlot.XYDataSet;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SolarPlot
{
    public partial class MainForm : Form
    {
        internal Dictionary<string, Inverter> inverter;
        private DayPlot dayPlot;
        private YearPlot yearPlot;
        private DecadePlot decadePlot;

        private Worker worker;

        public MainForm()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.SolarPlot;

            this.inverter = new Dictionary<string, Inverter>();
            this.worker = new Worker(this);

            if (Properties.Settings.Default.OpenGoodweCSVFile != "")
            {
                this.worker.Command("LoadGoodWeCSV " + Properties.Settings.Default.OpenGoodweCSVFile);
                this.worker.Command("CalculateEnergyPerPeriod");
                this.worker.Command("PlotInit");
            }
        }

        public void SetStatus(string status)
        {
            if (this.statusStrip.InvokeRequired)
            {
                this.statusStrip.Invoke((Action)delegate { SetStatus(status); });
            }
            else
            {
                this.toolStripStatusLabel1.Text = status;
            }
        }

        public void SetErrorStatus(string error)
        {
            if (this.statusStrip.InvokeRequired)
            {
                this.statusStrip.Invoke((Action)delegate { SetStatus(error); });
            }
            else
            {
                this.toolStripStatusLabel2.Text = error;
            }
        }

        public void SetProgress(int progress)
        {
            if (this.statusStrip.InvokeRequired)
            {
                this.statusStrip.Invoke((Action)delegate { SetProgress(progress); });
            }
            else
            {
                this.toolStripProgressBar.Value = progress;
            }
        }

        public void PlotInit()
        {
            if (this.PlotDayGraph.InvokeRequired)
            {
                this.PlotDayGraph.Invoke((Action)delegate { PlotInit(); });
            }
            else
            {
                this.comboBoxDayPlotInverterSelection.Items.Clear();
                foreach (KeyValuePair<string, Inverter> kvpInverter in this.inverter)
                {
                    this.comboBoxDayPlotInverterSelection.Items.Add(kvpInverter.Key);
                    foreach (KeyValuePair<string, InverterChannel> kvpChannel in kvpInverter.Value.channel)
                    {
                        this.comboBoxDayPlotInverterSelection.Items.Add(kvpInverter.Key + ":" + kvpChannel.Key);
                    }
                }
                this.comboBoxDayPlotInverterSelection.SelectedIndex = 0;

                this.yearPlot = new YearPlot(this.inverter, this.PlotYear, this.YearComboBoxSelectYear);
                this.decadePlot = new DecadePlot(this.inverter, this.PlotDecade);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("SolarPlot\nProgrammed by Victor van Acht\n\nhttps://github.com/victorvanacht/SolarPlot");
        }

        private void openGoodweCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "csv files (*.csv)|*.csv|txt files (*.txt)|*.txt|all files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.OpenGoodweCSVFile = openFileDialog.FileName;
                this.worker.Command("LoadGoodweCSV " + openFileDialog.FileName);
                this.worker.Command("CalculateEnergyPerPeriod");
                this.worker.Command("PlotInit");
            }
        }

        private void openOpenDTUCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "csv files (*.csv)|*.csv|txt files (*.txt)|*.txt|all files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.OpenOpenDTUCSVFile = openFileDialog.FileName;
                this.worker.Command("LoadOpenDTUCSV " + openFileDialog.FileName);
                this.worker.Command("CalculateEnergyPerPeriod");
                this.worker.Command("PlotInit");
            }
        }

        private void FormExit_Click(object sender, FormClosingEventArgs e)
        {
            /*
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                OTGWButtonDisconnect_Click(sender, e);
                MaxButtonDisconnect_Click(sender, e);
            }
            */
            Exit(sender, e);
        }

        private void Exit(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            this.worker.Close(5);
            Application.Exit();
        }

        private void YearTrackBarAngle_ValueChanged(object sender, EventArgs e)
        {
            this.yearPlot.DrawChart(this.YearTrackBarAngle.Value);
        }

        private void YearComboBoxSelectYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.yearPlot!=null)
            {
                int selectedIndex = this.YearComboBoxSelectYear.SelectedIndex;
                int year = Int32.Parse(this.YearComboBoxSelectYear.Items[selectedIndex].ToString());

                this.yearPlot.LoadYearDataInChart(year);
                this.yearPlot.DrawChart(this.YearTrackBarAngle.Value);
            }
        }

        private void DecadeTrackBarAngle_ValueChanged(object sender, EventArgs e)
        {
            this.decadePlot.DrawChart(this.DecadeTrackBarAngle.Value);
        }

        private void comboBoxDayPlotInverterSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedInverterChannel = this.comboBoxDayPlotInverterSelection.GetItemText(this.comboBoxDayPlotInverterSelection.SelectedItem);
            if (selectedInverterChannel.Contains(":"))
            {
                int index = selectedInverterChannel.IndexOf(":");
                string selectedInverter = selectedInverterChannel.Substring(0,index);
                string selectedChannel = selectedInverterChannel.Substring(index+ 1);
                this.dayPlot = new DayPlot(this.inverter[selectedInverter].channel[selectedChannel].dataSet, this.PlotDayGraph);
            }
            else
            {
                this.dayPlot = new DayPlot(this.inverter[selectedInverterChannel].dataSet, this.PlotDayGraph);
            }

            this.comboBoxDayPlotLineSelection.Items.Clear();
            this.comboBoxDayPlotLineSelection.Items.AddRange(this.dayPlot.GetLineNames());
            this.comboBoxDayPlotLineSelection.SelectedIndex= 0;
        }

        private void comboBoxDayPlotLineSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dayPlot.EnableLine(this.comboBoxDayPlotLineSelection.SelectedItem.ToString());
        }
    }
}

