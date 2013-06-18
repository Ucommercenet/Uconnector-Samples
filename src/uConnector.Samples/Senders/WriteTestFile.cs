using System;
using System.IO;
using UConnector.Framework;

namespace UConnector.Samples.Senders
{
	public class WriteTestFile : Configurable, ISender<DateTime>
	{
		private Random _random = new Random();

		[Required]
		public string Filename { get; set; }

		public void Send(DateTime input)
		{
			using (var writer = File.Exists(Filename) ? new StreamWriter(File.OpenWrite(Filename)) : File.CreateText(Filename))
			{
				WriteToFile(writer);
			}
		}

		private void WriteToFile(StreamWriter writer)
		{
			int numberOfNumbers = _random.Next(100);
			for (int i = 0; i < numberOfNumbers; i++)
			{
				int nextNumber = _random.Next(1000);
				writer.Write("{0} ", nextNumber == 0 ? "XX" : nextNumber.ToString());
				if ((i % 10) == 9)
				{
					writer.WriteLine();
				}
			}
		}
	}
}
