namespace SolarPlot
{
    partial class Form1
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
            this.MaxFormsPlot = new ScottPlot.FormsPlot();
            this.SuspendLayout();
            // 
            // MaxFormsPlot
            // 
            this.MaxFormsPlot.Location = new System.Drawing.Point(88, 64);
            this.MaxFormsPlot.Margin = new System.Windows.Forms.Padding(0);
            this.MaxFormsPlot.Name = "MaxFormsPlot";
            this.MaxFormsPlot.Size = new System.Drawing.Size(624, 322);
            this.MaxFormsPlot.TabIndex = 34;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MaxFormsPlot);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private ScottPlot.FormsPlot MaxFormsPlot;
    }
}

