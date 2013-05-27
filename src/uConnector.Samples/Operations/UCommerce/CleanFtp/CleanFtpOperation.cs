using UConnector.Config.Fluent.v1;
using UConnector.Extensions.Cogs.Adapters;

namespace UConnector.Samples.Operations.UCommerce.CleanFtp
{
    public class CleanFtpOperation : CustomOperation
    {
        protected override IOperation BuildOperation()
        {
            return FluentOperationBuilder
				.Receive<FtpFilesAdapter>()
                .ToOperation();
        }
    }
}