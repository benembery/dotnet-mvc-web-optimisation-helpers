using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace WebPerformanceHelpers.Tests.Web.App_Start
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            bundles.Add(new StyleBundle("~/bundles/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/css/theme").Include("~/Content/preboot.css"));

            bundles.Add(new ScriptBundle("~/bundles/js/asset-loader")
                .Include(new[] { "~/Scripts/filament-group/loadCSS.js", "~/Scripts/filament-group/loadJS.js" }));


            BundleTable.EnableOptimizations = true;
        }
    }
}