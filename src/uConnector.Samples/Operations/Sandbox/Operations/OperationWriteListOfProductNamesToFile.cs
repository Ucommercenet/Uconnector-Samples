using UConnector.Config.Fluent.V1;
using UConnector.Samples.Operations.Sandbox.Cogs;
using UConnector.Samples.Operations.UCommerce.ExportProductListToFtp.Cogs;
using UConnector.Samples.Operations.UCommerce.ImportLocalFile.Cogs;

namespace UConnector.Samples.Operations.Sandbox.Operations
{
	public class OperationWriteListOfProductNamesToFile : Operation
	{
		protected override IOperation BuildOperation()
		{
			return FluentOperationBuilder
				.Receive<ProductListFromUCommerce>()
				.Debatch()
				.Transform<ProductToProductName>()
				.Batch()
				.Send<StringIteratorToFile>()
					.WithOption(s => s.Directory = @"C:\uConnector\Out")
					.WithOption(s => s.Filename = @"ProductNames.txt")
				.ToOperation();
		}
	}
}
