using System;
using Northwind.Core;
using FluentNHibernate;
using FluentNHibernate.Mapping;
using SharpArch.Data.NHibernate.FluentNHibernate;

namespace Northwind.Data.NHibernateMappings
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap() {
            Table("Categories");

            Id(x => x.Id, "CategoryID")
                .UnsavedValue(0)
                .GeneratedBy.Identity();
            
            Map(x => x.CategoryName, "CategoryName");
        }

        #region IMapGenerator Members

        #endregion
    }
}
