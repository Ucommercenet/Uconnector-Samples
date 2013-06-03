using UCommerce.EntitiesV2;
using UConnector.Framework;

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
