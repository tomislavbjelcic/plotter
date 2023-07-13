namespace WindowsFormsApp1
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint6 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.fileLbl = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelTrackBar = new System.Windows.Forms.Panel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.hmPlot = new ScottPlot.FormsPlot();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.indexPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lengthLbl = new System.Windows.Forms.Label();
            this.offsetLbl = new System.Windows.Forms.Label();
            this.totalLbl = new System.Windows.Forms.Label();
            this.indexLbl = new System.Windows.Forms.Label();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.menuStripPanel = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseFileBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.clearFilenameBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.plotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plotBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.clearBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.scaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cbRelCheckBox = new System.Windows.Forms.ToolStripMenuItem();
            this.relCheckBox = new System.Windows.Forms.ToolStripMenuItem();
            this.testBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.bottomPanel.SuspendLayout();
            this.panelTrackBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.indexPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.menuStripPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bottomPanel
            // 
            this.bottomPanel.AutoSize = true;
            this.bottomPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bottomPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.bottomPanel.Controls.Add(this.fileLbl);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 576);
            this.bottomPanel.Margin = new System.Windows.Forms.Padding(2);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(1151, 17);
            this.bottomPanel.TabIndex = 2;
            // 
            // fileLbl
            // 
            this.fileLbl.AutoSize = true;
            this.fileLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileLbl.Location = new System.Drawing.Point(0, 0);
            this.fileLbl.Name = "fileLbl";
            this.fileLbl.Size = new System.Drawing.Size(0, 13);
            this.fileLbl.TabIndex = 7;
            this.fileLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fileLbl.TextChanged += new System.EventHandler(this.FilenameLbl_TextChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // panelTrackBar
            // 
            this.panelTrackBar.Controls.Add(this.trackBar1);
            this.panelTrackBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelTrackBar.Location = new System.Drawing.Point(1086, 0);
            this.panelTrackBar.Name = "panelTrackBar";
            this.panelTrackBar.Size = new System.Drawing.Size(65, 552);
            this.panelTrackBar.TabIndex = 4;
            this.panelTrackBar.Visible = false;
            // 
            // trackBar1
            // 
            this.trackBar1.BackColor = System.Drawing.SystemColors.Info;
            this.trackBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar1.LargeChange = 1;
            this.trackBar1.Location = new System.Drawing.Point(0, 0);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(2);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(65, 552);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar1.Scroll += new System.EventHandler(this.TrackBar_Scroll);
            this.trackBar1.ValueChanged += new System.EventHandler(this.TrackBar_Scroll);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.hmPlot);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chart1);
            this.splitContainer1.Panel2.Controls.Add(this.indexPanel);
            this.splitContainer1.Size = new System.Drawing.Size(1086, 552);
            this.splitContainer1.SplitterDistance = 520;
            this.splitContainer1.TabIndex = 5;
            // 
            // hmPlot
            // 
            this.hmPlot.AutoSize = true;
            this.hmPlot.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.hmPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hmPlot.Location = new System.Drawing.Point(0, 0);
            this.hmPlot.Name = "hmPlot";
            this.hmPlot.Size = new System.Drawing.Size(520, 552);
            this.hmPlot.TabIndex = 0;
            // 
            // chart1
            // 
            chartArea6.AxisX.InterlacedColor = System.Drawing.Color.Gray;
            chartArea6.AxisX.LabelStyle.Format = "{0}°";
            chartArea6.AxisX.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea6.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea6.AxisX.MajorTickMark.Size = 0F;
            chartArea6.AxisY.LabelStyle.Enabled = false;
            chartArea6.AxisY.LineColor = System.Drawing.Color.Gainsboro;
            chartArea6.AxisY.LineWidth = 0;
            chartArea6.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea6.AxisY.MajorTickMark.Enabled = false;
            chartArea6.AxisY.MajorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.AcrossAxis;
            chartArea6.AxisY2.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea6.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea6);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Margin = new System.Windows.Forms.Padding(2);
            this.chart1.Name = "chart1";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Polar;
            series6.Name = "emptySeries";
            dataPoint6.IsEmpty = true;
            series6.Points.Add(dataPoint6);
            this.chart1.Series.Add(series6);
            this.chart1.Size = new System.Drawing.Size(562, 501);
            this.chart1.TabIndex = 3;
            this.chart1.Text = "chart1";
            // 
            // indexPanel
            // 
            this.indexPanel.ColumnCount = 2;
            this.indexPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.indexPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.indexPanel.Controls.Add(this.lengthLbl, 0, 1);
            this.indexPanel.Controls.Add(this.offsetLbl, 0, 0);
            this.indexPanel.Controls.Add(this.totalLbl, 1, 1);
            this.indexPanel.Controls.Add(this.indexLbl, 1, 0);
            this.indexPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.indexPanel.Location = new System.Drawing.Point(0, 501);
            this.indexPanel.Name = "indexPanel";
            this.indexPanel.RowCount = 2;
            this.indexPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.indexPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.indexPanel.Size = new System.Drawing.Size(562, 51);
            this.indexPanel.TabIndex = 1;
            this.indexPanel.Visible = false;
            // 
            // lengthLbl
            // 
            this.lengthLbl.AutoSize = true;
            this.lengthLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lengthLbl.Location = new System.Drawing.Point(3, 25);
            this.lengthLbl.Name = "lengthLbl";
            this.lengthLbl.Size = new System.Drawing.Size(275, 26);
            this.lengthLbl.TabIndex = 5;
            this.lengthLbl.Text = "lengthLbl";
            this.lengthLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // offsetLbl
            // 
            this.offsetLbl.AutoSize = true;
            this.offsetLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.offsetLbl.Location = new System.Drawing.Point(3, 0);
            this.offsetLbl.Name = "offsetLbl";
            this.offsetLbl.Size = new System.Drawing.Size(275, 25);
            this.offsetLbl.TabIndex = 4;
            this.offsetLbl.Text = "offsetLbl";
            this.offsetLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // totalLbl
            // 
            this.totalLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalLbl.Location = new System.Drawing.Point(284, 25);
            this.totalLbl.Name = "totalLbl";
            this.totalLbl.Size = new System.Drawing.Size(275, 26);
            this.totalLbl.TabIndex = 3;
            this.totalLbl.Text = "totalLbl";
            this.totalLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // indexLbl
            // 
            this.indexLbl.AutoSize = true;
            this.indexLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.indexLbl.Location = new System.Drawing.Point(284, 0);
            this.indexLbl.Name = "indexLbl";
            this.indexLbl.Size = new System.Drawing.Size(275, 25);
            this.indexLbl.TabIndex = 2;
            this.indexLbl.Text = "indexLbl";
            this.indexLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // contentPanel
            // 
            this.contentPanel.Controls.Add(this.splitContainer1);
            this.contentPanel.Controls.Add(this.panelTrackBar);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 24);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(1151, 552);
            this.contentPanel.TabIndex = 4;
            // 
            // menuStripPanel
            // 
            this.menuStripPanel.AutoSize = true;
            this.menuStripPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.menuStripPanel.Controls.Add(this.menuStrip1);
            this.menuStripPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuStripPanel.Location = new System.Drawing.Point(0, 0);
            this.menuStripPanel.Name = "menuStripPanel";
            this.menuStripPanel.Size = new System.Drawing.Size(1151, 24);
            this.menuStripPanel.TabIndex = 6;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.plotToolStripMenuItem,
            this.scaleToolStripMenuItem,
            this.testBtn});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1151, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chooseFileBtn,
            this.clearFilenameBtn});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // chooseFileBtn
            // 
            this.chooseFileBtn.Name = "chooseFileBtn";
            this.chooseFileBtn.ShortcutKeyDisplayString = "Ctrl+O";
            this.chooseFileBtn.Size = new System.Drawing.Size(186, 22);
            this.chooseFileBtn.Text = "Choose";
            this.chooseFileBtn.Click += new System.EventHandler(this.ChooseFileBtn_Click);
            // 
            // clearFilenameBtn
            // 
            this.clearFilenameBtn.Name = "clearFilenameBtn";
            this.clearFilenameBtn.ShortcutKeyDisplayString = "Ctrl+F";
            this.clearFilenameBtn.Size = new System.Drawing.Size(192, 22);
            this.clearFilenameBtn.Text = "Clear Selection";
            this.clearFilenameBtn.Click += new System.EventHandler(this.ClearFilenameBtn_Click);
            // 
            // plotToolStripMenuItem
            // 
            this.plotToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plotBtn,
            this.clearBtn});
            this.plotToolStripMenuItem.Name = "plotToolStripMenuItem";
            this.plotToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.plotToolStripMenuItem.Text = "Plot";
            // 
            // plotBtn
            // 
            this.plotBtn.Name = "plotBtn";
            this.plotBtn.ShortcutKeyDisplayString = "Ctrl+P";
            this.plotBtn.Size = new System.Drawing.Size(180, 22);
            this.plotBtn.Text = "Plot Draw";
            this.plotBtn.Click += new System.EventHandler(this.PlotBtn_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.ShortcutKeyDisplayString = "Ctrl+G";
            this.clearBtn.Size = new System.Drawing.Size(180, 22);
            this.clearBtn.Text = "Clear Plot";
            this.clearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // scaleToolStripMenuItem
            // 
            this.scaleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbRelCheckBox,
            this.relCheckBox});
            this.scaleToolStripMenuItem.Name = "scaleToolStripMenuItem";
            this.scaleToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.scaleToolStripMenuItem.Text = "Scale";
            // 
            // cbRelCheckBox
            // 
            this.cbRelCheckBox.CheckOnClick = true;
            this.cbRelCheckBox.Name = "cbRelCheckBox";
            this.cbRelCheckBox.ShortcutKeyDisplayString = "Ctrl+E";
            this.cbRelCheckBox.Size = new System.Drawing.Size(272, 22);
            this.cbRelCheckBox.Text = "Colorbar Thickness Relative";
            this.cbRelCheckBox.Click += new System.EventHandler(this.CbRelCheckBox_Click);
            // 
            // relCheckBox
            // 
            this.relCheckBox.CheckOnClick = true;
            this.relCheckBox.Name = "relCheckBox";
            this.relCheckBox.ShortcutKeyDisplayString = "Ctrl+R";
            this.relCheckBox.Size = new System.Drawing.Size(272, 22);
            this.relCheckBox.Text = "Polar Chart Thickness Relative";
            this.relCheckBox.Click += new System.EventHandler(this.RelCheckBox_Click);
            // 
            // testBtn
            // 
            this.testBtn.Name = "testBtn";
            this.testBtn.Size = new System.Drawing.Size(39, 20);
            this.testBtn.Text = "Test";
            this.testBtn.Visible = false;
            this.testBtn.Click += new System.EventHandler(this.TestBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 593);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.menuStripPanel);
            this.Controls.Add(this.bottomPanel);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Cross Section Plot";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            this.panelTrackBar.ResumeLayout(false);
            this.panelTrackBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.indexPanel.ResumeLayout(false);
            this.indexPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.menuStripPanel.ResumeLayout(false);
            this.menuStripPanel.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Label fileLbl;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panelTrackBar;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ScottPlot.FormsPlot hmPlot;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TableLayoutPanel indexPanel;
        private System.Windows.Forms.Label lengthLbl;
        private System.Windows.Forms.Label offsetLbl;
        private System.Windows.Forms.Label totalLbl;
        private System.Windows.Forms.Label indexLbl;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Panel menuStripPanel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem plotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chooseFileBtn;
        private System.Windows.Forms.ToolStripMenuItem clearFilenameBtn;
        private System.Windows.Forms.ToolStripMenuItem plotBtn;
        private System.Windows.Forms.ToolStripMenuItem clearBtn;
        private System.Windows.Forms.ToolStripMenuItem cbRelCheckBox;
        private System.Windows.Forms.ToolStripMenuItem relCheckBox;
        private System.Windows.Forms.ToolStripMenuItem testBtn;
    }
}

