using UConnector.Framework;
using UConnector.MvcApplication.Cogs.Models;

namespace UConnector.MvcApplication.Cogs.Transformers
{
    public class IfTypeInfoTypeIsExcelDecision : IDecision<TypeInfo>
    {
        public bool Decide(TypeInfo input)
        {
            if (input.Type == DownloadAs.Excel)
                return true;

            return false;
        }
    }
}