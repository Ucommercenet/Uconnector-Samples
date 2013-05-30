using UConnector.Config.Fluent.v1;
using UConnector.Extensions.Cogs.Receivers;
using UConnector.Extensions.Cogs.Senders;
using UConnector.Extensions.Cogs.Transformers;
using UConnector.Extensions.Cogs.TwoWayCogs;
using UConnector.Samples.Operations.UCommerce.ImportLocalFile.Cogs;

namespace UConnector.Samples.Operations.UCommerce.ImportLocalFile
{
	public class ImportLocalCsvFileOperation : CustomOperation
	{
		protected override IOperation BuildOperation()
		{
			return FluentOperationBuilder
				.Receive<FilesFromLocalDirectory>().WithOption(x => x.Pattern = "*.csv")
				.Debatch()
				.Transform<WorkFileToStream>()
				.Transform<FromCsvStreamToDataTable>()
				.Transform<DataTableToProductList>()
				.Send<ProductListToUCommerce>().ToOperation();
		}
	}
}
