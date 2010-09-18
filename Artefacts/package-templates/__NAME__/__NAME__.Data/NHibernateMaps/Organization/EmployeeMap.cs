using FluentNHibernate.Automapping;
using Northwind.Core.Organization;
using Northwind.Core;
using FluentNHibernate.Automapping.Alterations;

namespace Northwind.Data.NHibernateMappings.Organization
{
    public class EmployeeMap : IAutoMappingOverride<Employee>
    {
        public void Override(AutoMapping<Employee> mapping) {
            mapping.Id(x => x.Id, "EmployeeID")
                .UnsavedValue(0)
                .GeneratedBy.Identity();

            // No need to specify the column name when it's the same as the property name
            mapping.Map(x => x.FirstName);
            mapping.Map(x => x.LastName);
            mapping.Map(x => x.PhoneExtension, "Extension");

            mapping.HasManyToMany<Territory>(x => x.Territories)
                .Table("EmployeeTerritories")
                .ParentKeyColumn("EmployeeID")
                .ChildKeyColumn("TerritoryID")
                .AsBag();
        }
    }
}
