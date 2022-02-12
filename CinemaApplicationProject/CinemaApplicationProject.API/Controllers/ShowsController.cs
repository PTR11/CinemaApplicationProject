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
        }

        // GET: api/Shows
        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet("{date}")]
        public ActionResult<IEnumerable<MoviesDTO>> GetShows(String date)
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
        public ActionResult<IEnumerable<Shows>> GetTodaysShows()
        {
            return _service.GetTodaysShows();
        }

        // GET: api/Shows/5
        [HttpGet("movie/{id}")]
        public ActionResult<IEnumerable<Shows>> GetShowsByMovieId(int id)
        {
            var shows = _service.GetAllShowsByMovieId(id);

            if (shows == null)
            {
                return NotFound();
            }

            return shows;
        }

        // PUT: api/Shows/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutShows(int id, Shows shows)
        {
            if (id != shows.Id)
            {
                return BadRequest();
            }

            DatabaseManipulation.UpdateElement(shows);


            return NoContent();
        }

        // POST: api/Shows
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Shows> PostShows(Shows shows)
        {
            DatabaseManipulation.AddElement(shows);

            return CreatedAtAction("GetShows", new { id = shows.Id }, shows);
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

            return NoContent();
        }
    }
}
