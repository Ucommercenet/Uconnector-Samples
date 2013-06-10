using System.Xml.Linq;
using UConnector.Framework;

namespace UConnector.Samples.Transformers
{
	public class XElementToValue : ITransformer<XElement, string>
	{
		public string Execute(XElement input)
		{
			return input.Value;
		}
	}
}
