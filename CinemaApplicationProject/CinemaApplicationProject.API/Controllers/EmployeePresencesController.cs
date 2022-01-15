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
    public class EmployeePresencesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public EmployeePresencesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/EmployeePresences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeePresence>>> GetEmployeePresence()
        {
            return await _context.EmployeePresence.ToListAsync();
        }

        // GET: api/EmployeePresences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeePresence>> GetEmployeePresence(int id)
        {
            var employeePresence = await _context.EmployeePresence.FindAsync(id);

            if (employeePresence == null)
            {
                return NotFound();
            }

            return employeePresence;
        }

        // PUT: api/EmployeePresences/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeePresence(int id, EmployeePresence employeePresence)
        {
            if (id != employeePresence.Id)
            {
                return BadRequest();
            }

            _context.Entry(employeePresence).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeePresenceExists(id))
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

        // POST: api/EmployeePresences
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeePresence>> PostEmployeePresence(EmployeePresence employeePresence)
        {
            _context.EmployeePresence.Add(employeePresence);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeePresence", new { id = employeePresence.Id }, employeePresence);
        }

        // DELETE: api/EmployeePresences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeePresence(int id)
        {
            var employeePresence = await _context.EmployeePresence.FindAsync(id);
            if (employeePresence == null)
            {
                return NotFound();
            }

            _context.EmployeePresence.Remove(employeePresence);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeePresenceExists(int id)
        {
            return _context.EmployeePresence.Any(e => e.Id == id);
        }
    }
}
