using System.ServiceModel;

namespace cope.FOB
{
    public interface IForwardPortCallback
    {
        [OperationContract(IsOneWay = true)]
        void SendMessage(string message);
    }
}