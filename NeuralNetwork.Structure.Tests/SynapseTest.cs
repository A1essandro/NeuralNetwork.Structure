using NeuralNetwork.Structure.Nodes;
using NeuralNetwork.Structure.Synapses;
using Xunit;

namespace Tests
{
    public class SynapseTest
    {

        [Fact]
        public void TestConstructorWithWeight()
        {
            var synapse = new Synapse(new Bias(), 0.5);

            Assert.Equal(0.5, synapse.Weight);
        }

        [Fact]
        public void TestConstructorWithRandomWeight()
        {
            var synapse = new Synapse();

            Assert.Equal(0, synapse.Weight);
        }

    }
}
