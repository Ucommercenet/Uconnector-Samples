using System;
using UConnector.Framework;

namespace UConnector.Samples.Operations.Others.DateTimeManipulation.Cogs
{
	public class RemoveTimePart : ITransformer<DateTime, DateTime>
    {
        public DateTime Execute(DateTime input)
        {
            return input.Date;
        }
    }
}