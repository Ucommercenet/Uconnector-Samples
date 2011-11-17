using UConnector.Config;
using UConnector.Samples.Cogs.Receive;
using UConnector.Samples.Cogs.Send;

namespace UConnector.Samples.Operations
{
    public class DeleteFilesOperation : AbstractOperation
    {
        protected override OperationBuilder Build()
        {
            return OperationBuilder.Create()
                .Receive<ReceiveFiles>()
                .WithConfiguration("FileIn")
                .Send<SendDeleteFiles>().WithConfiguration("FileOutDelete");
        }
    }
}