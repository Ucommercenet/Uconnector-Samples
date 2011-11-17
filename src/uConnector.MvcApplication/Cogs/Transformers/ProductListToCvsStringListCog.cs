using System.Collections.Generic;
using UCommerce.EntitiesV2;
using UConnector.Cogs;
using UConnector.Extensions.Cogs.Transformers;

namespace UConnector.MvcApplication.Cogs.Transformers
{
    public class ProductListToCvsStringListCog : ICog<IEnumerable<Product>, IEnumerable<string>>
    {
        #region ICog<IEnumerable<Product>,IEnumerable<string>> Members

        public IEnumerable<string> ExecuteCog(IEnumerable<Product> @from)
        {
            var list = new List<string>();

            var transformer = new ProductToCvsStringCog();
            foreach (Product item in @from)
            {
                list.Add(transformer.ExecuteCog(item));
            }

            return list;
        }

        #endregion
    }
}