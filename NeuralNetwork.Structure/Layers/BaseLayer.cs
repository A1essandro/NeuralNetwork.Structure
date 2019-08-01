using NeuralNetwork.Structure.Contract.Common;
using NeuralNetwork.Structure.Contract.Layers;
using NeuralNetwork.Structure.Contract.Networks;
using NeuralNetwork.Structure.Contract.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Layers
{

    public abstract class BaseLayer<TNode> : ILayer<TNode>
        where TNode : INode
    {

        protected List<TNode> NodeList = new List<TNode>();

        public event Func<IReadOnlyLayer<TNode>, IEnumerable<double>, Task> OnNetworkInput;

        public IEnumerable<TNode> Nodes => NodeList.AsReadOnly();

        public int NodesQuantity => NodeList.Count;

        #region ctors

        public BaseLayer()
        {
        }

        public BaseLayer(IEnumerable<TNode> nodes)
        {
            System.Diagnostics.Contracts.Contract.Assert(nodes != null, nameof(nodes));

            NodeList = nodes.ToList();
        }

        public BaseLayer(params TNode[] nodes)
            : this(nodes.AsEnumerable())
        {
        }

        public BaseLayer(Func<TNode> factory, int qty, params TNode[] other)
        {
            System.Diagnostics.Contracts.Contract.Assert(factory != null, nameof(factory));
            System.Diagnostics.Contracts.Contract.Assert(qty >= 0, nameof(qty));

            for (var i = 0; i < qty; i++)
            {
                AddNode(factory());
            }
            foreach (var node in other)
            {
                AddNode(node);
            }
        }

        #endregion

        public void AddNode(TNode node)
        {
            System.Diagnostics.Contracts.Contract.Assert(node != null, nameof(node));

            NodeList.Add(node);

            node.InsertInto(this as IReadOnlyLayer<INode>);
        }

        public bool RemoveNode(TNode node)
        {
            System.Diagnostics.Contracts.Contract.Assert(node != null, nameof(node));

            return NodeList.Remove(node);
        }

        public virtual void InsertInto(ISimpleNetwork network)
        {
            network.OnInput += OnNetworkInputHandler;
        }

        public void RemoveFrom(ISimpleNetwork network)
        {
            network.OnInput -= OnNetworkInputHandler;
        }

        protected virtual async Task OnNetworkInputHandler(IInput<IEnumerable<double>> network, IEnumerable<double> input)
        {
            if (OnNetworkInput != null)
                await OnNetworkInput(this, input);
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    OnNetworkInput = null;

                    foreach (var node in Nodes)
                        node.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

    }
}