using System.Web;
using System.Web.Optimization;
using Microsoft.Ajax.Utilities;

namespace WebPerformanceHelpers.Bundles
{
    public static class StylesHelper
    {
        private const string InlineStyleFormat = "<style>{0}</style>";
        
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
    }
}
