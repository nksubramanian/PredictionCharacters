using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionLetter
{
    public class Observation
    {
        public int Length { get; set; }
        public double AvgWidth { get; set; }

        public Observation(int length, double avgWidth)
        {
            Length = length;
            AvgWidth = avgWidth;
        }
    }
}
