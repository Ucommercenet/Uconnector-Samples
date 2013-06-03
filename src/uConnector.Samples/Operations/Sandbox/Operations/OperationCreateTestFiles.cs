using UConnector.Config.Fluent.V1;
using UConnector.Samples.Operations.Others.DateTimeManipulation.Cogs;
using UConnector.Samples.Operations.Sandbox.Cogs;

namespace UConnector.Samples.Operations.Sandbox.Operations
{
	public class OperationCreateTestFiles : Operation
	{
		protected override IOperation BuildOperation()
		{
			return FluentOperationBuilder
				.Receive<DateTimeNow>()
				.Send<WriteTestFile>()
				.ToOperation();
		}
	}
}
