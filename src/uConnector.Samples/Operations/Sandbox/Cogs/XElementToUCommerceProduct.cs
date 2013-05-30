using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UCommerce.EntitiesV2;
using UConnector.Cogs;

namespace UConnector.Samples.Operations.Sandbox.Cogs
{
	public class XElementToUCommerceProduct : ITransformer<XElement, Product>
	{
		private XElement _element;
		private Product _product;

		public Product Execute(XElement input)
		{
			_element = input;
			_product = new Product();
			_product.Name = FindProductName();
			_product.Sku = FindSku();
			_product.ProductDefinition = new ProductDefinition() { Name = "Imported"};
			_product.DisplayOnSite = !_product.Name.Contains("DISCONTINUED");

			AddProductDescriptions();

			_product.CategoryProductRelations.Add(BuildCategoryProductRelation());

			return _product;
		}

		private CategoryProductRelation BuildCategoryProductRelation()
		{
			var categoryProductRelation = new CategoryProductRelation()
			{
				Product = _product,
				Category = BuildUConnectorCategory(),
				SortOrder = 0,
			};

			return categoryProductRelation;
		}

		private Category BuildUConnectorCategory()
		{
			return new Category()
			{
				Name = "uConnector",
				ProductCatalog = new ProductCatalog()
				{
					Name = "Demo Store",
					ProductCatalogGroup = new ProductCatalogGroup()
					{
						Name = "avenue-clothing.com"
					}
				}
			};

		}

		private void AddProductDescriptions()
		{
			var descriptionElements = FindDescendents(_element, "ItemIdInventTxt");
			foreach (var descriptionElement in descriptionElements)
			{
				_product.AddProductDescription(BuildProductDescription(descriptionElement));
			}
		}

		private ProductDescription BuildProductDescription(XElement description)
		{
			var productDescription = new ProductDescription
				{
					CultureCode = FindElementValue(description, "LanguageId"),
					DisplayName = "TODO",
					ShortDescription = FindElementValue(description, "Txt")
				};

			return productDescription;
		}

		private string FindProductName()
		{
			return FindElementValue(_element, "ItemName");
		}

		private string FindSku()
		{
			return FindElementValue(_element, "ItemId");
		}

		private string FindProductDefinitionName()
		{
			return FindElementValue(_element, "ItemGroupId");
		}

		private string FindElementValue(XElement element, string name)
		{
			var val = (from e in element.Elements()
						where e.Name.LocalName == name
						select e.Value).FirstOrDefault();

			return val;
		}

		private IEnumerable<XElement> FindDescendents(XElement element, string name)
		{
			return (from e in element.Elements() where e.Name.LocalName == name select e);
		}
	}
}
