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
    public class ActorsController : ControllerBase
    {
        private readonly IDatabaseService _service;

        public ActorsController(IDatabaseService service)
        {
           _service = service;
           DatabaseManipulation.context = _service.GetContext();
        }

        // GET: api/Actors
        [HttpGet]
        public ActionResult<IEnumerable<ActorsDTO>> GetActors()
        {
            return _service.GetActors().Select(m => (ActorsDTO)m).ToList();
        }


        // GET: api/Actors/5
        [HttpGet("{id}")]
        public ActionResult<ActorsDTO> GetActor(int id)
        {
            var actors = _service.GetActorById(id);

            if (actors == null)
            {
                return NotFound();
            }

            return (ActorsDTO)actors;
        }

        // POST: api/Actors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<ActorsDTO> PostActor(ActorsDTO actors)
        {
            
            Actors act = (Actors)actors;

            Actors find = _service.GetActorsByName(actors.Name);
            if(find == null)
            {
                var entity = DatabaseManipulation.AddElement(act);
                if (entity == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                act = entity;
            }
            _service.ConnectMovieWithActor(actors.MovieId, act.Id);
            return CreatedAtAction(nameof(GetActor), new { id = act.Id }, (ActorsDTO)act);
        }



        // DELETE: api/Actors/5
        [HttpDelete("{movieId}/{id}")]
        public IActionResult DeleteActor(int movieId,int id)
        {
            var actors = _service.GetActorById(id);
            if (actors == null)
            {
                return NotFound();
            }

            var movie = _service.GetMovieById(movieId);
            if(movie == null)
            {
                return NotFound();
            }

            _service.DeleteActorFromMovie(movieId, id);
            

            return Ok();
        }
    }
}
