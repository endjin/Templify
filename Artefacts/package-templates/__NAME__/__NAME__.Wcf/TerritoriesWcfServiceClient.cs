using Northwind.Wcf;
using Northwind.Wcf.Dtos;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel;

namespace Northwind.WcfServices
{
    /// <summary>
    /// Provides a strongly typed client proxy to use the WCF service without having to configure 
    /// it via WCF configuration.
    /// </summary>
    public partial class TerritoriesWcfServiceClient : ClientBase<ITerritoriesWcfService>, ITerritoriesWcfService
    {
        public TerritoriesWcfServiceClient() { }
        public TerritoriesWcfServiceClient(string endpointName) 
            : base(endpointName) { }
        public TerritoriesWcfServiceClient(Binding binding, EndpointAddress address) 
            : base(binding, address) { }

        public IList<TerritoryDto> GetTerritories() {
            return Channel.GetTerritories();
        }
    }
}
