using System.Web.Mvc;

namespace WebPerformanceHelpers.Core
{
    public interface IAjaxIncludeRequestEngine
    {
        string ExecuteRequest(AjaxIncludeProxyRequest request, ControllerContext context);
    }
}