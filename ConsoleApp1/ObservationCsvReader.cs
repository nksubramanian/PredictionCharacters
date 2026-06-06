using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionLetter
{
    public static class ObservationCsvReader
    {
        public static List<Observation> Read(string csvPath)
        {
            var observations = new List<Observation>();

            var lines = File.ReadAllLines(csvPath);

            foreach (string line in lines.Skip(1)) // Skip header
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] parts = line.Split(',');

                int length =
                    int.Parse(parts[0].Trim());

                double avgWidth =
                    double.Parse(
                        parts[1].Trim(),
                        CultureInfo.InvariantCulture);

                observations.Add(
                    new Observation(
                        length,
                        avgWidth));
            }

            return observations;
        }
    }
}
