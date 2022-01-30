using CinemaApplicationProject.Model.Database;
using CinemaApplicationProject.Model.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

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
        [HttpPost("register/")]
        public async Task<RedirectResult> Register(GuestsDTO newUser)
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
                var respone = new HttpResponseMessage();
                //respone.StatusCode = System.Net.HttpStatusCode.Redirect;
                //respone.Headers.Location = new Uri("http://google.com");
               // return respone;
                return Redirect("http://google.com");
            }
            return Redirect("http://facebook.com");
            
        }
    }
}
