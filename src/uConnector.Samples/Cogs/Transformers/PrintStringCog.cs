using System;
using UConnector.Cogs;

namespace UConnector.Samples.Cogs.Transformers
{
    public class PrintStringCog : ICog<string, string>
    {
        #region ICog<string,string> Members

        public string Execute(string input)
        {
            Console.WriteLine(input);
            return input;
        }

        #endregion
    }
}