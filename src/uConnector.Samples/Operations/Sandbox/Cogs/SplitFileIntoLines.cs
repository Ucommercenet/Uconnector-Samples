﻿using System.Collections.Generic;
using System.IO;
using UConnector.Extensions.Model;
using UConnector.Framework;

namespace UConnector.Samples.Operations.Sandbox.Cogs
{
	public class SplitFileIntoLines : ITransformer<WorkFile, IEnumerable<string>>
	{
		public IEnumerable<string> Execute(WorkFile input)
		{
			using (TextReader reader = new StreamReader(input.Stream))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					yield return line;
				}
			}
		}
	}
}
