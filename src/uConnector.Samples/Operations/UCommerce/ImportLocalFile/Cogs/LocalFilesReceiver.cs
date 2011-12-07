using System.Collections.Generic;
using System.IO;
using UConnector.Attributes;
using UConnector.Cogs;
using UConnector.Config;
using UConnector.Extensions.Model;
using UConnector.Extensions;

namespace UConnector.Samples.Operations.UCommerce.ImportLocalFile.Cogs
{
    public class LocalFilesReceiver : Configurable, IReceiver<IEnumerable<WorkFile>>
    {
        /// <summary>
        /// Gets or sets the pattern. Default is: "*.*".
        /// </summary>
        /// <value>
        /// The search pattern.
        /// </value>
        public string Pattern { get; set; }

        /// <summary>
        /// Gets or sets the directory where to search for files.
        /// </summary>
        /// <value>
        /// The directory path.
        /// </value>
        [Required]
        public string Directory { get; set; }

        /// <summary>
        /// Gets or sets the search option. Valid value are: TopDirectoryOnly(default) and AllDirectories.
        /// </summary>
        /// <value>
        /// The search option.
        /// </value>
        public SearchOption SearchOption { get; set; }

        public LocalFilesReceiver()
        {
            Pattern = "*.*";
            SearchOption = SearchOption.AllDirectories;
        }

        public IEnumerable<WorkFile> Receive()
        {
            var directoryInfo = new DirectoryInfo(Directory);
            if (!directoryInfo.Exists)
                throw new DirectoryNotFoundException(string.Format("Directory not found: '{0}'", directoryInfo.FullName));

            var fileInfos = directoryInfo.GetFiles(Pattern, SearchOption);

            var workFiles = new List<WorkFile>();
            foreach (var fileInfo in fileInfos)
            {
                var memoryStream = new MemoryStream();
                using(var fileStream = new FileStream(fileInfo.FullName, FileMode.Open))
                {
                    fileStream.CopyStreamTo(memoryStream);
                }
                var workFile = new WorkFile(memoryStream, fileInfo.Name, fileInfo.DirectoryName);
                workFiles.Add(workFile);
            }

            return workFiles;
        }
    }
}