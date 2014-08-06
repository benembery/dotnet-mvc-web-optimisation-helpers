using System.Linq;
using System.Web;
using System.Web.Optimization;
using Microsoft.Ajax.Utilities;

namespace WebPerformanceHelpers.Bundles
{
    public static class StylesHelper
    {
        private const string InlineStyleFormat = "<style>{0}</style>";
        private const string LoadCssAsyncWithNoScriptFallback = "<meta name=\"{0}\" content=\"{1}\" />{2}<noscript>{3}</noscript>";
        
        public static IHtmlString RenderStylesInline(string virtualPath)
        {
            if(string.IsNullOrWhiteSpace(virtualPath))
                return new HtmlString(string.Empty);
            
            var bundle = BundleTable.Bundles.GetBundleFor(virtualPath);
            
            if(bundle == null) 
                return new HtmlString(string.Empty);

            var context = BundlesContextHelper.GetBundleContext(virtualPath);

            var response = bundle.GenerateBundleResponse(context);

            return new HtmlString(InlineStyleFormat.FormatInvariant(response.Content));
        }


        public static IHtmlString RenderCriticalAndFullCss(string criticalCssVirtualPath, string fullCssVirtualPath,
            string cssCookieName)
        {
            if (BundlesContextHelper.Context.Request.Cookies.AllKeys.Contains(cssCookieName))
                return Styles.Render(fullCssVirtualPath);

            return new HtmlString(
                LoadCssAsyncWithNoScriptFallback.FormatInvariant(
                    cssCookieName,
                    Scripts.Url(fullCssVirtualPath),
                    RenderStylesInline(criticalCssVirtualPath).ToString(),
                    Styles.Render(fullCssVirtualPath)));
        }
    }
}
