using System;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Common
{

    public interface IInput<T>
    {

        /// <summary>
        /// Write input value
        /// </summary>
        /// <param name="input">Input value to write</param>
        Task Input(T input);

        event Func<IInput<T>, T, Task> OnInput;

    }

}
