using System.Linq;
using System.Web.Optimization;

namespace WebPerformanceHelpers.Bundles
{
    public static class BundleCollectionExtensions
    {
        public static bool BundleExists(this BundleCollection bundles, string virtualPath)
        {
            return bundles.Any(x => x.Path == virtualPath);
        }
    }
}