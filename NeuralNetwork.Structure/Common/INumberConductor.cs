using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Common
{
    public interface INumberConductor<out T>
        where T : INumberConductor<T>
    {

        event Func<T, double, Task> OnResultCalculated;

    }
}
