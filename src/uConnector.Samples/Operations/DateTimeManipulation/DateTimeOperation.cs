using UConnector.Config;
using UConnector.Samples.Operations.DateTimeManipulation.Cogs;

namespace UConnector.Samples.Operations.DateTimeManipulation
{
    /// <summary>
    /// Prints out the DateTime.Now to the console.
    /// </summary>
    public class DateTimeOperation : AbstractOperation
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