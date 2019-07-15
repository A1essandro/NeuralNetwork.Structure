﻿using NeuralNetwork.Structure.Nodes;
using System;
using System.Diagnostics.Contracts;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Synapses
{

    /// <summary>
    /// Synapse gets output from neuron-transmitter and convert the value via its weight.
    /// Result value gets neuron-reciever.
    /// </summary>
    [DataContract]
    public class Synapse : ISynapse
    {

        /// <summary>
        /// Node transmitter
        /// </summary>
        [DataMember]
        public INode MasterNode { get; set; }

        /// <summary>
        /// Current weight of synapse
        /// </summary>
        [DataMember]
        public double Weight { get; set; }

        public event Action<double> OnOutput;

        public event Func<ISynapse, double, Task> OnResultCalculated;

        /// <summary>
        /// Change value of weight
        /// </summary>
        /// <param name="delta">Delta value for change</param>
        public void ChangeWeight(double delta)
        {
            Weight += delta;
        }

        public async Task<double> Output()
        {
            var result = Weight * await MasterNode.Output().ConfigureAwait(false);
            OnOutput?.Invoke(result);

            return result;
        }

        public Synapse()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="masterNode">Node-transmitter</param>
        /// <param name="weight">Initial weight</param>
        public Synapse(INode masterNode, double weight)
        {
            Contract.Requires(masterNode != null, nameof(masterNode));

            MasterNode = masterNode;
            Weight = weight;

            MasterNode.OnResultCalculated += _conductData;
        }

        private async Task _conductData(INode synapse, double data)
        {
            var result = await Output();

            await OnResultCalculated(this, result);
        }

    }
}