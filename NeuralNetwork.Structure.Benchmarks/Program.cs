using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using NeuralNetwork.Structure.Layers;
using NeuralNetwork.Structure.Networks;
using NeuralNetwork.Structure.Nodes;
using NeuralNetwork.Structure.Synapses;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Benchmarks
{

    public class Program
    {

        [ClrJob(baseline: true), CoreJob, CoreRtJob]
        [MemoryDiagnoser]
        public class SignalPropagationTest
        {

            private Network _network;

            [GlobalSetup]
            public void Setup()
            {
                var network = new Network();
                var input = new InputNode();
                var output = new Neuron();

                network.InputLayer = new InputLayer(input);
                network.OutputLayer = new Layer(output);

                var synapse = new Synapse(input, output, 0.5);
                network.AddSynapse(synapse);

                _network = network;
            }

            [Benchmark]
            public async Task InputOutput()
            {
                await _network.Input(new[] { 1.0 });
                await _network.Output();
            }

        }

        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<SignalPropagationTest>();
        }

    }
}
