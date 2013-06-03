using UConnector.Config.Fluent.V1;
using UConnector.Extensions.Receivers;
using UConnector.Samples.Framework;
using UConnector.Samples.Senders;
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
				.Transform<XDocumentToXElements>()
				.Debatch()
				.Transform<XElementToCsvRow>()
				.Batch(size: 10)
				.Send<StringsToFile>()
				.WithRetryStrategy<SimpleRetryStrategy>()
				.ToOperation();
		}
	}
}
