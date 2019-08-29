using HealthyWork.API.Contracts.Models;
using HealthyWork.API.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HealthyWork.API.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IService<User> userService;
        private readonly IUserService loginService;

        public UserController(IService<User> userService, IUserService loginService)
        {
            this.userService = userService;
            this.loginService = loginService;
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
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            var user = await userService.Read(id);

            if (user.HasErrors) return NotFound(user.Results);
            else return Ok(user.Content);
        }

        // GET: api/User/Search
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetFiltered([FromBody] User model, [FromQuery]bool restricted = true)
        {
            if (ModelState.IsValid)
            {

                var users = await userService.ReadFiltered(model, restricted);

                if (users.HasErrors) return NotFound(users.Results);
                else return Ok(users.Content);
            }
            else return BadRequest();
        }

        // POST: api/User
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] User value)
        {
            if (ModelState.IsValid)
            {
                if (value.Id != Guid.Empty) value.Id = Guid.Empty;
                var checkIfExists = await userService.ReadFiltered(new User() { Email = value.Email }, false);
                if (!checkIfExists.HasErrors) return BadRequest("The user has already been created");
                var result = await userService.Create(value);

                if (result.HasErrors) return BadRequest(result.Results);
                else return CreatedAtAction(nameof(Get), value);
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

        [HttpPost]
        [Route("sendregister")]
        public async Task<IActionResult> SendRegisterEmail([FromBody]User model)
        {
            if (ModelState.IsValid)
            {
                model.IsActive = false;

                var result = await userService.Create(model);

                if (result.HasErrors) return BadRequest();
                else
                {
                    //TODO: mandar por email
                    return Ok(result.Content);
                }
            }
            else return BadRequest();
        }

        [HttpGet]
        [Route("register")]
        public async Task<IActionResult> ConfirmRegister([FromQuery] Guid id)
        {
            var userData = await userService.Read(id);

            if (!userData.HasErrors)
            {
                userData.Content.IsActive = true;
                var result = await userService.Update(userData.Content);
                if (!result.HasErrors) return Ok(userData.Content);
                else return BadRequest();
            }
            else return NotFound();
        }

        [HttpPost]
        [Route("login")]
        public  IActionResult Login([FromBody]User model)
        {

            if (ModelState.IsValid)
            {
                var userLogged = loginService.Login(model);
                if (userLogged.HasErrors) return BadRequest(userLogged.Results);
                else return Ok(userLogged.Content);
                
            }
            else return BadRequest();
        }
    }
}
