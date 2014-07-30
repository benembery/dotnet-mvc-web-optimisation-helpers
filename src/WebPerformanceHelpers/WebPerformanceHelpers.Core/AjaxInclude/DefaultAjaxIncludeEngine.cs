using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPerformanceHelpers.Extensions;

namespace WebPerformanceHelpers.AjaxInclude
{
    public class DefaultAjaxIncludeEngine : AjaxIncludeEngineBase, IAjaxIncludeRequestEngine {
        
        public string ExecuteRequest(AjaxIncludeProxyRequest request, ControllerContext context)
        {
            if (request == null || !request.RequestsList.Any())
                return null;

            var httpRequest = context.RequestContext.HttpContext.Request;

            if (httpRequest == null)
                return null;

            var host = httpRequest.GetAbsoluteHost();

            var factory = ControllerBuilder.Current.GetControllerFactory();

            var response = string.Empty;
            // TODO create multiple threads to execute actions more quickly.
            foreach (var requestUrl in request.RequestsList)
            {
                var uri = new Uri(host, requestUrl);
                var responseWriter = new StringWriter();
                var includeContext = BuildRequest(uri, responseWriter);
                                
                var controllerName = includeContext.Request.RequestContext.RouteData.GetRequiredString("controller");
                var controller = factory.CreateController(includeContext.Request.RequestContext, controllerName);
                
                controller.Execute(includeContext.Request.RequestContext);
                response += request.Wrap ? WrapInTag(responseWriter.ToString(), requestUrl) : responseWriter.ToString();

                factory.ReleaseController(controller);
                responseWriter.Dispose();
            }

            return response;
        }

        public static HttpContextBase BuildRequest(Uri requestUri, StringWriter writer)
        {
            var query = string.IsNullOrWhiteSpace(requestUri.Query) ? null : requestUri.Query.Substring(1);
            
            var request = new HttpRequest(null, requestUri.AbsoluteUri, query);
            var response = new HttpResponse(writer);
            var context = new HttpContext(request, response);
            var contextBase = new HttpContextWrapper(context);
            
            //Build and assign your route data.
            var routeData = System.Web.Routing.RouteTable.Routes.GetRouteData(contextBase);
            request.RequestContext.RouteData = routeData;

            return contextBase;
        }
    }
}