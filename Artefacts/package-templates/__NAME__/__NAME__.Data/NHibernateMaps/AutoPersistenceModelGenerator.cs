using System;
using System.Linq;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using Northwind.Core;
using Northwind.Data.NHibernateMappings;
using SharpArch.Core.DomainModel;
using Northwind.Data.NHibernateMappings.Organization;
using Northwind.Data.NHibernateMaps.Conventions;
using FluentNHibernate.Conventions;
using SharpArch.Data.NHibernate.FluentNHibernate;

namespace Northwind.Data.NHibernateMaps
{
    public class AutoPersistenceModelGenerator : IAutoPersistenceModelGenerator
    {

        public AutoPersistenceModel Generate()
        {
            var mappings = new AutoPersistenceModel();
            mappings.AddEntityAssembly(typeof(Category).Assembly).Where(GetAutoMappingFilter);
            mappings.Conventions.Setup(GetConventions());
            mappings.Setup(GetSetup());
            mappings.IgnoreBase<Entity>();
            mappings.IgnoreBase(typeof(EntityWithTypedId<>));
            mappings.UseOverridesFromAssemblyOf<AutoPersistenceModelGenerator>();
            return mappings;
        }

        private Action<AutoMappingExpressions> GetSetup()
        {
            return c =>
            {
                c.FindIdentity = type => type.Name == "Id";
            };
        }

        private Action<IConventionFinder> GetConventions()
        {
            return c =>
            {
                c.Add<PrimaryKeyConvention>();
                c.Add<HasManyConvention>();
                c.Add<TableNameConvention>();
            };
        }

        /// <summary>
        /// Provides a filter for only including types which inherit from the IEntityWithTypedId interface.
        /// </summary>
        private bool GetAutoMappingFilter(Type t)
        {
            return t.GetInterfaces().Any(x =>
                 x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEntityWithTypedId<>));
        }
    }
}
