using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;

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

    [RoutePrefix("ajax-include-examples")]
    public class AjaxIncludeExamplesController : Controller
    {
        [Route("replace")]
        public ActionResult Replace()
        {
            return PartialView();
        }

        [Route("proxy")]
        public ActionResult Proxy(AjaxIncludeProxyRequest request)
        {
            return new AjaxIncludeProxyResult(request);
        }
    }

    public class AjaxIncludeProxyResult : ActionResult
    {
        private readonly AjaxIncludeProxyRequest _request;

        public AjaxIncludeProxyResult(AjaxIncludeProxyRequest request)
        {
            _request = request;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (!_request.RequestsList.Any())
            {
                new EmptyResult().ExecuteResult(context);
                return;
            }

            foreach (var requestUrl in _request.RequestsList)
            {
                //var httpRequest= new HttpRequest(null, requestUrl)
                //TODO: call action request.
            }
        }
    }

    public class AjaxIncludeProxyRequest
    {
        public bool Wrap { get; set; }
        public string Requests { private get; set; }

        public IList<string> RequestsList
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Requests))
                    return new List<string>();

                return Requests.Split(',').ToList();
            }
            set { Requests = string.Join(",", value); }
        }
    }
}