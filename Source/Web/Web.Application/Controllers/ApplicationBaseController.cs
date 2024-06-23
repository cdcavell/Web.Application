using ClassLibrary.Mvc.Controllers;
using ClassLibrary.Mvc.Extensions;
using ClassLibrary.Mvc.Http;
using ClassLibrary.Mvc.Models;
using ClassLibrary.Mvc.Services.AppSettings;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using Web.Application.Models.AppSettings;

namespace Web.Application.Controllers
{
    public abstract class ApplicationBaseController<T>(
        ILogger<T> logger,
        IAppSettingsService appSettingsService
    ) : WebBaseController<ApplicationBaseController<T>>(logger) where T : ApplicationBaseController<T>
    {
        protected readonly AppSettings _appSettings = appSettingsService.ToObject<AppSettings>();
    }
}
