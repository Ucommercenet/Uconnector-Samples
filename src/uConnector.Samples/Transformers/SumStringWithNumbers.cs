using System;
using System.Linq;
using UConnector.Framework;

namespace UConnector.Samples.Transformers
{
	public class SumStringWithNumbers : ITransformer<string, int>
	{
		public int Execute(string input)
		{
			var parts = input.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
			int sum = parts.Sum(part => int.Parse(part));

			return sum;
		}
	}
}
