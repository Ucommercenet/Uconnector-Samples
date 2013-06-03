using System;
using UConnector.Framework;

namespace UConnector.Samples.Operations.Others.FetchFile.Cogs
{
    public class ToStandardOut : ISender<string>
    {
        #region ICog<string,string> Members

        public void Send(string input)
        {
            Console.WriteLine(input);
        }

        #endregion
    }
}