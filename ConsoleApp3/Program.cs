using ConsoleApp3;
using MathNet.Numerics;
using MathNet.Numerics.Statistics;

double[] matrix = {24,27, 26, 21  ,20, 31, 26, 22,20,18, 30, 29,24,26};

double[] matrixy = {
100,115,117,119,134, 94,105, 103,111,124, 122,109,110,86};

double[] p = Fit.Polynomial(matrix, matrixy, 2);

for (int i = 0; i < p.Length; i++) {

    Console.WriteLine(p[i]);
}