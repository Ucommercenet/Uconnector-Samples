using UConnector.Api.V1;
using UConnector.Filesystem;
using UConnector.Helpers;
using UConnector.Samples.Framework;
using UConnector.Samples.Transformers;

namespace UConnector.Samples.Operations.Sandbox
{
	public class OperationXmlToCsv : Operation
	{
		protected override IOperation BuildOperation()
		{
			return FluentOperationBuilder
				.Receive<FilesFromLocalDirectory>()
					.WithOption(x => x.Pattern = "*.xml")
				.Debatch()
				.Transform<WorkFileToXDocument>()
				.Debatch<XDocumentToXElements>()
				.Transform<XElementToCsvRow>()
				.Batch(size: 10)
				.Send<StringsToFileInLocalDirectory>()
				.WithRetryStrategy<SimpleRetryStrategy>()
				.ToOperation();
		}
	}
}
