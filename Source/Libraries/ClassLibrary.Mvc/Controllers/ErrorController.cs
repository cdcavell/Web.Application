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

            ErrorViewModel viewModel = new()
            {
                RequestId = requestId,
                StatusCode = kvp.Key,
                StatusMessage = kvp.Value,
                Exception = exceptionHandlerPathFeature?.Error
            };

            return View(viewModel);
        }
    }
}
