using NeuralNetwork.Structure.Layers;
using NeuralNetwork.Structure.Networks;
using NeuralNetwork.Structure.Nodes;
using NeuralNetwork.Structure.Synapses;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NeuralNetwork.Structure.Tests
{
    public class IntegrationTest
    {

        [Theory]
        [InlineData(-0.75)]
        [InlineData(0)]
        [InlineData(0.1)]
        [InlineData(0.5)]
        [InlineData(1)]
        public async Task TestSimpleNetwork(double synapseWeight)
        {
            var network = new Network();
            var input = new InputNode();
            var output = new Neuron();
            network.InputLayer = new InputLayer(input);
            network.OutputLayer = new Layer(output);

            var synapse = new Synapse(input, output, synapseWeight);

            var outputCalculatedValue = double.NaN;
            var raised = false;
            output.OnResultCalculated += (n, v) =>
            {
                raised = true;
                outputCalculatedValue = v;
                return Task.CompletedTask;
            };

            var networkResultFirstNode = double.NaN;
            network.OnResultCalculated += (n, v) =>
            {
                networkResultFirstNode = v.First();
                return Task.CompletedTask;
            };

            await network.Input(new[] { 1.0 });

            Assert.True(raised);
            Assert.Equal(synapseWeight, networkResultFirstNode);
            Assert.Equal(synapseWeight, outputCalculatedValue);
            Assert.Equal(synapseWeight, (await network.Output()).First());
        }

    }
}
