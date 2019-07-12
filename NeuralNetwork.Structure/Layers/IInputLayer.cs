using System.Collections.Generic;
using NeuralNetwork.Structure.Common;
using NeuralNetwork.Structure.Nodes;

namespace NeuralNetwork.Structure.Layers
{

    /// <summary>
    /// Input layer of network
    /// </summary>
    public interface IInputLayer : ILayer<IMasterNode>, IInput<IEnumerable<double>>
    {
    }

}
