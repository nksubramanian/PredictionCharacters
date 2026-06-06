using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionLetter
{
    public class CandidateModel
    {
        public int BucketCount { get; set; }
        public double Bic { get; set; }
        public BucketModel Model { get; set; }
    }
}
