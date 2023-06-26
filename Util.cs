

using System;

namespace WindowsFormsApp1
{
    public static class Util
    {

        public static double[] Linspace(double start, double stop, int num)
        {
            double[] linspace = new double[num];
            double diff = (stop - start) / (num - 1);
            double curr = start;
            for (int i = 0; i < num; i++)
            {
                linspace[i] = curr;
                curr += diff;
            }
            return linspace;
        }


        public static double FloorUpTo(double x, double acc)
        {
            return Math.Floor(x/acc) * acc;
            // 9.412, 0.1 => 9.4
            // 632, 100 => 600
        }


    }
}
