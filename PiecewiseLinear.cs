

using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;

namespace WindowsFormsApp1
{
    public class PiecewiseLinear
    {

        private readonly double[] xarr;
        private readonly double[] yarr;


        public PiecewiseLinear(params double[] xy)
        {
            if (xy.Length % 2 != 0)
            {
                throw new ArgumentException("Uneven number of arguments");
            }
            if (xy.Length < 4)
            {
                throw new ArgumentException("Need at least 2 points");
            }
            // Unsorted points, bijection, closeness of points is not being checked
            
            int len = xy.Length / 2;
            xarr = new double[len];
            yarr = new double[len];

            for (int i = 0; i < len; i++)
            {
                xarr[i] = xy[2*i];
                yarr[i] = xy[2*i + 1];
            }
        }

        public double Apply(double arg, bool inverse = false)
        {
            double[] domain = inverse ? yarr : xarr;
            double[] range = inverse ? xarr : yarr;

            if (arg < domain[0]) return range[0];
            if (arg > domain[domain.Length - 1]) return range[range.Length - 1];

            int idx = Array.BinarySearch(domain, arg);
            if (idx >= 0)
            {
                // rarely gonna happen but the argument is one of the points
                return range[idx];
            }

            idx = ~idx - 1;
            return range[idx] 
                + (range[idx + 1] - range[idx]) * (arg - domain[idx]) / (domain[idx + 1] - domain[idx]);

        }



    }
}
