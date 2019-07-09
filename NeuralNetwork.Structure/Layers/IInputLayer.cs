﻿using System.Collections.Generic;
using NeuralNetwork.Structure.Common;
using NeuralNetwork.Structure.Nodes;

namespace NeuralNetwork.Structure.Layers
{
    public interface IInputLayer : ILayer<IMasterNode>, IInput<IEnumerable<double>>
    {
    }

}
