using UConnector.Config.Fluent.v1;
using UConnector.Samples.Operations.UCommerce.ImportLocalFile.Cogs;

namespace UConnector.Samples.Operations.Sandbox.Operations
{
	public class OperationXmlToCsv : CustomOperation
	{
		protected override IOperation BuildOperation()
		{
			return FluentOperationBuilder.Receive<LocalFilesReceiver>().WithOption(x => x.Pattern = "*.xml")
								   .Debatch()
								   .Transform<WorkFileToXDocument>()
								   .Transform<XDocumentToXElementIterator>()
								   .Debatch()
								   .Transform<XElementToCsvRow>()
								   .Batch(10)
								   .Send<StringIteratorToFile>()
								   .ToOperation();
		}
	}
}
