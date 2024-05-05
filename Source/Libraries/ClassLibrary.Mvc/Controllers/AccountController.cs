using ClassLibrary.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Application.Controllers
{
    public class AccountController(ILogger<AccountController> logger) : WebBaseController<AccountController>(logger)
    {
        [HttpGet("Account/Login")]
        public IActionResult Login()
        {
            return Unauthorized();
        }
    }
}
