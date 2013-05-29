﻿using System.IO;
using UConnector.Config.Fluent.v1;
using UConnector.Extensions.Cogs.Senders;
using UConnector.Extensions.Cogs.Transformers;
using UConnector.Extensions.Cogs.TwoWayCogs;
using UConnector.Samples.Operations.Sandbox.Cogs;
using UConnector.Samples.Operations.UCommerce.CopyFilesFromFtpToLocalDirectory.Cogs;
using UConnector.Samples.Operations.UCommerce.ExportProductListToFtp.Cogs;
using UConnector.Samples.Operations.UCommerce.ImportLocalFile.Cogs;

namespace UConnector.Samples.Operations.Sandbox.Operations
{
	public class SandboxOperation : CustomOperation
	{
		protected override IOperation BuildOperation()
		{
			return FluentOperationBuilder
				.Receive<LocalFilesReceiver>()
				.WithOption(x => x.Pattern = "*.xml")
				.WithOption(x => x.DeleteFile = true)
				.WithOption(x => x.Directory = @"C:\uConnector\In")
				.WithOption(x => x.SearchOption = SearchOption.TopDirectoryOnly)
				.WithOption(x => x.Take = 10)
				.WithOption(x => x.Skip = 0)
				.Debatch()
				.Transform<WorkFileToXDocument>()
				.Transform<XDocumentToXElementIterator>()
				.WithOption(x => x.DescendendsName = "InventTable_1")
				.Debatch()
				.Transform<XElementToUCommerceProduct>()
				.Batch()
				.Send<ProductListSender>()
				.ToOperation();
			//.Transform<ProductListToDataTableCog>()
			//.Transform<ExcelCog>()
			//.Transform<NamingCog>().WithOption(a => a.Extension = ".xlsx")
			//.Send<CopyFileToDirectorySender>()
			//.WithOption(x => x.Directory = @"C:\uConnector\Out")
			//.WithOption(x => x.Overwrite = true)
			//.ToOperation();
		}
	}
}
