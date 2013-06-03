using System.Collections.Generic;
using System.IO;
using UConnector.Attributes;
using UConnector.Config;
using UConnector.Framework;

namespace UConnector.Samples.Operations.Sandbox.Cogs
{
	public class SenderWriteIntegersToFile : Configurable, ISender<IEnumerable<int>>
	{
		private static readonly object OneAccessAtATime = new object();

		[Required]
		public string LogFilename { get; set; }


		public void Send(IEnumerable<int> input)
		{
			foreach (var i in input)
			{
				Send(i);
			}
		}

		private void Send(int input)
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
