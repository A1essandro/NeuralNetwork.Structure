using NeuralNetwork.Structure.Synapses;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Summators
{

    public class Summator : ISummator
    {

        private ConcurrentDictionary<ISynapse, double> _memory = new ConcurrentDictionary<ISynapse, double>();

        public virtual double LastCalculatedValue { get; private set; }

        public virtual event Func<ISummator, double, Task> OnResultCalculated;

        public virtual void ConnectTo(ISynapse connectionElement)
        {
            connectionElement.OnResultCalculated += _retain;

            _memory.TryAdd(connectionElement, double.NaN);
        }

        public void DisconnectFrom(ISynapse connectionElement)
        {
            connectionElement.OnResultCalculated -= _retain;

            _memory.TryRemove(connectionElement, out var _);
        }

        //TODO: need optimization
        private async Task _retain(ISynapse synapse, double value)
        {
            _memory[synapse] = value;

            if (_memory.Values.All(x => !double.IsNaN(x)))
            {
                var result = Calculate(_memory.Values);

                LastCalculatedValue = result;

                if (OnResultCalculated != null)
                {
                    await Notify(result);
                }

                foreach (var key in _memory.Keys)
                {
                    _memory[key] = double.NaN;
                }
            }
        }

        protected virtual double Calculate(ICollection<double> values) => values.Sum();

        private Task Notify(double value) => OnResultCalculated(this, value);

    }
}
