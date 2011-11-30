using System.Collections.Generic;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using UCommerce.EntitiesV2;
using UConnector.Cogs;
using UConnector.Extensions.Cogs.Transformers;

namespace UConnector.MvcApplication.Cogs.Transformers
{
    public class ProductListToCvsStringListCog : ICog<IEnumerable<Product>, IEnumerable<string>>
    {
        public IEnumerable<string> Execute(IEnumerable<Product> @from)
        {
            var list = new List<string>();

            var transformer = new ProductToCvsStringCog();
            foreach (Product item in @from)
            {
                list.Add(transformer.Execute(item));
            }

            return list;
        }
    }
}