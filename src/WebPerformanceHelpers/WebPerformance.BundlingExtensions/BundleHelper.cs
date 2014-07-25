using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Optimization;
using Microsoft.Ajax.Utilities;

namespace WebPerformance.BundlingExtensions
{
    public static class BundleHelper
    {
        private const string InlineStyleFormat = "<style>{0}</style>";
        private static HttpContextBase _context;
        
        internal static HttpContextBase Context
        {
            get
            {
                return BundleHelper._context ?? (HttpContextBase)new HttpContextWrapper(HttpContext.Current);
            }
            set
            {
                BundleHelper._context = value;
            }
        }

        public static IHtmlString RenderStylesInline(string virtualPath)
        {
            var bundles = BundleTable.Bundles;
            var bundle = bundles.GetBundleFor(virtualPath);
            var context = new BundleContext(Context, bundles, virtualPath);

            var response = bundle.GenerateBundleResponse(context);

            return new HtmlString(InlineStyleFormat.FormatInvariant(response.Content));
        }

        
    }
}
