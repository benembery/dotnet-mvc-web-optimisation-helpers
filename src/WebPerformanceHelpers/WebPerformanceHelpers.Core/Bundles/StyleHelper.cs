using System.Web;

namespace WebPerformanceHelpers.Bundles
{
    public static class StyleHelper
    {
        private const string InlineStyleFormat = "<style>{0}</style>";
        
        public static IHtmlString RenderStyleInline(string virtualPath)
        {
            return new HtmlString(string.Format(InlineStyleFormat, BundleContentRenderer.GetContent(virtualPath)));
        }
    }
}
