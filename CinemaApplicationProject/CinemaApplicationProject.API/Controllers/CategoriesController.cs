using CinemaApplicationProject.Model.Database;
using CinemaApplicationProject.Model.DTOs;
using CinemaApplicationProject.Model.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CinemaApplicationProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IDatabaseService _service;

        public CategoriesController(IDatabaseService service)
        {
            _service = service;
            DatabaseManipulation.context = _service.GetContext();
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet("all")]
        public ActionResult<IEnumerable<CategoriesDTO>> GetCategories()
        {
            return _service.GetCategories().Select(list => (CategoriesDTO)list).ToList();
        }

        [HttpGet]
        public ActionResult<CategoriesDTO> GetCategoryById(int id)
        {
           return (CategoriesDTO)_service.GetCategoryById(id);
        }

        [HttpPost]
        public ActionResult<CategoriesDTO> PostActors(CategoriesDTO cat)
        {

            Categories category = (Categories)cat;

            Categories find = _service.GetCategoryByName(category.Category);
            if (find == null)
            {
                var entity = DatabaseManipulation.AddElement(category);
                if (entity == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                category = entity;
            }
            _service.ConnectMovieWithCategory(cat.MovieId, category.Id);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, (CategoriesDTO)category);
        }
        [HttpDelete("{movieId}/{id}")]
        public IActionResult DeleteCategory(int movieId, int id)
        {
            var actors = _service.GetCategoryById(id);
            if (actors == null)
            {
                return NotFound();
            }

            var movie = _service.GetMovieById(movieId);
            if (movie == null)
            {
                return NotFound();
            }

            _service.DeleteCategoryFromMovie(movieId, id);


            return NoContent();
        }


    }
}
