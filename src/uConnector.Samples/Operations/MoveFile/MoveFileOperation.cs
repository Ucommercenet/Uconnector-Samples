using UConnector.Config;
using UConnector.Samples.Operations.MoveFile.Cogs;

namespace UConnector.Samples.Operations.MoveFile
{
    public class MoveFileOperation : AbstractOperation
    {
        protected override OperationBuilder Build()
        {
            return OperationBuilder.Create()
                .Receive<FileReceiver>().WithConfiguration("FileIn")
                .Decision<BailOutIfFileInfoIsNullDecision>(
                    OperationBuilder.Create().Send<MoveFileSender>().WithConfiguration("FileOut").GetFirstStep(), null);
        }
    }
}