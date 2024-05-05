using ClassLibrary.Mvc.Extensions;
using ClassLibrary.Mvc.Http;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClassLibrary.Mvc.Exceptions.Handlers
{
    public sealed class NotFoundExceptionHandler(ILogger<NotFoundExceptionHandler> logger) : IExceptionHandler
    {
        private readonly ILogger<NotFoundExceptionHandler> _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not NotFoundException notFoundException)
                return false;

            _logger.LogWarning(exception, "{@logMessageHeader} Exception Occurred: {@message}", httpContext.Request.LogMessageHeader(), exception.Message);

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = notFoundException.Message.Trim()
            };

            if (string.IsNullOrEmpty(problemDetails.Detail))
            {
                KeyValuePair<int, string> kvp = StatusCodeDefinitions.GetCodeDefinition(problemDetails.Status.Value);
                problemDetails.Detail = kvp.Value.Trim();
            }

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            if (httpContext.Request.IsAjaxRequest())
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
            else
            {
                httpContext.Response.Redirect($"/Error/{problemDetails.Status.Value}");
                await httpContext.Response.StartAsync(cancellationToken).ConfigureAwait(false);
            }

            return true;
        }
    }
}
