using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Common
{
    public interface INumberConductor<out TThis>
        where TThis : INumberConductor<TThis>
    {

        event Func<TThis, double, Task> OnResultCalculated;

    }
}
