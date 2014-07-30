using System.Web.Mvc;
using WebPerformanceHelpers.AjaxInclude;

namespace WebPerformanceHelpers.Tests.Web.Controllers
{
    [RoutePrefix("ajax-include-examples")]
    public class AjaxIncludeExamplesController : Controller
    {
        [Route("replace")]
        public ActionResult Replace()
        {
            return PartialView();
        }

        [Route("proxy")]
        public ActionResult Proxy(AjaxIncludeProxyRequest proxyRequest, string includes)
        {
            return new AjaxIncludeProxyResult(proxyRequest);
        }
    }
}