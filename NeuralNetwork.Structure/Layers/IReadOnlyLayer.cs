using NeuralNetwork.Structure.Common;
using NeuralNetwork.Structure.Nodes;
using System.Collections.Generic;

namespace NeuralNetwork.Structure.Layers
{

    public interface IReadOnlyLayer<out TNode> : IRefreshable, IOutput<IEnumerable<double>>
        where TNode : INode
    {

        IEnumerable<TNode> Nodes { get; }

        int NodesQuantity { get; }

    }

}