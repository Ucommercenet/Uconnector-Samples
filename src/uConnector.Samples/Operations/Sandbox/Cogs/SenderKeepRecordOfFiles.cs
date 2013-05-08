using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UConnector.Attributes;
using UConnector.Cogs;
using UConnector.Config;
using UConnector.Extensions.Model;

namespace UConnector.Samples.Operations.Sandbox.Cogs
{
	public class SenderKeepRecordOfFiles : Configurable, ISender<WorkFile>
	{
		private static readonly object OneAccessAtATime = new object();

		[Required]
		public string LogFilename { get; set; }

		public void Send(WorkFile input)
		{
			string dataToWrite = string.Format("File '{0}' processed at {1}", input.FullName, DateTime.Now);

			lock (OneAccessAtATime)
			{
				if (!File.Exists(LogFilename))
				{
					using (StreamWriter writer = File.CreateText(LogFilename))
					{
						writer.WriteLine(dataToWrite);
					}
				}
				else
				{
					using (StreamWriter writer = File.AppendText(LogFilename))
					{
						writer.WriteLine(dataToWrite);
					}
				}
			}
		}
	}
}
