using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Regression
    {

        public int _degree;
        public int _numFeatures;
        public double[,] _coefficients;

        public Regression(int degree, int numFeatures)
        {
            _degree = degree;
            _numFeatures = numFeatures;
            _coefficients = new double[_getNumCoefficients(_degree, _numFeatures), 1];
        }

        public void Fit(double[,] X, double[] y)
        {
            int n = X.GetLength(0);
            double[,] A = new double[_getNumCoefficients(_degree, _numFeatures), n];

            // Формируем матрицу A
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < _numFeatures; j++)
                {
                    for (int k = 0; k <= _degree; k++)
                    {
                        int index = _getIndex(_degree, _numFeatures, j, k);
                        A[index, i] = Math.Pow(X[i, j], k);
                    }
                }
            }

            // Формируем вектор b
            double[] b = new double[n];
            for (int i = 0; i < n; i++)
            {
                b[i] = y[i];
            }

            // Решаем систему линейных уравнений Ax=b с помощью метода Гаусса с выбором главного элемента
            _coefficients = _solveLinearSystem(A, b);
        }

        public double Predict(double[] x)
        {
            double result = 0;

            for (int i = 0; i < _numFeatures; i++)
            {
                for (int j = 0; j <= _degree; j++)
                {
                    int index = _getIndex(_degree, _numFeatures, i, j);
                    result += _coefficients[index, 0] * Math.Pow(x[i], j);
                }
            }

            return result;
        }

        private int _getNumCoefficients(int degree, int numFeatures)
        {
            int numCoefficients = 0;

            for (int i = 0; i <= degree; i++)
            {
                numCoefficients += (int)Math.Pow(numFeatures + i - 1, i) / i!;
            }

            return numCoefficients;
        }

        private int _getIndex(int degree, int numFeatures, int featureIndex, int degreeIndex)
        {
            int index = 0;

            for (int i = 0; i < featureIndex; i++)
            {
                index += (int)Math.Pow(numFeatures + degree - 1, degree) / degree!;
            }

            for (int i = degreeIndex + 1; i <= degree; i++)
            {
                index += (int)Math.Pow(numFeatures + i - 2, i - 1) / (i - 1)!;
            }

            return index;
        }

        private double[,] _solveLinearSystem(double[,] A, double[] b)
        {
            int n = A.GetLength(0);
            int m = A.GetLength(1);

            double[,] augmentedMatrix = new double[n, m + 1];

            // Формируем расширенную матрицу
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    augmentedMatrix[i, j] = A[i, j];
                }

                augmentedMatrix[i, m] = b[i];
            }

            // Преобразуем расширенную матрицу к ступенчатому виду с помощью метода Гаусса с выбором главного элемента
            for (int i = 0; i < n; i++)
            {
                int maxIndex = i;

                for (int j = i + 1; j < n; j++)
                {
                    if (Math.Abs(augmentedMatrix[j, i]) > Math.Abs(augmentedMatrix[maxIndex, i]))
                    {
                        maxIndex = j;
                    }
                }

                if (maxIndex != i)
                {
                    _swapRows(augmentedMatrix, i, maxIndex);
                }

                for (int j = i + 1; j < n; j++)
                {
                    double factor = augmentedMatrix[j, i] / augmentedMatrix[i, i];

                    for (int k = i; k <= m; k++)
                    {
                        augmentedMatrix[j, k] -= factor * augmentedMatrix[i, k];
                    }
                }
            }

            // Возвращаем решение системы линейных уравнений
            double[,] solution = new double[n, 1];

            for (int i = n - 1; i >= 0; i--)
            {
                double sum = 0;

                for (int j = i + 1; j < n; j++)
                {
                    sum += augmentedMatrix[i, j] * solution[j, 0];
                }

                solution[i, 0] = (augmentedMatrix[i, m] - sum) / augmentedMatrix[i, i];
            }

            return solution;
        }

        private void _swapRows(double[,] matrix, int i, int j)
        {
            int n = matrix.GetLength(1);

            for (int k = 0; k < n; k++)
            {
                double temp = matrix[i, k];
                matrix[i, k] = matrix[j, k];
                matrix[j, k] = temp;
            }
        }
    }
}
