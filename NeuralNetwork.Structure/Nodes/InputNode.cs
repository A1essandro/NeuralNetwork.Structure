using NeuralNetwork.Structure.Common;
using NeuralNetwork.Structure.Layers;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Nodes
{

    [DataContract]
    public class InputNode : IInputNode
    {

        public event Func<IInput<double>, double, Task> OnInput;

        public event Func<INode, double, Task> OnResultCalculated;

        public virtual void InsertInto(IReadOnlyLayer<INode> layer)
        {
        }

        public async Task Input(double input)
        {
            if (OnInput != null)
                await OnInput(this, input);

            if (OnResultCalculated != null)
                await OnResultCalculated(this, input);
        }

    }
}
