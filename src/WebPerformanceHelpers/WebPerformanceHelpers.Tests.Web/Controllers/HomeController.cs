using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPerformanceHelpers.Tests.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
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