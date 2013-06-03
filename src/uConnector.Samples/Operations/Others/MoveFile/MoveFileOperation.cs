using UConnector.Config.Fluent.V1;
using UConnector.Samples.Operations.Others.MoveFile.Receivers;
using UConnector.Samples.Operations.Others.MoveFile.Senders;

namespace UConnector.Samples.Operations.Others.MoveFile
{
    public class MoveFileOperation : Operation
    {
        public MoveFileOperation() : base("MoveFileOperation")
        {
        }

        protected override IOperation BuildOperation()
        {
	        return FluentOperationBuilder
		        .Receive<FileReceiver>()
					.WithConfiguration("FileIn")
		        .Send<MoveFileSender>()
					.WithConfiguration("FileOut")
				.ToOperation();
        }
    }
}