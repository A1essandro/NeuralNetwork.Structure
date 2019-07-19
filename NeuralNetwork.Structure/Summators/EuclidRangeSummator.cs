using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork.Structure.Summators
{

    public class EuclidRangeSummator : Summator
    {

        protected override double Calculate(ICollection<double> values) => Math.Sqrt(values.Sum());

    }
}
