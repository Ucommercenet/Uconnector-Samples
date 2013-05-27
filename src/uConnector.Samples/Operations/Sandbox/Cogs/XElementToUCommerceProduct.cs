using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UCommerce.EntitiesV2;
using UConnector.Cogs;

namespace UConnector.Samples.Operations.Sandbox.Cogs
{
	public class XElementToUCommerceProduct : ICog<XElement, Product>
	{
		public Product Execute(XElement input)
		{
			var product = new Product();
			product.Name = FindProductName(input);
			product.Sku = FindSku(input);

			return product;
		}

		private string FindProductName(XElement input)
		{
			return FindElementValue(input, "ItemName");
		}

		private string FindSku(XElement input)
		{
			return FindElementValue(input, "ItemId");
		}

		private string FindElementValue(XElement element, string name)
		{
			var val = (from e in element.Elements()
						where e.Name.LocalName == name
						select e.Value).FirstOrDefault();

			return val;
		}
	}
}
