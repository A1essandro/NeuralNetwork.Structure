using NeuralNetwork.Structure.Common;
using NeuralNetwork.Structure.Networks;
using NeuralNetwork.Structure.Nodes;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Layers
{

    [DataContract]
    public abstract class BaseLayer<TNode> : ILayer<TNode>
        where TNode : INode
    {

        [DataMember]
        protected List<TNode> NodeList = new List<TNode>();

        public event Action<IEnumerable<double>> OnOutput;

        public event Func<IReadOnlyLayer<TNode>, IEnumerable<double>, Task> OnNetworkInput;

        public IEnumerable<TNode> Nodes => NodeList.AsReadOnly();

        public int NodesQuantity => NodeList.Count;

        public BaseLayer()
        {
        }

        public BaseLayer(IEnumerable<TNode> nodes)
        {
            Contract.Assert(nodes != null, nameof(nodes));

            NodeList = nodes.ToList();
        }

        public BaseLayer(params TNode[] nodes)
            : this(nodes.AsEnumerable())
        {
        }

        public BaseLayer(Func<TNode> factory, int qty, params TNode[] other)
        {
            Contract.Assert(factory != null, nameof(factory));
            Contract.Assert(qty >= 0, nameof(qty));

            for (var i = 0; i < qty; i++)
            {
                AddNode(factory());
            }
            foreach (var node in other)
            {
                AddNode(node);
            }
        }

        public void AddNode(TNode node)
        {
            Contract.Assert(node != null, nameof(node));

            NodeList.Add(node);

            node.AttachTo(this as IReadOnlyLayer<INode>);
        }

        public bool RemoveNode(TNode node)
        {
            Contract.Assert(node != null, nameof(node));

            return NodeList.Remove(node);
        }

        public async Task<IEnumerable<double>> Output()
        {
            var result = await Task.WhenAll(Nodes.Select(n => n.Output())).ConfigureAwait(false);

            if (OnOutput != null)
                OnOutput.Invoke(result);

            return result;
        }

        public virtual void AttachTo(ISimpleNetwork network)
        {
            network.OnInput += _onNetworkInputHandler;
        }

        private async Task _onNetworkInputHandler(IInput<IEnumerable<double>> network, IEnumerable<double> input)
        {
            if (OnNetworkInput != null)
                await OnNetworkInput(this, input);
        }
    }
}