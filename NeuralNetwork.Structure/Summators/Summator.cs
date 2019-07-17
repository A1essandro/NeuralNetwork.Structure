using NeuralNetwork.Structure.Nodes;
using NeuralNetwork.Structure.Synapses;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Summators
{

    [DataContract]
    public class Summator : ISummator
    {

        private ConcurrentDictionary<ISynapse, double> _memory = new ConcurrentDictionary<ISynapse, double>();

        public event Func<ISummator, double, Task> OnResultCalculated;

        public void ConnectTo(ISynapse connectionElement)
        {
            connectionElement.OnResultCalculated += _retain;

            _memory.TryAdd(connectionElement, double.NaN);
        }

        [Obsolete]
        public async Task<double> GetSum(ISlaveNode node)
        {
            var tasks = node.Synapses.Select(x => x.Output());
            var tasksResult = await Task.WhenAll(tasks).ConfigureAwait(false);

            return tasksResult.Sum();
        }

        //TODO: need optimization
        private async Task _retain(ISynapse synapse, double value)
        {
            _memory[synapse] = value;

            if (_memory.Values.All(x => !double.IsNaN(x)))
            {
                if (OnResultCalculated != null)
                    await OnResultCalculated(this, _memory.Values.Sum());

                foreach (var key in _memory.Keys)
                {
                    _memory[key] = double.NaN;
                }
            }
        }

    }
}
