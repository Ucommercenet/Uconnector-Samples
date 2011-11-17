using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Web.Mvc;
using UCommerce.EntitiesV2;
using UConnector.Config;
using UConnector.Extensions.Cogs.Adapters;
using UConnector.Extensions.Cogs.Transformers;
using UConnector.MvcApplication.Cogs.Models;
using UConnector.MvcApplication.Cogs.Transformers;
using ProductListToCvsStringListCog = UConnector.MvcApplication.Cogs.Transformers.ProductListToCvsStringListCog;

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

            OperationBuilder builder = OperationBuilder.Create()
                .Cog<TypeInfoToProductListCog>()
                .Debatching()
                .Cog<ProductListToCvsStringListCog>()
                .Batching()
                .Cog<StringListToStringCog>().WithOption(a => a.Seperator, ':')
                .Send(new FakeHttpContextAdapter());


            var runner = new OperationEngine();

            WorkItem workItem = runner.ExecuteSimpleWorker(builder.GetOperation(), typeInfo);

            return File(Encoding.Default.GetBytes(workItem.Context), MediaTypeNames.Application.Octet, "myfilename.csv");
        }
    }
}