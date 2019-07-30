using NeuralNetwork.Structure.Common;
using NeuralNetwork.Structure.Layers;
using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Nodes
{

    public class InputNode : IInputNode
    {

        public virtual double LastCalculatedValue { get; private set; }

        public virtual event Func<IInput<double>, double, Task> OnInput;

        public virtual event Func<INode, double, Task> OnResultCalculated;

        public virtual void InsertInto(IReadOnlyLayer<INode> layer)
        {
        }

        public void RemoveFrom(IReadOnlyLayer<INode> parentStructure)
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

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    OnInput = null;
                    OnResultCalculated = null;
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
