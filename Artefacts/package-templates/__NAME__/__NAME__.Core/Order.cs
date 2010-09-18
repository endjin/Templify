using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Core.DomainModel;
using SharpArch.Core;

namespace Northwind.Core
{
    public class Order : Entity
    {
        /// <summary>
        /// This is a placeholder constructor for NHibernate.
        /// A no-argument constructor must be avilable for NHibernate to create the object.
        /// </summary>
        protected Order() { }

        public Order(Customer orderedBy) {
            Check.Require(orderedBy != null, "orderedBy may not be null");

            OrderedBy = orderedBy;
        }

        public virtual DateTime? OrderDate { get; set; }
        public virtual string ShipToName { get; set; }
        public virtual Customer OrderedBy { get; protected set; }

        /// <summary>
        /// Should ONLY contain the "business value signature" of the object and not the Id, 
        /// which is handled by <see cref="Entity" />.  This method should return a unique 
        /// int representing a unique signature of the domain object.  For 
        /// example, no two different orders should have the same ShipToName, OrderDate and OrderedBy;
        /// therefore, the returned "signature" should be expressed as demonstrated below.
        /// 
        /// Alternatively, we could decorate properties with the [DomainSignature] attribute, as shown in
        /// <see cref="Customer" />, but here's an example of overriding it nonetheless.
        /// </summary>
        public override bool HasSameObjectSignatureAs(BaseObject compareTo) {
            Order orderCompareTo = compareTo as Order;

            return orderCompareTo != null && ShipToName.Equals(orderCompareTo.ShipToName) &&
                (OrderDate ?? DateTime.MinValue).Equals((orderCompareTo.OrderDate ?? DateTime.MinValue)) &&
                OrderedBy.Equals(orderCompareTo.OrderedBy);
        }
    }
}
