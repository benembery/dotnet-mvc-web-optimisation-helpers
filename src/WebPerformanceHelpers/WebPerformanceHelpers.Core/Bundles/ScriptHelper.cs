using System.Web;

namespace WebPerformanceHelpers.Bundles
{
    public static class ScriptHelper
    {
        private const string InlineScriptFormat = "<script>{0}</script>";

        public static IHtmlString RenderScriptInline(string virtualPath)
        {
            return new HtmlString(string.Format(InlineScriptFormat, BundleContentRenderer.GetContent(virtualPath)));
        }
    }
}