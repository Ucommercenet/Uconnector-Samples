using UConnector.Config;
using UConnector.Extensions.Cogs.Adapters;
using UConnector.Samples.Operations.Copy.Cogs;

namespace UConnector.Samples.Operations.Copy
{
    public class CopyFilesFromFtpToLocalDirectoryOperation : CustomOperation
    {
        protected override Operation BuildOperation()
        {
            return OperationBuilder.Create()
                .Receive<FtpFilesAdapter>()
                .WithConfiguration("FtpIn")
                .Send<CopyFileToDirectorySender>().GetOperation();
        }
    }
}