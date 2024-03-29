﻿using NeuralNetwork.Structure.ActivationFunctions;
using NeuralNetwork.Structure.Layers;
using NeuralNetwork.Structure.Nodes;
using System.Linq;
using Xunit;

namespace NeuralNetwork.Structure.Tests
{
    public class LayerTest
    {

        [Fact]
        public void TestConstructorWithGenerator()
        {
            ushort neuronsQty = 15;
            var layer = new Layer(() => new Neuron(new Rectifier()), neuronsQty, new Bias());

            Assert.Equal(neuronsQty + 1, layer.Nodes.Count());
            Assert.IsType<Neuron>(layer.Nodes.ToArray()[5]);
            Assert.IsType<Bias>(layer.Nodes.Last());
        }

        [Fact]
        public void TestInputLayer()
        {
            ushort neuronsQty = 7;
            var layer = new InputLayer(() => new InputNode(), neuronsQty, new Bias());

            Assert.Equal(neuronsQty + 1, layer.Nodes.Count());
            Assert.IsType<InputNode>(layer.Nodes.ToArray()[5]);
            Assert.IsType<Bias>(layer.Nodes.Last());
        }

    }
}
