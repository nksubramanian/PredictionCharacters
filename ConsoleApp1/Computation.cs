using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionLetter
{
    public class Computation
    {
        public static List<CandidateModel> BuildCandidateModels(
            List<Observation> data,
            int maxBucketCount)
        {
            var candidateModels = new List<CandidateModel>();

            for (int bucketCount = 1;
                 bucketCount <= maxBucketCount;
                 bucketCount++)
            {
                Console.WriteLine();
                Console.WriteLine(
                    $"===== {bucketCount} Bucket(s) =====");

                BucketModel model =
                    BucketFinder.FindBestModel(
                        data,
                        bucketCount);

                Console.WriteLine(
                    $"Total Cost = {model.Cost:F4}");

                model.Buckets.ForEach(Console.WriteLine);

                int parameterCount =
                    2 * bucketCount - 1;

                double bic =
                    data.Count *
                    Math.Log(model.Cost / data.Count)
                    +
                    parameterCount *
                    Math.Log(data.Count);

                Console.WriteLine(
                    $"BIC = {bic:F4}");

                candidateModels.Add(
                    new CandidateModel
                    {
                        BucketCount = bucketCount,
                        Bic = bic,
                        Model = model
                    });
            }

            return candidateModels;
        }

        public static void Print(CandidateModel candidateModel)
        {
            Console.WriteLine($"Bucket Count = {candidateModel.BucketCount}");
            Console.WriteLine($"BIC = {candidateModel.Bic:F4}");

            foreach (Bucket bucket in candidateModel.Model.Buckets)
            {
                Console.WriteLine(bucket);
            }

        }

    }
}
