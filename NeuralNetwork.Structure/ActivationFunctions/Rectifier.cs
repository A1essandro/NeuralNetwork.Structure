﻿using NeuralNetwork.Structure.Contract.ActivationFunctions;

namespace NeuralNetwork.Structure.ActivationFunctions
{

    /// <summary>
    /// Rectifier activation function.
    /// </summary>
    /// <remarks>
    /// Any negative number to alpha*x. Any positive number leaves unchanged.
    /// </remarks>
    public class Rectifier : IActivationFunction
    {

        private readonly double _alpha;

        public Rectifier()
            : this(0)
        {
        }

        public Rectifier(double alpha)
        {
            _alpha = alpha;
        }

        public double GetEquation(double x) => x >= 0 ? x : _alpha * x;

        public double GetDerivative(double x) => x >= 0 ? 1 : _alpha;

    }
}
