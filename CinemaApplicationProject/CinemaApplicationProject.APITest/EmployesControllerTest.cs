using CinemaApplicationProject.API.Controllers;
using CinemaApplicationProject.Model;
using CinemaApplicationProject.Model.Database;
using CinemaApplicationProject.Model.DTOs;
using CinemaApplicationProject.Model.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace CinemaApplicationProject.APITest
{
    [Collection("Sequential")]
    public class EmployesControllerTest : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly DatabaseService _service;
        private readonly EmployeeController _controller;

        public EmployesControllerTest()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new DatabaseContext(options);
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser, StatsAndPays, DatabaseContext, int>(_context), null,
                new PasswordHasher<ApplicationUser>(), null, null, null, null, null, null);
            TestDbInitializer.Initialize(_context, userManager);

            

            

            _service = new DatabaseService(_context, userManager);
            _controller = new EmployeeController(userManager,_service);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, "testName"),
                new Claim(ClaimTypes.NameIdentifier, "12"),
            });

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = claimsPrincipal
                }
            };
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async void GetEmployeesTest()
        {
            var result = await _controller.GetEmployees();


            var content = Assert.IsAssignableFrom<IEnumerable<EmployeesDTO>>(result.Value);
            //Assert.Empty(_controller.GetActors().Value);

            Assert.Equal(1, content.Count());
        }
        [Fact]
        public void DeleteEmployee()
        {
            var mCount = _context.Employees.Count();
            var result = _controller.DeleteUserAsync(1);

            var objectResult = Assert.IsAssignableFrom<Task<IActionResult>>(result);
            Assert.Equal(mCount - 1, _context.Employees.Count());
        }

        [Fact]
        public void PutEmployee()
        {
            EmployeesDTO user = new EmployeesDTO()
            {
                Id = 1,
                Name = "perec",
                UserName = "sajtos",
            };


            var result = _controller.PutUser(1, user);
            Assert.IsAssignableFrom<OkResult>(result.Result);
            Assert.Equal("perec", _context.Employees.FirstOrDefault(b => b.Id == 1).Name);
            Assert.Equal("sajtos", _context.Employees.FirstOrDefault(b => b.Id == 1).UserName);
        }

        [Fact]
        public void PostEmployee()
        {
            EmployeesDTO user = new EmployeesDTO()
            {
                Id = 4,
                Name = "kakaos csiga",
                UserName = "asd",
                Password = "Almafa123"
            };

            var count = _context.Employees.Count();

            var result = _controller.PostUser(user);

            var objectResult = Assert.IsAssignableFrom <Task<ActionResult<EmployeesDTO>>>(result);
            //Assert.IsAssignableFrom<EmployeesDTO>(objectResult.Value);
            Assert.Equal(count + 1, _context.Employees.Count());

        }
    }
}
