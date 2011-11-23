using System.IO;
using UConnector.Cogs;

namespace UConnector.Samples.Operations.MoveFile.Cogs
{
    public class BailOutIfFileInfoIsNullDecision : IDecision<FileInfo>
    {
        #region IDecision<FileInfo> Members

        public bool Decide(FileInfo input)
        {
            if (input == null)
                return false;

            return true;
        }

        #endregion
    }
}