using SharpArch.Core.PersistenceSupport;
using SharpArch.Core.DomainModel;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;
using Northwind.Core.Organization;
using SharpArch.Core;

namespace Northwind.Core
{
    public class Territory : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        public Territory() {
            InitMembers();
        }

        /// <summary>
        /// Creates valid domain object
        /// </summary>
        public Territory(string description, Region regionBelongingTo) : this() {
            RegionBelongingTo = regionBelongingTo;
            Description = description;
        }

        private void InitMembers() {
            // Init the collection so it's never null
            Employees = new List<Employee>();
        }

        /// <summary>
        /// Let me remind you that I completely disdane assigned Ids...another lesson to be learned
        /// from the fallacies of the Northwind DB.
        /// </summary>
        public virtual void SetAssignedIdTo(string assignedId) {
            Check.Require(!string.IsNullOrEmpty(assignedId) && assignedId.Length <= ID_MAX_LENGTH);
            Id = assignedId;
        }

        [DomainSignature]
        [NotNull]
        public virtual Region RegionBelongingTo { get; set; }
        
        [DomainSignature]
        [NotNullNotEmpty]
        public virtual string Description { get; set; }

        /// <summary>
        /// Note the protected set...only the ORM should set the collection reference directly
        /// after it's been initialized in <see cref="InitMembers" />
        /// </summary>
        public virtual IList<Employee> Employees { get; protected set; }

        private const int ID_MAX_LENGTH = 20;
    }
}
