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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CinemaApplicationProject.Model.DTOs;

namespace CinemaApplicationProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpinionsController : ControllerBase
    {
        private readonly IDatabaseService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        public OpinionsController(IDatabaseService service, UserManager<ApplicationUser> userManager)
        {
           _service = service;
            _userManager = userManager;
        }

        // GET: api/Opinions
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Opinions>> GetOpinions(int id)
        {

            return _service.GetAllOpinionsByMovie(id);
        }

        [HttpPost]
        public async Task<ActionResult<Opinions>> PostOpinions(OpinionsDTO rfg)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong");
            }
            else
            {
                ApplicationUser user;
                if (User.Identity.IsAuthenticated)
                {
                    user = await _userManager.FindByNameAsync(User.Identity.Name);
                }
                else
                {
                    ModelState.AddModelError("", "You need to login the reserve places!");
                    return BadRequest(ModelState);
                }
                
                if (!await _service.SaveOpinionAsync(rfg))
                {
                    ModelState.AddModelError("", "Something went wrong with the process");
                    return BadRequest(ModelState);
                }
                return Ok("Successfull reservation");
            }
        }

        //// GET: api/Opinions/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Opinions>> GetOpinions(int id)
        //{
        //    var opinions = await _context.Opinions.FindAsync(id);

        //    if (opinions == null)
        //    {
        //        return NotFound();
        //    }

        //    return opinions;
        //}

        //// PUT: api/Opinions/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutOpinions(int id, Opinions opinions)
        //{
        //    if (id != opinions.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(opinions).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!OpinionsExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Opinions
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Opinions>> PostOpinions(Opinions opinions)
        //{
        //    _context.Opinions.Add(opinions);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetOpinions", new { id = opinions.Id }, opinions);
        //}

        //// DELETE: api/Opinions/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteOpinions(int id)
        //{
        //    var opinions = await _context.Opinions.FindAsync(id);
        //    if (opinions == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Opinions.Remove(opinions);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool OpinionsExists(int id)
        //{
        //    return _context.Opinions.Any(e => e.Id == id);
        //}
    }
}
