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
        public ActionResult<IEnumerable<RoomsDTO>> GetRooms()
        {
            return _service.GetAllRooms().Select(x => (RoomsDTO) x).ToList();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public ActionResult<RoomsDTO> GetRoom(int id)
        {
            var rooms = _service.GetRoomById(id);

            if (rooms == null)
            {
                return NotFound();
            }

            return (RoomsDTO)rooms;
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutRoom(int id, RoomsDTO room)
        {
            var tmp = _service.GetRoomById(id);
            tmp.Name = room.Name;
            tmp.Width = room.Width;
            tmp.Heigth = room.Heigth;

            if (id != tmp.Id)
            {
                return BadRequest();
            }
            if (DatabaseManipulation.UpdateElementAsync(tmp))
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
        public ActionResult<RoomsDTO> PostRoom(RoomsDTO rooms)
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
        public IActionResult DeleteRoom(int id)
        {
            var rooms = _service.GetRoomById(id);
            if (rooms == null)
            {
                return NotFound();
            }

            var delete =  DatabaseManipulation.DeleteElement(rooms);
            if (!delete)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
