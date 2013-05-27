using System.IO;
using UConnector.Config.Fluent.v1;
using UConnector.Samples.Operations.Sandbox.Cogs;
using UConnector.Samples.Operations.UCommerce.ImportLocalFile.Cogs;

namespace UConnector.Samples.Operations.Sandbox.Operations
{
	public class SandboxOperation : CustomOperation
	{
		protected override IOperation BuildOperation()
		{
			return FluentOperationBuilder
				.Receive<LocalFilesReceiver>()
				.WithOption(x => x.Pattern = "*.xml")
				.WithOption(x => x.DeleteFile = true)
				.WithOption(x => x.Directory = @"C:\uConnector\In")
				.WithOption(x => x.SearchOption = SearchOption.TopDirectoryOnly)
				.WithOption(x => x.Take = 10)
				.WithOption(x => x.Skip = 0)
				.Debatching()
				.Transform<WorkFileToXDocument>()
				.Transform<XDocumentToXElementIterator>()
				.WithOption(x => x.DescendendsName = "InventTable_1")
				.Debatching()
				.Transform<XElementToUCommerceProduct>()
				.Transform<ProductToProductName>()
				.Batching()
				.Send<StringIteratorToFile>()
				.WithOption(x => x.Directory = @"C:\uConnector\Out")
				.WithOption(x => x.Filename = @"ProductNames.{DateTime.Now.Ticks}.txt")
				.GetOperation();
		}
	}
}
