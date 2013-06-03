using System.Xml.Linq;
using UConnector.Extensions.Model;
using UConnector.Framework;

namespace UConnector.Samples.Operations.UCommerce.ImportLocalFile.Cogs
{
	public class WorkFileToXDocument : ITransformer<WorkFile, XDocument>
	{
		public XDocument Execute(WorkFile input)
		{
			XDocument doc = XDocument.Load(input.Stream);
			return doc;
		}
	}
}
