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
    public class BuffetSalesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public BuffetSalesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/BuffetSales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BuffetSale>>> GetBuffetSales()
        {
            return await _context.BuffetSales.ToListAsync();
        }

        // GET: api/BuffetSales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BuffetSale>> GetBuffetSale(int id)
        {
            var buffetSale = await _context.BuffetSales.FindAsync(id);

            if (buffetSale == null)
            {
                return NotFound();
            }

            return buffetSale;
        }

        // PUT: api/BuffetSales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuffetSale(int id, BuffetSale buffetSale)
        {
            if (id != buffetSale.Id)
            {
                return BadRequest();
            }

            _context.Entry(buffetSale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuffetSaleExists(id))
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

        // POST: api/BuffetSales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BuffetSale>> PostBuffetSale(BuffetSale buffetSale)
        {
            _context.BuffetSales.Add(buffetSale);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuffetSale", new { id = buffetSale.Id }, buffetSale);
        }

        // DELETE: api/BuffetSales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuffetSale(int id)
        {
            var buffetSale = await _context.BuffetSales.FindAsync(id);
            if (buffetSale == null)
            {
                return NotFound();
            }

            _context.BuffetSales.Remove(buffetSale);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuffetSaleExists(int id)
        {
            return _context.BuffetSales.Any(e => e.Id == id);
        }
    }
}
