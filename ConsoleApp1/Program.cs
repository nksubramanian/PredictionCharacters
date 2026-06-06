namespace PredictionLetter
{
    internal class Program
    {
        public static void Main()
        {
            var data = new List<Observation>
            {
                new Observation(1, 6.2),
                new Observation(2, 6.0),
                new Observation(3, 5.8),
                new Observation(4, 5.9),
                new Observation(5, 5.7),
                new Observation(6, 5.1),
                new Observation(7, 5.0),
                new Observation(8, 4.9)
            };

            BucketModel bestModel = null;
            double bestBic = double.MaxValue;
            int bestBucketCount = 0;
            var candidateModels = new List<(int BucketCount, double Bic, BucketModel Model)>();

            for (int bucketCount = 1; bucketCount <= 4; bucketCount++)
            {
                Console.WriteLine();
                Console.WriteLine($"===== {bucketCount} Bucket(s) =====");

                BucketModel model = BucketFinder.FindBestModel(data, bucketCount);

                Console.WriteLine($"Total Cost = {model.Cost:F4}");
                model.Buckets.ForEach(Console.WriteLine);

                int parameterCount = 2 * bucketCount - 1;

                double bic =
                    data.Count * Math.Log(model.Cost / data.Count)
                    + parameterCount * Math.Log(data.Count);

                Console.WriteLine($"BIC = {bic:F4}");

                candidateModels.Add(
                (
                    BucketCount: bucketCount,
                    Bic: bic,
                    Model: model
                ));
            }

            var selectedModel = candidateModels.OrderBy(x => x.Bic).First();


            Console.WriteLine();
            Console.WriteLine("===== SELECTED MODEL =====");
            Console.WriteLine($"Bucket Count = {selectedModel.BucketCount}");
            Console.WriteLine($"BIC = {selectedModel.Bic:F4}");

            foreach (Bucket bucket in selectedModel.Model.Buckets)
            {
                Console.WriteLine(bucket);
            }
        }
    }
}
