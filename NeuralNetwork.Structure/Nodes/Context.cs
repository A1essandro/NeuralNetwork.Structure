﻿using NeuralNetwork.Structure.ActivationFunctions;
using NeuralNetwork.Structure.Summators;
using NeuralNetwork.Structure.Synapses;
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

        private bool _calculated = false;

        private readonly Queue<double> _memory;

        private double _currentValue;

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
            : base(function ?? Neuron.DefaultActivationFunction, synapses ?? new List<ISynapse>(), summator ?? Neuron.DefaultSummator)
        {
            _memory = new Queue<double>(Enumerable.Repeat(0.0, delay));
            Delay = delay;
        }

        /// <summary>
        /// Calculates output with delay <see cref="Delay"/>
        /// </summary>
        /// <returns></returns>
        public override async Task<double> Output()
        {
            _memory.Enqueue(await base.Output());
            _currentValue = _memory.Dequeue();

            return _currentValue;
        }

    }
}