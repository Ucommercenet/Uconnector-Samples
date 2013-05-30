using UConnector.Config.Fluent.v1;
using UConnector.Extensions.Cogs.Adapters;
using UConnector.Extensions.Cogs.Transformers;
using UConnector.Extensions.Cogs.TwoWayCogs;
using UConnector.Samples.Operations.UCommerce.ExportProductListToFtp.Cogs;

namespace UConnector.Samples.Operations.UCommerce.ExportProductListToFtp
{
    public class ExportProductListToFtpOperation : CustomOperation
    {
        protected override IOperation BuildOperation()
        {
            return FluentOperationBuilder
				.Receive<ProductListReceiver>()
                .Transform<ProductListToDataTable>()
				.Transform<ExcelCog>()
				.Transform<NamingCog>().WithOption(a => a.Extension = ".xlsx")
                .Batch()
                .Send<FtpFilesAdapter>().ToOperation();
        }
    }
}