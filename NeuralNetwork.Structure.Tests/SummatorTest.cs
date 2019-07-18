using Moq;
using NeuralNetwork.Structure.Summators;
using NeuralNetwork.Structure.Synapses;
using System.Threading.Tasks;
using Xunit;

namespace NeuralNetwork.Structure.Tests
{
    public class SummatorTest
    {

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 2, 3)]
        [InlineData(-1, 2, -3)]
        public void TestSimpleFunction(double v1, double v2, double v3)
        {
            var synapse1 = new Mock<ISynapse>();
            var synapse2 = new Mock<ISynapse>();
            var synapse3 = new Mock<ISynapse>();

            var raised = false;
            var value = double.NaN;
            var summator = new Summator();
            summator.OnResultCalculated += (s, v) => { raised = true; value = v;  return Task.CompletedTask; };
            summator.ConnectTo(synapse1.Object);
            summator.ConnectTo(synapse2.Object);
            summator.ConnectTo(synapse3.Object);

            synapse1.Raise(x => x.OnResultCalculated += null, synapse1.Object, v1);
            var isRaisedAfter1 = raised;
            synapse2.Raise(x => x.OnResultCalculated += null, synapse2.Object, v2);
            var isRaisedAfter2 = raised;
            synapse3.Raise(x => x.OnResultCalculated += null, synapse3.Object, v3);
            var isRaisedAfter3 = raised;

            Assert.False(isRaisedAfter1);
            Assert.False(isRaisedAfter2);
            Assert.True(isRaisedAfter3);
            Assert.Equal(v1 + v2 + v3, value);
        }

    }
}
