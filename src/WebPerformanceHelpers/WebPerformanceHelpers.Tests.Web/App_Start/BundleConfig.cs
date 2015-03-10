using System.Web.Optimization;

namespace WebPerformanceHelpers.Tests.Web.App_Start
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            bundles.Add(new StyleBundle("~/bundles/critical-css")
                .Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/css")
                .Include(new[] { "~/Content/preboot.css", "~/Content/site.css", "~/Content/site-theme.css" }));

            bundles.Add(new StyleBundle("~/bundles/css/code-blocks")
                .Include("~/Content/code-blocks.css"));

            bundles.Add(new ScriptBundle("~/bundles/js/enhance")
                .Include(new[] { "~/Scripts/filament-group/enhance.js" }));

            bundles.Add(new ScriptBundle("~/bundles/js/asset-loader")
                .Include(new[] { "~/Scripts/filament-group/loadCSS.js", "~/Scripts/filament-group/loadJS.js" }));

            var jQuery = new ScriptBundle("~/bundles/js/jquery", "//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.js")
                .Include("~/Scripts/jquery-{version}.js");

            jQuery.CdnFallbackExpression = "window.jQuery";

            bundles.Add(jQuery);


            bundles.Add(new ScriptBundle("~/bundles/js/ajax-include")
                .Include(new[] { "~/Scripts/filament-group/ajaxInclude.js", "~/Scripts/filament-group/ajaxIncludePlugins.js" }));

            //BundleTable.EnableOptimizations = true;
        }
    }
}