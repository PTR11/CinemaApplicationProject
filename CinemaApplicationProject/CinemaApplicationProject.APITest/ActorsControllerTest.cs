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
using Xunit;

namespace CinemaApplicationProject.APITest
{
    public class ActorsControllerTest : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly DatabaseService _service;
        private readonly ActorsController _controller;

        public ActorsControllerTest()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new DatabaseContext(options);
            TestDbInitializer.Initialize(_context);

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser,StatsAndPays,DatabaseContext,int>(_context), null,
                new PasswordHasher<ApplicationUser>(), null, null, null, null, null, null);

            var user = new ApplicationUser { UserName = "testName", Id = 1 };
            userManager.CreateAsync(user, "testPassword").Wait();

            _service = new DatabaseService(_context,userManager);
            _controller = new ActorsController(_service);

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
        public void GetActorsTest()
        {
            var result = _controller.GetActors();


            var content = Assert.IsAssignableFrom<IEnumerable<ActorsDTO>>(result.Value);
            //Assert.Empty(_controller.GetActors().Value);

            Assert.Equal(18, content.Count());
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetActorByIdTest(Int32 id)
        {
            // Act
            var result = _controller.GetActor(id);

            // Assert
            var content = Assert.IsAssignableFrom<ActorsDTO>(result.Value);
            Assert.Equal(id, content.Id);
        }

        [Fact]
        public void GetInvalidActorTest()
        {
            // Arrange
            var id = 19;

            // Act
            var result = _controller.GetActor(id);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }


    }
}