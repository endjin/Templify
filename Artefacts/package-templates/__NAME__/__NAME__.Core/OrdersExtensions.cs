using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Core.DomainModel;

namespace Northwind.Core
{
    /// <summary>
    /// Extends IList&lt;Order> with other, customer-specific collection methods.
    /// </summary>
    public static class OrdersExtensions
    {
        public static List<Order> FindOrdersPlacedOn(this IList<Order> orders, DateTime whenPlaced) {
            return (
                from order in orders
                where order.OrderDate == whenPlaced
                orderby order.OrderDate
                select order
                ).ToList<Order>();
        }
    }
}
