using SharpArch.Core.DomainModel;
using NHibernate.Validator.Constraints;

namespace Northwind.Core
{
    public class Product : Entity
    {
        public Product() { }

        /// <summary>
        /// Creates valid domain object
        /// </summary>
        public Product(string name, Supplier supplier) {
            Supplier = supplier;
            ProductName = name;
        }

        [DomainSignature]
        [NotNullNotEmpty]
        public virtual string ProductName { get; set; }

        [DomainSignature]
        [NotNull]
        public virtual Supplier Supplier { get; protected set; }

        public virtual Category Category { get; set; }
    }
}
