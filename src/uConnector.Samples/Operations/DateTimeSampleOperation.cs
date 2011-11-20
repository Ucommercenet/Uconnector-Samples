using UConnector.Config;
using UConnector.Samples.Cogs.Receive;
using UConnector.Samples.Cogs.Send;
using UConnector.Samples.Cogs.Transformers;

namespace UConnector.Worker.Operations
{
    /// <summary>
    /// Prints out the DateTime.Now to the console.
    /// </summary>
    public class DateTimeSampleOperation : AbstractOperation
    {
        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        protected override OperationBuilder Build()
        {
            return OperationBuilder.Create()
                .Receive<DateTimeNowReceiver>()
                .Cog<PrintDateTimeToConsoleWithFormatCog>()
                .Cog<RemoveTimePartCog>()
                .Cog<PrintDateTimeToConsoleWithFormatCog>()
                .Cog<AddHoursToDateTimeCog>()
                .Cog<PrintDateTimeToConsoleWithFormatCog>()
                .Send<DateTimeSender>();
        }
    }
}