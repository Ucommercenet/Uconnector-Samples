using System.Collections.Generic;
using System.IO;
using System.Linq;
using UConnector.Attributes;
using UConnector.Config;
using UConnector.Framework;

namespace UConnector.Samples.Operations.Others.DeleteFiles.Senders
{
    public class DeleteFilesSender : Configurable, ISender<FileInfo[]>
    {
        [Required]
        public int Count { get; set; }

        #region ISend<FileInfo[]> Members

        public void Send(FileInfo[] input)
        {
            IEnumerable<FileInfo> fileInfos = input.OrderByDescending(a => a.CreationTime).Skip(Count);

            foreach (FileInfo info in fileInfos)
            {
                if (!info.Exists)
                    continue;

                info.Delete();
            }
        }

        #endregion
    }
}