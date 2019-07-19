using NeuralNetwork.Structure.Common;
using NeuralNetwork.Structure.Layers;

namespace NeuralNetwork.Structure.Nodes
{

    /// <summary>
    /// Interface for neurons in network
    /// </summary>
    public interface INode : INumberConductor<INode>, IChildStructure<IReadOnlyLayer<INode>>
    {

    }

}