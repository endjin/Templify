using Castle.Windsor;
using SharpArch.Core.PersistenceSupport.NHibernate;
using SharpArch.Data.NHibernate;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Web.Castle;
using Castle.MicroKernel.Registration;

namespace Northwind.Wcf.Web.CastleWindsor
{
    public class ComponentRegistrar
    {
        public static void AddComponentsTo(IWindsorContainer container) {
            AddGenericRepositoriesTo(container);
            AddCustomRepositoriesTo(container);
            AddWcfServicesTo(container);
        }

        private static void AddWcfServicesTo(IWindsorContainer container) {
            // Since the TerritoriesService.svc must be associated with a concrete class,
            // we must register the concrete implementation here as the service
            container.AddComponent("territoriesWcfService", typeof(TerritoriesWcfService));
        }

        private static void AddCustomRepositoriesTo(IWindsorContainer container) {
            container.Register(
                AllTypes.Pick()
                .FromAssemblyNamed("Northwind.Data")
                .WithService.FirstNonGenericCoreInterface("Northwind.Core"));
        }

        private static void AddGenericRepositoriesTo(IWindsorContainer container) {
            container.AddComponent("repositoryType",
                typeof(IRepository<>), typeof(Repository<>));
            container.AddComponent("nhibernateRepositoryType",
                typeof(INHibernateRepository<>), typeof(NHibernateRepository<>));
            container.AddComponent("repositoryWithTypedId",
                typeof(IRepositoryWithTypedId<,>), typeof(RepositoryWithTypedId<,>));
            container.AddComponent("nhibernateRepositoryWithTypedId",
                typeof(INHibernateRepositoryWithTypedId<,>), typeof(NHibernateRepositoryWithTypedId<,>));
        }
    }
}
