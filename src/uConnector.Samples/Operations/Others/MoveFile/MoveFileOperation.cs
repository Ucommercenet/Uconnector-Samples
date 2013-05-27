using UConnector.Config.Fluent.v1;
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
            return FluentOperationBuilder
				.Receive<FileReceiver>().WithConfiguration("FileIn")
                .Decision<BailOutIfFileInfoIsNullDecision>(FluentOperationBuilder.CreateSubOperation().Send<MoveFileSender>().WithConfiguration("FileOut").ToOperation()).
                ToOperation();
        }
    }
}