﻿using System;
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
        }

        // GET: api/Actors
        [HttpGet]
        public ActionResult<IEnumerable<Actors>> GetActors()
        {
            return _service.GetActors();
        }

        [HttpGet("movie/{id}")]
        public ActionResult<IEnumerable<ActorsDTO>> GetActorsByMovieId(int id)
        {
            return _service.GetActorsByMovie(id).Select(m => (ActorsDTO)m).ToList();
        }


        // GET: api/Actors/5
        [HttpGet("{id}")]
        public ActionResult<Actors> GetActors(int id)
        {
            var actors = _service.GetActorById(id);

            if (actors == null)
            {
                return NotFound();
            }

            return actors;
        }

        // PUT: api/Actors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutActors(int id, Actors actors)
        {
            if (id != actors.Id)
            {
                return BadRequest();
            }

            DatabaseManipulation.UpdateElement(actors);
            return NoContent();
        }

        // POST: api/Actors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Actors> PostActors(ActorsDTO actors)
        {
            DatabaseManipulation.context = _service.GetContext();
            Actors actor = (Actors)actors;

            Actors find = _service.GetActorsByName(actors.Name);
            if(find == null)
            {
                DatabaseManipulation.AddElement(actor);
            }
            _service.ConnectMovieWithActor(actors.MovieId, actor.Id);

            return CreatedAtAction("GetActors", new { id = actors.Id }, actors);
        }

        // DELETE: api/Actors/5
        [HttpDelete("{id}")]
        public IActionResult DeleteActors(int id)
        {
            var actors = _service.GetActorById(id);
            if (actors == null)
            {
                return NotFound();
            }

            DatabaseManipulation.DeleteElement(actors);
            //_context.Actors.Remove(actors);
            //await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
