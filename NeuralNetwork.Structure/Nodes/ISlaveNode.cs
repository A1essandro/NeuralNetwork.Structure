using NeuralNetwork.Structure.ActivationFunctions;
using NeuralNetwork.Structure.Common;
using NeuralNetwork.Structure.Summators;
using NeuralNetwork.Structure.Synapses;
using System.Collections.Generic;

namespace NeuralNetwork.Structure.Nodes
{

    /// <summary>
    /// Node that has synapses
    /// </summary>
    public interface ISlaveNode : INotInputNode, IConnectedElement<ISynapse>
    {

        /// <summary>
        /// Collection of synapses to this node
        /// </summary>
        ICollection<ISynapse> Synapses { get; }

        /// <summary>
        /// Adding synapse from master node to this node
        /// </summary>
        /// <param name="synapse"></param>
        void AddSynapse(ISynapse synapse);

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
