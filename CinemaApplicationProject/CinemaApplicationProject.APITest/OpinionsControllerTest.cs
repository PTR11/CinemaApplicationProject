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
    public class OpinionsControllerTest : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly DatabaseService _service;
        private readonly OpinionsController _controller;

        public OpinionsControllerTest()
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

            _service = new DatabaseService(_context, userManager);
            _controller = new OpinionsController(_service,userManager);

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
        public void GetOpinionsTest()
        {
            var result = _controller.GetOpinions(1);


            var content = Assert.IsAssignableFrom<IEnumerable<OpinionsDTO>>(result.Value);

            Assert.Equal(0, content.Count());
        }

        [Fact]
        public async Task PostOpinionsTest()
        {
            OpinionsDTO opinions = new OpinionsDTO { 
                MovieId = 1,
                Anonymus = false,
                GuestName = "Sanyi",
                GuestId = 1,
                Description = "Nagyon jo",
                Ranking = 5
            };

            var result = await _controller.PostOpinions(opinions);
            Assert.IsAssignableFrom<ActionResult<OpinionsDTO>>(result);
        }
    }
}
