using UConnector.Config;
using UConnector.Samples.Operations.DeleteFiles.Cogs;

namespace UConnector.Samples.Operations.DeleteFiles
{
    public class DeleteFilesOperation : AbstractOperation
    {
        protected override OperationBuilder Build()
        {
            return OperationBuilder.Create()
                .Receive<FilesReceiver>()
                .WithConfiguration("FileIn")
                .Send<DeleteFilesSender>().WithConfiguration("FileOutDelete");
        }
    }
}