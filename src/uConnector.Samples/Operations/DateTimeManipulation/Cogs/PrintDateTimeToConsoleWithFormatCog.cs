using System;
using Common.Logging;
using UConnector.Attributes;
using UConnector.Cogs;
using UConnector.Config;

namespace UConnector.Samples.Operations.DateTimeManipulation.Cogs
{
    public class PrintDateTimeToConsoleWithFormatCog : AbstractConfiguable, ICog<DateTime, DateTime>
    {
        private readonly ILog _Log = LogManager.GetCurrentClassLogger();

        [Required]
        public string Format { get; set; }

        public DateTime Execute(DateTime input)
        {
            _Log.Debug(input.ToString(Format));
            return input;
        }
    }
}