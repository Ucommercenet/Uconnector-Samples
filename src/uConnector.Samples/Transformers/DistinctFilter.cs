using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UConnector.Framework;

namespace UConnector.Samples.Transformers
{
	public class DistinctFilter<T> : ITransformer<IEnumerable<T>, IEnumerable<T>>
	{
		public IEnumerable<T> Execute(IEnumerable<T> input)
		{
			return input.Distinct();
		}
	}
}
