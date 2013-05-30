using System.Collections.Generic;
using System.IO;
using UConnector.Cogs;
using UConnector.Extensions.Model;

namespace UConnector.Samples.Operations.UCommerce.ImportLocalFile.Cogs
{
	public class WorkFileToStream : ITransformer<WorkFile, Stream>, IMetadata
    {
        public Stream Execute(WorkFile input)
        {
            Meta.Add("Name", input.Name);
            return input.Stream;
        }

        public IDictionary<string, object> Meta { get; set; }
    }
}