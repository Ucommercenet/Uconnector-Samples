using System.IO;
using UConnector.Framework;

namespace UConnector.Samples.Senders
{
	public class WriteIntegerToFile : Configurable, ISender<int>
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
