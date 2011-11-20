using System;
using UConnector.Attributes;
using UConnector.Cogs;
using UConnector.Config;

namespace UConnector.Samples.Cogs.Transformers
{
    public class PrintDateTimeToConsoleWithFormatCog : AbstractConfiguable, ICog<DateTime, DateTime>
    {
        [Required]
        public string Format { get; set; }

        public DateTime ExecuteCog(DateTime input)
        {
            Console.WriteLine(input.ToString(Format));
            return input;
        }
    }
}