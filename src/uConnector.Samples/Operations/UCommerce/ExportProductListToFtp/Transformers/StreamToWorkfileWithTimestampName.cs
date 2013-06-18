using System;
using System.IO;
using UConnector.Filesystem;
using UConnector.Framework;
using UConnector.UCommerce;

namespace UConnector.Samples.Operations.UCommerce.ExportProductListToFtp.Transformers
{
	public class StreamToWorkfileWithTimestampName : ITransformer<Stream, WorkFile>
    {
        /// <summary>
        /// Gets or sets the extension. Including the dot.
        /// </summary>
        /// <value>
        /// The extension.
        /// </value>
        [Required]
        public string Extension { get; set; }

        public WorkFile Execute(Stream input)
        {
            var filename = DateTime.Now.ToString(UCommerceProduct.DATETIME_FORMAT
                .Replace(':', '_')
                .Replace('-', '_')
                .Replace(' ', '-')) + Extension;
            return new WorkFile(input, filename, "");
        }
    }
}