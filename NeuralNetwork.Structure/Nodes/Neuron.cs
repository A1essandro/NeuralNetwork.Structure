using NeuralNetwork.Structure.ActivationFunctions;
using NeuralNetwork.Structure.Layers;
using NeuralNetwork.Structure.Summators;
using NeuralNetwork.Structure.Synapses;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Nodes
{

    [DataContract]
    [KnownType(typeof(Summator))]
    [KnownType(typeof(Synapse))]
    [KnownType(typeof(Rectifier))]
    [KnownType(typeof(Logistic))]
    [KnownType(typeof(Linear))]
    [KnownType(typeof(Gaussian))]
    [KnownType(typeof(AsIs))]
    public class Neuron : ISlaveNode
    {

        #region serialization data

        [DataMember]
        private readonly ICollection<ISynapse> _synapses = new List<ISynapse>();

        [DataMember]
        protected ISummator _summator;

        [DataMember]
        private IActivationFunction _actFunction;

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

        public IActivationFunction Function
        {
            get => _actFunction;
            set => _actFunction = value;
        }

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
            _actFunction = function;
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

        protected virtual async Task Calculate(ISummator summator, double value)
        {
            var result = _actFunction.GetEquation(value);

            LastCalculatedValue = result;

            if (OnResultCalculated != null)
                await OnResultCalculated(this, result);
        }

    }

}
