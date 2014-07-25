using System;
using System.Collections.Generic;
using System.Linq;
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
	}
}