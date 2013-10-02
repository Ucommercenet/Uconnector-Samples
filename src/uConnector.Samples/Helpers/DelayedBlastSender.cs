using System;
using UConnector.Framework;

namespace UConnector.Samples.Helpers
{
	public class DelayedBlastSender : ISender<string>
	{
		public void Send(string input)
		{
			Console.WriteLine(input);
		}
	}
}
