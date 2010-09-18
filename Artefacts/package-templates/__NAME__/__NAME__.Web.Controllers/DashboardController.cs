using SharpArch.Core;
using System.Web.Mvc;
using Northwind.ApplicationServices;

namespace Northwind.Web.Controllers
{
    public class DashboardController : Controller
    {
        /// <summary>
        /// Note that the application service gets injected into the controller.  Since it's not a 
        /// repository (which gets automatically wired up for dependency injection), the service 
        /// needs to be manually registered within Northwind.Web.CastleWindsor.ComponentRegistrar
        /// </summary>
        public DashboardController(IDashboardService dashboardService) {
            Check.Require(dashboardService != null, "dashboardService may not be null");

            this.dashboardService = dashboardService;
        }

        /// <summary>
        /// Uses the application summary to collate the dashboard summary information
        /// </summary>
        public ActionResult Index() {
            DashboardService.DashboardSummaryDto summary = dashboardService.GetDashboardSummary();

            return View(summary);
        }

        private readonly IDashboardService dashboardService;
    }
}
