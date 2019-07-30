using NeuralNetwork.Structure.Layers;
using NeuralNetwork.Structure.Nodes;
using System.Collections.Generic;

namespace NeuralNetwork.Structure.Networks
{
    public interface IMultilayerNetwork : ISimpleNetwork
    {

        IReadOnlyList<IReadOnlyLayer<INotInputNode>> InnerLayers { get; }

        IMultilayerNetwork AddInnerLayer(ILayer<INotInputNode> layer);

        IMultilayerNetwork RemoveInnerLayer(ILayer<INotInputNode> layer);

    }
}
