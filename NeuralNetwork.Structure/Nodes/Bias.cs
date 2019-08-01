using NeuralNetwork.Structure.Contract.Layers;
using NeuralNetwork.Structure.Contract.Nodes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Nodes
{
    public sealed class Bias : IMasterNode, INotInputNode
    {

        private const double DEFAULT_VALUE = 1.0;

        public event Func<INode, double, Task> OnResultCalculated;

        public double LastCalculatedValue { get; }

        public Bias(double value = DEFAULT_VALUE)
        {
            LastCalculatedValue = value;
        }

        public void InsertInto(IReadOnlyLayer<INode> layer)
        {
            layer.OnNetworkInput += _onNetworkInputHandler;
        }

        public void RemoveFrom(IReadOnlyLayer<INode> layer)
        {
            layer.OnNetworkInput -= _onNetworkInputHandler;
        }

        private async Task _onNetworkInputHandler(IReadOnlyLayer<INode> layer, IEnumerable<double> input)
        {
            if (OnResultCalculated != null)
                await OnResultCalculated.Invoke(this, LastCalculatedValue);
        }

        #region IDisposable Support

        private bool disposedValue = false;

        private void _dispose(bool disposing)
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
            _dispose(true);
        }

        #endregion

    }
}
