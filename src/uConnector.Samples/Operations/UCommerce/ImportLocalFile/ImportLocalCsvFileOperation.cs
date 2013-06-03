using UConnector.Config.Fluent.v1;
using UConnector.Extensions.Receivers;
using UConnector.Extensions.Senders;
using UConnector.Extensions.Transformers;
using UConnector.Samples.Operations.UCommerce.ImportLocalFile.Cogs;

namespace UConnector.Samples.Operations.UCommerce.ImportLocalFile
{
	public class ImportLocalCsvFileOperation : Operation
	{
		protected override IOperation BuildOperation()
		{
			return FluentOperationBuilder
				.Receive<FilesFromLocalDirectory>()
					.WithOption(x => x.Pattern = "*.csv")
				.Debatch()
				.Transform<WorkFileToStream>()
				.Transform<FromCsvStreamToDataTable>()
				.Transform<DataTableToProductList>()
				.Send<ProductListToUCommerce>()
				.ToOperation();
		}
	}
}
