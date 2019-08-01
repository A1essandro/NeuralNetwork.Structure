using NeuralNetwork.Structure.Contract.ActivationFunctions;

namespace NeuralNetwork.Structure.ActivationFunctions
{
    /// <summary>
    /// Function y(x) = a*x
    /// </summary>
    public class Linear : IActivationFunction
    {

        private readonly double _multiplier;

        public Linear()
            : this(1)
        {
        }

        public Linear(double multiplier)
        {
            _multiplier = multiplier;
        }

        public double GetEquation(double x) => _multiplier * x;

        public double GetDerivative(double x) => _multiplier;

    }
}
