﻿using System.Web.Mvc;
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
            return new AjaxIncludeProxyResult(proxyRequest);
        }

        [Route("replace")]
        public ActionResult Replace()
        {
            return PartialView();
        }

        [Route("before")]
        public ActionResult Before()
        {
            return PartialView();
        }

        [Route("after")]
        public ActionResult After()
        {
            return PartialView();
        }

        [Route("append")]
        public ActionResult Append()
        {
            return PartialView();
        }
    }
}