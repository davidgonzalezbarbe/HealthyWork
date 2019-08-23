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
    public class UserController : ControllerBase
    {
        private readonly IService<User> userService;

        public UserController(IService<User> userService)
        {
            this.userService = userService;
        }
        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await userService.ReadAll();

            if (users.HasErrors) return BadRequest(users.Results);
            else return Ok(users.Content);
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            var user = await userService.Read(id);

            if (user.HasErrors) return NotFound(user.Results);
            else return Ok(user.Content);
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User value)
        {
            if (ModelState.IsValid)
            {
                if (value.Id != Guid.Empty) value.Id = Guid.Empty;

                var result = await userService.Create(value);

                if (result.HasErrors) return BadRequest(result.Results);
                else return CreatedAtAction(nameof(Get), value.Id);
            }
            else return BadRequest();
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] User value)
        {
            if (ModelState.IsValid)
            {
                if (value.Id != id || id == Guid.Empty) return NotFound();
                var updated = await userService.Update(value);

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

            var value = await userService.Delete(id);

            if (value.HasErrors) return BadRequest(value.Results);
            else return Ok(value.Content);
        }
    }
}
