using System.Linq;
using SharpArch.Core.DomainModel;
using System.Collections.Generic;

namespace Northwind.Core
{
    /// <summary>
    /// Extends IList&lt;Supplier> with other, customer-specific collection methods.
    /// </summary>
    public static class SuppliersExtensions
    {
        public static List<Supplier> FindSuppliersCarryingMostProducts(this IList<Supplier> suppliers) {
            int maxProductsCount = suppliers.Max(supplier => supplier.Products.Count);
            return GetSuppliersWithProductCountOf(maxProductsCount, suppliers);
        }

        public static List<Supplier> FindSuppliersCarryingFewestProducts(this IList<Supplier> suppliers) {
            int minProductsCount = suppliers.Min(supplier => supplier.Products.Count);
            return GetSuppliersWithProductCountOf(minProductsCount, suppliers);
        }

        private static List<Supplier> GetSuppliersWithProductCountOf(int productsCount, IList<Supplier> suppliers) {
            return (from supplier in suppliers
                    where supplier.Products.Count == productsCount
                    select supplier).ToList();
        }
    }
}
