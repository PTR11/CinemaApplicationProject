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
    public class BuffetWarehousesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public BuffetWarehousesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/BuffetWarehouses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BuffetWarehouse>>> GetBuffetWarehouse()
        {
            return await _context.BuffetWarehouse.ToListAsync();
        }

        // GET: api/BuffetWarehouses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BuffetWarehouse>> GetBuffetWarehouse(int id)
        {
            var buffetWarehouse = await _context.BuffetWarehouse.FindAsync(id);

            if (buffetWarehouse == null)
            {
                return NotFound();
            }

            return buffetWarehouse;
        }

        // PUT: api/BuffetWarehouses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuffetWarehouse(int id, BuffetWarehouse buffetWarehouse)
        {
            if (id != buffetWarehouse.Id)
            {
                return BadRequest();
            }

            _context.Entry(buffetWarehouse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuffetWarehouseExists(id))
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

        // POST: api/BuffetWarehouses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BuffetWarehouse>> PostBuffetWarehouse(BuffetWarehouse buffetWarehouse)
        {
            _context.BuffetWarehouse.Add(buffetWarehouse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuffetWarehouse", new { id = buffetWarehouse.Id }, buffetWarehouse);
        }

        // DELETE: api/BuffetWarehouses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuffetWarehouse(int id)
        {
            var buffetWarehouse = await _context.BuffetWarehouse.FindAsync(id);
            if (buffetWarehouse == null)
            {
                return NotFound();
            }

            _context.BuffetWarehouse.Remove(buffetWarehouse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuffetWarehouseExists(int id)
        {
            return _context.BuffetWarehouse.Any(e => e.Id == id);
        }
    }
}
