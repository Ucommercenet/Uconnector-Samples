using UConnector.Config;
using UConnector.Samples.Operations.DeleteFiles.Cogs;

namespace UConnector.Samples.Operations.DeleteFiles
{
    public class DeleteFilesOperation : CustomOperation
    {
        protected override Operation BuildOperation()
        {
            return OperationBuilder.Create()
                .Receive<FilesReceiver>()
                .WithConfiguration("FileIn")
                .Send<DeleteFilesSender>().WithConfiguration("FileOutDelete").GetOperation();
        }
    }
}