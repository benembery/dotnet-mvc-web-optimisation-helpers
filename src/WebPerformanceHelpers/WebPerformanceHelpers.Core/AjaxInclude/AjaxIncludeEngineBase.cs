using System;
using System.Web;
using System.Web.Mvc;

namespace WebPerformanceHelpers.AjaxInclude
{
    public abstract class AjaxIncludeEngineBase
    {
        protected static string WrapInTag(string response, string url)
        {
            var tag = new TagBuilder("entry");
            tag.MergeAttribute("url", url);

            tag.InnerHtml = response.Replace(Environment.NewLine, string.Empty);

            return tag.ToString();
        }

        protected static Uri GetAbsoluteHost(HttpRequestBase request)
        {
            if (request == null || request.Url == null)
                return null;

            return new Uri(request.Url.Scheme + Uri.SchemeDelimiter + request.Url.Host + (request.Url.Port == 80 ? string.Empty : ":" + request.Url.Port));
        }
    }
}