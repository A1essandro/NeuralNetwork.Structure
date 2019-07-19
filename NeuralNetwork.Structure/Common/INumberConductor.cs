namespace NeuralNetwork.Structure.Common
{
    public interface INumberConductor<out TThis> : IDataConductor<TThis, double>
        where TThis : IDataConductor<TThis, double>
    {

    }
}
