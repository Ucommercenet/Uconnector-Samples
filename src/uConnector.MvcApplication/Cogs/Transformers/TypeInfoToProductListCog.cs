﻿using System;
using System.Collections.Generic;
using System.Linq;
using UCommerce.EntitiesV2;
using UConnector.Cogs;
using UConnector.MvcApplication.Cogs.Models;

namespace UConnector.MvcApplication.Cogs.Transformers
{
    public class TypeInfoToProductListCog : ICog<TypeInfo, IEnumerable<Product>>
    {
        #region ICog<TypeInfo,IEnumerable<Product>> Members

        public IEnumerable<Product> ExecuteCog(TypeInfo input)
        {
            switch (input.TypeName)
            {
                case "Category":
                    Category category = Category.SingleOrDefault(a => a.CategoryId == input.Id);
                    return Product.GetForCategory(category).ToList();

                case "ProductCatalog":
                    ProductCatalog productCatalog = ProductCatalog.SingleOrDefault(a => a.ProductCatalogId == input.Id);
                    return productCatalog.Categories.SelectMany(a => a.Products).ToList();

                case "ProductCatalogGroup":
                    ProductCatalogGroup productCatalogGroup =
                        ProductCatalogGroup.SingleOrDefault(a => a.ProductCatalogGroupId == input.Id);
                    return
                        productCatalogGroup.ProductCatalogs.SelectMany(a => a.Categories).SelectMany(a => a.Products).
                            ToList();

                default:
                    throw new NotImplementedException(
                        "Only Category, ProductCatalog and ProductCatalogGroup are allowed.");
            }
        }

        #endregion
    }
}