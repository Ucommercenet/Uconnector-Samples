using UConnector.Config;
using UConnector.Samples.Operations.Others.DateTimeManipulation.Cogs;

namespace UConnector.Samples.Operations.Others.DateTimeManipulation
{
    /// <summary>
    /// Prints out the DateTime.Now to the console.
    /// </summary>
    public class DateTimeManipulationOperation : CustomOperation
    {
        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        protected override Operation BuildOperation()
        {
            return OperationBuilder.Create()
                .Receive<DateTimeNowReceiver>()
                .Cog<PrintDateTimeToConsoleWithFormatCog>()
                .Cog<RemoveTimePartCog>()
                .Cog<PrintDateTimeToConsoleWithFormatCog>()
                .Cog<AddHoursToDateTimeCog>()
                .Cog<PrintDateTimeToConsoleWithFormatCog>()
                .Send<DateTimeSender>().GetOperation();
        }
    }
}