using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaApplicationProject.Model;
using CinemaApplicationProject.Model.Database;

namespace CinemaApplicationProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesStatisticsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public MoviesStatisticsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/MoviesStatistics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MoviesStatistics>>> GetMoviesStatistics()
        {
            return await _context.MoviesStatistics.ToListAsync();
        }

        // GET: api/MoviesStatistics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MoviesStatistics>> GetMoviesStatistics(int id)
        {
            var moviesStatistics = await _context.MoviesStatistics.FindAsync(id);

            if (moviesStatistics == null)
            {
                return NotFound();
            }

            return moviesStatistics;
        }

        // PUT: api/MoviesStatistics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoviesStatistics(int id, MoviesStatistics moviesStatistics)
        {
            if (id != moviesStatistics.Id)
            {
                return BadRequest();
            }

            _context.Entry(moviesStatistics).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoviesStatisticsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MoviesStatistics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MoviesStatistics>> PostMoviesStatistics(MoviesStatistics moviesStatistics)
        {
            _context.MoviesStatistics.Add(moviesStatistics);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMoviesStatistics", new { id = moviesStatistics.Id }, moviesStatistics);
        }

        // DELETE: api/MoviesStatistics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoviesStatistics(int id)
        {
            var moviesStatistics = await _context.MoviesStatistics.FindAsync(id);
            if (moviesStatistics == null)
            {
                return NotFound();
            }

            _context.MoviesStatistics.Remove(moviesStatistics);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MoviesStatisticsExists(int id)
        {
            return _context.MoviesStatistics.Any(e => e.Id == id);
        }
    }
}
