using System;
using System.Threading.Tasks;
using NeuralNetwork.Structure.Common;
using NeuralNetwork.Structure.Nodes;
using NeuralNetwork.Structure.Synapses;

namespace NeuralNetwork.Structure.Summators
{
    public interface ISummator : IConnectedElement<ISynapse>, INumberConductor<ISummator>
    {

        [Obsolete]
        Task<double> GetSum(ISlaveNode node);

    }
}