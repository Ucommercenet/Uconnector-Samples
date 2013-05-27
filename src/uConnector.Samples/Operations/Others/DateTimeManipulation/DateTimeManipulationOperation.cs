using UConnector.Config.Fluent.v1;
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
        protected override IOperation BuildOperation()
        {
            return FluentOperationBuilder
				.Receive<DateTimeNowReceiver>()
                .Transform<PrintDateTimeToConsoleWithFormatCog>()
				.Transform<RemoveTimePartCog>()
				.Transform<PrintDateTimeToConsoleWithFormatCog>()
				.Transform<AddHoursToDateTimeCog>()
				.Transform<PrintDateTimeToConsoleWithFormatCog>()
                .Send<DateTimeSender>().ToOperation();
        }
    }
}