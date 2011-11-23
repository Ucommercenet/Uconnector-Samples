using System;
using UConnector.Cogs;
using UConnector.Config;

namespace UConnector.Samples.Operations.DateTimeManipulation.Cogs
{
    public class AddHoursToDateTimeCog : AbstractConfiguable, ICog<DateTime,DateTime>
    {
        public int Hours { get; set; }

        public DateTime Execute(DateTime input)
        {
            return input.AddHours(Hours);
        }
    }
}