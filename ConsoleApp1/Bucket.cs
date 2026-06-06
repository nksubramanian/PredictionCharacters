using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionLetter
{
    public class Bucket
    {
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public double PredictedWidth { get; set; }

        public override string ToString()
        {
            return $"Length {MinLength}-{MaxLength} => Predicted Width {PredictedWidth:F3}";
        }
    }
}
