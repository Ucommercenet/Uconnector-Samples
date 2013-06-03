using System.Collections.Generic;
using UConnector.Config.Fluent.v1;
using UConnector.Extensions.Adapters;
using UConnector.Extensions.Model;
using UConnector.Framework;

namespace UConnector.Samples.Operations.UCommerce.CleanFtp
{
    public class CleanFtpOperation : Operation
    {
        protected override IOperation BuildOperation()
        {
            return FluentOperationBuilder
				.Receive<FtpFilesAdapter>().Send(new NoOpSender())
                .ToOperation();
        }
	
		private class NoOpSender : ISender<IEnumerable<WorkFile>>
		{
			public void Send(IEnumerable<WorkFile> input) { }
		}
	}
}