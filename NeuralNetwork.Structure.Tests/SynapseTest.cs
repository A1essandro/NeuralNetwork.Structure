using Moq;
using NeuralNetwork.Structure.Contract.Nodes;
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
        public void TestConstructor(double val)
        {
            var master = new Mock<INode>();
            var slave = new Mock<ISlaveNode>();

            var synapse = new Synapse(master.Object, slave.Object, val);

            Assert.Equal(master.Object, synapse.MasterNode);
            Assert.Equal(slave.Object, synapse.SlaveNode);
            Assert.Equal(val, synapse.Weight);
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
            var slave = new Mock<ISlaveNode>();
            var synapse = new Synapse(master.Object, slave.Object, 0.5);

            var result = double.NaN;
            synapse.OnResultCalculated += (s, v) => { result = v; return Task.CompletedTask; };
            master.Raise(x => x.OnResultCalculated += null, master.Object, val);

            Assert.Equal(val * 0.5, result);
        }

    }
}
