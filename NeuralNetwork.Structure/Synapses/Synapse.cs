﻿using NeuralNetwork.Structure.Contract.Networks;
using NeuralNetwork.Structure.Contract.Nodes;
using NeuralNetwork.Structure.Contract.Synapses;
using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Synapses
{

    /// <summary>
    /// Synapse gets output from neuron-transmitter and convert the value via its weight.
    /// Result value gets neuron-reciever.
    /// </summary>
    public class Synapse : ISynapse
    {

        /// <summary>
        /// Transmitter node
        /// </summary>
        public virtual INode MasterNode { get; protected set; }

        /// <summary>
        /// Receiver node
        /// </summary>
        public virtual ISlaveNode SlaveNode { get; protected set; }

        /// <summary>
        /// Current weight of synapse
        /// </summary>
        public double Weight { get; set; }

        public double LastCalculatedValue { get; private set; }

        public event Func<ISynapse, double, Task> OnResultCalculated;

        /// <summary>
        /// Change value of weight
        /// </summary>
        /// <param name="delta">Delta value for change</param>
        public void ChangeWeight(double delta)
        {
            Weight += delta;
        }

        public Synapse(INode masterNode, ISlaveNode slaveNode, double weight)
        {
            System.Diagnostics.Contracts.Contract.Requires(masterNode != null, nameof(masterNode));

            MasterNode = masterNode;
            SlaveNode = slaveNode;
            Weight = weight;

            ConnectTo(masterNode);
        }

        public virtual void ConnectTo(INode connectionElement)
        {
            MasterNode = connectionElement;

            MasterNode.OnResultCalculated += _conductData;
        }

        public void DisconnectFrom(INode connectionElement)
        {
            MasterNode = null;

            MasterNode.OnResultCalculated -= _conductData;
        }

        public void InsertInto(ISimpleNetwork parentStructure)
        {
            SlaveNode.ConnectTo(this);
        }

        public void RemoveFrom(ISimpleNetwork parentStructure)
        {
            SlaveNode.DisconnectFrom(this);
        }

        #region private methods

        private async Task _conductData(INode masterNode, double data)
        {
            var result = _calculate(data);

            LastCalculatedValue = result;

            if (OnResultCalculated != null)
                await OnResultCalculated(this, result);
        }

        private double _calculate(double data) => Weight * data;

        #endregion

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    OnResultCalculated = null;
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
