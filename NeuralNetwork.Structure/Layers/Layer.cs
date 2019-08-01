using NeuralNetwork.Structure.Contract.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork.Structure.Layers
{

    public class Layer : BaseLayer<INotInputNode>
    {

        public Layer()
        {
        }

        public Layer(IEnumerable<INotInputNode> nodes)
            : base(nodes)
        {
        }

        public Layer(params INotInputNode[] nodes)
            : base(nodes.AsEnumerable())
        {
        }

        public Layer(Func<INotInputNode> getter, int qty, params INotInputNode[] other)
            : base(getter, qty, other)
        {
        }

        private static Type[] GetKnownType() => new Type[] { typeof(BaseLayer<INotInputNode>) };

    }
}
