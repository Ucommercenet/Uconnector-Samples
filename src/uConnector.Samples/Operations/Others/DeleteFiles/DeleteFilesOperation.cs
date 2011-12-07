using UConnector.Config;
using UConnector.Samples.Operations.Others.DeleteFiles.Cogs;

namespace UConnector.Samples.Operations.Others.DeleteFiles
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