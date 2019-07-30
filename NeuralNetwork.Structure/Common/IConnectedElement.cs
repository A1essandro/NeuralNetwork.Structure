namespace NeuralNetwork.Structure.Common
{

    public interface IConnectedElement<in T>
    {

        void ConnectTo(T connectionElement);

        void DisconnectFrom(T connectionElement);

    }

}
