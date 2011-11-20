using System;
using UConnector.Cogs;

namespace UConnector.Samples.Cogs.Transformers
{
    public class RemoveTimePartCog :  ICog<DateTime, DateTime>
    {
        public DateTime ExecuteCog(DateTime input)
        {
            return input.Date;
        }
    }
}