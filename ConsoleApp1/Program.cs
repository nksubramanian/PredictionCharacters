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

            var candidateModels = Computation.BuildCandidateModels(data, maxBucketCount: 4);
            var selectedModel = candidateModels.OrderBy(x => x.Bic).First();


            Console.WriteLine();
            Console.WriteLine("===== SELECTED MODEL =====");
            Computation.Print(selectedModel);

        }
    }
}
