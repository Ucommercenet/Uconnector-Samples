using UConnector.Config;
using UConnector.Extensions.Cogs.Senders;
using UConnector.Extensions.Cogs.Transformers;
using UConnector.Extensions.Cogs.TwoWayCogs;
using UConnector.Samples.Operations.UCommerce.ImportLocalFile.Cogs;

namespace UConnector.Samples.Operations.UCommerce.ImportLocalFile
{
    public class ImportLocalFileOperation : CustomOperation
    {
        protected override Operation BuildOperation()
        {
            return OperationBuilder.Create()
                .Receive<LocalFilesReceiver>()
                .Debatching()
                .Cog<WorkFileToStreamCog>()
                .Cog<ExcelCog>()
                .Cog<DataTableToProductListCog>()
                .Send<ProductListSender>().GetOperation();
        }
    }
}