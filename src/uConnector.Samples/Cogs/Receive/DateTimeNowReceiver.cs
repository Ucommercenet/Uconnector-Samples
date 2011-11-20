using System;
using UConnector.Cogs;

namespace UConnector.Samples.Cogs.Receive
{
    public class DateTimeNowReceiver : IReceive<DateTime>
    {
        public DateTime Receive()
        {
            return DateTime.Now;
        }
    }
}