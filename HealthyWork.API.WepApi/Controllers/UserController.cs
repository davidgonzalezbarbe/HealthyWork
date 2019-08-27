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
        private readonly IService<User> serviceUserType;
        private readonly IUserService userService;

        public UserController(IService<User> serviceUserType, IUserService userService)
        {
            this.serviceUserType = serviceUserType;
            this.userService = userService;
        }
        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await serviceUserType.ReadAll();

            if (users.HasErrors) return BadRequest(users.Results);
            else return Ok(users.Content);
        }

        // GET: api/User/5
        [HttpGet()]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            var user = await serviceUserType.Read(id);

            if (user.HasErrors) return NotFound(user.Results);
            else return Ok(user.Content);
        }
        
        // GET: api/User/Search
        [Route("search")]
        [HttpGet()]
        public async Task<IActionResult> Get([FromBody] User model, [FromQuery]bool restricted = true)
        {
            if (ModelState.IsValid)
            {
                
                var users = await serviceUserType.ReadFiltered(model, restricted);

                if (users.HasErrors) return NotFound(users.Results);
                else return Ok(users.Content);
            }
            else return BadRequest();
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User value)
        {
            if (ModelState.IsValid)
            {
                if (value.Id != Guid.Empty) value.Id = Guid.Empty;

                var result = await serviceUserType.Create(value);

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
                var updated = await serviceUserType.Update(value);

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

            var value = await serviceUserType.Delete(id);

            if (value.HasErrors) return BadRequest(value.Results);
            else return Ok(value.Content);
        }

        [HttpPost]
        [Route("sendregister")]
        public async Task<IActionResult> SendRegisterEmail([FromBody]User model)
        {
            if (ModelState.IsValid)
            {
                var message = userService.EncryptData(model);

                //TODO: mandar por email
                return Ok();
            }
            else return BadRequest();
        }

        [HttpGet]
        [Route("confirmregister")]
        public async Task<IActionResult> ConfirmRegister([FromQuery] string code)
        {
            var userData = userService.DecryptData(code);
            if (!userData.HasErrors)
            {
                var result = await serviceUserType.Create(userData.Content);
                if (result.HasErrors)  return  BadRequest(result.Results);
                else return CreatedAtAction(nameof(Get), result.Content);
            }
            else return BadRequest(userData.Results);
        }
    }
}
