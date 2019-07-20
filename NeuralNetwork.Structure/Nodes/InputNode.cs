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

        public virtual double LastCalculatedValue { get; private set; }

        public virtual event Func<IInput<double>, double, Task> OnInput;

        public virtual event Func<INode, double, Task> OnResultCalculated;

        public virtual void InsertInto(IReadOnlyLayer<INode> layer)
        {
        }

        public virtual async Task Input(double input)
        {
            if (OnInput != null)
                await OnInput(this, input);

            LastCalculatedValue = input;

            if (OnResultCalculated != null)
                await OnResultCalculated(this, input);
        }

    }
}
