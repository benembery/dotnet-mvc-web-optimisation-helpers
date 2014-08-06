using System;
using System.Web.Mvc;

namespace WebPerformanceHelpers.Tests.Web.Controllers
{
    public class HomeController : Controller
    {
        [OutputCache(CacheProfile = "Static")]
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult GoBackLink()
        {
            var routeData = ControllerContext.ParentActionViewContext.RouteData;

            if (routeData.GetRequiredString("action").Equals("Index", StringComparison.InvariantCultureIgnoreCase)
                && routeData.GetRequiredString("controller").Equals("Home", StringComparison.InvariantCultureIgnoreCase))
                return new EmptyResult();

            return PartialView();
        }
    }
}