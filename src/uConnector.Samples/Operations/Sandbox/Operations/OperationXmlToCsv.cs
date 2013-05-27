using UConnector.Config.Fluent.v1;
using UConnector.Samples.Operations.UCommerce.ImportLocalFile.Cogs;

namespace UConnector.Samples.Operations.Sandbox.Operations
{
	public class OperationXmlToCsv : CustomOperation
	{
		protected override IOperation BuildOperation()
		{
			return FluentOperationBuilder.Receive<LocalFilesReceiver>().WithOption(x => x.Pattern = "*.xml")
								   .Debatching()
								   .Transform<WorkFileToXDocument>()
								   .Transform<XDocumentToXElementIterator>()
								   .Debatching()
								   .Transform<XElementToCsvRow>()
								   .Batching(10)
								   .Send<StringIteratorToFile>()
								   .GetOperation();
		}
	}
}
