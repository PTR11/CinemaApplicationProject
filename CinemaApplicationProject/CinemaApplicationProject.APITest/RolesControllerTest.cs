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
    public class RolesControllerTest : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly DatabaseService _service;
        private readonly StatsAndPaysController _controller;

        public RolesControllerTest()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new DatabaseContext(options);
            TestDbInitializer.Initialize(_context);

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser, StatsAndPays, DatabaseContext, int>(_context), null,
                new PasswordHasher<ApplicationUser>(), null, null, null, null, null, null);


            

            _service = new DatabaseService(_context, userManager);
            var roleManager = new RoleManager<StatsAndPays>(new RoleStore<StatsAndPays, DatabaseContext, int>(_context),null,null,null,null);

            _controller = new StatsAndPaysController(_service, roleManager);

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
        public void GetRolesTest()
        {
            var result = _controller.GetRoles();


            var content = Assert.IsAssignableFrom<IEnumerable<StatsDTO>>(result.Value);

            Assert.Equal(5, content.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetRoleByIdTest(Int32 id)
        {
            // Act
            var result = _controller.GetRolesById(id);

            // Assert
            var content = Assert.IsAssignableFrom<StatsDTO>(result.Value);
            Assert.Equal(id, content.Id);
        }

        [Fact]
        public void DeleteRoleTest()
        {
            var mCount = _context.StatsAndPays.Count();
            var result = _controller.DeleteRole(1);

            var objectResult = Assert.IsAssignableFrom<OkResult>(result);
            Assert.Equal(mCount - 1, _context.StatsAndPays.Count());
        }

        [Fact]
        public void PutMoviesItemTest()
        {
            StatsDTO movie = new StatsDTO()
            {
                Id = 3,
                Name = "perec",
                Salary = 1
            };


            var result = _controller.PutRole(3, movie);
            Assert.IsAssignableFrom<OkResult>(result.Result);
            Assert.Equal("perec", _context.StatsAndPays.FirstOrDefault(b => b.Id == 3).Name);
            Assert.Equal(1, _context.StatsAndPays.FirstOrDefault(b => b.Id == 3).Salary);
        }

        [Fact]
        public async Task PostRoleTest()
        {
            StatsDTO role = new StatsDTO()
            {
                Id = 6,
                Name = "sajt",
                UserId = 1,
            };

            var count = _context.StatsAndPays.Count();

            var result =await _controller.PostRole(role);

            var objectResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
            
            Assert.Equal(count + 1, _context.StatsAndPays.Count());

        }
    }
}
