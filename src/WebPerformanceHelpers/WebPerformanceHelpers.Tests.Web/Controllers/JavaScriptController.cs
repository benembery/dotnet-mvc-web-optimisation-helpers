using System;
using System.Web;
using System.Web.Mvc;

namespace WebPerformanceHelpers.Tests.Web.Controllers
{
    [RoutePrefix("js")]
    public class JavaScriptController : Controller
    {
        [Route("cdn-script-fallback")]
        public ActionResult CdnScriptFallback()
        {
            return View();
        }

        [Route("ajax-include")]
        public ActionResult AjaxIncludes()
        {
            return View();
        }
    }
}