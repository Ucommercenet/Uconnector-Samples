using UConnector.Config;
using UConnector.Extensions.Cogs.Adapters;
using UConnector.Extensions.Cogs.Transformers;
using UConnector.Samples.Cogs.Decision;
using UConnector.Samples.Cogs.Transformers;

namespace UConnector.Samples.Operations
{
    public class FetchFileOperation : AbstractOperation
    {
        protected override OperationBuilder Build()
        {
            return OperationBuilder.Create()
                .Receive<FtpFileAdapter>()
                .WithOption(x => x.Username, "ftp.syska.dk|syska")
                .WithOption(x => x.Password, "super")
                .WithOption(x => x.Hostname, "ftp.syska.dk")
                .WithOption(x => x.Filename, "file.txt")
                .WithOption(x => x.Directory, "/uconnector-src")
                .Decision<BailOutIfStreamIsNullDecision>(
                    OperationBuilder.Create().Cog<StreamToStringCog>()
                        .Cog<PrintStringCog>().GetFirstStep(), null);
        }
    }
}