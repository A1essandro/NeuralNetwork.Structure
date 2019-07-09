using System;

namespace NeuralNetwork.Structure.ActivationFunctions
{
    public class Sinus : IActivationFunction
    {
        
        public double GetDerivative(double x) => Math.Cos(x);

        public double GetEquation(double x) => Math.Sin(x);

    }
}