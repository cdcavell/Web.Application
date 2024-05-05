using ClassLibrary.Mvc.Extensions;
using ClassLibrary.Mvc.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

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

        protected IActionResult InvalidModelState()
        {
            string message = Tags.Strong("Invalid Model State");
            List<ModelErrorCollection> errors = ModelState.Values
                .Where(x => x.ValidationState.ToString().Trim().ToLower() == "invalid")
                .Select(x => x.Errors)
                .ToList();

            foreach (ModelErrorCollection errorCollection in errors)
                foreach (ModelError error in errorCollection)
                    message += Tags.LineBreak() + error.ErrorMessage;

            _logger.LogWarning("{@logMessageHeader} InvalidModelState() {@message}", Request.LogMessageHeader(), message);
            throw new ArgumentException(message);
        }
    }
}
