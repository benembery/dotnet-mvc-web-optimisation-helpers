using System;
using System.Web.Mvc;

namespace WebPerformanceHelpers.AjaxInclude
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AjaxIncludeActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.RequestContext.RouteData.DataTokens.ContainsKey(AjaxIncludeConstants.AjaxIncludeKeyName)
                || (Guid) filterContext.RequestContext.RouteData.DataTokens[AjaxIncludeConstants.AjaxIncludeKeyName] != AjaxIncludeConstants.AjaxIncludeKey)
            {
                filterContext.Result = new EmptyResult();
                return;
            }
                

            base.OnActionExecuting(filterContext);
        }
    }
}