using System.ServiceModel;

namespace cope.FOB
{
    [ServiceContract(Namespace = "copeService",
        SessionMode=SessionMode.Required,
        CallbackContract=typeof(IForwardPortCallback))]
    public interface IForwardPort
    {
        [OperationContract(IsOneWay = true)]
        void LoadAssembly(string path, string startClass);

        [OperationContract(IsOneWay = true)]
        void LoadAssemblyAndStartMethod(string path, string startClass, string startMethod, bool isStatic);

        [OperationContract]
        int Ping();

        [OperationContract]
        string ReceiveCommand(string command);

        [OperationContract(IsOneWay = true)]
        void RegisterCallbackClient();

        [OperationContract(IsOneWay = true)]
        void KillCallbackClient();
    }
}