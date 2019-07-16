namespace NeuralNetwork.Structure.Common
{

    public interface IChildStructure<in TParentStructure>
    {

        void AttachTo(TParentStructure parentStructure);

    }

}
