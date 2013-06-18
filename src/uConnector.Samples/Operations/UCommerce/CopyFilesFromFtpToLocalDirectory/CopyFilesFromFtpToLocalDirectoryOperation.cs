using UConnector.Api.V1;
using UConnector.Filesystem;
using UConnector.Samples.Operations.UCommerce.CopyFilesFromFtpToLocalDirectory.Senders;

namespace UConnector.Samples.Operations.UCommerce.CopyFilesFromFtpToLocalDirectory
{
    public class CopyFilesFromFtpToLocalDirectoryOperation : Operation
    {
        protected override IOperation BuildOperation()
        {
            return FluentOperationBuilder
				.Receive<FtpFilesAdapter>()
					.WithConfiguration("FtpIn")
                .Debatch()
                .Send<CopyOfFileToDirectory>()
				.ToOperation();
        }
    }
}