using ScottPlot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static WindowsFormsApp1.Util;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private static readonly ScottPlot.Drawing.Colormap CMAP 
            = new ScottPlot.Drawing.Colormap(ReverseIColormap(new ScottPlot.Drawing.Colormaps.Turbo()));
        private const string MEASURING_UNIT_RADIUS = "mm";
        private const string MEASURING_UNIT_ANGLE = "°";

        private string fileName = null;
        private Series innerSeries = null;
        private Series outerSeries = null;
        private Series scatterSeries = null;
        private Series odSeries = null;
        private List<double[]> radiuses = null;
        private double[] angles = null;
        private List<double[]> radiusesInterpolated = null;
        private double[] anglesInterpolated = null;
        private double innerDiameter = 0.0;
        private double outerDiameter = 0.0;
        private double spacing = 0.0;
        private ScottPlot.Plottable.Heatmap hm = null;
        private ScottPlot.Plottable.HLine hline = null;

        public Form1()
        {
            InitializeComponent();
            InitializeHeatmapPlot();


            trackBar1.MouseWheel += (sender, e) =>
            {
                ((HandledMouseEventArgs)e).Handled = true;
            };
            trackBar1.MouseWheel += TrackBar1_MouseWheel;
            chart1.MouseWheel += TrackBar1_MouseWheel;
        }

        
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            this.mousePosLbl.Text = $"x={e.X} y={e.Y}";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        

        private void ChooseFileBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.fileName = openFileDialog1.FileName;
                fileLbl.Text = this.fileName;
            }
        }

        private void TrackBar1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!trackBar1.Visible) { return; }
            // Increment or decrement the TrackBar value by one tick
            int finalValue = trackBar1.Value + Math.Sign(e.Delta);

            // Prevent the TrackBar from scrolling beyond its minimum and maximum values
            if (finalValue < trackBar1.Minimum)
                finalValue = trackBar1.Minimum;
            else if (finalValue > trackBar1.Maximum)
                finalValue = trackBar1.Maximum;


            trackBar1.Value = finalValue;
        }

        private void TrackBar_Scroll(object sender, EventArgs e)
        {
            PlotChange(trackBar1.Value);
        }


        private void PlotBtn_Click(object sender, EventArgs e)
        {
            if (this.fileName is null)
            {
                MessageBox.Show(this, "No file chosen.");
                return;
            }

            if (!File.Exists(this.fileName))
            {
                MessageBox.Show(this, $"File {this.fileName} does not exist.");
                return;
            }


            UpdatePlotData(this.fileName);

        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            RemoveSeries();
            RemoveHeatmap();
            RemoveHline();
            this.radiuses = null;
            this.angles = null;
            this.radiusesInterpolated = null;
            this.anglesInterpolated = null;

            this.panelTrackBar.Visible = false;
            this.indexLbl.Text = string.Empty;
            this.totalLbl.Text = string.Empty;
            this.fileName = null;
            this.fileLbl.Text = string.Empty;

            hmPlot.Refresh();

        }

        private void RelCheckBox_Click(object sender, EventArgs e)
        {
            if (this.innerSeries is null) return;
            PlotChange(this.trackBar1.Value);
        }

        private void CreateSeries()
        {
            this.innerSeries = new Series();
            this.innerSeries.ChartArea = "ChartArea1";
            this.innerSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Polar;
            this.innerSeries.Color = System.Drawing.Color.DodgerBlue;
            this.innerSeries.Name = "Series1";
            this.innerSeries.BorderWidth = 5;

            this.outerSeries = new Series();
            this.outerSeries.ChartArea = "ChartArea1";
            this.outerSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Polar;
            this.outerSeries.Name = "Series2";
            this.outerSeries.BorderWidth = 5;
            this.outerSeries.Color = System.Drawing.Color.DodgerBlue;

            scatterSeries = new Series();
            scatterSeries.ChartArea = "ChartArea1";
            scatterSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Polar;
            scatterSeries.CustomProperties = "PolarDrawingStyle=Marker";
            scatterSeries.MarkerSize = 15;
            scatterSeries.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            scatterSeries.Name = "Series3";
            scatterSeries.Color = System.Drawing.Color.DodgerBlue;

            odSeries = new Series();
            odSeries.ChartArea = "ChartArea1";
            odSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Polar;
            odSeries.Name = "Series4";
            odSeries.Color = System.Drawing.Color.Red;
            odSeries.BorderWidth = 1;
        }

        private void RemoveSeries()
        {
            this.chart1.Series.Clear();
            this.innerSeries = null;
            this.outerSeries = null;
            this.scatterSeries = null;
            this.odSeries = null;
            this.chart1.Invalidate();
        }

        

        private void AddSeries()
        {
            this.chart1.Series.Add(this.innerSeries);
            this.chart1.Series.Add(this.outerSeries);
            this.chart1.Series.Add(this.scatterSeries);
            this.chart1.Series.Add(this.odSeries);
        }

        private void ClearSeriesPoints()
        {
            this.innerSeries.Points.Clear();
            this.outerSeries.Points.Clear();
            this.scatterSeries.Points.Clear();
            this.odSeries.Points.Clear();
        }

        private void UpdatePlotData(string fileName)
        {
            if (this.innerSeries is null)
            {
                CreateSeries();
                AddSeries();
                this.panelTrackBar.Visible = true;
            }
            ClearSeriesPoints();
            var pd = Loader.LoadPlotData(fileName);
            this.innerDiameter = pd.Item1;
            this.outerDiameter = pd.Item2;
            this.spacing = pd.Item3;
            
            var rds = pd.Item4;
            int rows = rds.Count;
            int n = rds[0].Length; // broj zavojnica

            this.radiuses = new List<double[]>(rows);
            this.radiusesInterpolated = new List<double[]>(rows);
            this.angles = Linspace(0, 360, n+1);

            const int min = 64;
            int factor = min / n;
            int interpolatedCount = (factor+1)*min + 1; // promijeniti mozda

            double[] anglesExtended = Linspace(-360, 720, 3*n + 1);
            

            this.anglesInterpolated = Linspace(0, 360, interpolatedCount);
            foreach (double[] r in rds)
            {

                // utrostruči radi interpolacije
                double[] rRepeated = new double[3 * n + 1];
                Array.Copy(r, 0, rRepeated, 0, n);
                Array.Copy(r, 0, rRepeated, n, n);
                Array.Copy(r, 0, rRepeated, 2*n, n);
                rRepeated[3 * n] = rRepeated[0];
                


                double[] rPeriodic = new double[n + 1];
                Array.Copy(r, rPeriodic, n);
                rPeriodic[n] = rPeriodic[0];

                // interpolacija periodickim kubnim splajnom
                // alglib.spline1dbuildcubic(angles, rPeriodic, n+1, -1, 0, -1, 360, out alglib.spline1dinterpolant interpolant);

                // interpolacija akima splajnom
                alglib.spline1dbuildakima(anglesExtended, rRepeated, out alglib.spline1dinterpolant interpolant);
                double[] rInterpolated = new double[interpolatedCount];
                for (int i = 0; i < interpolatedCount; i++)
                {
                    rInterpolated[i] = alglib.spline1dcalc(interpolant, this.anglesInterpolated[i]);
                }


                this.radiusesInterpolated.Add(rInterpolated);
                this.radiuses.Add(rPeriodic);
            }

            RemoveHeatmap();
            RemoveHline();
            AddHeatmap();
            AddHline();
            
            trackBar1.Maximum = rows - 1;
            trackBar1.Minimum = 0;
            trackBar1.Value = 0;
            PlotChange(0);


        }

        
        private void PlotChange(int index)
        {
            ClearSeriesPoints();

            double[] rs = radiuses[index];
            double thickness = (this.outerDiameter - this.innerDiameter) / 2;
            double orig = this.innerDiameter / 2;
            bool ticked = this.relCheckBox.Checked;
            double ir = ticked ? 0.0 : orig;
            double or = ticked ? 1.0 * 100.0 : this.outerDiameter / 2;

            for (int i = 0; i < this.angles.Length; i++)
            {
                // this.outerSeries.Points.AddXY(angles[i], rs[i]);
                // this.innerSeries.Points.AddXY(angles[i], this.innerDiameter/2);
                double r = ticked ? ((rs[i]-orig)/thickness)*100 : rs[i];
                this.scatterSeries.Points.AddXY(angles[i], r);
            }
            

            double[] rsInterpolated = radiusesInterpolated[index];
            for (int i = 0; i < this.anglesInterpolated.Length; i++)
            {
                double r = ticked ? ((rsInterpolated[i]-orig)/thickness)*100 : rsInterpolated[i];
                this.outerSeries.Points.AddXY(anglesInterpolated[i], r);
                this.innerSeries.Points.AddXY(anglesInterpolated[i], ir);
                this.odSeries.Points.AddXY(anglesInterpolated[i], or);
            }

            double chartMin = ticked ? -0.8*100 : FloorUpTo(ir - 0.7*thickness, 0.1);
            double chartMax = ticked ? 1.2 * 100 : or + 0.2 * thickness;
            Axis yaxis = this.chart1.ChartAreas[0].AxisY;
            yaxis.Minimum = chartMin;
            yaxis.Maximum = chartMax;
            string fmt = ticked ? "{0}%" : $"{{0}}{MEASURING_UNIT_RADIUS}";

            
            
            
            
            
            

            // Add tick marks for every 20 percent
            double interval = ticked ? 20 : 0.1;
            yaxis.Interval = interval;
            double eps = 0.01 * interval;

            yaxis.CustomLabels.Clear();
            for (double v = chartMin; v <= chartMax; v+=interval)
            {
                string lbl = (v + eps) < ir ? string.Empty : string.Format(fmt, v);
                yaxis.CustomLabels.Add(v-eps, v+eps, lbl);
            }


            double y = index * this.spacing;
            this.hline.Y = y;



            this.indexLbl.Text = (index+1).ToString();
            this.totalLbl.Text = this.radiuses.Count.ToString();

            this.offsetLbl.Text = y + MEASURING_UNIT_RADIUS;
            this.lengthLbl.Text = this.radiuses.Count * this.spacing + MEASURING_UNIT_RADIUS;


            this.chart1.Invalidate();
            this.hmPlot.Refresh();
            
        }

        


        private void InitializeHeatmapPlot()
        {
            
            
            var plt = hmPlot.Plot;
            plt.XAxis.SetBoundary(-5, 365);
            plt.YAxis.SetBoundary(-1, 101);

            plt.XAxis.TickLabelFormat(v => $"{v}{MEASURING_UNIT_ANGLE}");
            plt.YAxis.TickLabelFormat(v => $"{v}{MEASURING_UNIT_RADIUS}");





        }

        private void AddHeatmap()
        {
            this.hm = hmPlot.Plot.AddHeatmap(
                ConvertToIntensities(
                    this.radiusesInterpolated, 
                    this.radiusesInterpolated.Count, 
                    this.anglesInterpolated.Length
                    ), 
                CMAP, 
                false
                );
            hm.Smooth = true;
            hm.Interpolation = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            hm.XMin = 0;
            hm.XMax = 360;
            hm.YMin = 0;
            double ymax = spacing * this.radiuses.Count;
            hm.YMax = ymax;

            hmPlot.Plot.XAxis.SetBoundary(-5, 365);
            hmPlot.Plot.YAxis.SetBoundary(-0.01*ymax, 1.01*ymax);
            hmPlot.Plot.AxisAuto();

        }

        private void RemoveHeatmap()
        {
            if (hm is null) return;
            hmPlot.Plot.Remove(hm);
            hm = null;
        }

        private void AddHline()
        {
            
            this.hline = hmPlot.Plot.AddHorizontalLine(0, color: System.Drawing.Color.Red);
        }

        private void RemoveHline()
        {
            if (hline is null) return;
            hmPlot.Plot.Remove(hline);
            hline = null;
        }

        private void TestBtn_Click(object sender, EventArgs e)
        {
            

        }
    }
}
