using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionLetter
{
    public static class BucketFinder
    {
        public static BucketModel FindBestModel(
            List<Observation> data,
            int bucketCount)
        {
            if (bucketCount < 1)
                throw new ArgumentException("Bucket count must be >= 1");

            if (bucketCount > data.Count)
                throw new ArgumentException("Bucket count cannot exceed number of observations");

            data = data
                .OrderBy(x => x.Length)
                .ToList();

            double bestCost = double.MaxValue;
            List<int> bestSplits = null;

            SearchSplits(
                data,
                bucketCount,
                new List<int>(),
                ref bestCost,
                ref bestSplits);

            return new BucketModel
            {
                Cost = bestCost,
                Buckets = BuildBuckets(data, bestSplits)
            };
        }

        private static void SearchSplits(
            List<Observation> data,
            int bucketCount,
            List<int> currentSplits,
            ref double bestCost,
            ref List<int> bestSplits)
        {
            int requiredSplits = bucketCount - 1;

            if (currentSplits.Count == requiredSplits)
            {
                double cost = CalculateCost(data, currentSplits);

                if (cost < bestCost)
                {
                    bestCost = cost;
                    bestSplits = new List<int>(currentSplits);
                }

                return;
            }

            int start =
                currentSplits.Count == 0
                    ? 1
                    : currentSplits.Last() + 1;

            for (int split = start;
                 split < data.Count;
                 split++)
            {
                currentSplits.Add(split);

                SearchSplits(
                    data,
                    bucketCount,
                    currentSplits,
                    ref bestCost,
                    ref bestSplits);

                currentSplits.RemoveAt(currentSplits.Count - 1);
            }
        }

        private static double CalculateCost(
            List<Observation> data,
            List<int> splits)
        {
            double totalCost = 0;

            int start = 0;

            foreach (int split in splits)
            {
                var bucketValues = data
                    .Skip(start)
                    .Take(split - start)
                    .Select(x => x.AvgWidth);

                totalCost += Variance(bucketValues);

                start = split;
            }

            totalCost += Variance(
                data
                    .Skip(start)
                    .Select(x => x.AvgWidth));

            return totalCost;
        }

        private static double Variance(
            IEnumerable<double> values)
        {
            List<double> list = values.ToList();

            if (list.Count <= 1)
                return 0;

            double mean = list.Average();

            return list.Sum(
                x => Math.Pow(x - mean, 2));
        }

        private static List<Bucket> BuildBuckets(
            List<Observation> data,
            List<int> splits)
        {
            var buckets = new List<Bucket>();

            int start = 0;

            foreach (int split in splits)
            {
                var values = data
                    .Skip(start)
                    .Take(split - start)
                    .ToList();

                buckets.Add(CreateBucket(values));

                start = split;
            }

            buckets.Add(
                CreateBucket(
                    data
                        .Skip(start)
                        .ToList()));

            return buckets;
        }

        private static Bucket CreateBucket(
            List<Observation> values)
        {
            return new Bucket
            {
                MinLength = values.First().Length,
                MaxLength = values.Last().Length,
                PredictedWidth =
                    values.Average(x => x.AvgWidth)
            };
        }
    }
}
