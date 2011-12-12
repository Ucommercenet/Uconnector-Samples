using System;
using UConnector.Cogs;
using UConnector.Extensions.Model;

namespace UConnector.Samples.Operations.UCommerce.ImportLocalFile.Cogs
{
    public class CsvWorkFileDecision : IDecision<WorkFile>
    {
        public bool Decide(WorkFile input)
        {
            if (input.Name.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }
    }
}