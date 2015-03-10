using System.Web.Optimization;

namespace WebPerformanceHelpers.Bundles
{
    internal static class BundleContentRenderer
    {
        public static string GetContent(string virtualPath)
        {
            if (string.IsNullOrWhiteSpace(virtualPath))
                return string.Empty;

            var bundle = BundleTable.Bundles.GetBundleFor(virtualPath);

            if (bundle == null)
                return string.Empty;

            return bundle.GenerateBundleResponse(BundlesContextHelper.GetBundleContext(virtualPath)).Content;
        }

    }
}