using UConnector.MvcApplication.Controllers;

namespace UConnector.MvcApplication.Cogs.Models
{
    public class TypeInfo
    {
        public string TypeName { get; set; }
        public int Id { get; set; }
        public DownloadAs Type { get; set; }
    }
}