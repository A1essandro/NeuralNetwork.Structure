using NeuralNetwork.Structure.Nodes;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Summators
{

    [DataContract]
    public class Summator : ISummator
    {

        public async Task<double> GetSum(ISlaveNode node)
        {
            var tasks = node.Synapses.Select(x => x.Output());
            return (await Task.WhenAll(tasks).ConfigureAwait(false)).Sum();
        }

    }
}
