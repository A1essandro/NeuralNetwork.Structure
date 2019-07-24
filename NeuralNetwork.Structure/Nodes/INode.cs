using NeuralNetwork.Structure.Common;
using NeuralNetwork.Structure.Layers;
using System;

namespace NeuralNetwork.Structure.Nodes
{

    /// <summary>
    /// Interface for neurons in network
    /// </summary>
    public interface INode : INumberConductor<INode>, IChildStructure<IReadOnlyLayer<INode>>, IDisposable
    {

    }

}