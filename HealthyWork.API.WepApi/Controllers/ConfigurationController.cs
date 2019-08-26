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
    public class ConfigurationController : ControllerBase
    {
        private readonly IService<Configuration> configurationService;

        public ConfigurationController(IService<Configuration> configurationService)
        {
            this.configurationService = configurationService;
        }

        // GET: api/Configuration
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var configurations = await configurationService.ReadAll();

            if (configurations.HasErrors) return BadRequest(configurations.Results);
            else return Ok(configurations.Content);

        }

        // GET: api/Configuration/5
        [HttpGet()]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            var configuration = await configurationService.Read(id);

            if (configuration.HasErrors) return NotFound(configuration.Results);
            else return Ok(configuration.Content);
        }

        // GET: api/Configuration/Search
        [HttpGet()]
        [Route("Search")]
        public async Task<IActionResult> Get([FromBody] Configuration model, [FromQuery]bool restricted = true)
        {
            if (ModelState.IsValid)
            {
                var configurations = await configurationService.ReadFiltered(model, restricted);

                if (configurations.HasErrors) return NotFound(configurations.Results);
                else return Ok(configurations.Content);
            }
            else return BadRequest();
        }

        // POST: api/Configuration
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Configuration model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != Guid.Empty) model.Id = Guid.Empty;
                var created = await configurationService.Create(model);

                if (created.HasErrors) return BadRequest(created.Results);
                else return CreatedAtAction(nameof(Get), model);
            }
            else return BadRequest();
        }



        // PUT: api/Configuration/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Configuration model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != id || id == Guid.Empty) return NotFound();
                var updated = await configurationService.Update(model);

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

            var configuration = await configurationService.Delete(id);

            if (configuration.HasErrors) return BadRequest(configuration.Results);
            else return Ok(configuration.Content);
        }
    }
}