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
    public class StatsAndPaysController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public StatsAndPaysController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/StatsAndPays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatsAndPays>>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        // GET: api/StatsAndPays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatsAndPays>> GetStatsAndPays(int id)
        {
            var statsAndPays = await _context.Roles.FindAsync(id);

            if (statsAndPays == null)
            {
                return NotFound();
            }

            return statsAndPays;
        }

        // PUT: api/StatsAndPays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatsAndPays(int id, StatsAndPays statsAndPays)
        {
            if (id != statsAndPays.Id)
            {
                return BadRequest();
            }

            _context.Entry(statsAndPays).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatsAndPaysExists(id))
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

        // POST: api/StatsAndPays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StatsAndPays>> PostStatsAndPays(StatsAndPays statsAndPays)
        {
            _context.Roles.Add(statsAndPays);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatsAndPays", new { id = statsAndPays.Id }, statsAndPays);
        }

        // DELETE: api/StatsAndPays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatsAndPays(int id)
        {
            var statsAndPays = await _context.Roles.FindAsync(id);
            if (statsAndPays == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(statsAndPays);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatsAndPaysExists(int id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}
