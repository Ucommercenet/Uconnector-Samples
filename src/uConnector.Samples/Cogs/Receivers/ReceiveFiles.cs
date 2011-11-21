using System.IO;
using System.Linq;
using UConnector.Attributes;
using UConnector.Cogs;
using UConnector.Config;

namespace UConnector.Samples.Cogs.Receive
{
    public class ReceiveFiles : AbstractConfiguable, IReceiver<FileInfo[]>
    {
        public ReceiveFiles()
        {
            Filter = "*.*";
        }

        [Required]
        public string Directory { get; set; }

        public string Filter { get; set; }

        #region IReceive<FileInfo[]> Members

        public FileInfo[] Receive()
        {
            var directoryInfo = new DirectoryInfo(Directory);
            if (!directoryInfo.Exists)
                throw new DirectoryNotFoundException(Directory);

            return directoryInfo.GetFiles(Filter).ToArray();
        }

        #endregion
    }
}