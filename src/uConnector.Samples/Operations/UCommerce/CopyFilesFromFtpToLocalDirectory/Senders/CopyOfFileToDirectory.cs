using System.IO;
using UConnector.Filesystem;
using UConnector.Framework;

namespace UConnector.Samples.Operations.UCommerce.CopyFilesFromFtpToLocalDirectory.Senders
{
    public class CopyOfFileToDirectory : Configurable, ISender<WorkFile>
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

            input.Stream.CopyTo(stream);
            stream.Dispose();
            input.Stream.Dispose();
        }
    }
}