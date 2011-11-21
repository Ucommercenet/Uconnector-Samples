using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using UConnector.Config;
using UConnector.Config.Configuration;
using UConnector.Config.Exceptions;
using UConnector.Config.Extensions;
using UConnector.Validatation;

namespace UConnector.Samples
{
    public class Program
    {
        private static readonly ILog _Log = LogManager.GetCurrentClassLogger();
        private static readonly OperationValidater _OperationValidater = new OperationValidater();
        private static readonly OperationEngine _OperationEngine = new OperationEngine();

        private static void Main(string[] args)
        {
            var reader = new OperationConfigurationReader();

            var connectorConfigurations = reader.GetOperationConfigurations();
            
            int i = 0;
            foreach (var configuration in connectorConfigurations)
            {
                OperationSection section = configuration.GetSection();
                Console.WriteLine("{0}) {1} - {2}", ++i, section.Name, section.Type.Name);
            }

            var input = Console.ReadLine();

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

            var selectedConfiguration = connectorConfigurations.Skip(menuItemNumber - 1).First();

            var operationSection = selectedConfiguration.GetSection();

            var operation = (IOperation)Activator.CreateInstance(operationSection.Type);
            Dictionary<string, List<Option>> settings = operationSection.GetConfiguration();
            
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

                //foreach (var item in exception.List)
                //{
                //    var configElement = connectorSection.Configs.GetOrAdd(item.ConfName);
                //    var optionElement = configElement.Options.GetOrAdd(item.PropertyName);
                //    optionElement.Value = "";
                //}

                selectedConfiguration.SaveSection(operationSection);

                return;
            }
            
            _OperationEngine.ExecuteWorker(operation);
        }
    }
}