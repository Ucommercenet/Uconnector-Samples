﻿using System;
using System.IO;
using UConnector.Attributes;
using UConnector.Cogs;
using UConnector.Extensions.Cogs.Transformers;
using UConnector.Extensions.Model;

namespace UConnector.Samples.Operations.UCommerce.ExportProductListToFtp.Cogs
{
    public class NamingCog : ICog<Stream, WorkFile>
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