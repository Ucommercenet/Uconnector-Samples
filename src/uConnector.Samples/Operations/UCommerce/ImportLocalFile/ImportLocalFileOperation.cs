using UConnector.Config.Fluent.v1;
using UConnector.Extensions.Cogs.Decisions;
using UConnector.Extensions.Model;
using UConnector.Samples.Operations.UCommerce.ImportLocalFile.Cogs;
using UConnector.Samples.Operations.UCommerce.ImportLocalFile.SubOperations;

namespace UConnector.Samples.Operations.UCommerce.ImportLocalFile
{
    public class ImportLocalFileOperation : CustomOperation
    {
        protected override IOperation BuildOperation()
        {
            return FluentOperationBuilder
				.Receive<LocalFilesReceiver>()
                .Debatch()
                .Decision<InvokeMethodDecision<WorkFile>, ImportExcelSubOperation>()
                .WithOption(x => x.Method = workFile => workFile.Name.EndsWith(".xlsx"))
                .Decision<CsvWorkFileDecision, ImportCsvSubOperation>().ToOperation();
        }
    }
}