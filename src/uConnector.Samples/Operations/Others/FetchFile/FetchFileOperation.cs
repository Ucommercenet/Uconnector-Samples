using UConnector.Config.Fluent.V1;
using UConnector.Extensions.Adapters;
using UConnector.Extensions.Transformers;
using UConnector.Samples.Operations.Others.FetchFile.Cogs;

namespace UConnector.Samples.Operations.Others.FetchFile
{
    public class FetchFileOperation : Operation
    {
        protected override IOperation BuildOperation()
        {
            return FluentOperationBuilder
				.Receive<FtpFilesAdapter>()
					.WithOption(x => x.Username = "ftp.syska.dk|uconnector")
					.WithOption(x => x.Password = "uconnector")
					.WithOption(x => x.Hostname = "ftp.syska.dk")
					.WithOption(x => x.Pattern = "file.txt")
					.WithOption(x => x.Directory = "/src")
                .Transform<WorkFilesToString>()
				.Send<ToStandardOut>()
				.ToOperation();
        }
    }
}