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
using CinemaApplicationProject.Model.DTOs;

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
            DatabaseManipulation.context = _service.GetContext();
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
        public IActionResult PutRooms(int id, RoomsDTO rooms)
        {
            var fasz = _service.GetRoomById(id);
            fasz.Name = rooms.Name;
            fasz.Width = rooms.Width;
            fasz.Heigth = rooms.Heigth;

            if (id != fasz.Id)
            {
                return BadRequest();
            }
            if (DatabaseManipulation.UpdateElementAsync((Rooms)fasz))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<RoomsDTO> PostRooms(RoomsDTO rooms)
        {
            var room = DatabaseManipulation.AddElement((Rooms)rooms);

            if (room == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                return CreatedAtAction(nameof(GetRooms), new { id = room.Id }, (RoomsDTO)room);
            }
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
