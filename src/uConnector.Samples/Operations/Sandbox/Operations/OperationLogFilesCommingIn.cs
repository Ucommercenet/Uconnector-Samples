using UConnector.Config.Fluent.v1;
using UConnector.Samples.Operations.Sandbox.Cogs;
using UConnector.Samples.Operations.UCommerce.ImportLocalFile.Cogs;

namespace UConnector.Samples.Operations.Sandbox.Operations
{
	public class OperationLogFilesCommingIn : CustomOperation
	{
		protected override IOperation BuildOperation()
		{
			return FluentOperationBuilder
				.Receive<LocalFilesReceiver>()
				.Debatch()
				.Send<SenderKeepRecordOfFiles>().ToOperation();
		}
	}
}
