using NeuralNetwork.Structure.Common;
using NeuralNetwork.Structure.Layers;
using NeuralNetwork.Structure.Nodes;
using System.Collections.Generic;

namespace NeuralNetwork.Structure.Networks
{

    public interface ISimpleNetwork : IInput<IEnumerable<double>>, IOutput<IEnumerable<double>>
    {

        IReadOnlyLayer<IMasterNode> InputLayer { get; set; }

        IReadOnlyLayer<INotInputNode> OutputLayer { get; set; }

    }

}
