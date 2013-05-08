using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UConnector.Attributes;
using UConnector.Cogs;
using UConnector.Config;

namespace UConnector.Samples.Operations.Sandbox.Cogs
{
	public class SenderWriteIntegerToFile : Configurable, ISender<int>
	{
		private static readonly object OneAccessAtATime = new object();

		[Required]
		public string LogFilename { get; set; }

		public void Send(int input)
		{
			string dataToWrite = string.Format("{0}", input);

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
