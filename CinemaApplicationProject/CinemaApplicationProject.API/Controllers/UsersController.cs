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
using System.Linq;
using System.Collections.Generic;

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
            DatabaseManipulation.context = _service.GetContext();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeesDTO>>> GetEmployees()
        {
            var tmpList = await _service.GetEmployees();
            return tmpList.Select(m => (EmployeesDTO)m).ToList();
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

                    Response.Cookies.Append("userId", user.Id.ToString(), new CookieOptions()
                    {
                        HttpOnly = false,
                        //Expires = HttpContext.Current.Session.Timout,
                        SameSite = SameSiteMode.Lax
                    });

                    return Ok(tmp);
                    //return RedirectService.RedirectMethod("Successfully logged in", HttpStatusCode.Redirect, new Uri("http://localhost:8080/"));
                }
                ModelState.AddModelError("loginError", "Sikertelen bejelentkezés");
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


        [HttpPost]
        public async Task<ActionResult<EmployeesDTO>> PostUser(EmployeesDTO newUser)
        {
            var user = (Employees)newUser;
            var stats = newUser.Stats;

            var result = await _userManager.CreateAsync(user, newUser.Password);
            if (result.Succeeded)
            {
                foreach(var stat in stats)
                {
                    if (!await _service.ConnectUserWithRole(user.Id, stat.Id))
                    {
                        return BadRequest("Something went wrong");
                    }
                }
                return (EmployeesDTO)await _service.GetEmployeeById(user.Id);
            }
            ModelState.AddModelError("", "Sikertelen regisztráció");
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, EmployeesDTO employees)
        {
            if (id != employees.Id)
            {
                return BadRequest();
            }
            if(employees.Stats == null)
            {
                employees.Stats = new List<StatsDTO>();
            }
            var asd = (Employees)employees;
            var user = await _service.GetEmployeeById(asd.Id);

            user.Name = asd.Name;
            user.UserName = asd.UserName;
            user.Email = asd.Email;
            user.Address = asd.Address;
            user.Birthday = asd.Birthday;

            var result = await _userManager.UpdateAsync(user);
            if (employees.Password != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var pwResult = await _userManager.ResetPasswordAsync(user, token, employees.Password);
                if (!pwResult.Succeeded)
                {
                    StatusCode(StatusCodes.Status500InternalServerError);
                }
            }

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var employee = await _service.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if(user.Id == id)
            {
                return BadRequest();
            }
            DatabaseManipulation.DeleteElement(employee);

            return NoContent();
        }
        
    }
}
