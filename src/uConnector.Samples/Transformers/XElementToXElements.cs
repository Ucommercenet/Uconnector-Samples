using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UConnector.Config;
using UConnector.Framework;
using UConnector.Attributes;

namespace UConnector.Samples.Transformers
{
	public class XElementToXElements : Configurable, ITransformer<XElement, IEnumerable<XElement>>
	{
		[Required]
		public string DescendendsName { get; set; }

		public IEnumerable<XElement> Execute(XElement input)
		{
			var elements = from e in input.Descendants()
						   where e.Name.LocalName == DescendendsName
						   select e;

			return elements;
		}
	}
}
