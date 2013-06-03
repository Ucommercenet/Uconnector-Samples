using UConnector.Config.Fluent.V1;
using UConnector.Samples.Operations.Others.DateTimeManipulation.Receivers;
using UConnector.Samples.Senders;

namespace UConnector.Samples.Operations.Sandbox
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
