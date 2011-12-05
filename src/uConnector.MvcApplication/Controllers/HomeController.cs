using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Web.Mvc;
using UCommerce.EntitiesV2;
using UConnector.Config;
using UConnector.Extensions.Cogs.Senders;
using UConnector.Extensions.Cogs.Transformers;
using UConnector.Extensions.Cogs.TwoWayCogs;
using UConnector.MvcApplication.Cogs.Models;
using UConnector.MvcApplication.Cogs.Transformers;
using UConnector.Extensions;

namespace UConnector.MvcApplication.Controllers
{
    public class IndexModel
    {
        //public List<ProductCatalog> ProductCatalogs { get; set; }
        public List<ProductCatalogGroup> ProductCatalogGroups { get; set; }
        //public List<Category> Categories { get; set; }
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            var model = new IndexModel
                            {
                                //ProductCatalogs = ProductCatalog.All().ToList(),
                                ProductCatalogGroups = ProductCatalogGroup.All().ToList(),
                                //Categories = Category.All().ToList()
                            };

            return View("Index", model);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Download(int? id, string typeName)
        {
            var typeInfo = new TypeInfo
                               {
                                   Id = id.GetValueOrDefault(0),
                                   TypeName = typeName ?? ""
                               };

            Stream output = Stream.Null;

            OperationBuilder builder = OperationBuilder.Create()
                .Cog<TypeInfoToProductListCog>()
                .Cog<ProductListToDataTableCog>()
                .Cog<CsvCog>()
                .Send<InvokeMethodSender<Stream>>().WithOption(x => x.Method = (value) => output = value);

            var runner = new OperationEngine();

            runner.Execute(builder.GetOperation(), typeInfo);

            string s = output.GetString(Encoding.UTF8);

            return File(Encoding.Default.GetBytes(s), MediaTypeNames.Application.Octet, string.Format("{0}.csv", DateTime.Now.ToLongTimeString()));
        }
    }
}