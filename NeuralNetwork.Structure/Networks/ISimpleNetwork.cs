using NeuralNetwork.Structure.Common;
using NeuralNetwork.Structure.Layers;
using NeuralNetwork.Structure.Nodes;
using NeuralNetwork.Structure.Synapses;
using System;
using System.Collections.Generic;

namespace NeuralNetwork.Structure.Networks
{

    public interface ISimpleNetwork : IInput<IEnumerable<double>>, IOutput<IEnumerable<double>>, IDataConductor<ISimpleNetwork, IEnumerable<double>>, IDisposable
    {

        /// <summary>
        /// All layers from input to output
        /// </summary>
        IEnumerable<IReadOnlyLayer<INode>> Layers { get; }

        /// <summary>
        /// Layer for input data. First layer of the network
        /// </summary>
        IReadOnlyLayer<IMasterNode> InputLayer { get; set; }

        /// <summary>
        /// Layer for output data. Last layer of the network
        /// </summary>
        IReadOnlyLayer<INotInputNode> OutputLayer { get; set; }

        /// <summary>
        /// Collection of synapses in the network
        /// </summary>
        ICollection<ISynapse> Synapses { get; }

        /// <summary>
        /// Add and connect synapse
        /// </summary>
        /// <param name="synapse"></param>
        void AddSynapse(ISynapse synapse);

    }

}
