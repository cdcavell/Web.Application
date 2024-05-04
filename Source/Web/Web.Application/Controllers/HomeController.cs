using ClassLibrary.Mvc.Exceptions;
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
            //throw new NotFoundException($"Not Found Exception Thrown");
            //throw new BadRequestException($"Bad Request Exception Thrown");
            //throw new System.Exception($"Exception Thrown");

            return View();
        }

        [HttpGet("Home/NotFoundException")]
        public IActionResult NotFoundException()
        {
            throw new NotFoundException($"Not Found Exception Thrown");
        }

        [HttpGet("Home/BadRequestException")]
        public IActionResult BadRequestException()
        {
            throw new BadRequestException($"Bad Request Exception Thrown");
        }

        [HttpGet("Home/SystemException")]
        public IActionResult SystemException()
        {
            throw new System.Exception($"System Exception Thrown");
        }
    }
}
