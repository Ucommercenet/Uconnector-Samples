using System;
using UConnector.Cogs;

namespace UConnector.Samples.Cogs.Receive
{
    public class DateTimeNowReceiver : IReceiver<DateTime>
    {
        public DateTime Receive()
        {
            return DateTime.Now;
        }
    }
}