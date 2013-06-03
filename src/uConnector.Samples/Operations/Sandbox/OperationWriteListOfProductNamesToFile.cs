using UConnector.Config.Fluent.V1;
using UConnector.Samples.Operations.UCommerce.ExportProductListToFtp.Receiver;
using UConnector.Samples.Senders;
using UConnector.Samples.Transformers;

namespace UConnector.Samples.Operations.Sandbox
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
				.Send<StringsToFile>()
					.WithOption(s => s.Directory = @"C:\uConnector\Out")
					.WithOption(s => s.Filename = @"ProductNames.txt")
				.ToOperation();
		}
	}
}
