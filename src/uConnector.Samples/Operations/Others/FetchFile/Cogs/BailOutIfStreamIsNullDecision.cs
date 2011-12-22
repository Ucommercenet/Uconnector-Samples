using System.IO;
using UConnector.Cogs;

namespace UConnector.Samples.Operations.Others.FetchFile.Cogs
{
    public class BailOutIfStreamIsNullDecision : IDecision<Stream>
    {
        #region IDecision<Stream> Members

        public bool Decide(Stream input)
        {
            if (input == null)
                return false;

            return true;
        }

        #endregion
    }
}