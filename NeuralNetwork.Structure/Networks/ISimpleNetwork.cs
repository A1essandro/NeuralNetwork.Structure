using NeuralNetwork.Structure.Common;
using NeuralNetwork.Structure.Layers;
using NeuralNetwork.Structure.Nodes;
using NeuralNetwork.Structure.Synapses;
using System.Collections.Generic;

namespace NeuralNetwork.Structure.Networks
{

    public interface ISimpleNetwork : IInput<IEnumerable<double>>, IOutput<IEnumerable<double>>, IDataConductor<ISimpleNetwork, IEnumerable<double>>
    {

        IReadOnlyLayer<IMasterNode> InputLayer { get; set; }

        IReadOnlyLayer<INotInputNode> OutputLayer { get; set; }

        ICollection<ISynapse> Synapses { get; }

        void AddSynapse(ISynapse synapse);

    }

}
