using UConnector.Config;
using UConnector.Extensions.Cogs.Adapters;
using UConnector.Extensions.Cogs.Transformers;
using UConnector.Samples.Operations.FetchFIle.Cogs;

namespace UConnector.Samples.Operations.FetchFIle
{
    public class FetchFileOperation : AbstractOperation
    {
        protected override OperationBuilder Build()
        {
            return OperationBuilder.Create()
                .Receive<FtpFileAdapter>()
                .WithOption(x => x.Username = "ftp.syska.dk|uconnector")
                .WithOption(x => x.Password = "uconnector")
                .WithOption(x => x.Hostname = "ftp.syska.dk")
                .WithOption(x => x.Filename = "file.txt")
                .WithOption(x => x.Directory = "/src")
                .Decision<BailOutIfStreamIsNullDecision>(
                    OperationBuilder.Create().Cog<StreamToStringCog>()
                        .Cog<PrintStringCog>().GetFirstStep(), null);
        }
    }
}