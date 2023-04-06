﻿namespace SolarPlot
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
            this.PlotDay = new ScottPlot.FormsPlot();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageDay = new System.Windows.Forms.TabPage();
            this.DayCheckBoxTemp = new System.Windows.Forms.CheckBox();
            this.DayCheckBoxFac = new System.Windows.Forms.CheckBox();
            this.DayCheckBoxIpv = new System.Windows.Forms.CheckBox();
            this.DayCheckBoxVpv = new System.Windows.Forms.CheckBox();
            this.DayCheckBoxIac = new System.Windows.Forms.CheckBox();
            this.DayCheckBoxVac = new System.Windows.Forms.CheckBox();
            this.tabPageYear = new System.Windows.Forms.TabPage();
            this.tabPageDecade = new System.Windows.Forms.TabPage();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tabControl.SuspendLayout();
            this.tabPageDay.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // PlotDay
            // 
            this.PlotDay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlotDay.Location = new System.Drawing.Point(3, 3);
            this.PlotDay.Margin = new System.Windows.Forms.Padding(0);
            this.PlotDay.Name = "PlotDay";
            this.PlotDay.Size = new System.Drawing.Size(968, 419);
            this.PlotDay.TabIndex = 34;
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
            this.tabPageDay.Controls.Add(this.DayCheckBoxTemp);
            this.tabPageDay.Controls.Add(this.DayCheckBoxFac);
            this.tabPageDay.Controls.Add(this.DayCheckBoxIpv);
            this.tabPageDay.Controls.Add(this.DayCheckBoxVpv);
            this.tabPageDay.Controls.Add(this.DayCheckBoxIac);
            this.tabPageDay.Controls.Add(this.DayCheckBoxVac);
            this.tabPageDay.Controls.Add(this.PlotDay);
            this.tabPageDay.Location = new System.Drawing.Point(4, 22);
            this.tabPageDay.Name = "tabPageDay";
            this.tabPageDay.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDay.Size = new System.Drawing.Size(976, 458);
            this.tabPageDay.TabIndex = 0;
            this.tabPageDay.Text = "Day";
            this.tabPageDay.UseVisualStyleBackColor = true;
            // 
            // DayCheckBoxTemp
            // 
            this.DayCheckBoxTemp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DayCheckBoxTemp.Location = new System.Drawing.Point(251, 431);
            this.DayCheckBoxTemp.Name = "DayCheckBoxTemp";
            this.DayCheckBoxTemp.Size = new System.Drawing.Size(104, 24);
            this.DayCheckBoxTemp.TabIndex = 40;
            this.DayCheckBoxTemp.Text = "Temp";
            this.DayCheckBoxTemp.UseVisualStyleBackColor = true;
            this.DayCheckBoxTemp.CheckedChanged += new System.EventHandler(this.ProcessDayPlotSelectionChanged);
            // 
            // DayCheckBoxFac
            // 
            this.DayCheckBoxFac.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DayCheckBoxFac.AutoSize = true;
            this.DayCheckBoxFac.Location = new System.Drawing.Point(201, 435);
            this.DayCheckBoxFac.Name = "DayCheckBoxFac";
            this.DayCheckBoxFac.Size = new System.Drawing.Size(44, 17);
            this.DayCheckBoxFac.TabIndex = 39;
            this.DayCheckBoxFac.Text = "Fac";
            this.DayCheckBoxFac.UseVisualStyleBackColor = true;
            this.DayCheckBoxFac.CheckedChanged += new System.EventHandler(this.ProcessDayPlotSelectionChanged);
            // 
            // DayCheckBoxIpv
            // 
            this.DayCheckBoxIpv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DayCheckBoxIpv.AutoSize = true;
            this.DayCheckBoxIpv.Location = new System.Drawing.Point(62, 435);
            this.DayCheckBoxIpv.Name = "DayCheckBoxIpv";
            this.DayCheckBoxIpv.Size = new System.Drawing.Size(41, 17);
            this.DayCheckBoxIpv.TabIndex = 38;
            this.DayCheckBoxIpv.Text = "Ipv";
            this.DayCheckBoxIpv.UseVisualStyleBackColor = true;
            this.DayCheckBoxIpv.CheckedChanged += new System.EventHandler(this.ProcessDayPlotSelectionChanged);
            // 
            // DayCheckBoxVpv
            // 
            this.DayCheckBoxVpv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DayCheckBoxVpv.AutoSize = true;
            this.DayCheckBoxVpv.Location = new System.Drawing.Point(11, 435);
            this.DayCheckBoxVpv.Name = "DayCheckBoxVpv";
            this.DayCheckBoxVpv.Size = new System.Drawing.Size(45, 17);
            this.DayCheckBoxVpv.TabIndex = 37;
            this.DayCheckBoxVpv.Text = "Vpv";
            this.DayCheckBoxVpv.UseVisualStyleBackColor = true;
            this.DayCheckBoxVpv.CheckedChanged += new System.EventHandler(this.ProcessDayPlotSelectionChanged);
            // 
            // DayCheckBoxIac
            // 
            this.DayCheckBoxIac.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DayCheckBoxIac.AutoSize = true;
            this.DayCheckBoxIac.Location = new System.Drawing.Point(154, 435);
            this.DayCheckBoxIac.Name = "DayCheckBoxIac";
            this.DayCheckBoxIac.Size = new System.Drawing.Size(41, 17);
            this.DayCheckBoxIac.TabIndex = 36;
            this.DayCheckBoxIac.Text = "Iac";
            this.DayCheckBoxIac.UseVisualStyleBackColor = true;
            this.DayCheckBoxIac.CheckedChanged += new System.EventHandler(this.ProcessDayPlotSelectionChanged);
            // 
            // DayCheckBoxVac
            // 
            this.DayCheckBoxVac.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DayCheckBoxVac.AutoSize = true;
            this.DayCheckBoxVac.Location = new System.Drawing.Point(103, 435);
            this.DayCheckBoxVac.Name = "DayCheckBoxVac";
            this.DayCheckBoxVac.Size = new System.Drawing.Size(45, 17);
            this.DayCheckBoxVac.TabIndex = 35;
            this.DayCheckBoxVac.Text = "Vac";
            this.DayCheckBoxVac.UseVisualStyleBackColor = true;
            this.DayCheckBoxVac.CheckedChanged += new System.EventHandler(this.ProcessDayPlotSelectionChanged);
            // 
            // tabPageYear
            // 
            this.tabPageYear.Location = new System.Drawing.Point(4, 22);
            this.tabPageYear.Name = "tabPageYear";
            this.tabPageYear.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageYear.Size = new System.Drawing.Size(976, 458);
            this.tabPageYear.TabIndex = 1;
            this.tabPageYear.Text = "Year";
            this.tabPageYear.UseVisualStyleBackColor = true;
            // 
            // tabPageDecade
            // 
            this.tabPageDecade.Location = new System.Drawing.Point(4, 22);
            this.tabPageDecade.Name = "tabPageDecade";
            this.tabPageDecade.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDecade.Size = new System.Drawing.Size(976, 458);
            this.tabPageDecade.TabIndex = 2;
            this.tabPageDecade.Text = "Decade";
            this.tabPageDecade.UseVisualStyleBackColor = true;
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
            this.openCSVToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openCSVToolStripMenuItem
            // 
            this.openCSVToolStripMenuItem.Name = "openCSVToolStripMenuItem";
            this.openCSVToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.openCSVToolStripMenuItem.Text = "Open CSV";
            this.openCSVToolStripMenuItem.Click += new System.EventHandler(this.openCSVToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
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
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormExit_Click);
            this.tabControl.ResumeLayout(false);
            this.tabPageDay.ResumeLayout(false);
            this.tabPageDay.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ScottPlot.FormsPlot PlotDay;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageDay;
        private System.Windows.Forms.TabPage tabPageYear;
        private System.Windows.Forms.TabPage tabPageDecade;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.CheckBox DayCheckBoxVac;
        private System.Windows.Forms.CheckBox DayCheckBoxIpv;
        private System.Windows.Forms.CheckBox DayCheckBoxVpv;
        private System.Windows.Forms.CheckBox DayCheckBoxIac;
        private System.Windows.Forms.CheckBox DayCheckBoxTemp;
        private System.Windows.Forms.CheckBox DayCheckBoxFac;
    }
}

