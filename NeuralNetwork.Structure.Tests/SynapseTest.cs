using Moq;
using NeuralNetwork.Structure.Nodes;
using NeuralNetwork.Structure.Synapses;
using System.Threading.Tasks;
using Xunit;

namespace NeuralNetwork.Structure.Tests
{
    public class SynapseTest
    {

        [Theory]
        [InlineData(-0.75)]
        [InlineData(0)]
        [InlineData(0.1)]
        [InlineData(0.5)]
        [InlineData(1)]
        public void TestConstructorWithWeight(double val)
        {
            var node = new Mock<INode>();
            node.Setup(x => x.Output()).Returns(Task.FromResult(1.0));

            var synapse = new Synapse(node.Object, val);

            Assert.Equal(val, synapse.Weight);
        }

        [Fact]
        public void TestConstructorWithDefaultWeight()
        {
            var synapse = new Synapse();

            Assert.Equal(0, synapse.Weight);
        }

        [Theory]
        [InlineData(-0.75)]
        [InlineData(0)]
        [InlineData(0.1)]
        [InlineData(0.5)]
        [InlineData(1)]
        public void ValuePassingTest(double val)
        {
            var master = new Mock<IMasterNode>();
            var synapse = new Synapse(master.Object, 0.5);

            var result = double.NaN;
            synapse.OnResultCalculated += (s, v) => { result = v; return Task.CompletedTask; };
            master.Raise(x => x.OnResultCalculated += null, master.Object, val);

            Assert.Equal(val / 2, result);
        }

    }
}
