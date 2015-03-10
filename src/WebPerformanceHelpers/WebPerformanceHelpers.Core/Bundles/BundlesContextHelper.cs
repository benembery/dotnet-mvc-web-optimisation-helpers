using System.Web;
using System.Web.Optimization;

namespace WebPerformanceHelpers.Bundles
{
    internal static class BundlesContextHelper
    {
        private static HttpContextBase _context;

        public static HttpContextBase Context
        {
            get
            {
                return _context ?? new HttpContextWrapper(HttpContext.Current);
            }
            set
            {
                _context = value;
            }
        }

        public static BundleContext GetBundleContext(string virtualPath)
        {
            return new BundleContext(Context, BundleTable.Bundles, virtualPath);
        }
    }
}