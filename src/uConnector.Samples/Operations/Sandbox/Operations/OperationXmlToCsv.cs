using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UConnector.Config;
using UConnector.Samples.Operations.Sandbox.Cogs;
using UConnector.Samples.Operations.UCommerce.ImportLocalFile.Cogs;

namespace UConnector.Samples.Operations.Sandbox.Operations
{
	public class OperationXmlToCsv : CustomOperation
	{
		protected override IOperation BuildOperation()
		{
			return OperationBuilder.Create()
								   .Receive<LocalFilesReceiver>().WithOption(x => x.Pattern = "*.xml")
								   .Debatching()
								   .Cog<WorkFileToXDocument>()
								   .Cog<XDocumentToXElementIterator>()
								   .Debatching()
								   .Cog<XElementToCsvRow>()
								   .Batching(6)
								   .Send<StringIteratorToFile>()
								   .GetOperation();
		}
	}
}
