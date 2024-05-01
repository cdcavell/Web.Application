using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ClassLibrary.Mvc.Extensions
{
    public static class HttpRequestExtensions
    {
        public static string LogMessageHeader(this HttpRequest httpRequest)
        {
            string controller = httpRequest.RouteValues
                .Where(x => x.Key.Equals("controller", StringComparison.OrdinalIgnoreCase))
                .Select(x => x.Value?.ToString())
                .FirstOrDefault() ?? string.Empty;

            string action = httpRequest.RouteValues
                .Where(x => x.Key.Equals("action", StringComparison.OrdinalIgnoreCase))
                .Select(x => x.Value?.ToString())
                .FirstOrDefault() ?? string.Empty;

            IPAddress? remoteIPAddress = httpRequest.HttpContext.Connection.RemoteIpAddress;

            string logMessageHeader = string.Empty;

            if (!string.IsNullOrEmpty(controller))
                logMessageHeader += $" [Controller]: {controller}";
            if (!string.IsNullOrEmpty(action))
                logMessageHeader += $" [Action]: {action}";
            if (remoteIPAddress != null)
                logMessageHeader += $" [Remote IP]: {remoteIPAddress.MapToIPv4()}";

            logMessageHeader += " - ";

            return $"{logMessageHeader}";
        }

        public static bool IsAjaxRequest(this HttpRequest httpRequest)
        {
            return string.Equals(httpRequest.Query["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal) ||
                string.Equals(httpRequest.Headers.XRequestedWith, "XMLHttpRequest", StringComparison.Ordinal);
        }
    }
}
