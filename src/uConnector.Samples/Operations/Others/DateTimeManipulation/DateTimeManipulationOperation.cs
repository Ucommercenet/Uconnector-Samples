using UConnector.Config.Fluent.V1;
using UConnector.Samples.Operations.Others.DateTimeManipulation.Receivers;
using UConnector.Samples.Operations.Others.DateTimeManipulation.Senders;
using UConnector.Samples.Operations.Others.DateTimeManipulation.Transformers;

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