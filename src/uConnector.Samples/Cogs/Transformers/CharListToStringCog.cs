using System.Collections.Generic;
using System.Text;
using UConnector.Cogs;

namespace UConnector.Samples.Cogs.Transformers
{
    public class CharListToStringCog : ICog<IEnumerable<object>, string>
    {
        #region ICog<IEnumerable<object>,string> Members

        public string Execute(IEnumerable<dynamic> input)
        {
            var sb = new StringBuilder();
            foreach (dynamic o in input)
            {
                sb.Append(o);
            }
            return sb.ToString();
        }

        #endregion
    }
}