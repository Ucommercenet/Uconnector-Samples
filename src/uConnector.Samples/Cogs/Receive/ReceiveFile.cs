using System.IO;
using System.Linq;
using UConnector.Attributes;
using UConnector.Cogs;
using UConnector.Config;

namespace UConnector.Samples.Cogs.Receive
{
    public class ReceiveFile : AbstractConfiguable, IReceive<FileInfo>
    {
        [Required]
        public string Directory { get; set; }

        #region IReceive<FileInfo> Members

        public FileInfo Receive()
        {
            var directoryInfo = new DirectoryInfo(Directory);
            if (!directoryInfo.Exists)
                throw new DirectoryNotFoundException(Directory);

            return directoryInfo.GetFiles().FirstOrDefault();
        }

        #endregion
    }
}