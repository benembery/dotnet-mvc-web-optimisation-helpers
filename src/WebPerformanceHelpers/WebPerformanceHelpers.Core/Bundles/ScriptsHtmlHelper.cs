using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace WebPerformanceHelpers.Bundles
{
    public static class ScriptsHtmlHelper
    {
        private static string ScriptKeyFormat = "registered_{0}_script_bundle_{1}";

        /// <summary>
        /// Register the dependency on a bundle.
        /// Note: We have to return an empty string to be able to use this in views
        /// </summary>
        /// <param name="html"></param>
        /// <param name="scriptUrl"></param>
        /// <param name="isDependentOnCriticalScripts"></param>
        /// <returns></returns>
        public static IHtmlString RegisterScript(this HtmlHelper html, string scriptUrl, bool isDependentOnCriticalScripts)
        {
            var registeredBundles = BundleTable.Bundles.GetRegisteredBundles();

            if (registeredBundles.All(x => x.Path != scriptUrl))
                return new HtmlString(string.Empty);

            var scriptKey = GetFormattedScriptKey(scriptUrl, isDependentOnCriticalScripts);

            if (html.ViewContext.HttpContext.Items[scriptKey] == null)
                html.ViewContext.HttpContext.Items[scriptKey] = scriptUrl;

            return new HtmlString(string.Empty);
        }

        public static IHtmlString RenderIndependentScriptsUrls(this HtmlHelper html)
        {
            return new HtmlString(html.RenderScriptUrls(false));
        }

        public static IHtmlString RenderDependentScriptsUrls(this HtmlHelper html)
        {
            return new HtmlString(html.RenderScriptUrls(true));
        }

        private static string RenderScriptUrls(this HtmlHelper html, bool isDependentOnCriticalScripts)
        {
            var urls = html.ViewContext.HttpContext.Items.Keys.OfType<string>().Where(x => x.StartsWith(GetFormattedScriptKey(string.Empty, isDependentOnCriticalScripts))).ToList();

            if (!urls.Any()) return string.Empty;

            return ","+string.Join(",", urls.Select(scriptUrl => Scripts.Url(html.ViewContext.HttpContext.Items[scriptUrl] as string)));
        }

        private static string GetFormattedScriptKey(string scriptUrl, bool isDependentOnCriticalScripts = false)
        {
            return string.Format(ScriptKeyFormat, isDependentOnCriticalScripts ? "dependent" : "independent", scriptUrl);
        }
    }
}