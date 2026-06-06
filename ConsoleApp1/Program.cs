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

            for (int bucketCount = 1;
                 bucketCount <= 4;
                 bucketCount++)
            {
                Console.WriteLine();
                Console.WriteLine($"===== {bucketCount} Bucket(s) =====");

                BucketModel model =
                    BucketFinder.FindBestModel(
                        data,
                        bucketCount);

                Console.WriteLine(
                    $"Total Cost = {model.Cost:F4}");

                foreach (Bucket bucket in model.Buckets)
                {
                    Console.WriteLine(bucket);
                }
            }
        }
    }
}
