using System;
using System.Configuration;
using System.IO;
using System.Linq;
using UConnector.Config;
using UConnector.Config.Configuration;

namespace UConnector.MissingConfigurationFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            var directory = ConfigurationManager.AppSettings["Directory"];
            if(string.IsNullOrWhiteSpace(directory))
                throw new Exception("Directory key must be present in the AppSettings");

            var directoryInfo = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, directory));
            if(!directoryInfo.Exists)
                throw new DirectoryNotFoundException(string.Format("Directory: '{0}' does not exist.",
                                                                   directoryInfo.FullName));

            var scanner = new OperationScanner(Environment.CurrentDirectory);
            var operationTypes = scanner.GetOperationTypes();

            var reader = new OperationConfigurationReader();
            var sectionItems = reader.GetOperationConfigurations().ToList();

            var service = new OperationSectionService();


            foreach (var type in operationTypes)
            {
                if(sectionItems.Any(x => x.Section.Type == type))
                {
                    var items = sectionItems.Where(x => x.Section.Type == type);
                    var operation = ObjectFactory.CreateOperation(type);
                    foreach (var sectionItem in items)
                    {
                        sectionItem.Section.AddMissingConfigurationOptions(operation);
                        service.SaveSection(sectionItem.Path, sectionItem.Section);
                    }
                }
                else
                {
                    var operationSection = new OperationSection();
                    operationSection.Type = type;
                    operationSection.Name = type.Name;
                    operationSection.Enabled = false;

                    var operation = ObjectFactory.CreateOperation(operationSection.Type);
                    operationSection.AddMissingConfigurationOptions(operation);

                    var path = Path.Combine(directoryInfo.FullName, operationSection.Type.Name + ".config");
                    service.SaveSection(new FileInfo(path), operationSection);
                }
            }
        }
    }
}
