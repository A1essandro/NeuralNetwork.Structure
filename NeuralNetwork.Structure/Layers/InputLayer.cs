using NeuralNetwork.Structure.Common;
using NeuralNetwork.Structure.Nodes;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Layers
{

    public class InputLayer : BaseLayer<IMasterNode>, IInputLayer
    {

        public InputLayer(IEnumerable<IMasterNode> nodes)
            : base(nodes)
        {
        }

        public InputLayer(params IMasterNode[] nodes)
            : base(nodes.AsEnumerable())
        {
        }

        public InputLayer(Func<IMasterNode> factory, int qty, params IMasterNode[] other)
            : base(factory, qty, other)
        {
        }

        public event Func<IInput<IEnumerable<double>>, IEnumerable<double>, Task> OnInput;

        public async Task Input(IEnumerable<double> input)
        {
            var inputNodes = Nodes.OfType<IInputNode>().ToArray();
            Contract.Requires(input.Count() == inputNodes.Length, nameof(input));

            if (OnInput != null)
                await OnInput.Invoke(this, input);

            var index = 0;
            foreach (var value in input)
            {
                await inputNodes[index++].Input(value);
            }
        }

        private static Type[] GetKnownType() => new Type[] { typeof(BaseLayer<IMasterNode>) };

    }
}
