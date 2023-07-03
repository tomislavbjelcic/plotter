

using System;

namespace WindowsFormsApp1
{
    public static class Util
    {

        public static double[] Linspace(double start, double stop, int num)
        {
            double[] linspace = new double[num];
            double diff = (stop - start) / (num - 1);
            for (int i = 0; i < num; i++)
            {
                linspace[i] = start + diff * i;
            }
            return linspace;
        }


        public static double FloorUpTo(double x, double acc)
        {
            return Math.Floor(x/acc) * acc;
            // 9.412, 0.1 => 9.4
            // 632, 100 => 600
        }

        //private class Rev : ScottPlot.Drawing.IColormap
        //{

        //    private readonly ScottPlot.Drawing.IColormap cmap;
            
        //    public Rev(ScottPlot.Drawing.IColormap cmap)
        //    {
        //        this.cmap = cmap;
        //    }
            
            
        //    public string Name => cmap.Name;



        //    public (byte r, byte g, byte b) GetRGB(byte value) => cmap.GetRGB((byte)~value);
        //}

        //public static ScottPlot.Drawing.IColormap ReverseIColormap(ScottPlot.Drawing.IColormap icmap)
        //{

        //    return new Rev(icmap);
        //}




    }
}
