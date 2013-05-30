using UConnector.Config.Fluent.v1;
using UConnector.Extensions.Cogs.Senders;
using UConnector.Extensions.Cogs.Transformers;
using UConnector.Extensions.Cogs.TwoWayCogs;
using UConnector.Samples.Operations.UCommerce.ImportLocalFile.Cogs;

namespace UConnector.Samples.Operations.UCommerce.ImportLocalFile
{
	public class ImportLocalExcelFileOperation : CustomOperation
	{
		protected override IOperation BuildOperation()
		{
			return FluentOperationBuilder
				.Receive<LocalFilesReceiver>().WithOption(x => x.Pattern = "*.xslx")
				.Debatch()
				.Transform<WorkFileToStream>()
				.Transform<ExcelCog>()
				.Transform<DataTableToProductList>()
				.Send<ProductListSender>().ToOperation();
		}
	}
}
