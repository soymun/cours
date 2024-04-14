using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class CvsReader
    {
        public Dictionary<String, List<List<double>>> ReadCsv(String path)
        {
            Dictionary<String, List<List<double>>> result = new Dictionary<string, List<List<double>>>();

                using (TextFieldParser csvParser = new TextFieldParser(path))
                {
                    csvParser.CommentTokens = new string[] { "#" };
                    csvParser.SetDelimiters(new string[] { "," });
                    csvParser.HasFieldsEnclosedInQuotes = true;

                    while (!csvParser.EndOfData)
                    {
                        string[] fields = csvParser.ReadFields();

                        if (fields.Length < 6 || fields.Length > 7)
                        {
                            continue;
                        }

                        string country = fields[0];
                        double gdp, lifeExpectancy, inflation, unemploymentRate, mortalityRate = 0.0;
                        int year;

                        if (!int.TryParse(fields[1], out year) ||
                            !double.TryParse(fields[2], out gdp) ||
                            !double.TryParse(fields[3], out lifeExpectancy) ||
                            !double.TryParse(fields[4], out inflation) ||
                            !double.TryParse(fields[5], out unemploymentRate))
                        {
                            continue;
                        }

                        if (fields.Length == 6 & !double.TryParse(fields[6], out mortalityRate))
                        {
                            continue;
                        }

                        if (!result.ContainsKey(country))
                        {
                            result[country] = new List<List<double>>();
                        }

                        result[country].Add(new List<double> { year, gdp, lifeExpectancy, inflation, unemploymentRate, mortalityRate });
                    }
                }
            return result;
        }
    }
}
