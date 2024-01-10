using ScottPlot.Plottable;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static WindowsFormsApp1.Util;
using System.Globalization;
using ScottPlot.Renderable;
using System.Windows.Forms.DataVisualization.Charting;
using Message = System.Windows.Forms.Message;
using Axis = System.Windows.Forms.DataVisualization.Charting.Axis;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        private static readonly ScottPlot.Drawing.IColormap BASE_ICMAP = new ScottPlot.Drawing.Colormaps.Balance();

        private const double COLORMAP_OD_FRACTION = 0.5;
        private const double THICKNESS_LOW_THRESHOLD_COLORMAP = 0.5;
        private const double THICKNESS_OFF_HEALTHY = 0.1;
        private const double FRACTION_OFF_HEALTHY = 0.05;
        private const int FONT_SIZE = 14;
        private const System.Drawing.Drawing2D.InterpolationMode HEATMAP_INTERPOLATION =
            System.Drawing.Drawing2D.InterpolationMode.Bilinear;
        private const string MEASURING_UNIT_RADIUS = "mm";
        private const string MEASURING_UNIT_ANGLE = "°";
        private static readonly System.Drawing.Color PLOT_COLOR = System.Drawing.Color.Gray;
        private static readonly System.Drawing.Color OD_PLOT_COLOR = System.Drawing.Color.Red;
        private const int INTERPOLATION_POINTS_MIN = 128;
        private const int HEATMAP_ROWS_MAX = 32768;
        private const bool DIAM = false;

        private readonly Dictionary<Keys, Action> keyCommands = new Dictionary<Keys, Action>();

        private void RegisterCommands()
        {
            keyCommands.Add(Keys.Control | Keys.O, chooseFileBtn.PerformClick);
            keyCommands.Add(Keys.Control | Keys.F, clearFilenameBtn.PerformClick);
            keyCommands.Add(Keys.Control | Keys.P, plotBtn.PerformClick);
            keyCommands.Add(Keys.Control | Keys.G, clearBtn.PerformClick);
            keyCommands.Add(Keys.Control | Keys.E, cbRelCheckBox.PerformClick);
            keyCommands.Add(Keys.Control | Keys.R, relCheckBox.PerformClick);
            keyCommands.Add(Keys.Control | Keys.T, testBtn.PerformClick);
            keyCommands.Add(Keys.Control | Keys.Shift | Keys.C,
                () =>
                {
                    GC.Collect();
                    MessageBox.Show(this, "Invoked GC.Collect()");
                }
                );
        }




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
            scale == ABSOLUTE_SCALE ? 0.1 : 0.1;
        private static double IntervalColorBar(int scale) =>
            scale == ABSOLUTE_SCALE ? -1.0 : 0.1;   // na REL skali mora dijeliti 1
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



        private double[] cbTickFractions = null;
        private string[] cbTickLabels = null;



        // ID/2
        private double ir = 0.0;

        // OD/2
        private double or = 0.0;

        // Razmak između redova zavojnica
        private double spacing = 0.0;

        // Minimum i maksimum radijusi za skaliranje za heatmap intensities
        private double scaleMin = 0.0;
        private double scaleMax = 0.0;


        // Bit će postavljeni prilikom poziva MakeIntensities
        private ThresholdColormap icmap = null;
        private ScottPlot.Drawing.Colormap cmap = null;


        private double[] thresholdIntensities = null;



        private ScottPlot.Plottable.Heatmap hm = null;
        private ScottPlot.Plottable.HLine hline = null;
        private ScottPlot.Plottable.Colorbar colorbar = null;

        public Form1()
        {
            InitializeComponent();
            RegisterCommands();
            AdjustPlotControls();
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyCommands.ContainsKey(keyData))
            {
                Action ac = keyCommands[keyData];
                ac();
            }
            return base.ProcessCmdKey(ref msg, keyData);
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
            AdjustFilenameClearBtnEnabled();
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
        private void AdjustFilenameClearBtnEnabled()
        {
            // nije enabled jedino kad plot postoji
            clearFilenameBtn.Enabled = !PlotPresent;
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

            icmap = null;
            cmap = null;

            cbTickFractions = null;
            cbTickLabels = null;

            thresholdIntensities = null;


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
            Array.Copy(radiuses, 0, radiusesExtendedBuf, 2 * n, n);
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
                double[] rds = data[row];
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

            // downscale ako ima previše redaka
            if (Rows > HEATMAP_ROWS_MAX)
            {
                // a baš i ne radi...
                intensities = Downscale(intensities, HEATMAP_ROWS_MAX);

            }


            double thickness = or - ir;
            double[] prms = new double[]
            {
                Normalize(ir + THICKNESS_LOW_THRESHOLD_COLORMAP*thickness, scaleMin, scaleMax, or, COLORMAP_OD_FRACTION),
                0.0,

                Normalize(or - THICKNESS_OFF_HEALTHY*thickness, scaleMin, scaleMax, or, COLORMAP_OD_FRACTION),
                COLORMAP_OD_FRACTION - FRACTION_OFF_HEALTHY,

                Normalize(or, scaleMin, scaleMax, or, COLORMAP_OD_FRACTION),
                COLORMAP_OD_FRACTION,

                Normalize(or + THICKNESS_OFF_HEALTHY*thickness, scaleMin, scaleMax, or, COLORMAP_OD_FRACTION),
                COLORMAP_OD_FRACTION + FRACTION_OFF_HEALTHY,

                Normalize(scaleMax, scaleMin, scaleMax, or, COLORMAP_OD_FRACTION),
                1.0
            };

            thresholdIntensities = new double[prms.Length / 2];
            for (int i = 0; i < thresholdIntensities.Length; i++)
            {
                thresholdIntensities[i] = prms[2 * i];
            }


            icmap = new ThresholdColormap(BASE_ICMAP, prms);
            cmap = new ScottPlot.Drawing.Colormap(icmap);


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


            angles = Linspace(0, 360, n + 1);
            radiusesBuf = new double[n + 1];

            anglesExtended = Linspace(-360, 720, 3 * n + 1);
            radiusesExtendedBuf = new double[3 * n + 1];

            anglesInterpolated = Linspace(0, 360, interpolatedCount);
            radiusesInterpolatedBuf = new double[interpolatedCount];

            //icmap = new ThresholdColormap(BASE_ICMAP, cmf:COLORMAP_OD_FRACTION);
            //cmap = new ScottPlot.Drawing.Colormap(icmap);


            SeriesFillEmpty(idSeries, anglesInterpolated);
            SeriesFillEmpty(odSeries, anglesInterpolated);
            SeriesFillEmpty(interpolatedSeries, anglesInterpolated);
            SeriesFillEmpty(datapointSeries, angles);


            double[,] intensities = MakeIntensities();

            CalculateColorbarLabelsRelative();


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
            // double yor = Scale_Radius(or, sc, ir, or);
            Axis yaxis = chart1.ChartAreas[0].AxisY;


            double interval = IntervalPolarChart(sc);
            if (sc == ABSOLUTE_SCALE && DIAM) interval /= 2;
            double thickness = or - ir;
            double chartMin = FloorUpTo(Scale_Radius(Math.Min(scaleMin, ir + MIN_THICKNESS * thickness), sc, ir, or), interval);
            double chartMax = CeilUpTo(Scale_Radius(Math.Max(scaleMax, ir + MAX_THICKNESS * thickness), sc, ir, or), interval);

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
            //hm = new Heatmap();
            //hm.Update(intensities, cmap, 0.0, 1.0);
            //hmPlot.Plot.Add(hm);
            hm = hmPlot.Plot.AddHeatmap(intensities, cmap, lockScales: false);
            hm.Update(intensities, cmap, 0.0, 1.0);

            hm.Smooth = true;
            hm.Interpolation = HEATMAP_INTERPOLATION;
            hm.FlipVertically = true;
            hm.XMin = 0;
            hm.XMax = 360;
            hm.YMin = 0;
            double ymax = spacing * Rows;
            hm.YMax = ymax;

            colorbar = hmPlot.Plot.AddColorbar(hm);
            AdjustColorbarLabels();

            colorbar.TickLabelFont.Size = FONT_SIZE;



            hmPlot.Plot.XAxis.SetBoundary(0, 360);
            hmPlot.Plot.YAxis.SetBoundary(0, ymax);
            hmPlot.Plot.AxisAuto();



        }

        private void CalculateColorbarLabelsRelative()
        {
            // prvo konstruirati piecewise linearnu funkciju
            // tr(scaleMin) -> 0.0, 1.0 -> COLORMAP_OD_FRACTION, tr(scaleMax) -> 1.0
            // oznake će piecewise linearno rasti sa laktom u 100%
            double trscmin = Scale_Radius(scaleMin, RELATIVE_SCALE, ir, or);
            double trscmax = Scale_Radius(scaleMax, RELATIVE_SCALE, ir, or);
            double[] prms = new double[]
            {
                trscmin, 0.0, 1.0, COLORMAP_OD_FRACTION, trscmax, 1.0
            };
            PiecewiseLinear f = new PiecewiseLinear(prms);


            double intrvl = IntervalColorBar(RELATIVE_SCALE);
            double bottomTr = CeilUpTo(trscmin, intrvl);
            int bottomIntervalCount = Convert.ToInt32((1.0 - bottomTr) / intrvl);

            double topTr = FloorUpTo(trscmax, intrvl);
            int topIntervalCount = Convert.ToInt32((topTr - 1.0) / intrvl);

            int tickCount = bottomIntervalCount + topIntervalCount + 1;


            cbTickFractions = new double[tickCount];
            cbTickLabels = new string[tickCount];

            for (int i = 0; i < tickCount; i++)
            {
                double tr = bottomTr + i * intrvl;
                double frac = f.Apply(tr);
                cbTickFractions[i] = frac;
                cbTickLabels[i] = Format(tr, RELATIVE_SCALE);
            }
        }

        private void AdjustColorbarLabels()
        {
            int sc = GetScaleColorbar();
            colorbar.ClearTicks();
            colorbar.AutomaticTicks(enable: false);
            if (sc == ABSOLUTE_SCALE)
            {

                colorbar.AutomaticTicks(formatter: ColorbarTicksFormatter);
            }
            else if (sc == RELATIVE_SCALE)
            {
                colorbar.SetTicks(cbTickFractions, cbTickLabels);
            }
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

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

            //hmPlot.RefreshRequest();
            hmPlot.Refresh();

        }

        private void CbRelCheckBox_Click(object sender, EventArgs e)
        {
            AdjustColorbarLabels();
            hmPlot.Refresh();
        }
    }
}
