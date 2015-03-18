using System;
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
    }
}