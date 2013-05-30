using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UCommerce.EntitiesV2;
using UConnector.Cogs;

namespace UConnector.Samples.Operations.Sandbox.Cogs
{
	public class ProductToProductName : ITransformer<Product, string>
	{
		public string Execute(Product input)
		{
			return input.Name;
		}
	}
}
