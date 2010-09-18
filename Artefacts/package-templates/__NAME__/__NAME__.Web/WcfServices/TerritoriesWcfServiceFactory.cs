using Northwind.Wcf;
using System.ServiceModel;
using Northwind.WcfServices;
using System.Configuration;

namespace Northwind.Web.WcfServices
{
    public class TerritoriesWcfServiceFactory
    {
        public ITerritoriesWcfService Create() {
            EndpointAddress address = new EndpointAddress(
                // I see the below as a magic string; I typically like to move these to a 
                // web.config reader to consolidate the app setting names
                ConfigurationManager.AppSettings["territoryWcfServiceUri"]);
            WSHttpBinding binding = new WSHttpBinding();

            return new TerritoriesWcfServiceClient(binding, address);
        }
    }
}
