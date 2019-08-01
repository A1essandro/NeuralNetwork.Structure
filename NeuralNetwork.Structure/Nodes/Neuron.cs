using NeuralNetwork.Structure.ActivationFunctions;
using NeuralNetwork.Structure.Contract.ActivationFunctions;
using NeuralNetwork.Structure.Contract.Layers;
using NeuralNetwork.Structure.Contract.Nodes;
using NeuralNetwork.Structure.Contract.Summators;
using NeuralNetwork.Structure.Contract.Synapses;
using NeuralNetwork.Structure.Summators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Nodes
{

    public class Neuron : ISlaveNode
    {

        #region serialization data

        private readonly ICollection<ISynapse> _synapses = new List<ISynapse>();

        protected ISummator _summator;

        #endregion

        protected static IActivationFunction DefaultActivationFunction = new AsIs();

        protected static ISummator DefaultSummator => new Summator();

        #region public properties

        public virtual ISummator Summator
        {
            get => _summator;
            set
            {
                if (_summator != null)
                    _summator.OnResultCalculated -= Calculate;

                _summator = value;
                _summator.OnResultCalculated += Calculate;
            }
        }

        public virtual IActivationFunction Function { get; set; }

        public virtual double LastCalculatedValue { get; private set; } = double.NaN;

        #endregion

        public virtual event Func<INode, double, Task> OnResultCalculated;

        #region ctors

        public Neuron()
            : this(DefaultActivationFunction, DefaultSummator)
        {
        }

        public Neuron(IActivationFunction function, ISummator summator = null)
        {
            Function = function;
            Summator = summator ?? DefaultSummator;
        }

        #endregion

        public virtual void InsertInto(IReadOnlyLayer<INode> layer)
        {
        }

        public virtual void ConnectTo(ISynapse connectionElement)
        {
            _summator.ConnectTo(connectionElement);
        }

        public void RemoveFrom(IReadOnlyLayer<INode> parentStructure)
        {;
        }

        public void DisconnectFrom(ISynapse connectionElement)
        {
            _summator.DisconnectFrom(connectionElement);
        }

        protected virtual async Task Calculate(ISummator summator, double value)
        {
            var result = Function.GetEquation(value);

            LastCalculatedValue = result;

            if (OnResultCalculated != null)
                await OnResultCalculated(this, result);
        }

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
