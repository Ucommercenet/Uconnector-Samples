using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using UCommerce.EntitiesV2;
using UConnector.Framework;
using UConnector.Samples.UCommerce.Helpers;

namespace UConnector.Samples.UCommerce
{
    public class ProductListFromUCommerce : Configurable, IReceiver<IEnumerable<Product>>
    {
	    public string ConnectionString { get; set; }

        public IEnumerable<Product> Receive()
        {
	        var session = GetSessionProvider().GetSession();
			return session.Query<Product>().Where(x => x.ParentProduct == null);
        }

	    private ISessionProvider GetSessionProvider()
	    {
		    return new SessionProvider(
			    new InMemoryCommerceConfigurationProvider(ConnectionString),
			    new SingleUserService("UConnector"));
	    }
    }
}