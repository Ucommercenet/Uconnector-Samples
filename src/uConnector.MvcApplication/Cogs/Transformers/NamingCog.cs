using System;
using System.IO;
using UConnector.IO;
using UConnector.Framework;

namespace UConnector.MvcApplication.Cogs.Transformers
{
	public class NamingCog : ITransformer<Stream, WorkFile>
    {
        /// <summary>
        /// Gets or sets the extension. Including the dot.
        /// </summary>
        /// <value>
        /// The extension.
        /// </value>
        [Required]
        public string Extension { get; set; }

		public string DateTimeFormat { get; set; }

        public WorkFile Execute(Stream input)
        {
			var filename = DateTime.Now.ToString((DateTimeFormat ?? "yyyy-MM-dd HH:mm:ss").Replace(':', '_').Replace('-', '_').Replace(' ', '-')) + Extension;
            return new WorkFile(input, filename, "");
        }
    }
}