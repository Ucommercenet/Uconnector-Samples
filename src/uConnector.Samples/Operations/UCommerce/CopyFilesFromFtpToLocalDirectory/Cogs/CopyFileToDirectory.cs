using System.IO;
using UConnector.Attributes;
using UConnector.Config;
using UConnector.Extensions;
using UConnector.Extensions.Model;
using UConnector.Framework;

namespace UConnector.Samples.Operations.UCommerce.CopyFilesFromFtpToLocalDirectory.Cogs
{
    public class CopyFileToDirectory : Configurable, ISender<WorkFile>
    {
        [Required]
        public bool Overwrite { get; set; }

        [Required]
        public string Directory { get; set; }

        public void Send(WorkFile input)
        {
            var directoryInfo = new DirectoryInfo(Directory);
            if (!directoryInfo.Exists)
                throw new DirectoryNotFoundException(string.Format("Directory not found: '{0}'", directoryInfo.FullName));

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