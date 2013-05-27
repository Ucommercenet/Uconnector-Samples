using UConnector.Config.Fluent.v1;
using UConnector.Samples.Operations.Others.DeleteFiles.Cogs;

namespace UConnector.Samples.Operations.Others.DeleteFiles
{
    public class DeleteFilesOperation : CustomOperation
    {
        protected override IOperation BuildOperation()
        {
            return FluentOperationBuilder
				.Receive<FilesReceiver>()
                .WithConfiguration("FileIn")
                .Send<DeleteFilesSender>().WithConfiguration("FileOutDelete").ToOperation();
        }
    }
}