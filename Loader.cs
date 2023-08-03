
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System;

namespace WindowsFormsApp1
{
    public static class Loader
    {

        public static (double, double, double, List<double[]>) LoadPlotData(string fileName, List<double[]> dest = null, int removeEvery = -1)
        {
            double innerDiameter;
            double outerDiameter;
            double spacing;
            List<double[]> data = null;
            
            using (StreamReader sr = File.OpenText(fileName))
            {
                innerDiameter = double.Parse(sr.ReadLine(), CultureInfo.InvariantCulture);
                outerDiameter = double.Parse(sr.ReadLine(), CultureInfo.InvariantCulture);
                spacing = double.Parse(sr.ReadLine(), CultureInfo.InvariantCulture);
                data = dest is null ? new List<double[]>() : dest;
                sr.ReadLine();

                int row = 0;
                while (!sr.EndOfStream)
                {
                    

                    string[] splitted = sr.ReadLine().Split(',');
                    row++;
                    if (removeEvery > 0 && (row % removeEvery == 0))
                        continue;

                    double[] radiuses = new double[splitted.Length];
                    for (int j = 0; j < radiuses.Length; j++)
                    {
                        radiuses[j] = double.Parse(splitted[j], CultureInfo.InvariantCulture);
                    }
                    data.Add(radiuses);
                }
            }
            //GC.Collect();

            return (innerDiameter, outerDiameter, spacing, data);
        }


    }
}
