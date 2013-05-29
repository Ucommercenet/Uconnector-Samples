using System.Collections.Generic;
using UCommerce.EntitiesV2;
using UConnector.Cogs;

namespace UConnector.Samples.Operations.UCommerce.ExportProductListToFtp.Cogs
{
    public class ProductListReceiver : IReceiver<IEnumerable<Product>>
    {
        public IEnumerable<Product> Receive()
        {
            return Product.Find(x => x.ParentProduct == null);
        }
    }
}