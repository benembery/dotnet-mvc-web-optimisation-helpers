using System.Web.Mvc;
using System.Web.Optimization;

namespace WebPerformanceHelpers.Extensions
{
    public static class ControllerExtensions
    {
        public static void RenderBundle(this ControllerContext context, string virtualPath)
        {
            var bundleContext = new BundleContext(context.HttpContext, BundleTable.Bundles, virtualPath);
            var response = BundleTable.Bundles.GetBundleFor(virtualPath).GenerateBundleResponse(bundleContext);

            context.RequestContext.HttpContext.Response.Cache.SetCacheability(response.Cacheability);
            context.RequestContext.HttpContext.Response.ContentType = response.ContentType;
            context.RequestContext.HttpContext.Response.Write(response.Content);
        }
    }
}