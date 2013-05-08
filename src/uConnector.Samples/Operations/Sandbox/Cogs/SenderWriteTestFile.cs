﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UConnector.Attributes;
using UConnector.Cogs;
using UConnector.Config;

namespace UConnector.Samples.Operations.Sandbox.Cogs
{
	public class SenderWriteTestFile : Configurable, ISender<DateTime>
	{
		private Random _random = new Random();

		[Required]
		public string Filename { get; set; }

		public void Send(DateTime input)
		{
			if (!File.Exists(Filename))
			{
				using (StreamWriter writer = File.CreateText(Filename))
				{
					WriteToFile(writer);
				}
			}
			else
			{
				using (var writer = new StreamWriter(File.OpenWrite(Filename)))
				{
					WriteToFile(writer);
				}
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
