using System;
using UConnector.Cogs;
using UConnector.Config;

namespace UConnector.Samples.Cogs.Transformers
{
    public class AddHoursToDateTimeCog : AbstractConfiguable, ICog<DateTime,DateTime>
    {
        public int Hours { get; set; }

        public DateTime ExecuteCog(DateTime input)
        {
            return input.AddHours(Hours);
        }
    }
}