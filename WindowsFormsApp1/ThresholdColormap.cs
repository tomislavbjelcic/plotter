using System;
using ScottPlot.Drawing;

namespace WindowsFormsApp1
{
    public class ThresholdColormap : IColormap
    {

        private readonly IColormap icmap;
        private readonly PiecewiseLinear f;


        public ThresholdColormap(IColormap icmap, params double[] fractionPoints)
        {
            this.icmap = icmap;
            double[] byteDoubleValues = new double[fractionPoints.Length];
            for (int i = 0; i<fractionPoints.Length; i++)
            {
                byteDoubleValues[i] = FractionToByteDouble(fractionPoints[i]);
            }
            f = new PiecewiseLinear(byteDoubleValues);
        }


        public string Name => icmap.Name;

        public (byte r, byte g, byte b) GetRGB(byte value) => icmap.GetRGB(MapValue(value));

        private byte MapValue(byte value)
        {
            //if (value <= lowValue) return byte.MinValue;
            //if (value >= highValue) return byte.MaxValue;
            //if (value == cmfValue) return cmfValue;
            //double r = value < cmfValue
            //    ? (cmfValue * (value - lowValue)) / ((double)(cmfValue - lowValue))
            //    : ((byte.MaxValue - cmfValue) * (value - highValue)) / ((double)(highValue - cmfValue)) + byte.MaxValue;
            //return (byte)r;

            return (byte)f.Apply(value);
        }


        private static double FractionToByteDouble(double fraction)
        {
            return (byte)(fraction * byte.MaxValue);
        }

    }
}
