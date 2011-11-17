using UConnector.Config;
using UConnector.Samples.Cogs.Decision;
using UConnector.Samples.Cogs.Receive;
using UConnector.Samples.Cogs.Send;

namespace UConnector.Samples.Operations
{
    public class MoveFileOperation : AbstractOperation
    {
        protected override OperationBuilder Build()
        {
            return OperationBuilder.Create()
                .Receive<ReceiveFile>().WithConfiguration("FileIn")
                .Decision<BailOutIfFileInfoIsNullDecision>(
                    OperationBuilder.Create().Send<SendMoveFile>().WithConfiguration("FileOut").GetFirstStep(), null);
        }
    }
}