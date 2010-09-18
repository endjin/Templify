using System.Web.Mvc;
using System;

namespace Northwind.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index() {
            return View();
        }
    }
}
