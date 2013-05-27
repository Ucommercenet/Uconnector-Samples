using UConnector.Config.Fluent.v1;
using UConnector.Extensions.Cogs.Adapters;
using UConnector.Extensions.Cogs.Transformers;
using UConnector.Samples.Operations.Others.FetchFile.Cogs;

namespace UConnector.Samples.Operations.Others.FetchFile
{
    public class FetchFileOperation : CustomOperation
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
                .Decision<BailOutIfStreamIsNullDecision>(
                    FluentOperationBuilder.CreateSubOperation().Transform<WorkFilesToStringCog>()
						.Transform<PrintStringCog>().ToOperation())
						.ToOperation();
        }
    }
}