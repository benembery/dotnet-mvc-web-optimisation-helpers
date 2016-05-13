using System.Collections.Generic;
using System.Web.Routing;
using WebPerformanceHelpers.AjaxInclude;

namespace WebPerformanceHelpers.Extensions
{
    public static class RouteDataExtensions
    {
        public static string GetActionName(this RouteData routeData)
        {
            routeData = routeData.GetRouteDataForAttributeRoute() ?? routeData;

            return routeData.Values.ContainsKey("action") ?
                routeData.Values["action"] as string :
                string.Empty;
        }

        public static bool IsAttributeRoute(this RouteData routeData)
        {
            return routeData.Values.ContainsKey("MS_DirectRouteMatches");
        }

        public static RouteData GetRouteDataForAttributeRoute(this RouteData routeData)
        {
            if (!routeData.IsAttributeRoute())
                return null;

            return ((IList<RouteData>)routeData.Values["MS_DirectRouteMatches"])[0];
        }

        public static void AddAjaxIncludeDataTokens(this RouteData routeData)
        {
            if (routeData == null)
                return;

            var routeDataToModify = routeData.GetRouteDataForAttributeRoute() ?? routeData;
            
            routeDataToModify.DataTokens.Add(AjaxIncludeConstants.AjaxIncludeKeyName, AjaxIncludeConstants.AjaxIncludeKey);
        }
    }
}