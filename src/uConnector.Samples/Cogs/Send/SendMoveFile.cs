using System;
using System.IO;
using UConnector.Attributes;
using UConnector.Cogs;
using UConnector.Config;

namespace UConnector.Samples.Cogs.Send
{
    public class SendMoveFile : AbstractConfiguable, ISend<FileInfo>
    {
        [Required]
        public string Directory { get; set; }

        #region ISend<FileInfo> Members

        public void Send(FileInfo input)
        {
            if (!input.Exists)
                throw new FileNotFoundException(input.FullName);

            string combine = Path.Combine(Directory, input.Name);
            Console.WriteLine(combine);
            input.MoveTo(combine);
        }

        #endregion
    }
}