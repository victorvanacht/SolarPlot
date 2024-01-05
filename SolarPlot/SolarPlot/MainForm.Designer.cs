namespace SolarPlot
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.PlotDayGraph = new ScottPlot.FormsPlot();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageDay = new System.Windows.Forms.TabPage();
            this.comboBoxDayPlotLineSelection = new System.Windows.Forms.ComboBox();
            this.comboBoxDayPlotInverterSelection = new System.Windows.Forms.ComboBox();
            this.tabPageYear = new System.Windows.Forms.TabPage();
            this.YearComboBoxSelectYear = new System.Windows.Forms.ComboBox();
            this.YearTrackBarAngle = new System.Windows.Forms.TrackBar();
            this.PlotYear = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPageDecade = new System.Windows.Forms.TabPage();
            this.DecadeTrackBarAngle = new System.Windows.Forms.TrackBar();
            this.PlotDecade = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openGoodweCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openOpenDTUCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tabControl.SuspendLayout();
            this.tabPageDay.SuspendLayout();
            this.tabPageYear.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.YearTrackBarAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlotYear)).BeginInit();
            this.tabPageDecade.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DecadeTrackBarAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlotDecade)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // PlotDayGraph
            // 
            this.PlotDayGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlotDayGraph.Location = new System.Drawing.Point(3, 3);
            this.PlotDayGraph.Margin = new System.Windows.Forms.Padding(0);
            this.PlotDayGraph.Name = "PlotDayGraph";
            this.PlotDayGraph.Size = new System.Drawing.Size(968, 419);
            this.PlotDayGraph.TabIndex = 34;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageDay);
            this.tabControl.Controls.Add(this.tabPageYear);
            this.tabControl.Controls.Add(this.tabPageDecade);
            this.tabControl.Location = new System.Drawing.Point(0, 27);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(984, 484);
            this.tabControl.TabIndex = 35;
            // 
            // tabPageDay
            // 
            this.tabPageDay.Controls.Add(this.comboBoxDayPlotLineSelection);
            this.tabPageDay.Controls.Add(this.comboBoxDayPlotInverterSelection);
            this.tabPageDay.Controls.Add(this.PlotDayGraph);
            this.tabPageDay.Location = new System.Drawing.Point(4, 22);
            this.tabPageDay.Name = "tabPageDay";
            this.tabPageDay.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDay.Size = new System.Drawing.Size(976, 458);
            this.tabPageDay.TabIndex = 0;
            this.tabPageDay.Text = "Day";
            this.tabPageDay.UseVisualStyleBackColor = true;
            // 
            // comboBoxDayPlotLineSelection
            // 
            this.comboBoxDayPlotLineSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDayPlotLineSelection.FormattingEnabled = true;
            this.comboBoxDayPlotLineSelection.Location = new System.Drawing.Point(785, 433);
            this.comboBoxDayPlotLineSelection.Name = "comboBoxDayPlotLineSelection";
            this.comboBoxDayPlotLineSelection.Size = new System.Drawing.Size(163, 21);
            this.comboBoxDayPlotLineSelection.TabIndex = 43;
            this.comboBoxDayPlotLineSelection.SelectedIndexChanged += new System.EventHandler(this.comboBoxDayPlotLineSelection_SelectedIndexChanged);
            // 
            // comboBoxDayPlotInverterSelection
            // 
            this.comboBoxDayPlotInverterSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxDayPlotInverterSelection.FormattingEnabled = true;
            this.comboBoxDayPlotInverterSelection.Location = new System.Drawing.Point(6, 433);
            this.comboBoxDayPlotInverterSelection.Name = "comboBoxDayPlotInverterSelection";
            this.comboBoxDayPlotInverterSelection.Size = new System.Drawing.Size(183, 21);
            this.comboBoxDayPlotInverterSelection.TabIndex = 42;
            this.comboBoxDayPlotInverterSelection.SelectedIndexChanged += new System.EventHandler(this.comboBoxDayPlotInverterSelection_SelectedIndexChanged);
            // 
            // tabPageYear
            // 
            this.tabPageYear.Controls.Add(this.YearComboBoxSelectYear);
            this.tabPageYear.Controls.Add(this.YearTrackBarAngle);
            this.tabPageYear.Controls.Add(this.PlotYear);
            this.tabPageYear.Location = new System.Drawing.Point(4, 22);
            this.tabPageYear.Name = "tabPageYear";
            this.tabPageYear.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageYear.Size = new System.Drawing.Size(976, 458);
            this.tabPageYear.TabIndex = 1;
            this.tabPageYear.Text = "Year";
            this.tabPageYear.UseVisualStyleBackColor = true;
            // 
            // YearComboBoxSelectYear
            // 
            this.YearComboBoxSelectYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.YearComboBoxSelectYear.FormattingEnabled = true;
            this.YearComboBoxSelectYear.Location = new System.Drawing.Point(6, 410);
            this.YearComboBoxSelectYear.Name = "YearComboBoxSelectYear";
            this.YearComboBoxSelectYear.Size = new System.Drawing.Size(92, 21);
            this.YearComboBoxSelectYear.TabIndex = 40;
            this.YearComboBoxSelectYear.SelectedIndexChanged += new System.EventHandler(this.YearComboBoxSelectYear_SelectedIndexChanged);
            // 
            // YearTrackBarAngle
            // 
            this.YearTrackBarAngle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.YearTrackBarAngle.Location = new System.Drawing.Point(104, 410);
            this.YearTrackBarAngle.Maximum = 180;
            this.YearTrackBarAngle.Minimum = -180;
            this.YearTrackBarAngle.Name = "YearTrackBarAngle";
            this.YearTrackBarAngle.Size = new System.Drawing.Size(869, 45);
            this.YearTrackBarAngle.TabIndex = 39;
            this.YearTrackBarAngle.ValueChanged += new System.EventHandler(this.YearTrackBarAngle_ValueChanged);
            // 
            // PlotYear
            // 
            this.PlotYear.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.PlotYear.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.PlotYear.Legends.Add(legend1);
            this.PlotYear.Location = new System.Drawing.Point(3, 3);
            this.PlotYear.Name = "PlotYear";
            this.PlotYear.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.PlotYear.Series.Add(series1);
            this.PlotYear.Size = new System.Drawing.Size(973, 401);
            this.PlotYear.TabIndex = 38;
            this.PlotYear.Text = "PlotYear";
            // 
            // tabPageDecade
            // 
            this.tabPageDecade.Controls.Add(this.DecadeTrackBarAngle);
            this.tabPageDecade.Controls.Add(this.PlotDecade);
            this.tabPageDecade.Location = new System.Drawing.Point(4, 22);
            this.tabPageDecade.Name = "tabPageDecade";
            this.tabPageDecade.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDecade.Size = new System.Drawing.Size(976, 458);
            this.tabPageDecade.TabIndex = 2;
            this.tabPageDecade.Text = "Decade";
            this.tabPageDecade.UseVisualStyleBackColor = true;
            // 
            // DecadeTrackBarAngle
            // 
            this.DecadeTrackBarAngle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DecadeTrackBarAngle.Location = new System.Drawing.Point(8, 407);
            this.DecadeTrackBarAngle.Maximum = 180;
            this.DecadeTrackBarAngle.Minimum = -180;
            this.DecadeTrackBarAngle.Name = "DecadeTrackBarAngle";
            this.DecadeTrackBarAngle.Size = new System.Drawing.Size(962, 45);
            this.DecadeTrackBarAngle.TabIndex = 40;
            this.DecadeTrackBarAngle.ValueChanged += new System.EventHandler(this.DecadeTrackBarAngle_ValueChanged);
            // 
            // PlotDecade
            // 
            this.PlotDecade.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ChartArea1";
            this.PlotDecade.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.PlotDecade.Legends.Add(legend2);
            this.PlotDecade.Location = new System.Drawing.Point(3, 6);
            this.PlotDecade.Name = "PlotDecade";
            this.PlotDecade.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.PlotDecade.Series.Add(series2);
            this.PlotDecade.Size = new System.Drawing.Size(973, 401);
            this.PlotDecade.TabIndex = 39;
            this.PlotDecade.Text = "PlotDecade";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(984, 24);
            this.menuStrip.TabIndex = 36;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openGoodweCSVToolStripMenuItem,
            this.openOpenDTUCSVToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openGoodweCSVToolStripMenuItem
            // 
            this.openGoodweCSVToolStripMenuItem.Name = "openGoodweCSVToolStripMenuItem";
            this.openGoodweCSVToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openGoodweCSVToolStripMenuItem.Text = "Open Goodwe CSV";
            this.openGoodweCSVToolStripMenuItem.Click += new System.EventHandler(this.openGoodweCSVToolStripMenuItem_Click);
            // 
            // openOpenDTUCSVToolStripMenuItem
            // 
            this.openOpenDTUCSVToolStripMenuItem.Name = "openOpenDTUCSVToolStripMenuItem";
            this.openOpenDTUCSVToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openOpenDTUCSVToolStripMenuItem.Text = "Open OpenDTU CSV";
            this.openOpenDTUCSVToolStripMenuItem.Click += new System.EventHandler(this.openOpenDTUCSVToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.Exit);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 521);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(619, 22);
            this.statusStrip.TabIndex = 37;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(200, 17);
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.AutoSize = false;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(300, 17);
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 543);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "SolarPlot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormExit_Click);
            this.tabControl.ResumeLayout(false);
            this.tabPageDay.ResumeLayout(false);
            this.tabPageYear.ResumeLayout(false);
            this.tabPageYear.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.YearTrackBarAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlotYear)).EndInit();
            this.tabPageDecade.ResumeLayout(false);
            this.tabPageDecade.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DecadeTrackBarAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlotDecade)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ScottPlot.FormsPlot PlotDayGraph;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageDay;
        private System.Windows.Forms.TabPage tabPageYear;
        private System.Windows.Forms.TabPage tabPageDecade;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openGoodweCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart PlotYear;
        private System.Windows.Forms.TrackBar YearTrackBarAngle;
        private System.Windows.Forms.ComboBox YearComboBoxSelectYear;
        private System.Windows.Forms.TrackBar DecadeTrackBarAngle;
        private System.Windows.Forms.DataVisualization.Charting.Chart PlotDecade;
        private System.Windows.Forms.ToolStripMenuItem openOpenDTUCSVToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBoxDayPlotInverterSelection;
        private System.Windows.Forms.ComboBox comboBoxDayPlotLineSelection;
    }
}

