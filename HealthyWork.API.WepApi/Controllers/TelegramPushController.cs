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
    public class TelegramPushController : ControllerBase
    {
        private readonly IService<TelegramPush> telegramPushService;

        public TelegramPushController(IService<TelegramPush> telegramPushService)
        {
            this.telegramPushService = telegramPushService;
        }

        // GET: api/TelegramPush
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var telegramPushs = await telegramPushService.ReadAll();

            if (telegramPushs.HasErrors) return BadRequest(telegramPushs.Results);
            else return Ok(telegramPushs.Content);

        }

        // GET: api/TelegramPush/5
        [HttpGet()]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            var telegramPush = await telegramPushService.Read(id);

            if (telegramPush.HasErrors) return NotFound(telegramPush.Results);
            else return Ok(telegramPush.Content);
        }

        // GET: api/TelegramPush/Search
        [HttpGet()]
        [Route("Search")]
        public async Task<IActionResult> Get([FromBody] TelegramPush model, [FromQuery]bool restricted = true)
        {
            if (ModelState.IsValid)
            {
                var telegramPushs = await telegramPushService.ReadFiltered(model, restricted);

                if (telegramPushs.HasErrors) return NotFound(telegramPushs.Results);
                else return Ok(telegramPushs.Content);
            }
            else return BadRequest();
        }

        // POST: api/TelegramPush
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TelegramPush model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != Guid.Empty) model.Id = Guid.Empty;
                var created = await telegramPushService.Create(model);

                if (created.HasErrors) return BadRequest(created.Results);
                else return CreatedAtAction(nameof(Get), model);
            }
            else return BadRequest();
        }



        // PUT: api/TelegramPush/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] TelegramPush model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != id || id == Guid.Empty) return NotFound();
                var updated = await telegramPushService.Update(model);

                if (updated.HasErrors) return BadRequest(updated.Results);
                else return Ok(updated.Content);
            }
            else return BadRequest();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            var telegramPush = await telegramPushService.Delete(id);

            if (telegramPush.HasErrors) return BadRequest(telegramPush.Results);
            else return Ok(telegramPush.Content);
        }
    }
}