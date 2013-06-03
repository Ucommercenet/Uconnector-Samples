using System;
using UConnector.Config;
using UConnector.Framework;

namespace UConnector.Samples.Operations.Others.DateTimeManipulation.Transformers
{
	public class AddHoursToDateTime : Configurable, ITransformer<DateTime, DateTime>
    {
        public int Hours { get; set; }

        public DateTime Execute(DateTime input)
        {
            return input.AddHours(Hours);
        }
    }
}