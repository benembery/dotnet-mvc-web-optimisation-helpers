using System.Web.Mvc;

namespace WebPerformanceHelpers.AjaxInclude
{
    public interface IAjaxIncludeRequestEngine
    {
        string ExecuteRequest(AjaxIncludeProxyRequest request, ControllerContext proxyControllerContext, int requestLimit);
    }
}