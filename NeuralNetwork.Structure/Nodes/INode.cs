using NeuralNetwork.Structure.Common;
using NeuralNetwork.Structure.Layers;

namespace NeuralNetwork.Structure.Nodes
{

    /// <summary>
    /// Interface for neurons in network
    /// </summary>
    public interface INode : IOutput<double>, INumberConductor<INode>, IChildStructure<IReadOnlyLayer<INode>>
    {

    }

}