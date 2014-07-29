using System.CodeDom;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;

namespace WebPerformanceHelpers.Core
{
    public class AjaxIncludeProxyResult : ActionResult
    {
        private readonly AjaxIncludeProxyRequest _request;
        private readonly IAjaxIncludeRequestEngine _engine;

        public AjaxIncludeProxyResult(AjaxIncludeProxyRequest request)
        {
            _request = request;
            _engine =  new DefaultAjaxIncludeEngine();
        }

        public AjaxIncludeProxyResult(AjaxIncludeProxyRequest request, IAjaxIncludeRequestEngine engine)
        {
            _request = request;
            _engine = engine;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = _engine.ExecuteRequest(_request, context);

            if (string.IsNullOrWhiteSpace(response))
            {
                new EmptyResult().ExecuteResult(context);
                return;
            }

            context.RequestContext.HttpContext.Response.ContentType = "text/html";
            context.RequestContext.HttpContext.Response.Write(response);
        }
    }
}