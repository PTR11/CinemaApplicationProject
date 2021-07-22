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
    public class OpinionsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public OpinionsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Opinions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Opinions>>> GetOpinions()
        {
            return await _context.Opinions.ToListAsync();
        }

        // GET: api/Opinions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Opinions>> GetOpinions(int id)
        {
            var opinions = await _context.Opinions.FindAsync(id);

            if (opinions == null)
            {
                return NotFound();
            }

            return opinions;
        }

        // PUT: api/Opinions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOpinions(int id, Opinions opinions)
        {
            if (id != opinions.Id)
            {
                return BadRequest();
            }

            _context.Entry(opinions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OpinionsExists(id))
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

        // POST: api/Opinions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Opinions>> PostOpinions(Opinions opinions)
        {
            _context.Opinions.Add(opinions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOpinions", new { id = opinions.Id }, opinions);
        }

        // DELETE: api/Opinions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOpinions(int id)
        {
            var opinions = await _context.Opinions.FindAsync(id);
            if (opinions == null)
            {
                return NotFound();
            }

            _context.Opinions.Remove(opinions);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OpinionsExists(int id)
        {
            return _context.Opinions.Any(e => e.Id == id);
        }
    }
}
