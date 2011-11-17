using System;
using UConnector.Cogs;

namespace UConnector.Samples.Cogs.Transformers
{
    public class PrintListOfListCog : ICog<object, object>
    {
        #region ICog<object,object> Members

        public dynamic ExecuteCog(dynamic input)
        {
            foreach (dynamic parent in input)
            {
                Console.WriteLine("Parent: " + parent);
                foreach (dynamic child in parent)
                {
                    Console.WriteLine("\t{0}", child);
                }
            }

            return input;
        }

        #endregion
    }
}