namespace MyApp.WebCore.Controllers
{
    #region Using Directives

    using System.Web.Mvc;

    using MyApp.Domain;
    using MyApp.Tasks;

    #endregion

    public class PersonController : Controller
    {
        public ActionResult Index()
        {
            new PersonTasks().Process(new Person());

            return View();
        }
    }
}