using System.Collections.Generic;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Core.DomainModel;
using NHibernate.Validator.Constraints;
using SharpArch.Core;

namespace Northwind.Core
{
    /// <summary>
    /// I'd like to be perfectly clear that I think assigned IDs are almost always a terrible
    /// idea; this is a major complaint I have with the Northwind database.  With that said, 
    /// some legacy databases require such techniques.
    /// </summary>
    public class Customer : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        public Customer() {
            InitMembers();
        }

        /// <summary>
        /// Creates valid domain object
        /// </summary>
        public Customer(string companyName) : this() {
            CompanyName = companyName;
        }

        /// <summary>
        /// Since we want to leverage automatic properties, init appropriate members here.
        /// </summary>
        private void InitMembers() {
            Orders = new List<Order>();
        }

        [DomainSignature]
        [NotNullNotEmpty]
        public virtual string CompanyName { get; set; }

        [DomainSignature]
        public virtual string ContactName { get; set; }

        public virtual string Country { get; set; }
        public virtual string Fax { get; set; }

        /// <summary>
        /// Note the protected set...only the ORM should set the collection reference directly
        /// after it's been initialized in <see cref="InitMembers" />
        /// </summary>
        public virtual IList<Order> Orders { get; protected set; }

        public virtual void SetAssignedIdTo(string assignedId) {
            Check.Require(!string.IsNullOrEmpty(assignedId), "assignedId may not be null or empty");
            Check.Require(assignedId.Trim().Length == 5, "assignedId must be exactly 5 characters");

            Id = assignedId.Trim().ToUpper();
        }
    }
}
