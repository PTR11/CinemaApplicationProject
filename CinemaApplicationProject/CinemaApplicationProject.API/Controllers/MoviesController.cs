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

namespace CinemaApplicationProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IDatabaseService _service;

        public MoviesController(IDatabaseService service)
        {
            _service = service;
        }

        // GET: api/Movies
        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet]
        public ActionResult<IEnumerable<MoviesDTO>> GetMovies()
        {
            return _service.GetMovies().Select(list => (MoviesDTO)list).ToList();
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet("today")]
        public ActionResult<IEnumerable<MoviesDTO>> GetTodaysMovies()
        {
            return _service.GetTodaysMovies().Select(list => (MoviesDTO)list).ToList();
        }


        // GET: api/Movies/5
        [HttpGet("{id}")]
        [EnableCors("_myAllowSpecificOrigins")]
        public ActionResult<MoviesDTO> GetMovies(int id)
        {
            var movie = (MoviesDTO)_service.GetMovieById(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet("title/{title}")]
        public ActionResult<IEnumerable<MoviesDTO>> GetMoviesByTitlePart(string title)
        {
            return _service.GetMoviesByNamePart(title).Select(m => (MoviesDTO)m).ToList();
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet("category/{category}")]
        public ActionResult<IEnumerable<MoviesDTO>> GetMoviesByCategory(string category)
        {
            return _service.GetMoviesByCategory(category).Select(m => (MoviesDTO)m).ToList();
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutMovies(int id, Movies movies)
        {
            if (id != movies.Id)
            {
                return BadRequest();
            }

            DatabaseManipulation.UpdateElement(movies);

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Movies> PostMovies(Movies movies)
        {
            DatabaseManipulation.AddElement(movies);

            return CreatedAtAction("GetMovies", new { id = movies.Id }, movies);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public IActionResult DeleteMovies(int id)
        {
            var movies = _service.GetMovieById(id);
            if (movies == null)
            {
                return NotFound();
            }

            DatabaseManipulation.DeleteElement(movies);

            return NoContent();
        }
    }
}
