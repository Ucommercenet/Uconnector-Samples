using UConnector.Config.Fluent.v1;
using UConnector.Samples.Operations.Others.DateTimeManipulation.Cogs;
using UConnector.Samples.Operations.Sandbox.Cogs;

namespace UConnector.Samples.Operations.Sandbox.Operations
{
	public class OperationCreateTestFiles : CustomOperation
	{
		protected override IOperation BuildOperation()
		{
			return FluentOperationBuilder
				.Receive<DateTimeNowReceiver>()
				.Send<SenderWriteTestFile>().GetOperation();
		}
	}
}
