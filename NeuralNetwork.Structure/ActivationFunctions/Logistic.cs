using NeuralNetwork.Structure.Contract.ActivationFunctions;
using System;

namespace NeuralNetwork.Structure.ActivationFunctions
{

    /// <summary>
    /// 
    /// </summary>
    public class Logistic : IActivationFunction
    {

        private readonly double _param;
        private static double _equation(double x, double alpha) => 1 / (1 + Math.Exp(-alpha * x));

        public Logistic()
            : this(1)
        {
        }

        public Logistic(double param)
        {
            _param = param;
        }

        public double GetEquation(double x) => _equation(x, _param);

        public double GetDerivative(double x)
        {
            var func = _equation(x, 1);
            return _param * func * (1 - func);
        }

    }
}
