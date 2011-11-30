using System.Collections.Generic;
using System.Data;
using UCommerce.EntitiesV2;
using UConnector.Cogs;
using UConnector.Extensions;

namespace UConnector.MvcApplication.Cogs.Transformers
{
    public class DataTableToProductListCog : ICog<DataTable, IEnumerable<Product>>
    {
        public IEnumerable<Product> Execute(DataTable @from)
        {
            var products = new List<Product>();

            foreach (DataRow row in @from.Rows)
            {
                var product = new Product();
                // product.ProductId = row["ProductId"].ToInt();
                product.ParentProductId = row["ParentProductId"].ToNullableInt();
                product.Sku = row["Sku"].ToString();
                product.VariantSku = row["VariantSku"].ToString();
                product.Name = row["Name"].ToString();
                product.DisplayOnSite = row["DisplayOnSite"].ToBool();
                product.ThumbnailImageMediaId = row["ThumbnailImageMediaId"].ToNullableInt();
                product.PrimaryImageMediaId = row["PrimaryImageMediaId"].ToNullableInt();
                product.Weight = row["Weight"].ToDecimal();
                // product.ProductDefinitionId = row["ProductDefinitionId"];
                product.AllowOrdering = row["AllowOrdering"].ToBool();
                product.ModifiedBy = row["ModifiedBy"].ToString();
                product.ModifiedOn = row["ModifiedOn"].ToDateTime();
                product.CreatedOn = row["CreatedOn"].ToDateTime();
                product.CreatedBy = row["CreatedBy"].ToString();
                product.Rating = row["Rating"].ToNullableDouble();

                products.Add(product);
            }

            return products;
        }

        private void AddProductToDataTable(DataTable dataTable, Product item)
        {
            DataRow dataRow = dataTable.NewRow();

            dataRow["ProductId"] = item.ProductId;
            dataRow["ParentProductId"] = item.ParentProductId;
            dataRow["Sku"] = item.Sku;
            dataRow["VariantSku"] = item.VariantSku;
            dataRow["Name"] = item.Name;
            dataRow["DisplayOnSite"] = item.DisplayOnSite;
            dataRow["ThumbnailImageMediaId"] = item.ThumbnailImageMediaId;
            dataRow["PrimaryImageMediaId"] = item.PrimaryImageMediaId;
            dataRow["Weight"] = item.Weight;
            dataRow["ProductDefinitionId"] = item.ProductDefinitionId;
            dataRow["AllowOrdering"] = item.AllowOrdering;
            dataRow["ModifiedBy"] = item.ModifiedBy;
            dataRow["ModifiedOn"] = item.ModifiedOn;
            dataRow["CreatedOn"] = item.CreatedOn;
            dataRow["CreatedBy"] = item.CreatedBy;
            dataRow["Rating"] = item.Rating;

            dataTable.Rows.Add(dataRow);
        }
    }
}