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
    [Collection("Sequential")]
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

        [Fact]
        public void ConnectActorToMovies()
        {
            var movieCount = _context.Movies.FirstOrDefault(m => m.Id == 1).Actors.Count;
            ActorsDTO tmp = new ActorsDTO()
            {
                Id = 0,
                MovieId = 1,
                Name = "Tmp User"
            };

            var count = _context.Actors.Count();

            // Act
            var result = _controller.PostActor(tmp);

            // Assert
            var objectResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
            var content = Assert.IsAssignableFrom<ActorsDTO>(objectResult.Value);
            Assert.Equal(count + 1, _context.Actors.Count());
            Assert.Equal(movieCount + 1, _context.Movies.FirstOrDefault(m => m.Id == 1).Actors.Count);
        }

        [Fact]
        public void DeleteActor()
        {
            var aCount =  _context.Actors.FirstOrDefault(a => a.Id == 1).Movies.Count;
            var mCount = _context.Movies.FirstOrDefault(m => m.Id == 1).Actors.Count;
            var result = _controller.DeleteActor(1, 1);

            var objectResult = Assert.IsAssignableFrom<OkResult>(result);
            Assert.Equal(aCount - 1, _context.Actors.FirstOrDefault(a => a.Id == 1).Movies.Count);
            Assert.Equal(mCount - 1, _context.Movies.FirstOrDefault(m => m.Id == 1).Actors.Count);
        }


    }
}