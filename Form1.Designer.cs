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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mousePosLbl = new System.Windows.Forms.Label();
            this.testBtn = new System.Windows.Forms.Button();
            this.relCheckBox = new System.Windows.Forms.CheckBox();
            this.clearBtn = new System.Windows.Forms.Button();
            this.plotBtn = new System.Windows.Forms.Button();
            this.fileLbl = new System.Windows.Forms.Label();
            this.chooseFileBtn = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelTrackBar = new System.Windows.Forms.Panel();
            this.totalLbl = new System.Windows.Forms.Label();
            this.indexLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panelTrackBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBar1
            // 
            this.trackBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar1.LargeChange = 1;
            this.trackBar1.Location = new System.Drawing.Point(0, 31);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(2);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(63, 521);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar1.Scroll += new System.EventHandler(this.TrackBar_Scroll);
            this.trackBar1.ValueChanged += new System.EventHandler(this.TrackBar_Scroll);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.mousePosLbl);
            this.panel1.Controls.Add(this.testBtn);
            this.panel1.Controls.Add(this.relCheckBox);
            this.panel1.Controls.Add(this.clearBtn);
            this.panel1.Controls.Add(this.plotBtn);
            this.panel1.Controls.Add(this.fileLbl);
            this.panel1.Controls.Add(this.chooseFileBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 552);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1082, 30);
            this.panel1.TabIndex = 2;
            // 
            // mousePosLbl
            // 
            this.mousePosLbl.AutoSize = true;
            this.mousePosLbl.Dock = System.Windows.Forms.DockStyle.Right;
            this.mousePosLbl.Location = new System.Drawing.Point(710, 0);
            this.mousePosLbl.Name = "mousePosLbl";
            this.mousePosLbl.Size = new System.Drawing.Size(0, 13);
            this.mousePosLbl.TabIndex = 6;
            // 
            // testBtn
            // 
            this.testBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.testBtn.Location = new System.Drawing.Point(710, 0);
            this.testBtn.Name = "testBtn";
            this.testBtn.Size = new System.Drawing.Size(75, 28);
            this.testBtn.TabIndex = 5;
            this.testBtn.Text = "Test";
            this.testBtn.UseVisualStyleBackColor = true;
            this.testBtn.Click += new System.EventHandler(this.TestBtn_Click);
            // 
            // relCheckBox
            // 
            this.relCheckBox.AutoSize = true;
            this.relCheckBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.relCheckBox.Location = new System.Drawing.Point(785, 0);
            this.relCheckBox.Name = "relCheckBox";
            this.relCheckBox.Size = new System.Drawing.Size(140, 28);
            this.relCheckBox.TabIndex = 4;
            this.relCheckBox.Text = "Thickness relative scale";
            this.relCheckBox.UseVisualStyleBackColor = true;
            this.relCheckBox.Click += new System.EventHandler(this.RelCheckBox_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.clearBtn.Location = new System.Drawing.Point(925, 0);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(75, 28);
            this.clearBtn.TabIndex = 3;
            this.clearBtn.Text = "Clear";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // plotBtn
            // 
            this.plotBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.plotBtn.Location = new System.Drawing.Point(1000, 0);
            this.plotBtn.Margin = new System.Windows.Forms.Padding(2);
            this.plotBtn.Name = "plotBtn";
            this.plotBtn.Size = new System.Drawing.Size(80, 28);
            this.plotBtn.TabIndex = 2;
            this.plotBtn.Text = "Plot";
            this.plotBtn.UseVisualStyleBackColor = true;
            this.plotBtn.Click += new System.EventHandler(this.PlotBtn_Click);
            // 
            // fileLbl
            // 
            this.fileLbl.AutoSize = true;
            this.fileLbl.Location = new System.Drawing.Point(93, 8);
            this.fileLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.fileLbl.Name = "fileLbl";
            this.fileLbl.Size = new System.Drawing.Size(0, 13);
            this.fileLbl.TabIndex = 1;
            // 
            // chooseFileBtn
            // 
            this.chooseFileBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.chooseFileBtn.Location = new System.Drawing.Point(0, 0);
            this.chooseFileBtn.Margin = new System.Windows.Forms.Padding(2);
            this.chooseFileBtn.Name = "chooseFileBtn";
            this.chooseFileBtn.Size = new System.Drawing.Size(88, 28);
            this.chooseFileBtn.TabIndex = 0;
            this.chooseFileBtn.Text = "Choose File";
            this.chooseFileBtn.UseVisualStyleBackColor = true;
            this.chooseFileBtn.Click += new System.EventHandler(this.ChooseFileBtn_Click);
            // 
            // chart1
            // 
            chartArea1.AxisX.InterlacedColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisX.MajorTickMark.Size = 0F;
            chartArea1.AxisY.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisY.LineWidth = 0;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisY.MajorTickMark.Enabled = false;
            chartArea1.AxisY.MajorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.AcrossAxis;
            chartArea1.AxisY2.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Margin = new System.Windows.Forms.Padding(2);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(1082, 552);
            this.chart1.TabIndex = 3;
            this.chart1.Text = "chart1";
            this.chart1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panelTrackBar);
            this.panel2.Controls.Add(this.chart1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1082, 552);
            this.panel2.TabIndex = 4;
            // 
            // panelTrackBar
            // 
            this.panelTrackBar.Controls.Add(this.trackBar1);
            this.panelTrackBar.Controls.Add(this.totalLbl);
            this.panelTrackBar.Controls.Add(this.indexLbl);
            this.panelTrackBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelTrackBar.Location = new System.Drawing.Point(1019, 0);
            this.panelTrackBar.Name = "panelTrackBar";
            this.panelTrackBar.Size = new System.Drawing.Size(63, 552);
            this.panelTrackBar.TabIndex = 4;
            this.panelTrackBar.Visible = false;
            // 
            // totalLbl
            // 
            this.totalLbl.AutoSize = true;
            this.totalLbl.Dock = System.Windows.Forms.DockStyle.Top;
            this.totalLbl.Location = new System.Drawing.Point(0, 18);
            this.totalLbl.Name = "totalLbl";
            this.totalLbl.Size = new System.Drawing.Size(0, 13);
            this.totalLbl.TabIndex = 3;
            // 
            // indexLbl
            // 
            this.indexLbl.Dock = System.Windows.Forms.DockStyle.Top;
            this.indexLbl.Location = new System.Drawing.Point(0, 0);
            this.indexLbl.Name = "indexLbl";
            this.indexLbl.Size = new System.Drawing.Size(63, 18);
            this.indexLbl.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 582);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Cross Section Plot";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panelTrackBar.ResumeLayout(false);
            this.panelTrackBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button chooseFileBtn;
        private System.Windows.Forms.Label fileLbl;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button plotBtn;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox relCheckBox;
        private System.Windows.Forms.Button testBtn;
        private System.Windows.Forms.Panel panelTrackBar;
        private System.Windows.Forms.Label indexLbl;
        private System.Windows.Forms.Label totalLbl;
        private System.Windows.Forms.Label mousePosLbl;
    }
}

