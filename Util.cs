

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

        public static double CeilUpTo(double x, double acc)
        {
            return Math.Ceiling(x/acc) * acc;
            // 9.412, 0.1 => 9.5
            // 632, 100 => 700
        }

        

        public static double Normalize(double x, double min, double max, double healthy, double cmf)
        {
            
            return (x > healthy) 
                ? (  cmf + ((1.0 - cmf) / (max - healthy)) * (x - healthy)  ) 
                : (        (cmf         / (healthy - min)) * (x - min));
            
        }

        public static double NormalizeInverse(double y, double min, double max, double healthy, double cmf)
        {
            return (y > cmf)
                ? ( (y - cmf)*(max - healthy)/(1.0 - cmf) + healthy)
                : (  y       *(healthy - min)/cmf           + min);
        }

    }
}
