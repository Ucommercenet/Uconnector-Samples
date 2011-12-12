using UConnector.Config;
using UConnector.Extensions.Cogs.Adapters;

namespace UConnector.Samples.Operations.UCommerce.CleanFtp
{
    public class CleanFtpOperation : CustomOperation
    {
        protected override Operation BuildOperation()
        {
            return OperationBuilder.Create()
                .Receive<FtpFilesAdapter>()
                .GetOperation();
        }
    }
}