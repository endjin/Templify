using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Core.DomainModel;
using NHibernate.Validator.Constraints;

namespace Northwind.Core
{
    public class Supplier : Entity
    {
        protected Supplier() {
            InitMembers();
        }

        /// <summary>
        /// Creates valid domain object
        /// </summary>
        public Supplier(string companyName) : this() {
            CompanyName = companyName;
        }

        private void InitMembers() {
            Products = new List<Product>();
        }

        [DomainSignature]
        [NotNullNotEmpty]
        public virtual string CompanyName { get; set; }

        /// <summary>
        /// Note the protected set...only the ORM should set the collection reference directly
        /// after it's been initialized in <see cref="InitMembers" />
        /// </summary>
        public virtual IList<Product> Products { get; protected set; }
    }
}
