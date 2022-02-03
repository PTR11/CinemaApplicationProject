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
using System.Net;

namespace CinemaApplicationProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [EnableCors("_myAllowSpecificOrigins")]
        [HttpPost("login")]
        public async Task<HttpResponseMessage> Login(GuestsDTO login)
            //A login ne redirecteljen, old meg úgy,hogy modelstateel működjön
        {
            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);
            if (result.Succeeded)
            {
                ModelState.AddModelError("", "Sikertelen bejelentkezés");
                return RedirectService.RedirectMethod("Successfully logged in", HttpStatusCode.Redirect, new Uri("http://localhost:8080/"));
            }
            return RedirectService.RedirectMethod("Something went wrong", HttpStatusCode.BadRequest);


        }


        [EnableCors("_myAllowSpecificOrigins")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(GuestsDTO newUser)
        {
            var user = new Guests { UserName = newUser.UserName,
                Name = newUser.Name,
                Email = newUser.Email,
                Address = newUser.Address,
                CreditCardNumber = newUser.CreditCardNumber
            };
            var result = await _userManager.CreateAsync(user, newUser.Password);
            if (result.Succeeded)
            {
                return Ok("request is incorrect");
                Ok("Str");
                //return RedirectService.RedirectMethod("Successfully created", HttpStatusCode.Redirect, new Uri("http://localhost:8080/"));
            }
            if (!ModelState.IsValid)
            {
                int A = 1;
                String c = A.ToString();
            }
            return BadRequest();
            //return RedirectService.RedirectMethod("Not able to create the requested user", HttpStatusCode.BadRequest);
        }
    }
}
