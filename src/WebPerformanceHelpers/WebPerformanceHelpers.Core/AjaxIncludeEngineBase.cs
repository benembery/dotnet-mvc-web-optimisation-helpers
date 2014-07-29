using System;
using System.Web.Mvc;

namespace WebPerformanceHelpers.Core
{
    public abstract class AjaxIncludeEngineBase
    {
        protected static string WrapInTag(string response, string url)
        {
            var tag = new TagBuilder("entry");
            tag.MergeAttribute("url", url);

            tag.InnerHtml = response.Replace(Environment.NewLine, string.Empty); // Remove new lines from response to side step ajax-include regex bug.

            return tag.ToString();
        }
    }
}