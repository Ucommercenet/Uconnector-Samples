using System;
using System.Linq;
using Common.Logging;
using UConnector.Config;
using UConnector.Config.Configuration;
using UConnector.Config.Exceptions;
using UConnector.Config.Extensions;
using UConnector.Extensions;
using UConnector.Validatation;

namespace UConnector.Samples
{
    public class Program
    {
        private static readonly ILog _Log = LogManager.GetCurrentClassLogger();
        private static readonly OperationValidater _OperationValidater = OperationValidater.GetDefault();
        private static readonly OperationEngine _OperationEngine = new OperationEngine();

        private static void Main(string[] args)
        {
            SettingsHelper.Instance.Init();
            var reader = new OperationConfigurationReader();

            var connectorConfigurations = reader.GetOperationConfigurations();
            int i = 0;
            foreach (var item in connectorConfigurations)
            {
                Console.WriteLine("{0}) {1} - {2}", ++i, item.Section.Name, item.Section.Type.Name);
            }

            var input = Console.ReadLine();

            var service = new OperationSectionService();

            int menuItemNumber;
            if(!int.TryParse(input, out menuItemNumber))
            {
                Console.WriteLine("This is not a valid number.");
                return;
            }

            if (!menuItemNumber.IsBetween(1, connectorConfigurations.Count()))
            {
                Console.WriteLine("Invalid item selected.");
                return;
            }

            var selectedItem = connectorConfigurations.Skip(menuItemNumber - 1).First();

            var operationSection = selectedItem.Section;

            var operation = (IOperation)Activator.CreateInstance(operationSection.Type);
            var settings = operationSection.GetConfiguration();
            
            operation.SetConfiguration(settings);

            try
            {
                operation.ValidateConfiguration();
                _OperationValidater.Validate(operation);
            }
            catch (ConnectorConfigurationException exception)
            {
                foreach (var item in operation.GetConfiguration())
                {
                    var configElement = operationSection.Configs.AddOrGet(item.Name);
                    foreach (var option in item.GetOptions().Where(a => a.Required == Required.Yes))
                    {
                        var optionElement = configElement.Options.AddOrGet(option.Name, false);
                        if (string.IsNullOrWhiteSpace(optionElement.Value))
                            optionElement.Value = "";
                    }
                }

                service.SaveSection(selectedItem.Path, selectedItem.Section);
                _Log.ErrorFormat("Validation of {0} with name {1} failed.", exception, operationSection.Type.FullName, operationSection.Name);

                return;
            }
            
            _OperationEngine.Execute(operation);
        }
    }
}