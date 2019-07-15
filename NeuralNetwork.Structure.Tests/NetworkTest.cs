using NeuralNetwork.Structure.Layers;
using NeuralNetwork.Structure.Networks;
using NeuralNetwork.Structure.Nodes;
using System.Linq;
using Xunit;

namespace NeuralNetwork.Structure.Tests
{
    public class NetworkTest
    {

        [Fact]
        public void TestInputLayerAdd()
        {
            var network = new Network();

            var inputLayer = new InputLayer(new InputNode(), new InputNode(), new InputNode());

            network.InputLayer = inputLayer;

            Assert.Equal(3, network.InputLayer.Nodes.Count());
        }

        [Fact]
        public void TestInnerLayerAdd()
        {
            var network = new Network();

            var innerLayer1 = new Layer(new Neuron(), new Neuron(), new Neuron(), new Neuron());
            var innerLayer2 = new Layer(new Neuron(), new Neuron(), new Neuron());

            network.AddInnerLayer(innerLayer1)
                   .AddInnerLayer(innerLayer2);

            Assert.Equal(2, network.InnerLayers.Count());
            Assert.Equal(4, network.InnerLayers[0].Nodes.Count());
        }

        [Fact]
        public void TestOutputLayerAdd()
        {
            var network = new Network();

            var outputLayer = new Layer(new Neuron(), new Neuron());

            network.OutputLayer = outputLayer;

            Assert.Equal(2, network.OutputLayer.Nodes.Count());
        }

    }
}
