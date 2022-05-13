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

        [HttpGet("sell/{id}")]
        public ActionResult<IEnumerable<RentsDTO>> GetRentsForSellById(int id)
        {
            return _service.GetAllRentsByShowId(id).Select(m => (RentsDTO)m).ToList();
        }

        [HttpGet("sellUsers/{id}")]
        public ActionResult<IEnumerable<GuestVDTO>> GetRentUsersForSellById(int id)
        {

            return _service.GetAllRentUserByShowId(id).Select(m => m != null? (GuestVDTO)m : new GuestVDTO()).ToList();
        }

        //POST: api/Rents
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [EnableCors("_myAllowSpecificOrigins")]
        [HttpPost]
        public async Task<ActionResult<Rents>> PostRent(RentFromGuestDTO rent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong");
            }
            else
            {
                ApplicationUser user = null;
                if (User.Identity.IsAuthenticated)
                {
                    user = await _userManager.FindByNameAsync(User.Identity.Name);
                }
                else if (rent.UserId != 0 && user == null)
                {
                    user = await _userManager.FindByIdAsync(rent.UserId + "");
                }
                else
                {
                    ModelState.AddModelError("errors", "You need to login the reserve places!");
                    return BadRequest(ModelState); 
                }
                if(rent.Places.Count == 0)
                {
                    ModelState.AddModelError("error", "You need choose places");
                    return BadRequest(ModelState);
                }
                if(!await _service.SaveRents(rent))
                {
                    ModelState.AddModelError("error", "Something went wrong with the process");
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
    }
}
