using NeuralNetwork.Structure.Common;
using NeuralNetwork.Structure.Layers;
using NeuralNetwork.Structure.Nodes;
using System.Collections.Generic;

namespace NeuralNetwork.Structure.Networks
{

    public interface INetwork : IRefreshable, IInput<IEnumerable<double>>, IOutput<IEnumerable<double>>
    {

        IReadOnlyLayer<IMasterNode> InputLayer { get; }

        ICollection<IReadOnlyLayer<INotInputNode>> Layers { get; }

        IReadOnlyLayer<INotInputNode> OutputLayer { get; }

    }

}
