using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Common
{
    public interface IOutput<T>
    {

        /// <summary>
        /// Getting output
        /// </summary>
        /// <returns>Calculated output</returns>
        Task<T> Output();

        event Action<T> OnOutput;

    }

}
