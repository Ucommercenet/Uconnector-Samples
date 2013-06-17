using UConnector.Api.V1;
using UConnector.Extensions.Adapters;
using UConnector.Extensions.Transformers;
using UConnector.Samples.Operations.UCommerce.ExportProductListToFtp.Receiver;
using UConnector.Samples.Operations.UCommerce.ExportProductListToFtp.Transformers;

namespace UConnector.Samples.Operations.UCommerce.ExportProductListToFtp
{
    public class ExportProductListToFtpOperation : Operation
    {
        protected override IOperation BuildOperation()
        {
            return FluentOperationBuilder
				.Receive<ProductListFromUCommerce>()
                .Transform<ProductListToDataTable>()
				.Transform<FromDataTableToExcelStream>()
				.Transform<StreamToWorkfileWithTimestampName>()
					.WithOption(a => a.Extension = ".xlsx")
                .Batch()
                .Send<FtpFilesAdapter>().ToOperation();
        }
    }
}