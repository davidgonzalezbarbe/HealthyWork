using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthyWork.API.Contracts.Models;
using HealthyWork.API.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthyWork.API.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramBotController : ControllerBase
    {
        private readonly ITelegramBotService botService;

        public TelegramBotController(ITelegramBotService botService)
        {
            this.botService = botService;
        }

        [HttpGet]
        [Route("start")]
        public IActionResult Start()
        {
            botService.Run();
            return Ok();
        }
        [HttpGet]
        [Route("stop")]
        public IActionResult Stop()
        {
            botService.Stop();
            return Ok();
        }

        [HttpPost]
        [Route("send")]
        public IActionResult Send([FromBody]Value value)
        {
            if (ModelState.IsValid)
            {
                botService.Send(value);
                return Ok();
            }
            else return BadRequest();
        }

    }
}