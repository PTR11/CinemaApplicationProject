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
    public class StatsAndPaysController : ControllerBase
    {
        private readonly IDatabaseService _service;

        public StatsAndPaysController(IDatabaseService service)
        {
            _service = service;
        }

        // GET: api/StatsAndPays
        [HttpGet]
        public ActionResult<IEnumerable<StatsAndPays>> GetRoles()
        {
            return _service.GetStats();
        }

        // GET: api/StatsAndPays/5
        [HttpGet("{id}")]
        public ActionResult<StatsAndPays> GetStatsAndPays(int id)
        {
            var statsAndPays = _service.GetStatById(id);

            if (statsAndPays == null)
            {
                return NotFound();
            }

            return statsAndPays;
        }

        // PUT: api/StatsAndPays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutStatsAndPays(int id, StatsAndPays statsAndPays)
        {
            if (id != statsAndPays.Id)
            {
                return BadRequest();
            }

            DatabaseManipulation.UpdateElementAsync(statsAndPays);

            return NoContent();
        }

        // POST: api/StatsAndPays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<StatsAndPays> PostStatsAndPays(StatsAndPays statsAndPays)
        {
            DatabaseManipulation.AddElement(statsAndPays);

            return CreatedAtAction("GetStatsAndPays", new { id = statsAndPays.Id }, statsAndPays);
        }

        // DELETE: api/StatsAndPays/5
        [HttpDelete("{id}")]
        public IActionResult DeleteStatsAndPays(int id)
        {
            var statsAndPays = _service.GetStatById(id);
            if (statsAndPays == null)
            {
                return NotFound();
            }

            DatabaseManipulation.DeleteElement(statsAndPays);

            return NoContent();
        }
    }
}
