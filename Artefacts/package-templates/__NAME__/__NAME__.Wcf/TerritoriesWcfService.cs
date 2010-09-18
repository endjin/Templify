using System.Runtime.Serialization;
using System.ServiceModel;
using Northwind.Core;
using SharpArch.Core.PersistenceSupport;
using Northwind.Wcf.Dtos;
using System.Collections.Generic;
using SharpArch.Core;

namespace Northwind.Wcf
{
    /// <summary>
    /// Concrete implementation of the service.
    /// </summary>
    public class TerritoriesWcfService : ITerritoriesWcfService
    {
        public TerritoriesWcfService(IRepository<Territory> territoryRepository) {
            Check.Require(territoryRepository != null, "territoryRepository may not be null");

            this.territoryRepository = territoryRepository;
        }

        public IList<TerritoryDto> GetTerritories() {
            // I'd rather have the transaction begun via an attribute, like with a controller action, 
            // or within a service object, but this works for the current example.
            territoryRepository.DbContext.BeginTransaction();

            IList<Territory> territories = territoryRepository.GetAll();
            List<TerritoryDto> territoryDtos = new List<TerritoryDto>();

            foreach (Territory territory in territories) {
                territoryDtos.Add(TerritoryDto.Create(territory));
            }

            // Since we're certainly not going to require lazy loading, commit the transcation
            // before returning the data.
            territoryRepository.DbContext.CommitTransaction();

            return territoryDtos;
        }

        private readonly IRepository<Territory> territoryRepository;

        /// <summary>
        /// Doesn't do anything because it only exists to be interchangeable with WCF client 
        /// proxies, such as <see cref="TerritoriesWcfServiceClient" />
        /// </summary>
        public void Abort() { }

        /// <summary>
        /// Doesn't do anything because it only exists to be interchangeable with WCF client 
        /// proxies, such as <see cref="TerritoriesWcfServiceClient" />
        /// </summary>
        public void Close() { }
    }
}
