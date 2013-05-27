using UConnector.Config.Fluent.v1;
using UConnector.Extensions.Cogs.Senders;
using UConnector.Extensions.Cogs.Transformers;
using UConnector.Extensions.Cogs.TwoWayCogs;
using UConnector.Samples.Operations.UCommerce.ImportLocalFile.Cogs;

namespace UConnector.Samples.Operations.UCommerce.ImportLocalFile.SubOperations
{
    public class ImportExcelSubOperation : CustomOperation
    {
        protected override IOperation BuildOperation()
        {
            return FluentOperationBuilder.CreateSubOperation()
				.Transform<WorkFileToStreamCog>()
				.Transform<ExcelCog>()
				.Transform<DataTableToProductListCog>()
                .Send<ProductListSender>().GetOperation();
        }
    }
}