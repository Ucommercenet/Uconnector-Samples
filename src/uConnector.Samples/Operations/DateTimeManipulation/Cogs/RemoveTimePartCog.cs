using System;
using UConnector.Cogs;

namespace UConnector.Samples.Operations.DateTimeManipulation.Cogs
{
    public class RemoveTimePartCog :  ICog<DateTime, DateTime>
    {
        public DateTime Execute(DateTime input)
        {
            return input.Date;
        }
    }
}