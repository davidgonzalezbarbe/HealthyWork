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
    public class HeadQuartersController : ControllerBase
    {
        private readonly IService<HeadQuarters> hqService;

        public HeadQuartersController(IService<HeadQuarters> hqService)
        {
            this.hqService = hqService;
        }

        // GET: api/HeadQuarters
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var hqs = await hqService.ReadAll();

            if (hqs.HasErrors) return BadRequest(hqs.Results);
            else return Ok(hqs.Content);

        }

        // GET: api/HeadQuarters/5
        [HttpGet()]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            var hq = await hqService.Read(id);

            if (hq.HasErrors) return NotFound(hq.Results);
            else return Ok(hq.Content);
        }

        // GET: api/HeadQuarters/Search
        [HttpGet()]
        [Route("Search")]
        public async Task<IActionResult> Get([FromBody] HeadQuarters model, [FromQuery]bool restricted = true)
        {
            if (ModelState.IsValid)
            {
                var hqs = await hqService.ReadFiltered(model, restricted);

                if (hqs.HasErrors) return NotFound(hqs.Results);
                else return Ok(hqs.Content);
            }
            else return BadRequest();
        }

        // POST: api/HeadQuarters
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HeadQuarters model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != Guid.Empty) model.Id = Guid.Empty;
                var created = await hqService.Create(model);

                if (created.HasErrors) return BadRequest(created.Results);
                else return CreatedAtAction(nameof(Get), model);
            }
            else return BadRequest();
        }



        // PUT: api/HeadQuarters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] HeadQuarters model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != id || id == Guid.Empty) return NotFound();
                var updated = await hqService.Update(model);

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

            var hq = await hqService.Delete(id);

            if (hq.HasErrors) return BadRequest(hq.Results);
            else return Ok(hq.Content);
        }
    }
}