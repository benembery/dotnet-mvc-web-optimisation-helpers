using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebPerformanceHelpers.Extensions;

namespace WebPerformanceHelpers.AjaxInclude
{
    public class WebClientAjaxIncludeEngine : AjaxIncludeEngineBase, IAjaxIncludeRequestEngine
    {
        public string ExecuteRequest(AjaxIncludeProxyRequest request, ControllerContext proxyControllerContext, int requestLimit)
        {
            if (request == null || !request.RequestsList.Any() || request.RequestsList.Count > requestLimit)
                return null;

            var httpRequest = proxyControllerContext.RequestContext.HttpContext.Request;

            if (httpRequest == null)
                return null;

            var host = httpRequest.GetAbsoluteHost();
            var webClient = new WebClient();

            var response = string.Empty;

            foreach (var requestUrl in request.RequestsList)
            {
                var uri = new Uri(host, requestUrl);

                var requestResponse = webClient.DownloadString(uri);

                response += request.Wrap ? WrapInTag(requestResponse, requestUrl) : requestResponse;
            }

            return response;
        }
    }
}