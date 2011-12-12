using UConnector.Config;
using UConnector.Extensions.Cogs.Senders;
using UConnector.Extensions.Cogs.Transformers;
using UConnector.Extensions.Cogs.TwoWayCogs;
using UConnector.Samples.Operations.UCommerce.ImportLocalFile.Cogs;

namespace UConnector.Samples.Operations.UCommerce.ImportLocalFile.SubOperations
{
    public class ImportExcelSubOperation : CustomOperation
    {
        protected override Operation BuildOperation()
        {
            return OperationBuilder.Create()
                .Cog<WorkFileToStreamCog>()
                .Cog<ExcelCog>()
                .Cog<DataTableToProductListCog>()
                .Send<ProductListSender>().GetOperation();
        }
    }
}