using NeuralNetwork.Structure.Contract.Common;
using NeuralNetwork.Structure.Contract.Layers;
using NeuralNetwork.Structure.Contract.Networks;
using NeuralNetwork.Structure.Contract.Nodes;
using NeuralNetwork.Structure.Contract.Synapses;
using NeuralNetwork.Structure.Internal.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Networks
{

    public class SimpleNetwork : ISimpleNetwork
    {

        private IReadOnlyLayer<IMasterNode> _inputLayer;
        private IReadOnlyLayer<INotInputNode> _outputLayer;
        private readonly ICollection<ISynapse> _synapses = new List<ISynapse>();

        private IDictionary<INode, int> _outputPositions;
        private double[] _output;

        protected SemaphoreSlim ProcessingLocker = new SemaphoreSlim(1, 1);

        #region Events

        public event Action<IEnumerable<double>> OnOutput;

        public event Func<IInput<IEnumerable<double>>, IEnumerable<double>, Task> OnInput;

        public event Func<ISimpleNetwork, IEnumerable<double>, Task> OnResultCalculated;

        #endregion

        #region Public properties

        public virtual IReadOnlyLayer<IMasterNode> InputLayer
        {
            get => _inputLayer;
            set
            {
                _inputLayer = value;
                _inputLayer.InsertInto(this);
            }
        }

        public virtual IReadOnlyLayer<INotInputNode> OutputLayer
        {
            get => _outputLayer;
            set
            {
                _outputLayer = value;
                _outputLayer.InsertInto(this);

                var nodesQty = _outputLayer.Nodes.Count();
                _outputPositions = new Dictionary<INode, int>();
                _output = new double[nodesQty];
                var i = 0;
                foreach (var node in _outputLayer.Nodes)
                {
                    _outputPositions.Add(node, i++);
                    node.OnResultCalculated += _processOutput;
                }
            }
        }

        public virtual ICollection<ISynapse> Synapses => _synapses;

        /// <summary>
        /// All layers from input to output
        /// </summary>
        public virtual IEnumerable<IReadOnlyLayer<INode>> Layers
        {
            get
            {
                yield return InputLayer;
                yield return OutputLayer;
            }
        }

        public virtual IEnumerable<double> LastCalculatedValue { get; protected set; }

        #endregion

        /// <summary> 
        /// Write input value to each input-neuron (<see cref="IInput{double}"/>) in input-layer.
        /// </summary>
        /// <param name="input"></param>
        public virtual async Task Input(IEnumerable<double> input)
        {
            System.Diagnostics.Contracts.Contract.Requires(input != null, nameof(input));

            using (await ProcessingLocker.UseWaitAsync())
            {
                await _processInput(input);

                LastCalculatedValue = _output;
                if (OnResultCalculated != null)
                    await OnResultCalculated(this, _output);
            }
        }

        public virtual async Task<IEnumerable<double>> Output()
        {
            using (await ProcessingLocker.UseWaitAsync())
            {
                OnOutput?.Invoke(_output);

                return _output;
            }
        }

        public ISimpleNetwork AddSynapse(ISynapse synapse)
        {
            using (ProcessingLocker.UseWait())
            {
                System.Diagnostics.Contracts.Contract.Requires(synapse != null, nameof(synapse));

                _synapses.Add(synapse);

                synapse.InsertInto(this);

                return this;
            }
        }

        #region private methods

        private async Task _processInput(IEnumerable<double> input)
        {
            if (OnInput != null)
                await OnInput(this, input);

            await _inputToNodes(input);
        }

        private Task _inputToNodes(IEnumerable<double> input)
        {
            var inputNodes = _inputLayer.Nodes.OfType<IInputNode>().ToArray();
            var taskList = new List<Task>(inputNodes.Length);

            var index = 0;
            foreach (var value in input)
            {
                taskList.Add(inputNodes[index++].Input(value));
            }

            return Task.WhenAll(taskList);
        }

        private Task _processOutput(INode node, double value)
        {
            var position = _outputPositions[node];
            _output[position] = value;

            return Task.CompletedTask;
        }

        #endregion

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    OnInput = null;
                    OnResultCalculated = null;
                    OnOutput = null;

                    foreach(var synapse in Synapses)
                        synapse.Dispose();
                    foreach (var layer in Layers)
                        layer.Dispose();
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
