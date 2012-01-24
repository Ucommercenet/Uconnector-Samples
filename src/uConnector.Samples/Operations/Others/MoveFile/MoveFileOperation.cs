using UConnector.Config;
using UConnector.Samples.Operations.Others.MoveFile.Cogs;

namespace UConnector.Samples.Operations.Others.MoveFile
{
    public class MoveFileOperation : CustomOperation
    {
        public MoveFileOperation() : base("MoveFileOperation")
        {
        }

        protected override IOperation BuildOperation()
        {
            return OperationBuilder.Create()
                .Receive<FileReceiver>().WithConfiguration("FileIn")
                .Decision<BailOutIfFileInfoIsNullDecision>(OperationBuilder.Create().Send<MoveFileSender>().WithConfiguration("FileOut").GetFirstStep()).
                GetOperation();
        }
    }
}