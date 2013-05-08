using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UConnector.Cogs;

namespace UConnector.Samples.Operations.Sandbox.Cogs
{
	public class SumStringWithNumbers : ICog<string, int>
	{
		public int Execute(string input)
		{
			var parts = input.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
			int sum = parts.Sum(part => int.Parse(part));

			return sum;
		}
	}
}
