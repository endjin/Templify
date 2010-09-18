using System.Web.Mvc;
using Northwind.Wcf;
using System.ServiceModel;
using Northwind.WcfServices;
using System;
using Northwind.Wcf.Dtos;
using System.Collections.Generic;
using SharpArch.Core;

namespace Northwind.Web.Controllers
{
    public class TerritoriesController : Controller
    {
        public TerritoriesController(ITerritoriesWcfService territoriesWcfService) {
            Check.Require(territoriesWcfService != null, "territoriesWcfService may not be null");

            this.territoriesWcfService = territoriesWcfService;
        }

        public ActionResult Index() {
            IList<TerritoryDto> territories = null;

            // WCF service closing advice taken from http://msdn.microsoft.com/en-us/library/aa355056.aspx
            // As alternative to this verbose-ness, use the SharpArch.WcfClient.Castle.WcfSessionFacility
            // for automatically closing the WCF service.
            try {
                territories = territoriesWcfService.GetTerritories();
                territoriesWcfService.Close();
            }
            catch (CommunicationException) {
                territoriesWcfService.Abort();
            }
            catch (TimeoutException) {
                territoriesWcfService.Abort();
            }
            catch (Exception) {
                territoriesWcfService.Abort();
                throw;
            }

            return View(territories);
        }

        private readonly ITerritoriesWcfService territoriesWcfService;
    }
}
