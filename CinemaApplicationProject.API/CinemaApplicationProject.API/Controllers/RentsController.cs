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
    public class RentsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public RentsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Rents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rents>>> GetRents()
        {
            return await _context.Rents.ToListAsync();
        }

        // GET: api/Rents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rents>> GetRents(int id)
        {
            var rents = await _context.Rents.FindAsync(id);

            if (rents == null)
            {
                return NotFound();
            }

            return rents;
        }

        // PUT: api/Rents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRents(int id, Rents rents)
        {
            if (id != rents.Id)
            {
                return BadRequest();
            }

            _context.Entry(rents).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentsExists(id))
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

        // POST: api/Rents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rents>> PostRents(Rents rents)
        {
            _context.Rents.Add(rents);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRents", new { id = rents.Id }, rents);
        }

        // DELETE: api/Rents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRents(int id)
        {
            var rents = await _context.Rents.FindAsync(id);
            if (rents == null)
            {
                return NotFound();
            }

            _context.Rents.Remove(rents);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RentsExists(int id)
        {
            return _context.Rents.Any(e => e.Id == id);
        }
    }
}
