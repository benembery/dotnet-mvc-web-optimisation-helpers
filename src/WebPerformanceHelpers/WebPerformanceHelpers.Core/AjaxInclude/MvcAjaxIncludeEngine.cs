using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPerformanceHelpers.AjaxInclude
{
    public class MvcAjaxIncludeEngine : AjaxIncludeEngineBase, IAjaxIncludeRequestEngine
    {
        /// <summary>
        /// Generate a response for a list of requests.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="proxyControllerContext"></param>
        /// <param name="requestLimit"></param>
        /// <returns></returns>
        public string ExecuteRequest(AjaxIncludeProxyRequest request, ControllerContext proxyControllerContext, int requestLimit)
        {
            if (request == null || !request.RequestsList.Any() || request.RequestsList.Count > requestLimit)
                return null;

            var host = GetHostUri(proxyControllerContext);

            if (host == null)
                return null;

            var factory = ControllerBuilder.Current.GetControllerFactory();
            var response = string.Empty;

            foreach (var requestUrl in request.RequestsList)
            {
                using (var responseWriter = new StringWriter())
                {
                    var uri = new Uri(host, requestUrl);
                    var requestHttpContext = BuildRequest(uri, responseWriter);
                    var controllerName = requestHttpContext.Request.RequestContext.RouteData.GetRequiredString("controller");
                    var controller = factory.CreateController(requestHttpContext.Request.RequestContext, controllerName);

                    if (IsValidRequest(requestHttpContext, controller))
                        controller.Execute(requestHttpContext.Request.RequestContext);

                    response += request.Wrap
                            ? WrapInTag(responseWriter.ToString(), requestUrl)
                            : responseWriter.ToString();

                    factory.ReleaseController(controller);
                }
            }

            return response;
        }

        /// <summary>
        /// Ensure that the action has a AjaxIncludeAction attribute associated with it.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        private bool IsValidRequest(HttpContextBase httpContext, IController controller)
        {
            var controllerContext = new ControllerContext(httpContext.Request.RequestContext, controller as ControllerBase);
            var action = new ReflectedControllerDescriptor(controller.GetType()).FindAction(controllerContext,
                       httpContext.Request.RequestContext.RouteData.GetRequiredString("action"));

            if (action == null)
                return false;

            return action.GetCustomAttributes(typeof(AjaxIncludeActionAttribute), true).Length > 0;
        }

        /// <summary>
        /// Get Host Uri from Controller Context.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static Uri GetHostUri(ControllerContext context)
        {
            if (context == null
                || context.RequestContext == null
                || context.RequestContext.HttpContext == null
                || context.RequestContext.HttpContext.Request == null)
                return null;

            return GetAbsoluteHost(context.RequestContext.HttpContext.Request);
        }

        /// <summary>
        /// Build a new HttpRequest to make our Controller Request.
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="writer"></param>
        /// <returns></returns>
        public static HttpContextBase BuildRequest(Uri requestUri, StringWriter writer)
        {
            var query = string.IsNullOrWhiteSpace(requestUri.Query) ? null : requestUri.Query.Substring(1);

            var request = new HttpRequest(null, requestUri.AbsoluteUri, query);
            var response = new HttpResponse(writer);
            var context = new HttpContext(request, response);
            var contextBase = new HttpContextWrapper(context);

            //Build and assign route data.
            var routeData = System.Web.Routing.RouteTable.Routes.GetRouteData(contextBase);
            if (routeData != null)
                routeData.DataTokens.Add(AjaxIncludeConstants.AjaxIncludeKeyName, AjaxIncludeConstants.AjaxIncludeKey);

            request.RequestContext.RouteData = routeData;

            return contextBase;
        }
    }
}