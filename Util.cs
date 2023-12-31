﻿

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

        public static double[,] Downscale(double[,] input, int rows)
        {
            int inputRows = input.GetLength(0);
            int inputCols = input.GetLength(1);
            double scaleFactor = inputRows / rows;
            double[,] output = new double[rows, inputCols];

            for (int r = 0; r < rows; r++)
            {
                double originalR = r * scaleFactor;
                int r1 = (int)originalR;
                int r2 = r1 + 1;

                //if (r2 >= inputRows)
                //    r2 = r1; // Avoid accessing out-of-bounds

                double weightX = originalR - r1;

                for (int c = 0; c < inputCols; c++)
                {
                    output[r, c] = BilinearInterpolation(
                        input[r1, c], input[r2, c], weightX);
                }
            }

            return output;
        }

        private static double BilinearInterpolation(double p1, double p2, double weight)
        {
            return (1 - weight) * p1 + weight * p2;
        }



    }
}
