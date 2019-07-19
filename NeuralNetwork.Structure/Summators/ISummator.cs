using NeuralNetwork.Structure.Common;
using NeuralNetwork.Structure.Synapses;

namespace NeuralNetwork.Structure.Summators
{
    public interface ISummator : IConnectedElement<ISynapse>, INumberConductor<ISummator>
    {

    }
}