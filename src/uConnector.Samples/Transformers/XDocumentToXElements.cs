using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UConnector.Attributes;
using UConnector.Config;
using UConnector.Framework;

namespace UConnector.Samples.Transformers
{
	public class XDocumentToXElements : Configurable, ITransformer<XDocument, IEnumerable<XElement>>
	{
        [Required]
        public string DescendendsName { get; set; }

		public IEnumerable<XElement> Execute(XDocument input)
		{
			var elements = from e in input.Descendants()
			               where e.Name.LocalName == DescendendsName
			               select e;
			
			return elements;
		}
	}
}
