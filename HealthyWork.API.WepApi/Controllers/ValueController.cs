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
    public class ValueController : ControllerBase
    {
        private readonly IService<Value> valueService;

        public ValueController(IService<Value> valueService)
        {
            this.valueService = valueService;
        }

        // GET: api/Value
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> Get()
        {
            var values = await valueService.ReadAll();

            if (values.HasErrors) return BadRequest(values.Results);
            else return Ok(values.Content);

        }

        // GET: api/Value/5
        [HttpGet()]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            var value = await valueService.Read(id);

            if (value.HasErrors) return NotFound(value.Results);
            else return Ok(value.Content);
        }

        // GET: api/Value/Search
        [HttpGet()]
        [Route("Search")]
        public async Task<IActionResult> Get([FromBody] Value model, [FromQuery]bool restricted = true)
        {
            if (ModelState.IsValid)
            {
                var values = await valueService.ReadFiltered(model, restricted);

                if (values.HasErrors) return NotFound(values.Results);
                else return Ok(values.Content);
            }
            else return BadRequest();
        }

        // POST: api/Value
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Value model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != Guid.Empty) model.Id = Guid.Empty;
                var created = await valueService.Create(model);

                if (created.HasErrors) return BadRequest(created.Results);
                else return CreatedAtAction(nameof(Get), model);
            }
            else return BadRequest();
        }

       

        // PUT: api/Value/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Value model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != id || id == Guid.Empty) return NotFound();
                var updated = await valueService.Update(model);

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

            var value = await valueService.Delete(id);

            if (value.HasErrors) return BadRequest(value.Results);
            else return Ok(value.Content);
        }
    }
}
