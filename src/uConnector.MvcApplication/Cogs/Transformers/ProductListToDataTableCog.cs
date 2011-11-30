using System.Collections.Generic;
using System.Data;
using UCommerce.EntitiesV2;
using UConnector.Cogs;

namespace UConnector.MvcApplication.Cogs.Transformers
{
    public class ProductListToDataTableCog : ICog<IEnumerable<Product>, DataTable>
    {
        public DataTable Execute(IEnumerable<Product> @from)
        {
            var dataTable = new DataTable("Products");

            dataTable.Columns.Add("ProductId");
            dataTable.Columns.Add("ParentProductId"); 
            dataTable.Columns.Add("Sku");
            dataTable.Columns.Add("VariantSku"); 
            dataTable.Columns.Add("Name"); 
            dataTable.Columns.Add("DisplayOnSite"); 
            dataTable.Columns.Add("ThumbnailImageMediaId"); 
            dataTable.Columns.Add("PrimaryImageMediaId"); 
            dataTable.Columns.Add("Weight"); 
            dataTable.Columns.Add("ProductDefinitionId"); 
            dataTable.Columns.Add("AllowOrdering"); 
            dataTable.Columns.Add("ModifiedBy"); 
            dataTable.Columns.Add("ModifiedOn"); 
            dataTable.Columns.Add("CreatedOn"); 
            dataTable.Columns.Add("CreatedBy"); 
            dataTable.Columns.Add("Rating");

            foreach (var product in @from)
            {
                AddProductToDataTable(dataTable, product);
                foreach (var variant in product.Variants)
                {
                    AddProductToDataTable(dataTable, variant);
                }
            }

            return dataTable;
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