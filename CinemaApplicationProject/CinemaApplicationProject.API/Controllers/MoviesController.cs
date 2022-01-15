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
    public class MoviesController : ControllerBase
    {
        private readonly IDatabaseService _service;

        public MoviesController(IDatabaseService service)
        {
            _service = service;
        }

        // GET: api/Movies
        [HttpGet]
        public ActionResult<IEnumerable<Movies>> GetMovies()
        {
            return _service.GetMovies();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public ActionResult<Movies> GetMovies(int id)
        {
            var movies = _service.GetMovieById(id);

            if (movies == null)
            {
                return NotFound();
            }

            return movies;
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
