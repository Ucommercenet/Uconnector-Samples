﻿using UConnector.Api.V1;
using UConnector.IO;
using UConnector.Samples.UCommerce;

namespace UConnector.Samples.Operations.UCommerce.ImportLocalFile
{
	public class ImportLocalExcelFile: Operation
	{
		protected override IOperation BuildOperation()
		{
			return FluentOperationBuilder
				.Receive<FilesFromLocalDirectory>()
					.WithOption(x => x.Pattern = "*.xslx")
				.Debatch()
				.Transform<WorkFileToStream>()
				.Transform<FromExcelStreamToDataTable>()
				.Transform<DataTableToProductList>()
				.Send<ProductListToUCommerce>()
					.WithOption(x => x.ConnectionString = "server=.;database=U4;integrated security=SSPI;")
				.ToOperation();
		}
	}
}
