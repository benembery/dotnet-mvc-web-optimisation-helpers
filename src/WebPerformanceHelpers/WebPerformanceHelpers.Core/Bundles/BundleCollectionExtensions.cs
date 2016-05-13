using System.Linq;
using System.Web.Optimization;
using BundleTransformer.Core.Bundles;

namespace WebPerformanceHelpers.Bundles
{
    internal static class BundleCollectionExtensions
    {
        public static bool BundleExists(this BundleCollection bundles, string virtualPath)
        {
            return bundles.Any(x => x.Path == virtualPath);
        }

        public static void RegisterDynamicBundleIfNotExists(this BundleCollection bundles, string virtualPath,
            string[] files, IBundleBuilder bundleBuilder)
        {
            if (bundles.BundleExists(virtualPath))
                return;

            var bundle = new CustomScriptBundle(virtualPath);

            if (files != null)
                bundle.Include(files);

            bundle.Builder = bundleBuilder;
            bundles.Add(bundle);
        }
    }
}