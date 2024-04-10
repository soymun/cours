using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Correlition
    {
        public static double[] PearsonCorrelation(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            int lenght = matrix.GetLength(1);
            int m = matrix.GetLength(1);
            double[,] correlations = new double[lenght, m];

            for (int i = 0; i < lenght; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    double sumX = 0, sumY = 0, sumXSq = 0, sumYSq = 0, sumXY = 0;
                    for (int k = 0; k < n; k++)
                    {
                        double x = matrix[k, i];
                        double y = matrix[k, j];
                        sumX += x;
                        sumY += y;
                        sumXSq += Math.Pow(x, 2);
                        sumYSq += Math.Pow(y, 2);
                        sumXY += x * y;
                    }
                    double numerator = n * sumXY - sumX * sumY;
                    double denominator = Math.Sqrt((n * sumXSq - Math.Pow(sumX, 2)) * (n * sumYSq - Math.Pow(sumY, 2)));
                    correlations[i, j] = numerator / denominator;
                }
            }

            double[] result = new double[m-1];
            for (int i = 0; i < m-1; i++) {
                result[i] = Math.Round(correlations[lenght-1, i], 4);
            }

            return result;
        }
    }
}
