﻿using NeuralNetwork.Structure.Layers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Nodes
{
    public sealed class Bias : IMasterNode, INotInputNode
    {

        public event Action<double> OnOutput;

        public event Func<INode, double, Task> OnResultCalculated;

        private const double VALUE = 1.0;

        public Task<double> Output()
        {
            OnOutput?.Invoke(VALUE);
            return Task.FromResult(VALUE);
        }

        public void AttachToLayer(IReadOnlyLayer<INode> layer)
        {
            layer.OnNetworkInput += _onNetworkInputHandler;
        }

        private async Task _onNetworkInputHandler(IReadOnlyLayer<INode> layer, IEnumerable<double> input)
        {
            if (OnResultCalculated != null)
                await OnResultCalculated.Invoke(this, VALUE);
        }
    }
}
