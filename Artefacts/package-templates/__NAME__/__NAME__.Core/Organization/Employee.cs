using SharpArch.Core.DomainModel;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;
using SharpArch.Core.NHibernateValidator;

namespace Northwind.Core.Organization
{
    /// <summary>
    /// The domain signature of this object isn't very realistic as you'll likely have same named 
    /// people in a large company.  Regardless, the Northwind DB doesn't provide a great domain 
    /// identifier, so the full name will have to do.  Alternatively, you don't have to have 
    /// domain signature properties.  If you don't, then Equals will use it's default behavior and
    /// compare the object references themselves.
    /// </summary>
    [HasUniqueDomainSignature(Message="An employee already exists with the same first and last name")]
    public class Employee : Entity
    {
        public Employee() {
            InitMembers();
        }

        /// <summary>
        /// Creates valid domain object
        /// </summary>
        public Employee(string firstName, string lastName)
            : this() {
            FirstName = firstName;
            LastName = lastName;
        }

        private void InitMembers() {
            // Init the collection so it's never null
            Territories = new List<Territory>();
        }

        [DomainSignature]
        [NotNullNotEmpty(Message = "Last name must be provided")]
        public virtual string LastName { get; set; }

        [DomainSignature]
        [NotNullNotEmpty(Message = "First name must be provided")]
        public virtual string FirstName { get; set; }

        [Range(1, 9999, Message = "Phone extension must be between 1 and 9999")]
        public virtual int PhoneExtension { get; set; }

        public virtual string FullName {
            get {
                return LastName + ", " + FirstName;
            }
        }

        /// <summary>
        /// Note the protected set...only the ORM should set the collection reference directly
        /// after it's been initialized in <see cref="InitMembers" />
        /// </summary>
        public virtual IList<Territory> Territories { get; protected set; }
    }
}
