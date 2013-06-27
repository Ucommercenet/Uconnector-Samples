using UConnector.Api.V1;
using UConnector.IO;
using UConnector.IO.Csv;
using UConnector.Samples.UCommerce;

namespace UConnector.Samples.Operations.UCommerce.ImportLocalFile
{
	public class ImportLocalCsvFile : Operation
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
