using UConnector.Config.Fluent.v1;
using UConnector.Samples.Operations.Sandbox.Cogs;
using UConnector.Samples.Operations.UCommerce.ExportProductListToFtp.Cogs;
using UConnector.Samples.Operations.UCommerce.ImportLocalFile.Cogs;

namespace UConnector.Samples.Operations.Sandbox.Operations
{
	public class OperationWriteListOfProductNamesToFile : CustomOperation
	{
		protected override IOperation BuildOperation()
		{
			return FluentOperationBuilder
				.Receive<ProductListReceiver>()
				.Debatching()
				.Transform<ProductToProductName>()
				.Batching()
				.Send<StringIteratorToFile>()
				.WithOption(s => s.Directory = @"C:\uConnector\Out")
				.WithOption(s => s.Filename = @"ProductNames.txt")
				.GetOperation();
		}
	}
}
