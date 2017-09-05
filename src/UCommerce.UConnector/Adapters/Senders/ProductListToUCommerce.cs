using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using UCommerce.EntitiesV2;
using uCommerce.uConnector.Helpers;
using UCommerce.Infrastructure;
using UConnector.Framework;

namespace uCommerce.uConnector.Adapters.Senders
{
	public class ProductListToUCommerce : Configurable, ISender<IEnumerable<Product>>
	{
		private IStatelessSession _session;

		public void Send(IEnumerable<Product> input)
		{
		    _session = GetStatelessSessionProvider().GetStatelessSession();

		    foreach (var newProduct in input)
		    {
		        var productDefinition = _session.Query<ProductDefinition>().FirstOrDefault(x => x.Name == newProduct.ProductDefinition.Name);
		        var product = _session.Query<Product>().Fetch(x => x.ProductDefinition).SingleOrDefault(a => a.Sku == newProduct.Sku && a.VariantSku == null);
		        var priceGroups = _session.Query<PriceGroup>().Where(x => !x.Deleted).ToList();

		        if (product == null) // Create product
		        {
		            product = new Product
		            {
		                Sku = newProduct.Sku,
		                Name = newProduct.Name,
		                VariantSku = null,
		                CreatedOn = DateTime.Now,
		                ModifiedOn = DateTime.Now,
		                ProductDefinition = productDefinition
		            };
		        }
		        using (var tx = _session.BeginTransaction())
		        {
		            UpdateProduct(product, newProduct, productDefinition, priceGroups);
		            tx.Commit();
		        }
		    }
        }

	    private void UpdateProduct(Product currentProduct, Product newProduct, ProductDefinition productDefinition, IList<PriceGroup> priceGroups)
	    {
	        // Product
	        UpdateProductValueTypes(currentProduct, newProduct);

	        // Product.Definiton ( Multilingual and Definitions )
	        UpdateProductDescriptions(currentProduct, newProduct);

	        // ProductProperties
	        UpdateProductProperties(currentProduct, newProduct, productDefinition);

	        // Prices
	        UpdateProductPrices(currentProduct, newProduct, priceGroups);

	        // Variants
	        UpdateProductVariants(currentProduct, newProduct, productDefinition, priceGroups);

	        _session.Insert(currentProduct);

	        // Categories
	        UpdateProductCategories(currentProduct, newProduct);
	    }

	    private void UpdateProductProperties(Product currentProduct, Product newProduct, ProductDefinition productDefinition)
	    {
	        if (productDefinition == null)
	            return;

	        if (currentProduct.ProductDefinition.Name != productDefinition.Name)
	        {
	            currentProduct.ProductDefinition = productDefinition;
	        }

	        var newProductProperties = newProduct.ProductProperties;

	        foreach (var newProperty in newProductProperties)
	        {
	            var currentProductProperty = currentProduct.GetProperties().Cast<ProductProperty>().SingleOrDefault(
	                x => !x.ProductDefinitionField.Deleted && (x.ProductDefinitionField.Name == newProperty.ProductDefinitionField.Name));

	            if (currentProductProperty != null) // Update
	            {
	                currentProductProperty.Value = newProperty.Value;
	                _session.Update(currentProductProperty);

	            }
	            else // Insert
	            {
	                var productDefinitionField =
	                    currentProduct.GetDefinition()
	                        .GetDefinitionFields().Cast<ProductDefinitionField>()
	                        .SingleOrDefault(x => x.Name == newProperty.ProductDefinitionField.Name);

	                if (productDefinitionField != null) // Field exist, insert it.
	                {
	                    currentProductProperty = new ProductProperty
	                    {
	                        ProductDefinitionField = productDefinitionField,
	                        Value = newProperty.Value
	                    };
	                    currentProduct.AddProductProperty(currentProductProperty);
	                }

	                _session.Insert(currentProductProperty);
	            }
	        }
	    }

	    private void UpdateProductVariants(Product currentProduct, Product newProduct, ProductDefinition productDefinition, IList<PriceGroup> priceGroups)
	    {
	        var newVariants = newProduct.Variants;
	        foreach (var newVariant in newVariants)
	        {
	            var currentVariant = currentProduct.Variants.SingleOrDefault(x => x.VariantSku == newVariant.VariantSku);
	            if (currentVariant != null) // Update
	            {
	                UpdateProduct(currentVariant, newVariant, productDefinition, priceGroups);
	            }
	            else // Insert
	            {
	                if (string.IsNullOrWhiteSpace(newVariant.VariantSku))
	                    throw new Exception("VariantSku is empty");

	                var product = new Product
	                {
	                    Sku = newVariant.Sku,
	                    Name = newVariant.Name,
	                    VariantSku = newVariant.VariantSku,
	                    ProductDefinition = currentProduct.ProductDefinition
	                };

	                UpdateProduct(product, newVariant, productDefinition, priceGroups);
	                currentProduct.AddVariant(product);
	            }
	        }
	    }

	    private void UpdateProductCategories(Product currentProduct, Product newProduct)
	    {
	        var newCategories = newProduct.CategoryProductRelations;

	        foreach (var relation in newCategories)
	        {
	            var category = GetExistingCategory(relation.Category);
	            if (category == null)
	            {
	                throw new Exception(string.Format("Could not find category: {0}", relation.Category.Name));
	            }

	            if (!_session.Query<CategoryProductRelation>().Any(x => x.Category == category && x.Product.Sku == currentProduct.Sku && x.Product.VariantSku == currentProduct.VariantSku))
	            {
	                var categoryRelation = new CategoryProductRelation();
	                categoryRelation.Product = currentProduct;
	                categoryRelation.SortOrder = 0;
	                categoryRelation.Category = category;

	                _session.Insert(categoryRelation);
	            }
	        }
	    }

	    private Category GetExistingCategory(Category newCategory)
	    {
	        if (newCategory.ProductCatalog != null && newCategory.ProductCatalog.ProductCatalogGroup != null)
	        {
	            return _session.Query<Category>().SingleOrDefault(x => x.Name == newCategory.Name && x.ProductCatalog.Name == newCategory.ProductCatalog.Name && x.ProductCatalog.ProductCatalogGroup.Name == newCategory.ProductCatalog.ProductCatalogGroup.Name);
	        }

	        if (newCategory.ProductCatalog != null)
	        {
	            return _session.Query<Category>().SingleOrDefault(x => x.Name == newCategory.Name && x.ProductCatalog.Name == newCategory.ProductCatalog.Name);
	        }

	        return _session.Query<Category>().SingleOrDefault(x => x.Name == newCategory.Name);
	    }

        private void UpdateProductPrices(Product currentProduct, Product newProduct, IList<PriceGroup> priceGroups)
	    {
	        var newPrices = newProduct.PriceGroupPrices;

	        foreach (var price in newPrices)
	        {
	            var priceGroupPrice = currentProduct.PriceGroupPrices.SingleOrDefault(a => a.PriceGroup.Name == price.PriceGroup.Name);
	            if (priceGroupPrice != null) // Update
	            {
	                priceGroupPrice.Price = price.Price;
	                _session.Update(priceGroupPrice);
	            }
	            else // Insert
	            {
	                var priceGroup = priceGroups.FirstOrDefault(x => x.Name == price.PriceGroup.Name);
	                if (priceGroup != null) // It exist, then insert it
	                {
	                    price.PriceGroup = priceGroup;
	                    currentProduct.AddPriceGroupPrice(price);
	                    _session.Insert(price);
	                }
	            }
	        }
	    }

        private void UpdateProductValueTypes(Product currentProduct, Product newProduct)
		{
			currentProduct.Name = newProduct.Name;
			currentProduct.DisplayOnSite = newProduct.DisplayOnSite;
			currentProduct.ThumbnailImageMediaId = newProduct.ThumbnailImageMediaId;
			currentProduct.PrimaryImageMediaId = newProduct.PrimaryImageMediaId;
			currentProduct.Weight = newProduct.Weight;
			currentProduct.AllowOrdering = newProduct.AllowOrdering;
			currentProduct.Rating = newProduct.Rating;
		}

	    private void UpdateProductDescriptions(Product currentProduct, Product newProduct)
	    {
	        foreach (var productDescription in newProduct.ProductDescriptions)
	        {
	            var currentProductDescription = currentProduct.ProductDescriptions.SingleOrDefault(a => a.CultureCode == productDescription.CultureCode);
	            if (currentProductDescription != null) // Update
	            {
	                currentProductDescription.DisplayName = productDescription.DisplayName;
	                currentProductDescription.ShortDescription = productDescription.ShortDescription;
	                currentProductDescription.LongDescription = productDescription.LongDescription;
	                _session.Update(currentProductDescription);
	            }
	            else // Insert
	            {
	                currentProduct.AddProductDescription(productDescription);
	                _session.Insert(productDescription);
	            }
	        }
	    }

        private IStatelessSessionProvider GetStatelessSessionProvider()
		{
			return new SessionProvider(
				new InMemoryCommerceConfigurationProvider(ConnectionString),
				new SingleUserService("UConnector"),
                ObjectFactory.Instance.ResolveAll<IContainsNHibernateMappingsTag>());
		}

		public string ConnectionString { get; set; }
	}
}