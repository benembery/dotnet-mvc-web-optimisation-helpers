using System.Web.Mvc;
using System.Web.Optimization;

namespace WebPerformanceHelpers.Bundles
{
    public class DynamicScriptBundleResult : ActionResult
    {
        private readonly string _virtualPath;
        private readonly string[] _bundleFiles;
        private readonly IBundleBuilder _dynamicBundleBuilder;

        public DynamicScriptBundleResult(string virtualPath, string[] bundleFiles, IBundleBuilder dynamicBundleBuilder)
        {
            _virtualPath = virtualPath;
            _bundleFiles = bundleFiles;
            _dynamicBundleBuilder = dynamicBundleBuilder;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            BundleTable.Bundles.RegisterDynamicBundleIfNotExists(_virtualPath, _bundleFiles, _dynamicBundleBuilder);
            context.RenderBundle(_virtualPath);
        }

    }
}