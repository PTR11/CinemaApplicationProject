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
using Microsoft.AspNetCore.Identity;
using CinemaApplicationProject.Model.DTOs;

namespace CinemaApplicationProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsAndPaysController : ControllerBase
    {
        private readonly IDatabaseService _service;
        private static RoleManager<StatsAndPays> _roleManager;

        public StatsAndPaysController(IDatabaseService service, RoleManager<StatsAndPays> roleManager)
        {
            _service = service;
            _roleManager = roleManager;
            DatabaseManipulation.context = _service.GetContext();
        }

        // GET: api/StatsAndPays
        [HttpGet]
        public ActionResult<IEnumerable<StatsDTO>> GetRoles()
        {
            return _service.GetStats().Select(m => (StatsDTO)m).ToList();
        }

        // GET: api/StatsAndPays/5
        [HttpGet("{id}")]
        public ActionResult<StatsDTO> GetRolesById(int id)
        {
            var statsAndPays = _service.GetStatById(id);

            if (statsAndPays == null)
            {
                return NotFound();
            }

            return (StatsDTO)statsAndPays;
        }

        // PUT: api/StatsAndPays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, StatsDTO roles)
        {
            if (id != roles.Id)
            {
                return BadRequest();
            }
            var asd = (StatsAndPays)roles;
            var role = _service.GetStatById(asd.Id);

            role.Name = asd.Name;
            role.Salary = asd.Salary;

            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<StatsDTO>> PostRole(StatsDTO roles)
        {
            StatsAndPays find = _service.GetStatByName(roles.Name);

            if(find == null)
            {
                var entity = await _roleManager.CreateAsync((StatsAndPays)roles);
                if (!entity.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }

            await _service.ConnectUserWithRole(roles.UserId, roles.Id);
            return CreatedAtAction(nameof(GetRolesById), new { id = roles.Id }, (StatsDTO)roles);
        }

        

        // DELETE: api/StatsAndPays/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRole(int id)
        {
            var statsAndPays = _service.GetStatById(id);
            if (statsAndPays == null)
            {
                return NotFound();
            }

            DatabaseManipulation.DeleteElement(statsAndPays);

            return Ok();
        }
    }
}
