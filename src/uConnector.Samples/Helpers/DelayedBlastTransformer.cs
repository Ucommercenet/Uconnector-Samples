using UConnector.Framework;

namespace UConnector.Samples.Helpers
{
	public class DelayedBlastTransformer : ITransformer<string,string>
	{
		public string Execute(string input)
		{
			return input;
		}
	}
}
