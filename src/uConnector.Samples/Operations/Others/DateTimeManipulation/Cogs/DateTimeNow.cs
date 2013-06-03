using System;
using UConnector.Framework;

namespace UConnector.Samples.Operations.Others.DateTimeManipulation.Cogs
{
    public class DateTimeNow : IReceiver<DateTime>
    {
        public DateTime Receive()
        {
            return DateTime.Now;
        }
    }
}