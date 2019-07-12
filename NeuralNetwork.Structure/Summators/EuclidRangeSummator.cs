using NeuralNetwork.Structure.Nodes;
using NeuralNetwork.Structure.Synapses;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Summators
{

    public class EuclidRangeSummator : ISummator
    {

        public Task<double> GetSum(ISlaveNode node) => GetEuclidRange(node);

        public async static Task<double> GetEuclidRange(ISlaveNode node)
        {
            var tasks = node.Synapses.Select(GetSynapsesOutput);

            var tasksResult = await Task.WhenAll(tasks).ConfigureAwait(false);
            var sum = tasksResult.Sum();

            return Math.Sqrt(sum);
        }

        private async static Task<double> GetSynapsesOutput(ISynapse synapse)
        {
            var output = await synapse.MasterNode.Output().ConfigureAwait(false);

            return Math.Pow(output - synapse.Weight, 2);
        }
    }
}
