using UConnector.Config;
using UConnector.Extensions.Cogs.Adapters;
using UConnector.Samples.Operations.UCommerce.CopyFilesFromFtpToLocalDirectory.Cogs;

namespace UConnector.Samples.Operations.UCommerce.CopyFilesFromFtpToLocalDirectory
{
    public class CopyFilesFromFtpToLocalDirectoryOperation : CustomOperation
    {
        protected override Operation BuildOperation()
        {
            return OperationBuilder.Create()
                .Receive<FtpFilesAdapter>()
                .WithConfiguration("FtpIn")
                .Debatching()
                .Send<CopyFileToDirectorySender>().GetOperation();
        }
    }
}