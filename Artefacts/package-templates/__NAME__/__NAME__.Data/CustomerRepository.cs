using Northwind.Core.DataInterfaces;
using Northwind.Core;
using NHibernate;
using SharpArch.Data.NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;

namespace Northwind.Data
{
    public class CustomerRepository : NHibernateRepositoryWithTypedId<Customer, string>, ICustomerRepository
    {
        public List<Customer> FindByCountry(string countryName) {
            ICriteria criteria = Session.CreateCriteria(typeof(Customer))
                .Add(Expression.Eq("Country", countryName));

            return criteria.List<Customer>() as List<Customer>;
        }
    }
}
