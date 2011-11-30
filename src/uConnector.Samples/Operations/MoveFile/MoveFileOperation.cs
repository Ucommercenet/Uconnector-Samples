using UConnector.Config;
using UConnector.Samples.Operations.MoveFile.Cogs;

namespace UConnector.Samples.Operations.MoveFile
{
    public class MoveFileOperation : CustomOperation
    {
        public MoveFileOperation() : base("MoveFileOperation")
        {
        }

        protected override Operation BuildOperation()
        {
            return OperationBuilder.Create()
                .Receive<FileReceiver>().WithConfiguration("FileIn")
                .Decision<BailOutIfFileInfoIsNullDecision>(
                    OperationBuilder.Create().Send<MoveFileSender>().WithConfiguration("FileOut").GetFirstStep(), null).
                GetOperation();
        }
    }
}