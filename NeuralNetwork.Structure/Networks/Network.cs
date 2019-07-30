using NeuralNetwork.Structure.Internal.Extensions;
using NeuralNetwork.Structure.Layers;
using NeuralNetwork.Structure.Nodes;
using System.Collections.Generic;

namespace NeuralNetwork.Structure.Networks
{

    public class Network : SimpleNetwork, IMultilayerNetwork
    {

        private List<IReadOnlyLayer<INotInputNode>> _innerLayers = new List<IReadOnlyLayer<INotInputNode>>();

        #region Public properties

        public virtual IReadOnlyList<IReadOnlyLayer<INotInputNode>> InnerLayers => _innerLayers.AsReadOnly();

        /// <summary>
        /// All layers from input to output
        /// </summary>
        public override IEnumerable<IReadOnlyLayer<INode>> Layers
        {
            get
            {
                yield return InputLayer;
                foreach (var layer in InnerLayers)
                    yield return layer;
                yield return OutputLayer;
            }
        }

        #endregion

        public virtual IMultilayerNetwork AddInnerLayer(ILayer<INotInputNode> layer)
        {
            using (ProcessingLocker.UseWait())
            {
                _innerLayers.Add(layer);

                layer.InsertInto(this);

                return this;
            }
        }

        public virtual IMultilayerNetwork RemoveInnerLayer(ILayer<INotInputNode> layer)
        {
            using (ProcessingLocker.UseWait())
            {
                _innerLayers.Remove(layer);

                layer.RemoveFrom(this);

                return this;
            }
        }

    }
}
