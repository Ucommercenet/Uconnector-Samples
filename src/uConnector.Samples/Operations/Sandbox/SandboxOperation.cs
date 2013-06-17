using System.IO;
using UConnector.Config.Fluent.V1;
using UConnector.Extensions.Receivers;
using UConnector.Extensions.Senders;
using UConnector.Samples.Transformers;

namespace UConnector.Samples.Operations.Sandbox
{
	public class SandboxOperation : Operation
	{
		protected override IOperation BuildOperation()
		{
			return FluentOperationBuilder
				.Receive<FilesFromLocalDirectory>()
					.WithOption(x => x.Pattern = "*.xml")
					.WithOption(x => x.DeleteFile = true)
					.WithOption(x => x.Directory = @"C:\uConnector\In")
					.WithOption(x => x.SearchOption = SearchOption.TopDirectoryOnly)
					.WithOption(x => x.Take = 10)
					.WithOption(x => x.Skip = 0)
				.Debatch()
				.Transform<WorkFileToXDocument>()
				.Debatch<XDocumentToXElements>()
					.WithOption(x => x.DescendendsName = "InventTable_1")
				.Transform<XElementToUCommerceProduct>()
				.Batch()
				.Send<ProductListToUCommerce>()
				.ToOperation();
		}
	}
}
