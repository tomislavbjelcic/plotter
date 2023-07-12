﻿using ScottPlot.Plottable;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static WindowsFormsApp1.Util;
using System.Globalization;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private static readonly ScottPlot.Drawing.Colormap CMAP
            = ScottPlot.Drawing.Colormap.Balance;//new ScottPlot.Drawing.Colormap(ReverseIColormap(new ScottPlot.Drawing.Colormaps.Turbo()));
        private const double COLORMAP_OD_FRACTION = 0.5;
        private const int FONT_SIZE = 12;
        private const System.Drawing.Drawing2D.InterpolationMode HEATMAP_INTERPOLATION =
            System.Drawing.Drawing2D.InterpolationMode.Bilinear;
        private const string MEASURING_UNIT_RADIUS = "mm";
        private const string MEASURING_UNIT_ANGLE = "°";
        private static readonly System.Drawing.Color PLOT_COLOR = System.Drawing.Color.DodgerBlue;
        private static readonly System.Drawing.Color OD_PLOT_COLOR = System.Drawing.Color.Red;
        private const int INTERPOLATION_POINTS_MIN = 128;
        private const bool DIAM = true;




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
            scale == ABSOLUTE_SCALE ? r : (r - ir) / (or - ir);
        private int GetScale() => relCheckBox.Checked ? RELATIVE_SCALE : ABSOLUTE_SCALE;
        private static double IntervalPolarChart(int scale) =>
            scale == ABSOLUTE_SCALE ? 0.1 : 0.2;
        private const double MAX_THICKNESS = 1.2;
        private const double MIN_THICKNESS = -0.8;
        private const int DECIMAL_PLACES = 2;
        private static string Format(double r, int scale, int decimalPlaces = DECIMAL_PLACES, bool diam = DIAM)
        {
            string str = string.Empty;
            
            if (scale == ABSOLUTE_SCALE)
            {
                if (diam) r *= 2;
                str = $"{r.ToString($"N{decimalPlaces}", CultureInfo.InvariantCulture)} {MEASURING_UNIT_RADIUS}";
            }
            else if (scale == RELATIVE_SCALE)
            {
                str = r.ToString("P0", CultureInfo.InvariantCulture);
            }
            else { }

            return str;
        }
        private int GetScaleColorbar() => cbRelCheckBox.Checked ? RELATIVE_SCALE : ABSOLUTE_SCALE;


        private readonly Series idSeries = CreateCurveSeries("IdSeries");
        private readonly Series odSeries = CreateOdSeries();
        private readonly Series datapointSeries = CreateDatapointSeries();
        private readonly Series interpolatedSeries = CreateCurveSeries("InterpolatedSeries");
        



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

        // Minimum i maksimum radijusi za skaliranje za heatmap intensities
        private double scaleMin = 0.0;
        private double scaleMax = 0.0;



        private ScottPlot.Plottable.Heatmap hm = null;
        private ScottPlot.Plottable.HLine hline = null;
        private ScottPlot.Plottable.Colorbar colorbar = null;

        public Form1()
        {
            InitializeComponent();
            ResetVariables();
            InitializeDynamic();
            InitializeHeatmapPlot();
            AddSeries();
            chart1.Invalidate();
            hmPlot.Refresh();
            
            

            trackBar1.MouseWheel += (sender, e) =>
            {
                ((HandledMouseEventArgs)e).Handled = true;
            };
            trackBar1.MouseWheel += TrackBar1_MouseWheel;
            chart1.MouseWheel += TrackBar1_MouseWheel;
        }

        private void InitializeDynamic()
        {
            Font f = new Font(plotBtn.Font.FontFamily, FONT_SIZE);


            foreach (Control c in this.Controls)
            {
                c.Font = f;
            }

            ChartArea ca = chart1.ChartAreas[0];
            ca.AxisX.LabelStyle.Font = f;
            ca.AxisY.LabelStyle.Font = f;

            var plt = hmPlot.Plot;
            plt.XAxis.TickLabelStyle(fontName: f.Name, fontSize: f.Size);
            plt.YAxis.TickLabelStyle(fontName: f.Name, fontSize: f.Size);
            
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
            AdjustTimerEnable();
            AdjustIndexPanelVisible();
            AdjustCbRelCheckBoxEnabled();
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
        private void AdjustTimerEnable()
        {
            if (timer1.Enabled == PlotPresent) return;
            timer1.Enabled = PlotPresent;
        }
        private void AdjustIndexPanelVisible()
        {
            indexPanel.Visible = PlotPresent;
        }
        private void AdjustCbRelCheckBoxEnabled()
        {
            // jedino kad nije enabled je u slucaju da nije plot prisutan i nije datoteka odabrana
            cbRelCheckBox.Enabled = PlotPresent || !string.IsNullOrEmpty(FileName);
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


            
            RemoveHeatmapAndColorbar();
            RemoveHline();
            ClearResources();
            ResetVariables();
            

            PlotPresent = false;
            chart1.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
            hmPlot.Refresh();
            chart1.Invalidate();
            InitializeHeatmapPlot();
            


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

        private void AddSeries()
        {
            chart1.Series.Add(idSeries);
            chart1.Series.Add(odSeries);
            chart1.Series.Add(datapointSeries);
            chart1.Series.Add(interpolatedSeries);
        }

        private void ClearResources()
        {

            idSeries.Points.Clear();
            odSeries.Points.Clear();
            interpolatedSeries.Points.Clear();
            datapointSeries.Points.Clear();


            data.Clear();

            
            angles = null;
            radiusesBuf = null;
            anglesInterpolated = null;
            radiusesInterpolatedBuf = null;
            anglesExtended = null;
            anglesInterpolated = null;


        }

        private void ResetVariables()
        {
            ir = 0.0;
            or = 0.0;
            spacing = 0.0;
            scaleMax = double.NegativeInfinity;
            scaleMin = double.PositiveInfinity;
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
                double y = Scale_Radius(radiusesInterpolatedBuf[i], sc, ir, or);
                idSeries.Points[i].SetValueY(yir);
                odSeries.Points[i].SetValueY(yor);
                interpolatedSeries.Points[i].SetValueY(y);
            }


            for (int i = 0; i < angles.Length; i++)
            {
                double y = Scale_Radius(radiusesBuf[i], sc, ir, or);
                datapointSeries.Points[i].SetValueY(y);
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

            scaleMin = ir;
            scaleMax = ir + MAX_THICKNESS * (or - ir);


            for (int row = 0; row < Rows; row++)
            {
                double[] rds = data[Rows - row - 1];
                int n = rds.Length;
                Array.Copy(rds, 0, radiusesExtendedBuf, 0, n);
                Array.Copy(rds, 0, radiusesExtendedBuf, n, n);
                Array.Copy(rds, 0, radiusesExtendedBuf, 2 * n, n);
                radiusesExtendedBuf[3 * n] = rds[0];

                Interpolate(anglesExtended, radiusesExtendedBuf, anglesInterpolated, radiusesInterpolatedBuf);
                for (int col = 0; col < cols; col++)
                {
                    double intensity = radiusesInterpolatedBuf[col];
                    scaleMin = Math.Min(scaleMin, intensity);
                    scaleMax = Math.Max(scaleMax, intensity);
                    intensities[row, col] = intensity;
                }
            }

            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    intensities[row, col] = Normalize(
                        intensities[row, col], min: scaleMin, max: scaleMax, healthy: or, cmf: COLORMAP_OD_FRACTION
                        );

                }
            }

            //int cols = angles.Length;
            //double[,] intensities = new double[Rows, cols];

            //for (int row = 0; row < Rows; row++)
            //{
            //    double[] rds = data[Rows - row - 1];
            //    int n = rds.Length;
            //    Array.Copy(rds, 0, radiusesBuf, 0, n);
            //    radiusesBuf[n] = rds[0];

            //    //Interpolate(anglesExtended, radiusesExtendedBuf, anglesInterpolated, radiusesInterpolatedBuf);
            //    for (int col = 0; col < cols-1; col++)
            //    {
            //        intensities[row, col] = radiusesBuf[col];
            //    }
            //    intensities[row, cols - 1] = intensities[row, 0];
            //}

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


            SeriesFillEmpty(idSeries, anglesInterpolated);
            SeriesFillEmpty(odSeries, anglesInterpolated);
            SeriesFillEmpty(interpolatedSeries, anglesInterpolated);
            SeriesFillEmpty(datapointSeries, angles);


            double[,] intensities = MakeIntensities();

            
            AddHeatmapAndColorbar(intensities);
            AddHline();
            
            trackBar1.Maximum = Rows - 1;
            trackBar1.Minimum = 0;
            trackBar1.Value = 0;

            totalLbl.Text = Rows.ToString();
            lengthLbl.Text = Rows * spacing + MEASURING_UNIT_RADIUS;
            chart1.ChartAreas[0].AxisY.LabelStyle.Enabled = true;
            panelTrackBar.Visible = true;
            PlotChange(0);
            PlotPresent = true;

            GC.Collect();


        }

        
        private void PlotChange(int index)
        {

            double y = index * spacing;
            hline.Y = y;


            indexLbl.Text = (index + 1).ToString();
            //totalLbl.Text = Rows.ToString();

            offsetLbl.Text = y + MEASURING_UNIT_RADIUS;
            //lengthLbl.Text = Rows * spacing + MEASURING_UNIT_RADIUS;

            //Task.Run(() =>
            //{
            //    UpdateSeries(data[index]);
            //    AdjustLabels();
            //});
            UpdateSeries(data[index]);
            AdjustLabels();
            

            //chart1.Invalidate();
            //hmPlot.Refresh();
            
            
            

        }

        private void AdjustLabels()
        {
            int sc = GetScale();
            double yir = Scale_Radius(ir, sc, ir, or);
            double yor = Scale_Radius(or, sc, ir, or);
            Axis yaxis = chart1.ChartAreas[0].AxisY;
            

            double chartMin = 0.0;
            double chartMax = 0.0;
            double interval = IntervalPolarChart(sc);

            if (sc == ABSOLUTE_SCALE)
            {
                double thickness = yor - yir;
                if (DIAM) interval /= 2;
                chartMin = FloorUpTo(yir + MIN_THICKNESS * thickness, interval);
                chartMax = CeilUpTo(yir + MAX_THICKNESS * thickness, interval);
            } else if (sc == RELATIVE_SCALE)
            {
                chartMin = MIN_THICKNESS;
                chartMax = MAX_THICKNESS;
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
                
                string lbl = (v + eps) < yir ? string.Empty : Format(v, sc);
                yaxis.CustomLabels.Add(v - eps, v + eps, lbl);
            }


        }

        


        private static void SeriesFillEmpty(Series series, double[] angles)
        {
            series.Points.Clear();
            foreach (double angle in angles)
            {
                series.Points.AddXY(angle, 0);
            }

        }

        private void InitializeHeatmapPlot()
        {
            var plt = hmPlot.Plot;
            plt.XAxis.SetBoundary(-5, 365);
            plt.YAxis.SetBoundary(-1, 101);

            plt.XAxis.TickLabelFormat(v => $"{v}{MEASURING_UNIT_ANGLE}");
            plt.YAxis.TickLabelFormat(v => $"{v} {MEASURING_UNIT_RADIUS}");

            plt.XAxis.ManualTickSpacing(30);
            
        }

        private void AddHeatmapAndColorbar(double[,] intensities)
        {
            //hm = hmPlot.Plot.AddHeatmap(new double[,] { { 0} }, CMAP, false);
            hm = new Heatmap();
            hm.Update(intensities, CMAP, 0.0, 1.0);
            hmPlot.Plot.Add(hm);
            
            hm.Smooth = true;
            hm.Interpolation = HEATMAP_INTERPOLATION;
            hm.XMin = 0;
            hm.XMax = 360;
            hm.YMin = 0;
            double ymax = spacing * Rows;
            hm.YMax = ymax;

            colorbar = hmPlot.Plot.AddColorbar(hm);
            //colorbar = new Colorbar();
            //colorbar.UpdateColormap(hm.Colormap);
            
            

            
            colorbar.AutomaticTicks(formatter: ColorbarTicksFormatter);
            
            
            
            
            //colorbar.SetTicks(new double[] { 0, 0.5, 0.7 }, new string[] { "a", "b", "c" });
            colorbar.TickLabelFont.Size = FONT_SIZE;

            hmPlot.Plot.XAxis.SetBoundary(-5, 365);
            hmPlot.Plot.YAxis.SetBoundary(-0.01*ymax, 1.01*ymax);
            hmPlot.Plot.AxisAuto();

        }

        private string ColorbarTicksFormatter(double v)
        {
            double r = NormalizeInverse(y: v, min: scaleMin, max: scaleMax, healthy: or, cmf: COLORMAP_OD_FRACTION);
            int sc = GetScaleColorbar();
            double val = Scale_Radius(r, sc, ir, or);
            string tstr = Format(val, sc, decimalPlaces: 3);
            //Console.WriteLine($"v={v}, r={r}, val={val} tstr={tstr}");
            return tstr;
        }

        private void RemoveHeatmapAndColorbar()
        {
            if (hm is null) return;
            hmPlot.Plot.Remove(colorbar);
            hmPlot.Plot.Remove(hm);
            colorbar = null;
            hm = null;
        }

        private void AddHline()
        {
            
            hline = hmPlot.Plot.AddHorizontalLine(0, color: System.Drawing.Color.Red, width: 3);
            //hline.DragEnabled = true;
        }

        private void RemoveHline()
        {
            if (hline is null) return;
            hmPlot.Plot.Remove(hline);
            hline = null;
        }

        private void TestBtn_Click(object sender, EventArgs e)
        {

            //StringBuilder sb = new StringBuilder();
            //int count = chart1.Series.Count;
            //sb.AppendLine($"Data len: {data.Count}");
            //sb.AppendLine($"PlotPresent = {PlotPresent}");
            //sb.AppendLine($"Series count: {count}");
            //foreach (Series s in chart1.Series) {
            //    sb.AppendLine($"{s.Name}, Count={s.Points.Count}");
            //}
            //sb.AppendLine((idSeries is null).ToString());
            //MessageBox.Show(sb.ToString());


            //double off = chart1.ChartAreas[0].AxisY.LabelStyle.IntervalOffset;
            //MessageBox.Show(off.ToString());
            //MessageBox.Show(Process.GetCurrentProcess().Id.ToString());


        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

            hmPlot.RefreshRequest();
            
        }

        private void CbRelCheckBox_Click(object sender, EventArgs e)
        {
            // ne treba ništa jer se plot ionako refresha periodično sam
        }
    }
}
