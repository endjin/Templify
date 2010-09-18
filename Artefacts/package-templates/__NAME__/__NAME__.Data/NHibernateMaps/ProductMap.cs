using FluentNHibernate.Automapping;
using Northwind.Core;
using FluentNHibernate.Automapping.Alterations;

namespace Northwind.Data.NHibernateMappings
{
    public class ProductMap : IAutoMappingOverride<Product>
    {
        public void Override(AutoMapping<Product> mapping) {
            mapping.Id(x => x.Id, "ProductID")
                .UnsavedValue(0)
                .GeneratedBy.Identity();

            mapping.References(x => x.Supplier, "SupplierID");
            mapping.References(x => x.Category, "CategoryID");
        }
    }
}
