using System;
using System.Threading;
using UConnector.Framework;

namespace UConnector.Samples.Helpers
{
	public class DelayedBlastReceiver : IReceiver<string>
	{
		public string Receive()
		{
			new Thread(DelayedBlastFireball).Start();
			return "Incomming!";
		}

		private void DelayedBlastFireball()
		{
			// Count to ten!
			Thread.Sleep(10000);
			throw new Exception("BANG!");
		}
	}
}
