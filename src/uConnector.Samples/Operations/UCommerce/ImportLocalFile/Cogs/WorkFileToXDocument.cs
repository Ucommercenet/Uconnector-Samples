using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UConnector.Cogs;
using UConnector.Extensions.Model;

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
