using System;
using System.Collections.Generic;
using System.IO;
using Common.Logging;
using UConnector.Attributes;
using UConnector.Cogs;
using UConnector.Config;
using UConnector.Extensions.Cogs.Adapters;
using UConnector.Extensions.Model;
using UConnector.Extensions;
using UCommerce.Extensions;
using System.Linq;

namespace UConnector.Samples.Operations.UCommerce.ImportLocalFile.Cogs
{
    public class LocalFilesReceiver : Configurable, IReceiver<IEnumerable<WorkFile>>
    {
        private ILog _Log = LogManager.GetCurrentClassLogger();

        private class FileInfoEqualityComparer : IEqualityComparer<FileInfo>
        {
            public bool Equals(FileInfo x, FileInfo y)
            {
                return x.FullName == y.FullName;
            }

            public int GetHashCode(FileInfo obj)
            {
                return obj.FullName.GetHashCode();
            }
        }

        /// <summary>
        /// Gets or sets the pattern. Default is: "*.*". Multiple items can be given by using '|' as seperator.
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

        public bool DeleteFile { get; set; }

        public int Take { get; set; }
        public int Skip { get; set; }

        public LocalFilesReceiver()
        {
            Pattern = "*.*";
            Take = int.MaxValue;
            SearchOption = SearchOption.AllDirectories;
        }

        private void Delete(FileInfo fileInfo)
        {
            if (DeleteFile)
            {
                if (fileInfo.Exists)
                {
                    _Log.InfoFormat("Deleting file: {0}", fileInfo.FullName);
                    fileInfo.Delete();
                }
                else
                {
                    _Log.InfoFormat("Could not delete file: '{0}' bacause it does not exist.", fileInfo.FullName);

                }
            }
            else
            {
                _Log.Info(string.Format("Not deleting file: {0} because {1} was false.", fileInfo.FullName, PropertyName.For<FtpFilesAdapter>(a => a.DeleteFile)));
            }
        }

        public IEnumerable<WorkFile> Receive()
        {
            var directoryInfo = new DirectoryInfo(Directory);
            if (!directoryInfo.Exists)
                throw new DirectoryNotFoundException(string.Format("Directory not found: '{0}'", directoryInfo.FullName));

            var workFiles = new List<WorkFile>();
            var fileInfos = new HashSet<FileInfo>();
            _Log.InfoFormat("Searching for files in: {0}", directoryInfo.FullName);
            foreach(var pattern in Pattern.Split(new []{"|"}, StringSplitOptions.RemoveEmptyEntries))
            {
                _Log.InfoFormat("With pattern '{0}'", directoryInfo.FullName, pattern);
                directoryInfo.GetFiles(pattern.Trim(), SearchOption).ForEach(fileInfo => fileInfos.Add(fileInfo));
            }

            _Log.InfoFormat("Skipping '{0}', Taking '{1}'", Skip, Take);
            foreach (var fileInfo in fileInfos.Skip(Skip).Take(Take))
            {
                _Log.InfoFormat("Creating stream with '{0}'", fileInfo.FullName);
                var memoryStream = new MemoryStream();
                using (var fileStream = new FileStream(fileInfo.FullName, FileMode.Open))
                {
                    fileStream.CopyStreamTo(memoryStream);
                }
                var workFile = new WorkFile(memoryStream, fileInfo.Name, fileInfo.DirectoryName);
                workFiles.Add(workFile);
                Delete(fileInfo);
            }

            _Log.InfoFormat("Returning '{0}' work files from {1}", workFiles.Count, GetType().FullName);
            return workFiles;
        }
    }
}