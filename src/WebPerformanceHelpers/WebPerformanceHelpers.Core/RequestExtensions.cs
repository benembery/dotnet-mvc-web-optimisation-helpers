using System;
using System.Web;

namespace WebPerformanceHelpers.Core
{
    public static class RequestExtensions
    {
        public static Uri GetAbsoluteHost(this HttpRequestBase request)
        {
            if (request == null || request.Url == null)
                return null;

            return new Uri(request.Url.Scheme + Uri.SchemeDelimiter + request.Url.Host + (request.Url.Port == 80 ? string.Empty : ":" + request.Url.Port));
        }
    }
}