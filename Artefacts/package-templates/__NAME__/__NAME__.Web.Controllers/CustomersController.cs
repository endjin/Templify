using System.Web.Mvc;
using Northwind.Core;
using System.Collections.Generic;
using Northwind.Core.DataInterfaces;
using SharpArch.Web.NHibernate;
using SharpArch.Core;

namespace Northwind.Web.Controllers
{
    [HandleError]
    public class CustomersController : Controller
    {
        public CustomersController(ICustomerRepository customerRepository) {
            Check.Require(customerRepository != null, "customerRepository may not be null");

            this.customerRepository = customerRepository;
        }

        /// <summary>
        /// The transaction on this action is optional, but recommended for performance reasons
        /// </summary>
        [Transaction]
        public ActionResult Index() {
            List<Customer> customers = customerRepository.FindByCountry("Venezuela");
            return View(customers);
        }

        /// <summary>
        /// An example of creating an object with an assigned ID.  Because this uses a declarative 
        /// transaction, everything within this method is wrapped within a single transaction.
        /// 
        /// I'd like to be perfectly clear that I think assigned IDs are almost always a terrible
        /// idea; this is a major complaint I have with the Northwind database.  With that said, 
        /// some legacy databases require such techniques.
        /// </summary>
        [Transaction]
        public ActionResult Create(string companyName, string assignedId) {
            Customer customer = new Customer(companyName);
            customer.SetAssignedIdTo(assignedId);
            customerRepository.Save(customer);

            return View(customer);
        }

        private readonly ICustomerRepository customerRepository;
    }
}
