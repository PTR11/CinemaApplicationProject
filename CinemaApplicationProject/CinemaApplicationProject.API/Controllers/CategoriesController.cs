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
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet]
        public ActionResult<IEnumerable<CategoriesDTO>> GetMovies()
        {
            return _service.GetCategories().Select(list => (CategoriesDTO)list).ToList();
        }


        

    }
}
