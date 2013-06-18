using System.IO;
using UConnector.Api.V1;
using UConnector.Filesystem;
using UConnector.Helpers;
using UConnector.Samples.Transformers;
using UConnector.UCommerce;

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
