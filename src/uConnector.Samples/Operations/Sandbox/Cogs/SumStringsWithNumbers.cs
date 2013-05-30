using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UConnector.Cogs;

namespace UConnector.Samples.Operations.Sandbox.Cogs
{
	public class SumStringsWithNumbers : ITransformer<IEnumerable<string>, IEnumerable<int>>
	{
		public IEnumerable<int> Execute(IEnumerable<string> input)
		{
			foreach (var s in input)
			{
				var parts = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				int sum = parts.Sum(part => int.Parse(part));

				yield return sum;
			}
		}
	}
}
