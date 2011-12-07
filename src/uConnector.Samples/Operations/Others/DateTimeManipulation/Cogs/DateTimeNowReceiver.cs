using System;
using UConnector.Cogs;

namespace UConnector.Samples.Operations.Others.DateTimeManipulation.Cogs
{
    public class DateTimeNowReceiver : IReceiver<DateTime>
    {
        public DateTime Receive()
        {
            return DateTime.Now;
        }
    }
}