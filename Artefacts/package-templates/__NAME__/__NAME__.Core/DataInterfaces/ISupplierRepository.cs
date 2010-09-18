using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Core.DomainModel;

namespace Northwind.Core.DataInterfaces
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        List<Supplier> GetSuppliersBy(string productCategoryName);
    }
}
