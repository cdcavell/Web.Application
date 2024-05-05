using ClassLibrary.Mvc.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Web.Application.Controllers
{
    public class AccountController(ILogger<AccountController> logger) : ApplicationBaseController<AccountController>(logger)
    {
        [HttpGet("Account/Login")]
        public IActionResult Login()
        {
            throw new UnauthorizedAccessException($"Unauthorized Access Exception Thrown");
        }
    }
}
