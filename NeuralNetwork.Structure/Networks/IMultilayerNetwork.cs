using NeuralNetwork.Structure.Layers;
using NeuralNetwork.Structure.Nodes;
using System.Collections.Generic;

namespace NeuralNetwork.Structure.Networks
{
    public interface IMultilayerNetwork : ISimpleNetwork
    {

        ICollection<IReadOnlyLayer<INotInputNode>> InnerLayers { get; }

        ISimpleNetwork AddInnerLayer(ILayer<INotInputNode> layer);

    }
}
