using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebPerformanceHelpers.Core
{
    public class AjaxIncludeProxyResult : ActionResult
    {
        private readonly AjaxIncludeProxyRequest _request;

        public AjaxIncludeProxyResult(AjaxIncludeProxyRequest request)
        {
            _request = request;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var httpRequest = context.RequestContext.HttpContext.Request;

            var response = GetResponse(httpRequest, _request.RequestsList);

            if (string.IsNullOrWhiteSpace(response))
            {
                new EmptyResult().ExecuteResult(context);
                return;
            }

            context.RequestContext.HttpContext.Response.ContentType = "text/html";
            context.RequestContext.HttpContext.Response.Write(response);
        }

        private string GetResponse(HttpRequestBase httpRequest, IList<string> requestsList)
        {
            if (requestsList == null || !requestsList.Any())
                return null;

            Uri absoluteHost = httpRequest.GetAbsoluteHost();

            var response = string.Empty;

            using (var client = new WebClient())
            {
                //TODO create task array to handle all requests in parallel.
                foreach (var requestUrl in requestsList)
                {
                    Uri uri = new Uri(absoluteHost, requestUrl);
                    
                    var requestResponse = client.DownloadString(uri); //ExecuteRequest(uri.AbsoluteUri);
                    
                    if (!string.IsNullOrWhiteSpace(requestResponse))
                    {
                        response += WrapInTag(requestResponse, requestUrl);
                    }
                }
            }

            return response;
        }

        private static string WrapInTag(string requestResponse, string url)
        {
            var tag = new TagBuilder("entry");
            tag.MergeAttribute("url", url);

            tag.InnerHtml = requestResponse.Replace(Environment.NewLine, string.Empty); // Remove new lines from response to side step ajax-include regex bug.

            return tag.ToString();
        }
    }
}