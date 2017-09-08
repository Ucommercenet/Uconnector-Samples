﻿using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using uCommerce.uConnector.Helpers;
using UCommerce.EntitiesV2;
using UCommerce.Infrastructure;
using UConnector.Framework;

namespace uCommerce.uConnector.Adapters.Receivers
{
    public class ProductListFromUCommerce : Configurable, IReceiver<IEnumerable<Product>>
    {
	    public string ConnectionString { get; set; }

        public IEnumerable<Product> Receive()
        {
            var sessionProvider = GetSessionProvider();
            var session = sessionProvider.GetSession();
			return session.Query<Product>().Fetch(x => x.ProductDefinition).Where(x => x.ParentProduct == null);
        }

	    private ISessionProvider GetSessionProvider()
	    {
            return new SessionProvider(
                new InMemoryCommerceConfigurationProvider(ConnectionString),
                new SingleUserService("UConnector"),
                ObjectFactory.Instance.ResolveAll<IContainsNHibernateMappingsTag>());
        }
    }
}