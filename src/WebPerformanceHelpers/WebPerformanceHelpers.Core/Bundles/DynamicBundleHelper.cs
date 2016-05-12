using System.Web.Mvc;
using System.Web.Optimization;
using BundleTransformer.Core.Bundles;

namespace WebPerformanceHelpers.Bundles
{
    public static class DynamicBundleHelper
    {
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

        public static void RenderBundle(ControllerContext context, string virtualPath)
        {
            var bundleContext = new BundleContext(context.HttpContext, BundleTable.Bundles, virtualPath);
            var response = BundleTable.Bundles.GetBundleFor(virtualPath).GenerateBundleResponse(bundleContext);

            context.RequestContext.HttpContext.Response.Cache.SetCacheability(response.Cacheability);
            context.RequestContext.HttpContext.Response.ContentType = response.ContentType;
            context.RequestContext.HttpContext.Response.Write(response.Content);
        }
    }
}