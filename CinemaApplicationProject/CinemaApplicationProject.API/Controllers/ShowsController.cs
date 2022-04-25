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
using Microsoft.AspNetCore.Cors;
using CinemaApplicationProject.Model.DTOs;
using System.Net.Http;
using System.Net;

namespace CinemaApplicationProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly IDatabaseService _service;

        public ShowsController(IDatabaseService service)
        {
            _service = service;
            DatabaseManipulation.context = _service.GetContext();
        }


        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet]
        public ActionResult<IEnumerable<ShowsDTO>> GetShows()
        {
            return _service.GetAllShows().Select(m => (ShowsDTO)m).ToList();
        }


        // GET: api/Shows
        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet("{date}")]
        public ActionResult<IEnumerable<MoviesDTO>> GetShowsByDate(String date)
        {
            //var respone = new HttpResponseMessage();
            //respone.Headers.Location = new Uri("http://google.com");
            //return respone;
            return _service.GetShowsByDate(date).Select(m => (MoviesDTO)m).ToList();
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet("show/{id}")]
        public ActionResult<ShowsDTO> GetShowById(int id)
        {
            return (ShowsDTO)_service.GetShowById(id);
        }

        // GET: api/Shows
        [EnableCors("_myAllowSpecificOrigins")]
        [HttpPost("availableDates")]
        public ActionResult<IEnumerable<DateTime>> GetAvailableDates()
        {
            return _service.GetAvailableDates();
        }

        // GET: api/Shows/today
        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet("today")]
        public ActionResult<IEnumerable<ShowsDTO>> GetTodaysShows()
        {
            return _service.GetTodaysShows().Select(m => (ShowsDTO)m).ToList();
        }

        

        // PUT: api/Shows/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutShows(int id, ShowsDTO shows)
        {
            if (id != shows.Id)
            {
                return BadRequest();
            }
            var tmp = _service.GetShowById(shows.Id);
            tmp.MovieId = shows.MovieId;
            tmp.RoomId = shows.RoomId;
            tmp.Date = shows.Date;
            if (DatabaseManipulation.UpdateElementAsync(tmp))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/Shows
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Shows> PostShows(ShowsDTO shows)
        {
            var s = (Shows)shows;
            s.IsActiveShow =  true;
            var show = DatabaseManipulation.AddElement(s);

            if (show == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                return CreatedAtAction(nameof(GetShowById), new { id = show.Id }, (ShowsDTO)show);
            }
        }

        // DELETE: api/Shows/5
        [HttpDelete("{id}")]
        public IActionResult DeleteShows(int id)
        {
            var shows = _service.GetShowById(id);
            if (shows == null)
            {
                return NotFound();
            }
            DatabaseManipulation.DeleteElement(shows);

            return Ok();
        }
    }
}
