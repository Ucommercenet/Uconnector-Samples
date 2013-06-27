﻿using UConnector.Api.V1;
using UConnector.IO;
using UConnector.IO.Excel;
using UConnector.Samples.UCommerce;

namespace UConnector.Samples.Operations.UCommerce.ExportLocalFile
{
	public class ExportProductsToLocalExcelFile : Operation
	{
		protected override IOperation BuildOperation()
		{
			return FluentOperationBuilder
				.Receive<ProductListFromUCommerce>()
				.Transform<ProductListToDataTable>()
				.Transform<FromDataTableToExcelStream>()
				.Transform<StreamToWorkfileWithTimestampName>()
					.WithOption(a => a.Extension = ".xlsx")
				.Batch(size: 1)
				.Send<FilesToLocalDirectory>()
					.WithOption(x => x.Overwrite = true)
				.ToOperation();
		}
	}
}
