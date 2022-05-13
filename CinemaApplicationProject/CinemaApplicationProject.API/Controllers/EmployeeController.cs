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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDatabaseService _service;

        public EmployeeController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager, IDatabaseService service)
        {
            _signInManager = signInManager;
            _service = service;
            _userManager = userManager;
            DatabaseManipulation.context = _service.GetContext();
        }

        //public EmployeeController(UserManager<ApplicationUser> userManager,  IDatabaseService service)
        //{
        //    _service = service;
        //    _userManager = userManager;
        //    DatabaseManipulation.context = _service.GetContext();
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeesDTO>>> GetEmployees()
        {
            var tmpList = await _service.GetEmployees();
            return tmpList.Select(m => (EmployeesDTO)m).ToList();
        }

        [HttpGet("{role}")]
        public async Task<ActionResult<IEnumerable<EmployeesDTO>>> GetEmployeesByRole(String role)
        {
            var tmpList = await _service.GetEmployeesByRole(role);
            return tmpList.Select(m => (EmployeesDTO)m).ToList();
        }

        //api/Employee/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);
            if (result.Succeeded)
            {
                var user = _service.GetEmployeeByUserName(login.UserName);
                if (_service.AddEmployeeToEmployeePresence(user, "login"))
                {
                    return Ok(user.Id);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            return Unauthorized("Login failed");
        }

        [HttpPost("Logout/{id}")]
        public async Task<IActionResult> Logout(int id)
        {
            var user = await _service.GetEmployeeById(id);
            await _signInManager.SignOutAsync();
            if (_service.AddEmployeeToEmployeePresence(user, "logout"))
            {
                return Ok(user.Id);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("roles/{id}")]
        public async Task<ActionResult<IEnumerable<string>>> GetRoles(int id)
        {
            return await _service.GetStatsById(id);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeesDTO>> PostUser(EmployeesDTO newUser)
        {
            var user = (Employees)newUser;
            var stats = newUser.Stats;

            var result = await _userManager.CreateAsync(user, newUser.Password);
            if (result.Succeeded)
            {
                foreach (var stat in stats)
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
        public async Task<IActionResult> PutUser(int id, EmployeesDTO employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            if (employee.Stats == null)
            {
                employee.Stats = new List<StatsDTO>();
            }
            var tmp = (Employees)employee;
            var user = await _service.GetEmployeeById(tmp.Id);

            user.Name = tmp.Name;
            user.UserName = tmp.UserName;
            user.Email = tmp.Email;
            user.Address = tmp.Address;
            user.Birthday = tmp.Birthday;

            var result = await _userManager.UpdateAsync(user);
            if (employee.Password != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var pwResult = await _userManager.ResetPasswordAsync(user, token, employee.Password);
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
            
            DatabaseManipulation.DeleteElement(employee);

            return Ok();
        }
    }
}
