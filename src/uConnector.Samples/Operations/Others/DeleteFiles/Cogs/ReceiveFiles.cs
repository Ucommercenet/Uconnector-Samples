using System.IO;
using System.Linq;
using UConnector.Attributes;
using UConnector.Cogs;
using UConnector.Config;

namespace UConnector.Samples.Operations.Others.DeleteFiles.Cogs
{
    public class FilesReceiver : Configurable, IReceiver<FileInfo[]>
    {
        public FilesReceiver()
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