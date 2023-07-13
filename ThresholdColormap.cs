using System;
using ScottPlot.Drawing;

namespace WindowsFormsApp1
{
    public class ThresholdColormap : IColormap
    {
        private double high;
        public double High
        {
            get => high;
            set
            {
                high = value;
                highValue = FractionToByte(value);
            }
        }

        private double low;
        public double Low
        {
            get => low;
            set
            {
                low = value;
                lowValue = FractionToByte(value);
            }
        }

        private double cmf;
        public double Cmf
        {
            get => cmf;
            set
            {
                cmf = value;
                cmfValue = FractionToByte(value);
            }
        }


        private byte lowValue;
        private byte highValue;
        private byte cmfValue;
        public IColormap Icmap { get; set; }


        public ThresholdColormap(IColormap icmap, double cmf, double high = 1.0, double low = 0.0)
        {
            Icmap = icmap;
            Cmf = cmf;
            High = high;
            Low = low;
        }


        public string Name => Icmap.Name;

        public (byte r, byte g, byte b) GetRGB(byte value) => Icmap.GetRGB(MapValue(value));

        private byte MapValue(byte value)
        {
            if (value <= lowValue) return byte.MinValue;
            if (value >= highValue) return byte.MaxValue;
            if (value == cmfValue) return cmfValue;
            double r = value < cmfValue
                ? (cmfValue * (value - lowValue)) / ((double)(cmfValue - lowValue))
                : ((byte.MaxValue - cmfValue) * (value - highValue)) / ((double)(highValue - cmfValue)) + byte.MaxValue;
            return (byte)r;
        }

        private static byte FractionToByte(double fraction)
        {
            return (byte)(fraction * byte.MaxValue);
        }
    }
}
