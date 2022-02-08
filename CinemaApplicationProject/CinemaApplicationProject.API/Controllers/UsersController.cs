using CinemaApplicationProject.Model.Database;
using CinemaApplicationProject.Model.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using CinemaApplicationProject.Model.Services;

namespace CinemaApplicationProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IDatabaseService _service;

        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IDatabaseService service)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _service = service;
        }


        [EnableCors("_myAllowSpecificOrigins")]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginGuestDTO login)
        {
            //A login ne redirecteljen, old meg úgy,hogy modelstateel működjön
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);
                
                if (result.Succeeded)
                {
                    var user = _service.GetGuestByUserName(login.UserName);
                    var tmp = (GuestsDTO)user;
                    tmp.RedirectUrl = "http://localhost:8080";

                    Response.Cookies.Append("userId", user.Id.ToString(), new CookieOptions()
                    {
                        HttpOnly = false,
                        //Expires = HttpContext.Current.Session.Timout,
                        SameSite = SameSiteMode.Lax
                    }) ;
                    
                    return Ok(tmp);
                    //return RedirectService.RedirectMethod("Successfully logged in", HttpStatusCode.Redirect, new Uri("http://localhost:8080/"));
                }
                ModelState.AddModelError("", "Sikertelen bejelentkezés");
            }
            return BadRequest(ModelState);

            //return RedirectService.RedirectMethod("Something went wrong", HttpStatusCode.BadRequest);


        }


        [EnableCors("_myAllowSpecificOrigins")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(GuestsDTO newUser)
        {
            if (ModelState.IsValid)
            {
                var user = new Guests
                {
                    UserName = newUser.UserName,
                    Name = newUser.Name,
                    Email = newUser.Email,
                    Address = newUser.Address,
                    CreditCardNumber = newUser.CreditCardNumber
                };

                var result = await _userManager.CreateAsync(user, newUser.Password);
                if (result.Succeeded)
                {
                    return Ok(new Uri("http://localhost:8080/"));
                }
                ModelState.AddModelError("", "Sikertelen regisztráció");
            }
            return BadRequest(ModelState);
        }
    }
}
