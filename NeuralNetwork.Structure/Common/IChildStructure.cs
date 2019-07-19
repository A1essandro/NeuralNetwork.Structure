namespace NeuralNetwork.Structure.Common
{

    public interface IChildStructure<in TParentStructure>
    {

        void InsertInto(TParentStructure parentStructure);

    }

}
