using UConnector.Api.V1;
using UConnector.IO;
using UConnector.UCommerce;

namespace UConnector.Samples.Operations.UCommerce.ImportLocalFile
{
	public class ImportLocalExcelFileOperation : Operation
	{
		protected override IOperation BuildOperation()
		{
			return FluentOperationBuilder
				.Receive<FilesFromLocalDirectory>()
					.WithOption(x => x.Pattern = "*.xslx")
				.Debatch()
				.Transform<WorkFileToStream>()
				.Transform<FromExcelStreamToDataTable>()
				.Transform<DataTableToProductList>()
				.Send<ProductListToUCommerce>()
				.ToOperation();
		}
	}
}
