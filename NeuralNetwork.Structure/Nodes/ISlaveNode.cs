using NeuralNetwork.Structure.ActivationFunctions;
using NeuralNetwork.Structure.Common;
using NeuralNetwork.Structure.Summators;
using NeuralNetwork.Structure.Synapses;

namespace NeuralNetwork.Structure.Nodes
{

    /// <summary>
    /// Node that has synapses
    /// </summary>
    public interface ISlaveNode : INotInputNode, IConnectedElement<ISynapse>
    {

        /// <summary>
        /// Activation function
        /// </summary>
        /// <value></value>
        IActivationFunction Function { get; set; }

        /// <summary>
        /// Summator
        /// </summary>
        /// <value></value>
        ISummator Summator { get; set; }

    }

}
