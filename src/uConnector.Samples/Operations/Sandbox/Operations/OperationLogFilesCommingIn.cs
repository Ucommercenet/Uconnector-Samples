using UConnector.Config.Fluent.v1;
using UConnector.Extensions.Receivers;
using UConnector.Samples.Operations.Sandbox.Cogs;

namespace UConnector.Samples.Operations.Sandbox.Operations
{
	public class OperationLogFilesCommingIn : Operation
	{
		protected override IOperation BuildOperation()
		{
			return FluentOperationBuilder
				.Receive<FilesFromLocalDirectory>()
				.Debatch()
				.Send<SenderKeepRecordOfFiles>()
				.ToOperation();
		}
	}
}
