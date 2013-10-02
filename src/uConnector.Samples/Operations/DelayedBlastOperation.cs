using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UConnector.Api.V1;
using UConnector.Samples.Helpers;

namespace UConnector.Samples.Operations
{
	public class DelayedBlastOperation : Operation
	{
		protected override IOperation BuildOperation()
		{
			return
				FluentOperationBuilder.Receive<DelayedBlastReceiver>()
				                      .Transform<DelayedBlastTransformer>()
				                      .Send<DelayedBlastSender>()
				                      .ToOperation();
		}
	}
}
