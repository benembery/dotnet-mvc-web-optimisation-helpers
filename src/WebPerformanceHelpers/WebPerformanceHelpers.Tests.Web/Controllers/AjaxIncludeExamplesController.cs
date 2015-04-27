using System.Web.Mvc;
using WebPerformanceHelpers.AjaxInclude;

namespace WebPerformanceHelpers.Tests.Web.Controllers
{
    [OutputCache(CacheProfile = "Static")]
    [RoutePrefix("ajax-include-examples")]
    public class AjaxIncludeExamplesController : Controller
    {
        [Route("proxy")]
        public ActionResult Proxy(AjaxIncludeProxyRequest proxyRequest)
        {
            return new AjaxIncludeProxyResult(proxyRequest, 10);
        }

        [AjaxIncludeAction]
        [Route("replace")]
        public ActionResult Replace()
        {
            return PartialView();
        }

        [AjaxIncludeAction]
        [Route("before")]
        public ActionResult Before()
        {
            return PartialView();
        }

        [AjaxIncludeAction]
        [Route("after")]
        public ActionResult After()
        {
            return PartialView();
        }

        [AjaxIncludeAction]
        [Route("append")]
        public ActionResult Append()
        {
            return PartialView();
        }
    }
}