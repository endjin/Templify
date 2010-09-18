using NHibernate.Validator.Constraints;
using SharpArch.Core.DomainModel;

namespace Northwind.Core
{
    public class Category : Entity
    {
        public Category() { }

        /// <summary>
        /// Creates valid domain object
        /// </summary>
        public Category(string name) {
            CategoryName = name;
        }

        [DomainSignature]
        [NotNullNotEmpty]
        public virtual string CategoryName { get; protected set; }
    }
}
