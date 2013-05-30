using System.Collections.Generic;
using UConnector.Cogs;
using UConnector.Config.Fluent.v1;
using UConnector.Extensions.Cogs.Adapters;
using UConnector.Extensions.Model;

namespace UConnector.Samples.Operations.UCommerce.CleanFtp
{
    public class CleanFtpOperation : CustomOperation
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