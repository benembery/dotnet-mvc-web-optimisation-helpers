using System.Web;
using System.Web.Optimization;
using Microsoft.Ajax.Utilities;

namespace WebPerformanceHelpers.Bundles
{
    public static class ScriptsHelper
    {
        private const string InlineScriptFormat = "<script>{0}</script>";

        public static IHtmlString RenderScriptInline(string virtualPath)
        {
            if (string.IsNullOrWhiteSpace(virtualPath))
                return new HtmlString(string.Empty);

            var bundle = BundleTable.Bundles.GetBundleFor(virtualPath);

            if (bundle == null)
                return new HtmlString(string.Empty);

            var context = BundlesContextHelper.GetBundleContext(virtualPath);

            var response = bundle.GenerateBundleResponse(context);

            return new HtmlString(InlineScriptFormat.FormatInvariant(response.Content));
        }
    }
}