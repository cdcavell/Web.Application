using ClassLibrary.Mvc.Controllers;
using ClassLibrary.Mvc.Extensions;
using ClassLibrary.Mvc.Http;
using ClassLibrary.Mvc.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Web.Application.Controllers
{
    public abstract class ApplicationBaseController<T>(ILogger<T> logger) : WebBaseController<ApplicationBaseController<T>>(logger) where T : ApplicationBaseController<T>
    {
    }
}
