using System.IO;
using UConnector.Attributes;
using UConnector.Cogs;
using UConnector.Config;
using UConnector.Extensions;
using UConnector.Extensions.Model;

namespace UConnector.Samples.Operations.Copy.Cogs
{
    public class CopyFileToDirectorySender : Configurable, ISender<WorkFile>
    {
        [Required]
        public bool Overwrite { get; set; }

        [Required]
        public string Directory { get; set; }

        public void Send(WorkFile input)
        {
            var directoryInfo = new DirectoryInfo(Directory);
            if (!directoryInfo.Exists)
                throw new DirectoryNotFoundException(string.Format("Could not find: '{0}'", directoryInfo.FullName));

            Stream stream;
            var path = Path.Combine(directoryInfo.FullName, input.Name);
            if(Overwrite)
                stream = new FileStream(path, FileMode.Create);
            else
                stream = new FileStream(path, FileMode.CreateNew);

            input.Stream.CopyStreamTo(stream);
            stream.Dispose();
            input.Stream.Dispose();
        }
    }
}