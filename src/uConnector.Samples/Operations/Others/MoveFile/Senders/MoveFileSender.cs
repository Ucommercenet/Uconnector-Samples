using System;
using System.IO;
using UConnector.Framework;

namespace UConnector.Samples.Operations.Others.MoveFile.Senders
{
    public class MoveFileSender : Configurable, ISender<FileInfo>
    {
        [Required]
        public string Directory { get; set; }

        #region ISend<FileInfo> Members

        public void Send(FileInfo input)
        {
			if (input == null) { return; }

            if (!input.Exists)
                throw new FileNotFoundException(input.FullName);

            string combine = Path.Combine(Directory, input.Name);
            Console.WriteLine(combine);
            input.MoveTo(combine);
        }

        #endregion
    }
}