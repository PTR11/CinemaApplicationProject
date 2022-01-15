using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaApplicationProject.Model;
using CinemaApplicationProject.Model.Database;
using CinemaApplicationProject.Model.Services;

namespace CinemaApplicationProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        
        private readonly IDatabaseService _service;

        public RoomsController(IDatabaseService service)
        {
            _service = service;
        }

        // GET: api/Rooms
        [HttpGet]
        public ActionResult<IEnumerable<Rooms>> GetRooms()
        {
            return _service.GetAllRooms();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public ActionResult<Rooms> GetRooms(int id)
        {
            var rooms = _service.GetRoomById(id);

            if (rooms == null)
            {
                return NotFound();
            }

            return rooms;
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutRooms(int id, Rooms rooms)
        {
            if (id != rooms.Id)
            {
                return BadRequest();
            }

            DatabaseManipulation.UpdateElement(rooms);

            return NoContent();
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Rooms> PostRooms(Rooms rooms)
        {
            DatabaseManipulation.AddElement(rooms);

            return CreatedAtAction("GetRooms", new { id = rooms.Id }, rooms);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRooms(int id)
        {
            var rooms = _service.GetRoomById(id);
            if (rooms == null)
            {
                return NotFound();
            }

            DatabaseManipulation.DeleteElement(rooms);

            return NoContent();
        }
    }
}
