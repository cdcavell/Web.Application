using ClassLibrary.Mvc.Extensions;
using ClassLibrary.Mvc.Http;
using ClassLibrary.Mvc.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace ClassLibrary.Mvc.Controllers
{
    public abstract class WebBaseController<T>(ILogger<T> logger) : Controller where T : WebBaseController<T>
    {
        protected readonly ILogger _logger = logger;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            _logger.LogDebug("{@logMessageHeader} OnActionExecuting({@context})", Request.LogMessageHeader(), nameof(context));
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            _logger.LogDebug("{@logMessageHeader} OnActionExecuted({@context})", Request.LogMessageHeader(), nameof(context));
        }
    }
}
