﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Models.Home;
using Web.Application.Services;

namespace Web.Application.Controllers
{
    public class HomeController : ApplicationBaseController<HomeController>
    {
        private readonly TimedHostedService _timedHostedService;

        public HomeController(
            ILogger<HomeController> logger, 
            TimedHostedService timedHostedService
        ) : base (logger)
        {
            _timedHostedService = timedHostedService;
        }

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

        [HttpGet("Home/InvalidModelStateException")]
        public IActionResult InvalidModelStateException([Bind(InputModel.BindProperties)] InputModel model)
        {
            if (!ModelState.IsValid)
                return InvalidModelState();

            return NoContent();
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

        [HttpGet("Home/StartHostedService")]
        public IActionResult StartHostedService()
        {
            _timedHostedService.StartAsync(new System.Threading.CancellationToken());
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Home/StopHostedService")]
        public IActionResult StopHostedService()
        {
            _timedHostedService.StopAsync(new System.Threading.CancellationToken());
            return RedirectToAction(nameof(Index));
        }
    }
}
