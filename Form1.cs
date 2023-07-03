using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static WindowsFormsApp1.Util;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private static readonly ScottPlot.Drawing.Colormap CMAP
            = ScottPlot.Drawing.Colormap.Turbo;//new ScottPlot.Drawing.Colormap(ReverseIColormap(new ScottPlot.Drawing.Colormaps.Turbo()));
        private const System.Drawing.Drawing2D.InterpolationMode HEATMAP_INTERPOLATION = 
            System.Drawing.Drawing2D.InterpolationMode.Bilinear;
        private const string MEASURING_UNIT_RADIUS = "mm";
        private const string MEASURING_UNIT_ANGLE = "°";
        private static readonly System.Drawing.Color PLOT_COLOR = System.Drawing.Color.DodgerBlue;
        private static readonly System.Drawing.Color OD_PLOT_COLOR = System.Drawing.Color.Red;
        private const int INTERPOLATION_POINTS_MIN = 128;




        // ime odabrane datoteke
        private string fileName = null;
        private string FileName
        {
            get => fileName;
            set
            {
                fileName = value;
                AdjustPlotControls();
            }
        }

        // zastavica jesu li nacrtani neki podaci (true=jesu, false=prazno je)
        private bool plotPresent = false;
        private bool PlotPresent
        {
            get => plotPresent;
            set
            {
                plotPresent = value;
                AdjustPlotControls();
            }
        }

        
        private const int ABSOLUTE_SCALE = 0;
        private const int RELATIVE_SCALE = 1;
        private static double Scale_Radius(double r, int scale, double ir = 0.0, double or = 0.0) => 
            scale == ABSOLUTE_SCALE ? r : 100 * ((r - ir)/(or - ir));
        private int GetScale() => relCheckBox.Checked ? RELATIVE_SCALE : ABSOLUTE_SCALE;
        

        
        private Series idSeries = null;
        private Series odSeries = null;
        private Series datapointSeries = null;
        private Series interpolatedSeries = null;



        // Lista svih radijusa, npr R x n
        // R je broj redaka
        // n je broj zavojnica u svakom retku
        private readonly List<double[]> data = new List<double[]>();
        private int Rows
        {
            get => data.Count;
        }
        

        // Polje ekvidistantnih kuteva od 0 do 360.
        // n+1 mnogo elemenata
        private double[] angles = null;
        private double[] radiusesBuf = null;

        // polje ekvidistantnih kuteva nakon interpolacije za plotanje
        // k*n + 1 mnogo elemenata, gdje je k neki prirodan broj
        private double[] anglesInterpolated = null;
        private double[] radiusesInterpolatedBuf = null;

        // ekvidistantni kutevi proširenja angles sa svake strane jednako
        // u implementaciji bit će duljine 3*n + 1
        private double[] anglesExtended = null;
        private double[] radiusesExtendedBuf = null;
        
        

        // ID/2
        private double ir = 0.0;

        // OD/2
        private double or = 0.0;

        // Razmak između redova zavojnica
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


        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        

        private void ChooseFileBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // this.fileName = openFileDialog1.FileName;
                fileLbl.Text = openFileDialog1.FileName;
            }
        }

        private void FilenameLbl_TextChanged(object sender, EventArgs e)
        {
            FileName = fileLbl.Text;
        }

        private void AdjustPlotControls()
        {
            AdjustClearPlotBtnEnabled();
            AdjustRelCheckBoxEnabled();
            AdjustPlotBtnEnabled();
            AdjustTrackBarPanelVisible();
        }
        private void AdjustClearPlotBtnEnabled()
        {
            // clear plot gumb je enabled ako je plot prisutan
            clearBtn.Enabled = PlotPresent;
        }
        private void AdjustRelCheckBoxEnabled()
        {
            // jedino kad nije enabled je u slucaju da nije plot prisutan i nije datoteka odabrana
            relCheckBox.Enabled = PlotPresent || !string.IsNullOrEmpty(FileName);
        }
        private void AdjustPlotBtnEnabled()
        {
            // samo je enabled ako je datoteka odabrana i plot ne postoji
            plotBtn.Enabled = !PlotPresent && !string.IsNullOrEmpty(FileName);
        }
        private void AdjustTrackBarPanelVisible()
        {
            // vidljiv je samo kad je plot prisutan
            panelTrackBar.Visible = PlotPresent;
        }

        private void ClearFilenameBtn_Click(object sender, EventArgs e)
        {
            fileLbl.Text = string.Empty;
        }

        private void TrackBar1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!PlotPresent) { return; }
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
            // Nije moguće kliknuti Plot gumb dok je fileName null
            //if (this.fileName is null)
            //{
            //    MessageBox.Show(this, "No file chosen.");
            //    return;
            //}

            // OpenFileDialog provjerava kod selekcije postoji li datoteka
            //if (!File.Exists(this.fileName))
            //{
            //    MessageBox.Show(this, $"File {this.fileName} does not exist.");
            //    return;
            //}


            UpdatePlotData();

        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            RemoveSeries();
            RemoveHeatmap();
            RemoveHline();
            DisposeResources();


            hmPlot.Refresh();
            chart1.Invalidate();
            PlotPresent = false;


            GC.Collect();

        }

        private void RelCheckBox_Click(object sender, EventArgs e)
        {
            if (!PlotPresent) return;
            PlotChange(trackBar1.Value);
        }


        private static Series CreateDatapointSeries() => new Series
            {
                ChartArea = "ChartArea1",
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Polar,
                CustomProperties = "PolarDrawingStyle=Marker",
                MarkerSize = 15,
                MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle,
                Name = "DatapointSeries",
                Color = PLOT_COLOR
            };

        private static Series CreateCurveSeries(string name) => new Series
        {
            ChartArea = "ChartArea1",
            ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Polar,
            Name = name,
            BorderWidth = 5,
            Color = PLOT_COLOR
        };

        private static Series CreateOdSeries() => new Series
        {
            ChartArea = "ChartArea1",
            ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Polar,
            Name = "OdSeries",
            Color = OD_PLOT_COLOR,
            BorderWidth = 1
        };

        private void CreateAndAddEmptySeries()
        {
            idSeries = CreateCurveSeries("IdSeries");
            odSeries = CreateOdSeries();
            datapointSeries = CreateDatapointSeries();
            interpolatedSeries = CreateCurveSeries("InterpolatedSeries");

            chart1.Series.Add(idSeries);
            chart1.Series.Add(odSeries);
            chart1.Series.Add(datapointSeries);
            chart1.Series.Add(interpolatedSeries);

        }

        private void RemoveSeries()
        {
            chart1.Series.RemoveAt(4);
            chart1.Series.RemoveAt(3);
            chart1.Series.RemoveAt(2);
            chart1.Series.RemoveAt(1);
        }


        private void DisposeResources()
        {
            //idSeries.Points.Dispose();
            idSeries.Dispose();
            idSeries = null;

            //odSeries.Points.Dispose();
            odSeries.Dispose();
            odSeries = null;

            //interpolatedSeries.Points.Dispose();
            interpolatedSeries.Dispose();
            interpolatedSeries = null;

            //datapointSeries.Points.Dispose();
            datapointSeries.Dispose();
            datapointSeries = null;

            data.Clear();
            // data = null;

            
            angles = null;
            radiusesBuf = null;
            anglesInterpolated = null;
            radiusesInterpolatedBuf = null;
            anglesExtended = null;
            anglesInterpolated = null;


        }
        
        private void UpdateSeries(double[] radiuses)
        {
            int n = radiuses.Length;

            Array.Copy(radiuses, radiusesBuf, n);
            radiusesBuf[n] = radiuses[0];

            Array.Copy(radiuses, 0, radiusesExtendedBuf, 0, n);
            Array.Copy(radiuses, 0, radiusesExtendedBuf, n, n);
            Array.Copy(radiuses, 0, radiusesExtendedBuf, 2*n, n);
            radiusesExtendedBuf[3 * n] = radiuses[0];



            Interpolate(anglesExtended, radiusesExtendedBuf, anglesInterpolated, radiusesInterpolatedBuf);

            int sc = GetScale();
            double yor = Scale_Radius(or, sc, ir, or);
            double yir = Scale_Radius(ir, sc, ir, or);
            
            for (int i = 0; i < anglesInterpolated.Length; i++)
            {
                double ang = anglesInterpolated[i];
                double y = Scale_Radius(radiusesInterpolatedBuf[i], sc, ir, or);
                if (PlotPresent)
                {
                    idSeries.Points[i].SetValueY(yir);
                    odSeries.Points[i].SetValueY(yor);
                    interpolatedSeries.Points[i].SetValueY(y);
                } else
                {
                    
                    idSeries.Points.AddXY(ang, yir);
                    odSeries.Points.AddXY(ang, yor);
                    interpolatedSeries.Points.AddXY(ang, y);
                }
            }


            for (int i = 0; i < angles.Length; i++)
            {
                double ang = angles[i];
                double y = Scale_Radius(radiusesBuf[i], sc, ir, or);
                if (PlotPresent)
                    datapointSeries.Points[i].SetValueY(y);
                else datapointSeries.Points.AddXY(ang, y);
            }



        }

        private static void Interpolate(double[] xData, double[] yData, double[] domain, double[] y)
        {
            alglib.spline1dbuildakima(xData, yData, out alglib.spline1dinterpolant interpolant);
            for (int i = 0; i < domain.Length; i++)
            {
                y[i] = alglib.spline1dcalc(interpolant, domain[i]);
            }
            interpolant.Dispose();
            
        }

        private double[,] MakeIntensities()
        {
            int cols = anglesInterpolated.Length;
            double[,] intensities = new double[Rows, cols];

            for (int row = 0; row < Rows; row++)
            {
                double[] rds = data[Rows - row - 1];
                int n = rds.Length;
                Array.Copy(rds, 0, radiusesExtendedBuf, 0, n);
                Array.Copy(rds, 0, radiusesExtendedBuf, n, n);
                Array.Copy(rds, 0, radiusesExtendedBuf, 2*n, n);
                radiusesExtendedBuf[3*n] = rds[0];

                Interpolate(anglesExtended, radiusesExtendedBuf, anglesInterpolated, radiusesInterpolatedBuf);
                for (int col = 0; col < cols; col++)
                {
                    intensities[row, col] = radiusesInterpolatedBuf[col];
                }
            }

            return intensities;
        }

        

        private void UpdatePlotData()
        {
            // poziv ove metode je moguć samo ako nije prisutan plot
            
            var pd = Loader.LoadPlotData(fileName, dest: data);
            ir = pd.Item1 / 2;
            or = pd.Item2 / 2;
            spacing = pd.Item3;
            //data = pd.Item4;


            int n = data[0].Length; // broj zavojnica
            int factor = INTERPOLATION_POINTS_MIN / n;
            int interpolatedCount = (factor + 1) * n + 1;


            angles = Linspace(0, 360, n+1);
            radiusesBuf = new double[n + 1];

            anglesExtended = Linspace(-360, 720, 3 * n + 1);
            radiusesExtendedBuf = new double[3 * n + 1];

            anglesInterpolated = Linspace(0, 360, interpolatedCount);
            radiusesInterpolatedBuf = new double[interpolatedCount];


            CreateAndAddEmptySeries();
            

            double[,] intensities = MakeIntensities();
            
            AddHeatmap(intensities);
            AddHline();
            intensities = null;
            
            trackBar1.Maximum = Rows - 1;
            trackBar1.Minimum = 0;
            trackBar1.Value = 0;
            panelTrackBar.Visible = true;
            PlotChange(0);

            GC.Collect();


        }

        
        private void PlotChange(int index)
        {

            double y = index * this.spacing;
            hline.Y = y;



            indexLbl.Text = (index + 1).ToString();
            totalLbl.Text = Rows.ToString();

            offsetLbl.Text = y + MEASURING_UNIT_RADIUS;
            lengthLbl.Text = Rows * spacing + MEASURING_UNIT_RADIUS;


            UpdateSeries(data[index]);


            AdjustLabels();

            

            chart1.Invalidate();
            hmPlot.Refresh();


            PlotPresent = true;

        }

        private void AdjustLabels()
        {
            int sc = GetScale();
            double yir = Scale_Radius(ir, sc, ir, or);
            double yor = Scale_Radius(or, sc, ir, or);
            Axis yaxis = chart1.ChartAreas[0].AxisY;



            double chartMin = 0.0;
            double chartMax = 0.0;
            string fmt = string.Empty;
            double interval = 0.0;

            if (sc == ABSOLUTE_SCALE)
            {
                double thickness = yor - yir;
                chartMin = FloorUpTo(yir - 0.7 * thickness, 0.1);
                chartMax = yor + 0.2 * thickness;
                interval = 0.1;
                fmt = $"{{0}}{MEASURING_UNIT_RADIUS}";
            } else if (sc == RELATIVE_SCALE)
            {
                chartMin = -80;
                chartMax = 120;
                fmt = "{0}%";
                interval = 20;
            } else
            {
                // nemoguće
            }
            
            yaxis.Minimum = chartMin;
            yaxis.Maximum = chartMax;
            yaxis.Interval = interval;
            double eps = 0.01 * interval;

            yaxis.CustomLabels.Clear();
            for (double v = chartMin; v <= chartMax; v += interval)
            {
                string lbl = (v + eps) < yir ? string.Empty : string.Format(fmt, v);
                yaxis.CustomLabels.Add(v - eps, v + eps, lbl);
            }


        }




        private void InitializeHeatmapPlot()
        {
            
            
            var plt = hmPlot.Plot;
            plt.XAxis.SetBoundary(-5, 365);
            plt.YAxis.SetBoundary(-1, 101);

            plt.XAxis.TickLabelFormat(v => $"{v}{MEASURING_UNIT_ANGLE}");
            plt.YAxis.TickLabelFormat(v => $"{v}{MEASURING_UNIT_RADIUS}");





        }

        private void AddHeatmap(double[,] intensities)
        {
            hm = hmPlot.Plot.AddHeatmap(intensities, CMAP, false);
            hm.Smooth = true;
            hm.Interpolation = HEATMAP_INTERPOLATION;
            hm.XMin = 0;
            hm.XMax = 360;
            hm.YMin = 0;
            double ymax = spacing * Rows;
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
