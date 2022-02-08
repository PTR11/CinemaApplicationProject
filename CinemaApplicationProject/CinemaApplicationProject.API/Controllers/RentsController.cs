using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaApplicationProject.Model;
using CinemaApplicationProject.Model.Database;
using Microsoft.AspNetCore.Cors;
using CinemaApplicationProject.Model.Services;
using CinemaApplicationProject.Model.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CinemaApplicationProject.API.Controllers
{
    
    [Route("api/[controller]")]
    
    [ApiController]
    public class RentsController : ControllerBase
    {
        private readonly IDatabaseService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public RentsController(IDatabaseService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;

        }

        // GET: api/Rents
        [EnableCors("_myAllowSpecificOrigins")]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<RentsGDTO>> GetRentsById(int id)
        {
            return _service.GetAllRentsByShowId(id).Select(m => (RentsGDTO)m).ToList();
        }

        //POST: api/Rents
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[EnableCors("_myAllowSpecificOrigins")]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Rents>> PostRents(RentFromGuestDTO rfg)
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
                if(rfg.Places.Count == 0)
                {
                    ModelState.AddModelError("", "You need choose places");
                    return BadRequest(ModelState);
                }
                if(!await _service.SaveRentsAsync(rfg))
                {
                    ModelState.AddModelError("", "Something went wrong with the process");
                    return BadRequest(ModelState);
                }
                Response.Cookies.Append("userId", user.Id.ToString(), new CookieOptions()
                {
                    HttpOnly = false,
                    Expires = DateTime.Now.AddMinutes(15),
                    SameSite = SameSiteMode.Lax
                });
                return Ok("Successfull reservation");
            }
        }

        //// GET: api/Rents/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Rents>> GetRents(int id)
        //{
        //    var rents = await _context.Rents.FindAsync(id);

        //    if (rents == null)
        //    {
        //        return NotFound();
        //    }

        //    return rents;
        //}

        //// PUT: api/Rents/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutRents(int id, Rents rents)
        //{
        //    if (id != rents.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(rents).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RentsExists(id))
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



        //// DELETE: api/Rents/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteRents(int id)
        //{
        //    var rents = await _context.Rents.FindAsync(id);
        //    if (rents == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Rents.Remove(rents);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool RentsExists(int id)
        //{
        //    return _context.Rents.Any(e => e.Id == id);
        //}
    }
}
