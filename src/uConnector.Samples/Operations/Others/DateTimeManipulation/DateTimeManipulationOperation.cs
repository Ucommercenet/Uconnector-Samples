using UConnector.Config.Fluent.v1;
using UConnector.Samples.Operations.Others.DateTimeManipulation.Cogs;

namespace UConnector.Samples.Operations.Others.DateTimeManipulation
{
    /// <summary>
    /// Prints out the DateTime.Now to the console.
    /// </summary>
    public class DateTimeManipulationOperation : Operation
    {
        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        protected override IOperation BuildOperation()
        {
            return FluentOperationBuilder
				.Receive<DateTimeNow>()
                .Transform<PrintDateTimeToConsoleWithFormat>()
				.Transform<RemoveTimePart>()
				.Transform<PrintDateTimeToConsoleWithFormat>()
				.Transform<AddHoursToDateTime>()
				.Transform<PrintDateTimeToConsoleWithFormat>()
                .Send<DateTimeSender>().ToOperation();
        }
    }
}