using System;
using System.Collections.Generic;
using UConnector.Cogs;

namespace UConnector.Samples.Cogs.Transformers
{
    public class PrintStringListCog : ICog<List<string>, List<string>>
    {
        #region ICog<List<string>,List<string>> Members

        public List<string> ExecuteCog(List<string> input)
        {
            foreach (string item in input)
            {
                Console.WriteLine("\t " + item);
            }

            return input;
        }

        #endregion
    }
}