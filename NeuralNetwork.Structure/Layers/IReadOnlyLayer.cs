using NeuralNetwork.Structure.Common;
using NeuralNetwork.Structure.Networks;
using NeuralNetwork.Structure.Nodes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Layers
{

    public interface IReadOnlyLayer<out TNode> : IChildStructure<ISimpleNetwork>
        where TNode : INode
    {

        IEnumerable<TNode> Nodes { get; }

        int NodesQuantity { get; }

        event Func<IReadOnlyLayer<TNode>, IEnumerable<double>, Task> OnNetworkInput;

    }

}