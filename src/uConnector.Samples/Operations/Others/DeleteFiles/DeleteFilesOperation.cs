using UConnector.Config.Fluent.V1;
using UConnector.Samples.Operations.Others.DeleteFiles.Receivers;
using UConnector.Samples.Operations.Others.DeleteFiles.Senders;

namespace UConnector.Samples.Operations.Others.DeleteFiles
{
    public class DeleteFilesOperation : Operation
    {
        protected override IOperation BuildOperation()
        {
            return FluentOperationBuilder
				.Receive<FilesReceiver>()
					.WithConfiguration("FileIn")
                .Send<DeleteFilesSender>()
					.WithConfiguration("FileOutDelete").ToOperation();
        }
    }
}