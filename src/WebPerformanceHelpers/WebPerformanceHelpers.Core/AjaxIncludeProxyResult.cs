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

            foreach (var requestUrl in requestsList)
            {
                Uri uri = new Uri(absoluteHost, requestUrl);
                var requestResponse = ExecuteRequest(uri.AbsoluteUri);

                if (requestResponse != null)
                {
                    response += WrapInTag(requestResponse, requestUrl);
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

        private static string ExecuteRequest(string url)
        {
            string content;
            var req = HttpWebRequest.Create(url);

            req.Method = "GET";
            req.Credentials = CredentialCache.DefaultCredentials;

            using (var response = (HttpWebResponse)req.GetResponse())
            using (var dataStream = response.GetResponseStream())
            {
                var reader = new StreamReader(dataStream);
                content = reader.ReadToEnd();
            }

            return content;
        }
    }
}