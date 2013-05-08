using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UConnector.Config;
using UConnector.Samples.Operations.Sandbox.Cogs;
using UConnector.Samples.Operations.UCommerce.ImportLocalFile.Cogs;

namespace UConnector.Samples.Operations.Sandbox.Operations
{
	public class SandboxOperation : CustomOperation
	{
		protected override IOperation BuildOperation()
		{
			return OperationBuilder.Create()
								   .Receive<LocalFilesReceiver>()
								   .Debatching()
								   .Cog<SplitFileIntoLines>()
								   .Debatching()
								   .Cog<SumStringWithNumbers>()
								   .Send<SenderWriteIntegerToFile>()
								   .WithOption(s => s.LogFilename = @"C:\uConnector\Out\SumOfSums.txt")
								   .GetOperation();

			//return OperationBuilder.Create()
			//				   .Receive<LocalFilesReceiver>()
			//				   .Debatching()
			//				   .Cog<SplitFileIntoLines>()
			//				   .Cog<SumStringsWithNumbers>()
			//				   .Send<SenderWriteIntegersToFile>()
			//				   .WithOption(s => s.LogFilename = @"C:\uConnector\Out\SumOfSums.txt")
			//				   .GetOperation();
		}
	}
}
