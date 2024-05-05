using ClassLibrary.Mvc.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Application.Controllers
{
    public class HomeController(ILogger<HomeController> logger) : ApplicationBaseController<HomeController>(logger)
    {
        [HttpGet("/")]
        [HttpGet("Home")]
        [HttpGet("Home/Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Home/BadRequestException")]
        public IActionResult BadRequestException()
        {
            return BadRequest();
        }

        [Authorize]
        [HttpGet("Home/UnauthorizedAccessException")]
        public IActionResult UnauthorizedAccessException()
        {
            return NoContent();
        }

        [HttpGet("Home/SystemException")]
        public IActionResult SystemException()
        {
            throw new System.Exception($"System Exception Thrown");
        }
    }
}
