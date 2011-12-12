using UConnector.Config;
using UConnector.Extensions.Cogs.Adapters;
using UConnector.Extensions.Cogs.Transformers;
using UConnector.Samples.Operations.Others.FetchFile.Cogs;

namespace UConnector.Samples.Operations.Others.FetchFile
{
    public class FetchFileOperation : CustomOperation
    {
        protected override Operation BuildOperation()
        {
            return OperationBuilder.Create()
                .Receive<FtpFilesAdapter>()
                .WithOption(x => x.Username = "ftp.syska.dk|uconnector")
                .WithOption(x => x.Password = "uconnector")
                .WithOption(x => x.Hostname = "ftp.syska.dk")
                .WithOption(x => x.Pattern = "file.txt")
                .WithOption(x => x.Directory = "/src")
                .Decision<BailOutIfStreamIsNullDecision>(
                    OperationBuilder.Create().Cog<WorkFilesToStringCog>()
                        .Cog<PrintStringCog>().GetFirstStep()).GetOperation();
        }
    }
}