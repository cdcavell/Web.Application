using ClassLibrary.Mvc.Exceptions;
using ClassLibrary.Mvc.Http;
using ClassLibrary.Mvc.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ClassLibrary.Mvc.Controllers
{
    public class ErrorController(ILogger<ErrorController> logger) : WebBaseController<ErrorController>(logger)
    {

        [HttpGet("Error/{id?}")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index(int id)
        {
            if (!HttpContext.Response.Headers.ContainsKey("X-Robots-Tag"))
                HttpContext.Response.Headers.Append("X-Robots-Tag", "noindex");

            string requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            KeyValuePair<int, string> kvp = StatusCodeDefinitions.GetCodeDefinition(id);
            var exceptionHandlerPathFeature =
                HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            int statusCode = kvp.Key;
            string statusMessage = kvp.Value.Trim();

            if (exceptionHandlerPathFeature != null)
            {
                if (exceptionHandlerPathFeature.Error.Message.Contains("Invalid Model State", StringComparison.OrdinalIgnoreCase))
                {
                    statusCode = 400;
                    statusMessage = exceptionHandlerPathFeature?.Error.Message.Trim() ?? kvp.Value.Trim();
                    exceptionHandlerPathFeature = null;
                }
                else
                {
                    statusMessage = exceptionHandlerPathFeature?.Error.Message.Trim() ?? kvp.Value.Trim();
                }
            }

            ErrorViewModel viewModel = new()
            {
                RequestId = requestId.Trim(),
                StatusCode = statusCode,
                StatusMessage = statusMessage,
                Exception = exceptionHandlerPathFeature?.Error
            };

            return View(viewModel);
        }

        [HttpGet("Status/{id?}")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Status(int id)
        {
            if (!HttpContext.Response.Headers.ContainsKey("X-Robots-Tag"))
                HttpContext.Response.Headers.Append("X-Robots-Tag", "noindex");

            KeyValuePair<int, string> kvp = StatusCodeDefinitions.GetCodeDefinition(id);

            throw kvp.Key switch
            {
                StatusCodes.Status400BadRequest => new BadHttpRequestException($"{kvp.Value}"),
                StatusCodes.Status401Unauthorized => new UnauthorizedAccessException($"{kvp.Value}"),
                StatusCodes.Status404NotFound => new NotFoundException($"{kvp.Value}"),
                _ => new Exception($"Unhandled Status Code: {id}"),
            };
        }
    }
}
