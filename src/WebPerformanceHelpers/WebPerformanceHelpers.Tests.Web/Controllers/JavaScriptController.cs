using System.Web.Mvc;

namespace WebPerformanceHelpers.Tests.Web.Controllers
{

    [OutputCache(CacheProfile = "Static")]
    [RoutePrefix("js")]
    public class JavaScriptController : Controller
    {
        [Route("inline-critical-javascript")]
        public ActionResult InlineCriticalScript()
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