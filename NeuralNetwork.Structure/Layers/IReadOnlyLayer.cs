using NeuralNetwork.Structure.Common;
using NeuralNetwork.Structure.Networks;
using NeuralNetwork.Structure.Nodes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Layers
{

    public interface IReadOnlyLayer<out TNode> : IOutput<IEnumerable<double>>
        where TNode : INode
    {

        void AttachToNetwork(ISimpleNetwork network);

        IEnumerable<TNode> Nodes { get; }

        int NodesQuantity { get; }

        event Func<IReadOnlyLayer<TNode>, IEnumerable<double>, Task> OnNetworkInput;

    }

}