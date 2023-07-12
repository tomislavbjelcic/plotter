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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fileLblPanel = new System.Windows.Forms.Panel();
            this.cbRelCheckBox = new System.Windows.Forms.CheckBox();
            this.fileLbl = new System.Windows.Forms.Label();
            this.testBtn = new System.Windows.Forms.Button();
            this.clearFilenameBtn = new System.Windows.Forms.Button();
            this.relCheckBox = new System.Windows.Forms.CheckBox();
            this.chooseFileBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.plotBtn = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.hmPlot = new ScottPlot.FormsPlot();
            this.indexPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lengthLbl = new System.Windows.Forms.Label();
            this.offsetLbl = new System.Windows.Forms.Label();
            this.totalLbl = new System.Windows.Forms.Label();
            this.indexLbl = new System.Windows.Forms.Label();
            this.panelTrackBar = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panel1.SuspendLayout();
            this.fileLblPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.indexPanel.SuspendLayout();
            this.panelTrackBar.SuspendLayout();
            this.SuspendLayout();
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
            this.trackBar1.Size = new System.Drawing.Size(65, 536);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar1.Scroll += new System.EventHandler(this.TrackBar_Scroll);
            this.trackBar1.ValueChanged += new System.EventHandler(this.TrackBar_Scroll);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.fileLblPanel);
            this.panel1.Controls.Add(this.testBtn);
            this.panel1.Controls.Add(this.clearFilenameBtn);
            this.panel1.Controls.Add(this.relCheckBox);
            this.panel1.Controls.Add(this.chooseFileBtn);
            this.panel1.Controls.Add(this.clearBtn);
            this.panel1.Controls.Add(this.plotBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 536);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1151, 57);
            this.panel1.TabIndex = 2;
            // 
            // fileLblPanel
            // 
            this.fileLblPanel.Controls.Add(this.cbRelCheckBox);
            this.fileLblPanel.Controls.Add(this.fileLbl);
            this.fileLblPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileLblPanel.Location = new System.Drawing.Point(160, 0);
            this.fileLblPanel.Name = "fileLblPanel";
            this.fileLblPanel.Size = new System.Drawing.Size(654, 55);
            this.fileLblPanel.TabIndex = 9;
            // 
            // cbRelCheckBox
            // 
            this.cbRelCheckBox.AutoSize = true;
            this.cbRelCheckBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbRelCheckBox.Enabled = false;
            this.cbRelCheckBox.Location = new System.Drawing.Point(465, 0);
            this.cbRelCheckBox.Name = "cbRelCheckBox";
            this.cbRelCheckBox.Size = new System.Drawing.Size(189, 55);
            this.cbRelCheckBox.TabIndex = 10;
            this.cbRelCheckBox.Text = "Colorbar Thickness Relative Scale";
            this.cbRelCheckBox.UseVisualStyleBackColor = true;
            this.cbRelCheckBox.Click += new System.EventHandler(this.CbRelCheckBox_Click);
            // 
            // fileLbl
            // 
            this.fileLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileLbl.Location = new System.Drawing.Point(0, 0);
            this.fileLbl.Name = "fileLbl";
            this.fileLbl.Size = new System.Drawing.Size(654, 55);
            this.fileLbl.TabIndex = 7;
            this.fileLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fileLbl.TextChanged += new System.EventHandler(this.FilenameLbl_TextChanged);
            // 
            // testBtn
            // 
            this.testBtn.AutoSize = true;
            this.testBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.testBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.testBtn.Location = new System.Drawing.Point(814, 0);
            this.testBtn.Name = "testBtn";
            this.testBtn.Size = new System.Drawing.Size(38, 55);
            this.testBtn.TabIndex = 8;
            this.testBtn.Text = "Test";
            this.testBtn.UseVisualStyleBackColor = true;
            this.testBtn.Visible = false;
            this.testBtn.Click += new System.EventHandler(this.TestBtn_Click);
            // 
            // clearFilenameBtn
            // 
            this.clearFilenameBtn.AutoSize = true;
            this.clearFilenameBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.clearFilenameBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.clearFilenameBtn.Location = new System.Drawing.Point(72, 0);
            this.clearFilenameBtn.Name = "clearFilenameBtn";
            this.clearFilenameBtn.Size = new System.Drawing.Size(88, 55);
            this.clearFilenameBtn.TabIndex = 5;
            this.clearFilenameBtn.Text = "Clear Selection";
            this.clearFilenameBtn.UseVisualStyleBackColor = true;
            this.clearFilenameBtn.Click += new System.EventHandler(this.ClearFilenameBtn_Click);
            // 
            // relCheckBox
            // 
            this.relCheckBox.AutoSize = true;
            this.relCheckBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.relCheckBox.Enabled = false;
            this.relCheckBox.Location = new System.Drawing.Point(852, 0);
            this.relCheckBox.Name = "relCheckBox";
            this.relCheckBox.Size = new System.Drawing.Size(200, 55);
            this.relCheckBox.TabIndex = 4;
            this.relCheckBox.Text = "Polar Chart Thickness Relative scale";
            this.relCheckBox.UseVisualStyleBackColor = true;
            this.relCheckBox.Click += new System.EventHandler(this.RelCheckBox_Click);
            // 
            // chooseFileBtn
            // 
            this.chooseFileBtn.AutoSize = true;
            this.chooseFileBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chooseFileBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.chooseFileBtn.Location = new System.Drawing.Point(0, 0);
            this.chooseFileBtn.Margin = new System.Windows.Forms.Padding(2);
            this.chooseFileBtn.Name = "chooseFileBtn";
            this.chooseFileBtn.Size = new System.Drawing.Size(72, 55);
            this.chooseFileBtn.TabIndex = 6;
            this.chooseFileBtn.Text = "Choose File";
            this.chooseFileBtn.UseVisualStyleBackColor = true;
            this.chooseFileBtn.Click += new System.EventHandler(this.ChooseFileBtn_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.AutoSize = true;
            this.clearBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.clearBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.clearBtn.Enabled = false;
            this.clearBtn.Location = new System.Drawing.Point(1052, 0);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(62, 55);
            this.clearBtn.TabIndex = 3;
            this.clearBtn.Text = "Clear Plot";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // plotBtn
            // 
            this.plotBtn.AutoSize = true;
            this.plotBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.plotBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.plotBtn.Enabled = false;
            this.plotBtn.Location = new System.Drawing.Point(1114, 0);
            this.plotBtn.Margin = new System.Windows.Forms.Padding(2);
            this.plotBtn.Name = "plotBtn";
            this.plotBtn.Size = new System.Drawing.Size(35, 55);
            this.plotBtn.TabIndex = 2;
            this.plotBtn.Text = "Plot";
            this.plotBtn.UseVisualStyleBackColor = true;
            this.plotBtn.Click += new System.EventHandler(this.PlotBtn_Click);
            // 
            // chart1
            // 
            chartArea3.AxisX.InterlacedColor = System.Drawing.Color.Gray;
            chartArea3.AxisX.LabelStyle.Format = "{0}°";
            chartArea3.AxisX.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea3.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea3.AxisX.MajorTickMark.Size = 0F;
            chartArea3.AxisY.LabelStyle.Enabled = false;
            chartArea3.AxisY.LineColor = System.Drawing.Color.Gainsboro;
            chartArea3.AxisY.LineWidth = 0;
            chartArea3.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea3.AxisY.MajorTickMark.Enabled = false;
            chartArea3.AxisY.MajorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.AcrossAxis;
            chartArea3.AxisY2.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea3.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea3);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Margin = new System.Windows.Forms.Padding(2);
            this.chart1.Name = "chart1";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Polar;
            series3.Name = "emptySeries";
            dataPoint3.IsEmpty = true;
            series3.Points.Add(dataPoint3);
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(561, 485);
            this.chart1.TabIndex = 3;
            this.chart1.Text = "chart1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.splitContainer1);
            this.panel2.Controls.Add(this.panelTrackBar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1151, 536);
            this.panel2.TabIndex = 4;
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
            this.splitContainer1.Size = new System.Drawing.Size(1086, 536);
            this.splitContainer1.SplitterDistance = 521;
            this.splitContainer1.TabIndex = 5;
            // 
            // hmPlot
            // 
            this.hmPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hmPlot.Location = new System.Drawing.Point(0, 0);
            this.hmPlot.Name = "hmPlot";
            this.hmPlot.Size = new System.Drawing.Size(521, 536);
            this.hmPlot.TabIndex = 0;
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
            this.indexPanel.Location = new System.Drawing.Point(0, 485);
            this.indexPanel.Name = "indexPanel";
            this.indexPanel.RowCount = 2;
            this.indexPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.indexPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.indexPanel.Size = new System.Drawing.Size(561, 51);
            this.indexPanel.TabIndex = 1;
            this.indexPanel.Visible = false;
            // 
            // lengthLbl
            // 
            this.lengthLbl.AutoSize = true;
            this.lengthLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lengthLbl.Location = new System.Drawing.Point(3, 25);
            this.lengthLbl.Name = "lengthLbl";
            this.lengthLbl.Size = new System.Drawing.Size(274, 26);
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
            this.offsetLbl.Size = new System.Drawing.Size(274, 25);
            this.offsetLbl.TabIndex = 4;
            this.offsetLbl.Text = "offsetLbl";
            this.offsetLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // totalLbl
            // 
            this.totalLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalLbl.Location = new System.Drawing.Point(283, 25);
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
            this.indexLbl.Location = new System.Drawing.Point(283, 0);
            this.indexLbl.Name = "indexLbl";
            this.indexLbl.Size = new System.Drawing.Size(275, 25);
            this.indexLbl.TabIndex = 2;
            this.indexLbl.Text = "indexLbl";
            this.indexLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelTrackBar
            // 
            this.panelTrackBar.Controls.Add(this.trackBar1);
            this.panelTrackBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelTrackBar.Location = new System.Drawing.Point(1086, 0);
            this.panelTrackBar.Name = "panelTrackBar";
            this.panelTrackBar.Size = new System.Drawing.Size(65, 536);
            this.panelTrackBar.TabIndex = 4;
            this.panelTrackBar.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 593);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Cross Section Plot";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.fileLblPanel.ResumeLayout(false);
            this.fileLblPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.indexPanel.ResumeLayout(false);
            this.indexPanel.PerformLayout();
            this.panelTrackBar.ResumeLayout(false);
            this.panelTrackBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button plotBtn;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox relCheckBox;
        private System.Windows.Forms.Panel panelTrackBar;
        private System.Windows.Forms.Label indexLbl;
        private System.Windows.Forms.Label totalLbl;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ScottPlot.FormsPlot hmPlot;
        private System.Windows.Forms.Label lengthLbl;
        private System.Windows.Forms.Label offsetLbl;
        private System.Windows.Forms.Button clearFilenameBtn;
        private System.Windows.Forms.Button chooseFileBtn;
        private System.Windows.Forms.Label fileLbl;
        private System.Windows.Forms.Button testBtn;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel indexPanel;
        private System.Windows.Forms.Panel fileLblPanel;
        private System.Windows.Forms.CheckBox cbRelCheckBox;
    }
}

