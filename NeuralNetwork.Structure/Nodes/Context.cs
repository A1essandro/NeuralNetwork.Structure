using NeuralNetwork.Structure.ActivationFunctions;
using NeuralNetwork.Structure.Summators;
using NeuralNetwork.Structure.Synapses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Nodes
{

    /// <summary>
    /// Node with memory for Elman networks or Jordan networks
    /// </summary>
    public class Context : Neuron
    {

        public override event Func<INode, double, Task> OnResultCalculated;

        private readonly Queue<double> _memory;

        /// <summary>
        /// Delay between input and appropriate output
        /// </summary>
        public ushort Delay { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="function"></param>
        /// <param name="delay"><see cref="Delay"/></param>
        /// <param name="synapses"><see cref="Synapses"/> Create empty list if null</param>
        /// <param name="summator"></param>
        public Context(IActivationFunction function = null, ushort delay = 1, ICollection<ISynapse> synapses = null, ISummator summator = null)
            : base(function ?? DefaultActivationFunction, summator ?? DefaultSummator)
        {
            _memory = new Queue<double>(Enumerable.Repeat(0.0, delay));
            Delay = delay;
        }

        protected override async Task Calculate(ISummator summator, double value)
        {
            var result = Function.GetEquation(value);

            _memory.Enqueue(result);

            if (OnResultCalculated != null)
                await OnResultCalculated(this, _memory.Dequeue());
        }

    }
}
