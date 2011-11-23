using System;
using UConnector.Cogs;

namespace UConnector.Samples.Cogs.Transformers
{
    public class PrintCharCog : ICog<char, char>
    {
        #region ICog<char,char> Members

        public char Execute(char input)
        {
            Console.WriteLine("Char: " + input);
            return input;
        }

        #endregion
    }
}