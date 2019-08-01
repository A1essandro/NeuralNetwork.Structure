using Moq;
using NeuralNetwork.Structure.Contract.ActivationFunctions;
using NeuralNetwork.Structure.Contract.Layers;
using NeuralNetwork.Structure.Contract.Nodes;
using NeuralNetwork.Structure.Contract.Synapses;
using NeuralNetwork.Structure.Nodes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NeuralNetwork.Structure.Tests
{

    public class NodeTest
    {

        [Theory]
        [InlineData(-1)]
        [InlineData(0.5)]
        [InlineData(0)]
        [InlineData(1)]
        public async Task TestInputNode(double value)
        {
            var node = new InputNode();

            var onInputRaised = false;
            node.OnInput += (n, r) => { onInputRaised = true; return Task.CompletedTask; };

            var result = double.NaN;
            node.OnResultCalculated += (s, v) => { result = v; return Task.CompletedTask; };

            await node.Input(value);

            Assert.True(onInputRaised);
            Assert.Equal(value, result);
        }

        [Fact]
        public void TestBias()
        {
            var bias = new Bias();
            var layer = new Mock<IReadOnlyLayer<INode>>();

            var result = double.NaN;
            bias.OnResultCalculated += (s, v) => { result = v; return Task.CompletedTask; };

            bias.InsertInto(layer.Object);
            layer.Raise(x => x.OnNetworkInput += null, layer.Object, new double[] { 0 });

            Assert.Equal(1, result);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0.5)]
        [InlineData(0)]
        [InlineData(1)]
        public void TestNeuron(double value)
        {
            var synapse = new Mock<ISynapse>();

            var func = new Mock<IActivationFunction>();
            func.Setup(x => x.GetEquation(It.IsAny<double>()))
                .Returns<double>(x => x);

            var neuron = new Neuron(func.Object);
            neuron.ConnectTo(synapse.Object);

            var result = double.NaN;
            neuron.OnResultCalculated += (s, v) => { result = v; return Task.CompletedTask; };

            synapse.Raise(x => x.OnResultCalculated += null, synapse.Object, value);

            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0.5)]
        [InlineData(0)]
        [InlineData(1)]
        public void TestContext(double value)
        {
            var synapse = new Mock<ISynapse>();

            var context = new Context(delay: 1);
            context.ConnectTo(synapse.Object);

            var results = new List<double>();
            context.OnResultCalculated += (s, v) =>
            {
                results.Add(v);
                return Task.CompletedTask;
            };

            synapse.Raise(x => x.OnResultCalculated += null, synapse.Object, value);
            synapse.Raise(x => x.OnResultCalculated += null, synapse.Object, value);

            Assert.Equal(0.0, results.First());
            Assert.Equal(value, results.Last());
        }

    }
}
