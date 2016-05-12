using System.Web.Mvc;

namespace WebPerformanceHelpers.AjaxInclude
{
    public class AjaxIncludeProxyResult : ActionResult
    {
        private readonly AjaxIncludeProxyRequest _request;
        private readonly IAjaxIncludeRequestEngine _engine;
        private readonly int _requestLimit;

        public AjaxIncludeProxyResult(AjaxIncludeProxyRequest request, int requestLimit)
        {
            _request = request;
            _engine =  new MvcAjaxIncludeEngine();
            _requestLimit = requestLimit;
        }

        public AjaxIncludeProxyResult(AjaxIncludeProxyRequest request, IAjaxIncludeRequestEngine engine, int requestLimit)
        {
            _request = request;
            _engine = engine;
            _requestLimit = requestLimit;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = _engine.ExecuteRequest(_request, context, _requestLimit);
            context.RequestContext.HttpContext.Response.Clear();
            context.RequestContext.HttpContext.Response.ContentType = "text/html";
            context.RequestContext.HttpContext.Response.Write(response);
        }
    }
}