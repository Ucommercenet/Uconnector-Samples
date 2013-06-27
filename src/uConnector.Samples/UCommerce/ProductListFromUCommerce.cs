using System.Collections.Generic;
using UCommerce.EntitiesV2;
using UConnector.Framework;

namespace UConnector.Samples.UCommerce
{
    public class ProductListFromUCommerce : IReceiver<IEnumerable<Product>>
    {
        public IEnumerable<Product> Receive()
        {
            return Product.Find(x => x.ParentProduct == null);
        }
    }
}