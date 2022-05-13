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
            DatabaseManipulation.context = _service.GetContext();
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

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet("statistics")]
        public ActionResult<IEnumerable<MoviesDTO>> GetStatistics()
        {
            return _service.GetStatisticsForMovies();
        }


        // GET: api/Movies/5
        [HttpGet("{id}")]
        [EnableCors("_myAllowSpecificOrigins")]
        public ActionResult<MoviesDTO> GetMovie(int id)
        {
            var movie = _service.GetMovieById(id);

            if (movie == null)
            {
                return NotFound();
            }

            return (MoviesDTO)movie;
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
        public async Task<IActionResult> PutMovie(int id, MoviesDTO movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }
            var tmp = _service.GetMovieById(movie.Id);
            tmp.Title = movie.Title;
            tmp.Description = movie.Description;
            tmp.Image = movie.Image;
            tmp.Length = movie.Length;
            tmp.Director = movie.Director;
            if (DatabaseManipulation.UpdateElementAsync(tmp)) 
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<MoviesDTO> PostMovie(MoviesDTO movies)
        {
            var tmp = new MoviesDTO
            {
                Id = movies.Id,
                Title = movies.Title,
                Actors = new List<ActorsDTO>(),
                Categories = new List<CategoriesDTO>(),
                Length = movies.Length,
                Description = movies.Description,
                Trailer = movies.Trailer
            };




            var movie = DatabaseManipulation.AddElement((Movies)tmp);
            if (movie == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            foreach(var actor in tmp.Actors)
            {
                if(actor.Id == 0)
                {
                    var act = DatabaseManipulation.AddElement((Actors)actor);
                    if(act == null)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }
                    actor.Id = act.Id;
                }
                _service.ConnectMovieWithActor(movie.Id, actor.Id);
            }

            foreach (var category in tmp.Categories)
            {
                if (category.Id == 0)
                {
                    var cat = DatabaseManipulation.AddElement((Categories)category);
                    if(cat == null)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }
                    category.Id = cat.Id;
                }
                _service.ConnectMovieWithCategory(movie.Id, category.Id);
            }

            
            
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, (MoviesDTO)movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            var movies = _service.GetMovieById(id);
            if (movies == null)
            {
                return NotFound();
            }

            DatabaseManipulation.DeleteElement(movies);

            return Ok();
        }
    }
}
