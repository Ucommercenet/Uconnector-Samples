using System;
using UConnector.Framework;

namespace UConnector.Samples.Operations.Others.DateTimeManipulation.Transformers
{
	public class RemoveTimePart : ITransformer<DateTime, DateTime>
    {
        public DateTime Execute(DateTime input)
        {
            return input.Date;
        }
    }
}