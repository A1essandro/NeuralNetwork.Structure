﻿using NeuralNetwork.Structure.Contract.ActivationFunctions;
using System;

namespace NeuralNetwork.Structure.ActivationFunctions
{

    public class Gaussian : IActivationFunction
    {

        public double GetDerivative(double x) => -2 * x * GetEquation(x);

        public double GetEquation(double x) => Math.Exp(-x * x);

    }
}
