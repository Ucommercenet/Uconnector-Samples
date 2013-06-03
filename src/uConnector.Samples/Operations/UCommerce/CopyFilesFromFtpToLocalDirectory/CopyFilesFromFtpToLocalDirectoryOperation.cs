using UConnector.Config.Fluent.V1;
using UConnector.Extensions.Adapters;
using UConnector.Samples.Operations.UCommerce.CopyFilesFromFtpToLocalDirectory.Cogs;

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
                .Send<CopyFileToDirectory>()
				.ToOperation();
        }
    }
}