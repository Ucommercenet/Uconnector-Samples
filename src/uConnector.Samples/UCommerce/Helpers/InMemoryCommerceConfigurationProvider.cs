﻿using UCommerce.Infrastructure.Configuration;

namespace UConnector.Samples.UCommerce.Helpers
{
	/// <summary>
	/// In-memory config provider used to avoid
	/// having the full XML config as part of an
	/// app.
	/// </summary>
	internal class InMemoryCommerceConfigurationProvider : CommerceConfigurationProvider
	{
		private readonly string _conncetionString;

		public InMemoryCommerceConfigurationProvider(string conncetionString)
		{
			_conncetionString = conncetionString;
		}

		public override RuntimeConfigurationSection GetRuntimeConfiguration()
		{
			return new RuntimeConfigurationSection
			{
				EnableCache = true,
				CacheProvider = "NHibernate.Caches.SysCache2.SysCacheProvider, NHibernate.Caches.SysCache2",
				ConnectionString = _conncetionString
			};
		}
	}
}