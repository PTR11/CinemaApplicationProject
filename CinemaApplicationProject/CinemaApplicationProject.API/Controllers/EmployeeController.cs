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
    public class EmployeeController : ControllerBase
    {
        private SignInManager<ApplicationUser> _signInManager;
        private readonly IDatabaseService _service;

        public EmployeeController(SignInManager<ApplicationUser> signInManager, IDatabaseService service)
        {
            _signInManager = signInManager;
            _service = service;
        }

        //api/Employee/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);
            if (result.Succeeded)
            {
                return Ok(_service.GetEmployeeByUserName(login.UserName).Id);
            }
            return Unauthorized("Login failed");
        }
    }
}
