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
    public class UsersControllerTest : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly DatabaseService _service;
        private readonly UsersController _controller;

        public UsersControllerTest()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new DatabaseContext(options);
            TestDbInitializer.Initialize(_context);

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser, StatsAndPays, DatabaseContext, int>(_context), null,
                new PasswordHasher<ApplicationUser>(), null, null, null, null, null, null);

           

            var user = new ApplicationUser { UserName = "testName", Id = 1 };
            userManager.CreateAsync(user, "testPassword").Wait();

            var emp = new Employees { Name = "sajt", Id = 2, Birthday = "ma" };
            userManager.CreateAsync(emp, "testPassword").Wait();

            _service = new DatabaseService(_context, userManager);
            _controller = new UsersController( userManager, _service);

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
        public async Task GetEmployeesTest()
        {
            var result =await _controller.GetEmployees();


            var content = Assert.IsAssignableFrom<IEnumerable<EmployeesDTO>>(result.Value);
            //Assert.Empty(_controller.GetActors().Value);

            Assert.Equal(1, content.Count());
        }

        [Fact]
        public void PostMoviesItemTest()
        {
            EmployeesDTO emp = new EmployeesDTO()
            {
                Id = 0,
                Name = "kakaos csiga",
                UserName = "asd",
                Password = "sajt",
                Stats = new List<StatsDTO>()
            };

            var count = _context.Employees.Count();

            var result = _controller.PostUser(emp);

            var objectResult = Assert.IsAssignableFrom <ActionResult<EmployeesDTO>>(result.Result);
            Assert.IsAssignableFrom<EmployeesDTO>(objectResult.Value);
            Assert.Equal(count + 1, _context.Employees.Count());
        }

        [Fact]
        public void PutMoviesItemTest()
        {
            EmployeesDTO movie = new EmployeesDTO()
            {
                Id = 2,
                Name = "perec",
                UserName = "sa",
                Email = "asd@a.com",
                Address = "itthon",
                Birthday = "ma"
            };


            var result = _controller.PutUser(2, movie);
            Assert.IsAssignableFrom<OkResult>(result.Result);
            Assert.Equal("perec", _context.Employees.FirstOrDefault(b => b.Id == 2).Name);
            Assert.Equal("sa", _context.Employees.FirstOrDefault(b => b.Id == 2).UserName);
        }

        [Fact]
        public async Task DeleteMovie()
        {
            var mCount = _context.Employees.Count();
            var result = await _controller.DeleteUserAsync(2);

            var objectResult = Assert.IsAssignableFrom<OkResult>(result);
            Assert.Equal(mCount - 1, _context.Employees.Count());
        }
    }
}
