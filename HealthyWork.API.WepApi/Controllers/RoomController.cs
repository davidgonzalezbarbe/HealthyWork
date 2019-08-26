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
    public class RoomController : ControllerBase
    {
        private readonly IService<Room> roomService;

        public RoomController(IService<Room> roomService)
        {
            this.roomService = roomService;
        }

        // GET: api/Room
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rooms = await roomService.ReadAll();

            if (rooms.HasErrors) return BadRequest(rooms.Results);
            else return Ok(rooms.Content);

        }

        // GET: api/Room/5
        [HttpGet()]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            var room = await roomService.Read(id);

            if (room.HasErrors) return NotFound(room.Results);
            else return Ok(room.Content);
        }

        // GET: api/Room/Search
        [HttpGet()]
        [Route("Search")]
        public async Task<IActionResult> Get([FromBody] Room model, [FromQuery]bool restricted = true)
        {
            if (ModelState.IsValid)
            {
                var rooms = await roomService.ReadFiltered(model, restricted);

                if (rooms.HasErrors) return NotFound(rooms.Results);
                else return Ok(rooms.Content);
            }
            else return BadRequest();
        }

        // POST: api/Room
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Room model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != Guid.Empty) model.Id = Guid.Empty;
                var created = await roomService.Create(model);

                if (created.HasErrors) return BadRequest(created.Results);
                else return CreatedAtAction(nameof(Get), model);
            }
            else return BadRequest();
        }



        // PUT: api/Room/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Room model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != id || id == Guid.Empty) return NotFound();
                var updated = await roomService.Update(model);

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

            var room = await roomService.Delete(id);

            if (room.HasErrors) return BadRequest(room.Results);
            else return Ok(room.Content);
        }
    }
}