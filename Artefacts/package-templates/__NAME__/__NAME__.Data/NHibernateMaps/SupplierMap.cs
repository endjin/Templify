using FluentNHibernate.Automapping;
using Northwind.Core;
using FluentNHibernate.Automapping.Alterations;

namespace Northwind.Data.NHibernateMappings
{
    public class SupplierMap : IAutoMappingOverride<Supplier>
    {
        public void Override(AutoMapping<Supplier> mapping) {
            mapping.Not.LazyLoad();

            mapping.Id(x => x.Id, "SupplierID")
                .UnsavedValue(0)
                .GeneratedBy.Identity();

            mapping.Map(x => x.CompanyName);

            mapping.HasMany(x => x.Products)
                .Inverse()
                .KeyColumn("SupplierID")
                .AsBag();
        }
    }
}
