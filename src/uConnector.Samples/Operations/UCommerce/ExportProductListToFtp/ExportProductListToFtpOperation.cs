using UConnector.Config;
using UConnector.Extensions.Cogs.Adapters;
using UConnector.Extensions.Cogs.Transformers;
using UConnector.Extensions.Cogs.TwoWayCogs;
using UConnector.Samples.Operations.UCommerce.ExportProductListToFtp.Cogs;

namespace UConnector.Samples.Operations.UCommerce.ExportProductListToFtp
{
    public class ExportProductListToFtpOperation : CustomOperation
    {
        protected override Operation BuildOperation()
        {
            return OperationBuilder.Create()
                .Receive<ProductListReceiver>()
                .Cog<ProductListToDataTableCog>()
                .Cog<ExcelCog>()
                .Cog<NamingCog>().WithOption(a => a.Extension = ".xlsx")
                .Batching()
                .Send<FtpFilesAdapter>().GetOperation();
        }
    }
}