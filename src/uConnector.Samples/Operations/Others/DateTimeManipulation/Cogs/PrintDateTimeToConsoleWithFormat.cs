using System;
using Common.Logging;
using UConnector.Attributes;
using UConnector.Config;
using UConnector.Framework;

namespace UConnector.Samples.Operations.Others.DateTimeManipulation.Cogs
{
	public class PrintDateTimeToConsoleWithFormat : Configurable, ITransformer<DateTime, DateTime>
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