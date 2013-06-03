using System;
using System.Collections.Generic;
using System.IO;
using UConnector.Attributes;
using UConnector.Config;
using UConnector.Framework;

namespace UConnector.Samples.Operations.UCommerce.ImportLocalFile.Cogs
{
	public class StringIteratorToFile : Configurable, ISender<IEnumerable<string>>
	{
		[Required]
		public string Filename { get; set; }

		[Required]
		public string Directory { get; set; }

		public void Send(IEnumerable<string> input)
		{
			MaybeThrowException();
			var filename = Filename.Replace("{DateTime.Now.Ticks}", DateTime.Now.Ticks.ToString());
			filename = Path.Combine(Directory, filename);

			using ( StreamWriter writer = File.Exists(filename) ? new StreamWriter(File.OpenWrite(filename)) : File.CreateText(filename) )
			{
				WriteToFile(writer, input);
			}
		}

		private void MaybeThrowException()
		{

			int rand = new Random().Next(100);

			if (rand > 95)
			{
				throw new Exception("Random exception thrown! HAHAHAHAHA!");
			}
		}

		private void WriteToFile(StreamWriter writer, IEnumerable<string> data)
		{
			foreach (var line in data)
			{
				writer.WriteLine(line);
			}
		}
	}
}
