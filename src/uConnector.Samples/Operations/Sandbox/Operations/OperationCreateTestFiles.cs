using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UConnector.Config;
using UConnector.Samples.Operations.Others.DateTimeManipulation.Cogs;
using UConnector.Samples.Operations.Sandbox.Cogs;

namespace UConnector.Samples.Operations.Sandbox.Operations
{
	public class OperationCreateTestFiles : CustomOperation
	{
		protected override IOperation BuildOperation()
		{
			return OperationBuilder.Create()
								   .Receive<DateTimeNowReceiver>()
								   .Send<SenderWriteTestFile>().GetOperation();
		}
	}
}
