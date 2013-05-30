using System;
using UConnector.Cogs;

namespace UConnector.Samples.Operations.Others.DateTimeManipulation.Cogs
{
    public class RemoveTimePart :  ICog<DateTime, DateTime>
    {
        public DateTime Execute(DateTime input)
        {
            return input.Date;
        }
    }
}